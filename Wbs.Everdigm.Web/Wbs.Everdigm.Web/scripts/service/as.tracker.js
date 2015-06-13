var map;// google map object
var pointer;
var datas = new Array();
var markerArray = new Array();
var posArray = new Array();
var polyline;
// 计时器
var _timer = null;
var _animation_started = false;
var _animation_index = 0;

$(document).ready(function () {
    // 初始化地图
    InitializeMap(47.908287, 106.9308501);

    $("#tbodyBody").on("click", "tr", function (evt) {
        if (evt.target.tagName.toLowerCase() == "input") return;

        // 设置当前行是否选中
        var box = $(this).children("td:eq(0)").children("[type=\"checkbox\"]");
        box.click();
    });

    $("#tbodyBody").on("change", "input[type=\"checkbox\"]", function () {
        checkboxChange($(this));
    });

    $(".input-daterange").each(function () {
        var inputs = $(this).children(".input-md");
        var now = new Date();
        var then = now.dateAfter(-5, 1);
        $(inputs[0]).val(then.pattern(_datepatternFMT));
        $(inputs[1]).val(now.pattern(_datepatternFMT));
    });

    $(".input-daterange").datepicker({
        format: _datepickerFMT,
        weekStart: 0,
        autoclose: true
    });

    $("#animation").click(function () {
        if (_animation_started) {
            _animation_started = false;
            _timer.pause();
            setAnimationStatus();
        } else {
            _animation_index = 0;
            clearMarkers();
            animateTracking();
            _animation_started = true;
            setAnimationStatus();
        }
    });

    getTrackerHistory();
});

function setAnimationStatus() {
    $("#animationFlag").removeClass(_animation_started ? "glyphicon-play" : "glyphicon-stop")
        .addClass(_animation_started ? "glyphicon-stop" : "glyphicon-play");
}

// 初始化地图
function InitializeMap(lat, lng) {
    var pointer = new google.maps.LatLng(lat, lng);
    var mapOptions = {
        center: pointer,
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var target = document.getElementById("map_canvas");
    map = new google.maps.Map(target, mapOptions);
}
// 清除所有已有的标记
function clearMarkers() {
    for (var i in markerArray) {
        markerArray[i].Marker.setMap(null);
    }
    markerArray.length = 0;
}
function deleteMarker(id) {
    for (var i = 0; i < markerArray.length; i++) {
        if (markerArray[i].ID == id) {
            markerArray[i].Marker.setMap(null);
        }
    }
}
function getMarker(id) {
    for (var i = 0; i < markerArray.length; i++) {
        if (markerArray[i].ID == id) {
            return markerArray[i];
        }
    }
    return null;
}
// 显示一个新的标记
function setMarkerInMap(lat, lng, id) {
    var mrk = getMarker(id);
    if (null == mrk) {
        var latlng = new google.maps.LatLng(lat, lng);
        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            title: "I'm here!"
        });
        markerArray.push({ ID: id, Marker: marker });
    } else {
        mrk.Marker.setMap(map);
    }
    map.setCenter(latlng);
}
function getTrackerHistory() {
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();
    GetJsonData("../ajax/query.ashx", {
        "type": "tracker", "cmd": "position", "data": id, "start": start, "end": end
    }, function (data) {
        datas = data;
        showPositionHistory(datas);
    });
}

var posItem = "<tr data-latlng=\"%latlng%\">" +
              "    <td class=\"panel-body-td\"><input type=\"checkbox\" id=\"cb_%_id%\"/></td>" +
              "    <td class=\"panel-body-td\">%receive%</td>" +
              "    <td class=\"panel-body-td textoverflow\">%address%</td>" +
              "</tr>";

// 显示历史记录列表
function showPositionHistory(list) {
    var html = "";
    posArray = new Array();
    var dist = 0;
    for (var i in list) {
        var obj = list[i];
        var rtime = convertDateTimeToJavascriptDate(obj.GPSTime);
        var latlng = obj.Latitude + "," + obj.Longitude;
        html += posItem.replace(/%receive%/, rtime.pattern(_datetimepatternFMT))
            .replace(/%latlng%/, latlng).replace(/%address%/, obj.Type).replace(/%_id%/, obj.id);
        // 同时计算与上一点之间的距离，超过50m才加入路径点中
        if (posArray.length < 1) {
            posArray.push(new google.maps.LatLng(obj.Latitude, obj.Longitude));
        } else {
            var len = posArray.length - 1;
            // 先计算跟上一个的距离，如果超过50m才加入，否则不加入
            var distence = getFlatternDistance(obj.Latitude, obj.Longitude, posArray[len].lat(), posArray[len].lng());

            if (!isNaN(distence)) {
                //if (distence > 50) {
                    dist += distence;
                    // 距离大于指定距离才加入route列表
                    posArray.push(new google.maps.LatLng(obj.Latitude, obj.Longitude));
                //}
            }
        }
    }
    $("#distance").text(parseInt(dist) / 1000);
    $("#points").text(posArray.length);
    if (isStringNull(html)) {
        html = "<tr data-latlng=\"\"><td class=\"panel-body-td\" colspan=\"3\">No records exists.</td></tr>";
    }
    $(".table-hover tbody").html(html).children("tr").css("cursor", "pointer");

    // 划线的参数
    var polyOptions = {
        path: posArray,
        strokeColor: "blue",
        strokeOpacity: 0.5,
        strokeWeight: 3
    };
    polyline = new google.maps.Polyline(polyOptions);
    polyline.setMap(map);
    fitBounds(posArray);
}

function checkboxChange(obj) {
    var checked = $(obj).prop("checked");
    var input = $(obj).parent().parent().attr("data-latlng");
    if (null == input || isStringNull(input)) return;

    var id = $(obj).prop("id").replace(/cb_/, "");
    var latlngStr = input.split(",", 2);
    var lat = parseFloat(latlngStr[0]);
    var lng = parseFloat(latlngStr[1]);
    if (checked)
        setMarkerInMap(lat, lng, id);
    else
        deleteMarker(id);
}

function animateTracking() {
    polyline.setMap(null);

    var polyOptions = {
        path: new Array(),
        strokeColor: "blue",
        strokeOpacity: 0.5,
        strokeWeight: 3
    };
    polyline = new google.maps.Polyline(polyOptions);
    polyline.setMap(map);
    var bounds = new google.maps.LatLngBounds();
    if (null == _timer) {
        _timer = $.timer(300, function () {
            $("#animationIndex").text("(" + _animation_index + ")");
            if (_animation_index >= datas.length) {
                _timer.pause();
                _animation_started = false;
                setAnimationStatus();
                //map.fitBounds(bounds);
            } else {
                var path = polyline.getPath();
                var p = new google.maps.LatLng(datas[_animation_index].Latitude, datas[_animation_index].Longitude);
                path.push(p);
                map.setCenter(p);
                if (_animation_index == 0) {
                    var maker = createMarker(map, p, "Start", "", "blue");
                    maker.setMap(map);
                    markerArray.push({ ID: _animation_index, Marker: maker });
                } else if (_animation_index == datas.length - 1) {
                    var maker = createMarker(map, p, "Stop", "", "blue");
                    maker.setMap(map);
                    markerArray.push({ ID: _animation_index, Marker: maker });
                }
                if (_animation_index < 10) {
                    bounds.extend(p);
                    map.fitBounds(bounds);
                }
                //fitBounds();
            }
            _animation_index++;
        });
    }
    else {
        _timer.resume();
    }
}