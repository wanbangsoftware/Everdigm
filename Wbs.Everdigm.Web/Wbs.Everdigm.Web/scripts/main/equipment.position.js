var map;// google map object
var pointer;
var markerArray = new Array();
var posArray = new Array();
var polyline;

$(document).ready(function () {
    // 初始化地图
    InitializeMap(47.908287, 106.9308501);

    $("#span_map").click(function () { $("#span_malf").click(); });
    $("#span_malf").click(function () {
        var _map = $("#map_canvas");
        var mapHeight = _map.height();
        if (mapHeight == 200) {
            $("#span_malf").parent().next().hide();
            _map.css({ "height": "400px" });
        } else {
            $("#span_malf").parent().next().show();
            _map.css({ "height": "200px" });
        }
    });
    $(".btn-success").click(function () { queryPositionHistory(); });
    $(".position tbody").on("click", "tr", function () {
        var input = $(this).attr("data-latlng");
        if (null == input || isStringNull(input)) return;

        var latlngStr = input.split(",", 2);
        var lat = parseFloat(latlngStr[0]);
        var lng = parseFloat(latlngStr[1]);
        setMarkerInMap(lat, lng, new Date($(this).children("td:eq(1)").html()));
    });
    queryPositionHistory();
});
// 清除所有已有的标记
function clearMarkers() {
    for (var i in markerArray) {
        markerArray[i].setMap(null);
    }
    markerArray.length = 0;
}
// 显示一个新的标记
function setMarkerInMap(lat, lng, time) {
    clearMarkers();
    var latlng = new google.maps.LatLng(lat, lng);
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: "I'm here!"
    });
    markerArray.push(marker);
    map.setCenter(latlng);

    $("#lat").html(lat);
    $("#lng").html(lng);
    $("#time").html(time.pattern(_datetimepatternFMT));
}
// 初始化地图
function InitializeMap(lat, lng) {
    var pointer = new google.maps.LatLng(lat, lng);
    var mapOptions = {
        center: pointer,
        zoom: 11,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var target = document.getElementById("map_canvas");
    map = new google.maps.Map(target, mapOptions);
}
// 查询定位信息历史记录
function queryPositionHistory() {
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();

    GetJsonData("../ajax/query.ashx", {
        "type": "equipment", "cmd": "positions", "data": id, "start": start, "end": end
    }, function (data) {
        //for (var i = 0; i <= 10; i++)
        //    posArray = $.merge(posArray, data);
        posArray = data;
        showPolyline();
        showPositionPagging();
    });
}
var isPaggingFirstInitialize = true;
function showPositionPagging() {
    var len = posArray.length;
    var pages = parseInt(len / _pageSize) + (len % _pageSize > 0 ? 1 : 0);
    if (pages < 1) pages = 1;
    // 分页
    if (isPaggingFirstInitialize) {
        isPaggingFirstInitialize = false;
    } else {
        // 不是刷新页面首次加载时，需要销毁分页UI待后面重建
        $(".pagination-sm").twbsPagination("destroy");
    }

    $(".pagination-sm").twbsPagination({
        totalPages: pages,
        visiblePages: 5,
        first: "First",
        prev: "Prev",
        next: "Next",
        last: "Last",
        onPageClick: function (event, page) {
            //$('#page-content').text('Page ' + page);
            var l = jLinq.from(posArray).skipTake((page - 1) * _pageSize, _pageSize);
            showPositionHistory(l);
        }
    });
    // 初始化第一页
    var l = jLinq.from(posArray).skipTake(0, _pageSize);
    showPositionHistory(l);
}
var posItem = "<tr data-latlng=\"%latlng%\">"+
              "    <td class=\"panel-body-td\">%type%</td>"+
              "    <td class=\"panel-body-td\">%receive%</td>"+
              "    <td class=\"panel-body-td\">%gpstime%</td>"+
              "    <td class=\"panel-body-td textoverflow\">%address%</td>"+
              "</tr>";

// 显示历史记录列表
function showPositionHistory(list) {
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var rtime = convertDateTimeToJavascriptDate(obj.ReceiveTime);
        var gtime = convertDateTimeToJavascriptDate(obj.GpsTime);
        if (0 == i) {
            setMarkerInMap(obj.Latitude, obj.Longitude, rtime);
        }
        var latlng = obj.Latitude + "," + obj.Longitude;
        html += posItem.replace(/%type%/, obj.Type)
            .replace(/%receive%/, rtime.pattern(_datetimepatternFMT))
            .replace(/%gpstime%/, gtime.pattern(_datetimepatternFMT))
            .replace(/%latlng%/, latlng).replace(/%address%/, obj.Address);
    }
    if (isStringNull(html)) {
        html = "<tr data-latlng=\"\"><td class=\"panel-body-td\" colspan=\"4\">No records exists.</td></tr>";
    }
    $(".position tbody").html(html);
}
// 显示轨迹
function showPolyline() {
    if (posArray.length < 1) return;
    var polyPath = new Array();
    for (var i in posArray) {
        var obj = posArray[i];
        polyPath.push(new google.maps.LatLng(obj.Latitude, obj.Longitude));
    }
    // 划线的参数
    var polyOptions = {
        path: polyPath,
        strokeColor: "blue",
        strokeOpacity: 0.5,
        strokeWeight: 3
    };
    polyline = new google.maps.Polyline(polyOptions);
    polyline.setMap(map);
    fitBounds(polyPath);
}