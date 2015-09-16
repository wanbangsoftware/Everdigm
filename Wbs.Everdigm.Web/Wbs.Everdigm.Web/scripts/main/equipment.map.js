var markers = [];
var markerCluster;
var mcOptions = {
    maxZoom: 19,
    averageCenter: true,
    imagePath: "../../../js/js.marker.clusterer/images/m"
};
// 暂存的设备列表
var tempList = [];

$(document).ready(function () {
    $("#map_canvas").css("height", (parent.innerHeight - parseInt(topHeight) - 21 - 10) + "px");
    $("#equipment_list").BootSideMenu({ side: "right" });
    $("#provice_list").BootSideMenu({ side: "left" });
    // 初始化地图
    InitializeMap(47.908287, 106.9308501);

    // 省份点击时
    $(".list-group:eq(0) a").click(function (e) {
        getEquipmentsByProvince(e);
    });
    $("#grp_equipments").on("click", "a", function (event) { equipmentClick(event); });
    // 点击第二个
    getEquipments("Ulaanbaatar");
});

// 初始化地图
function InitializeMap(lat, lng) {
    var pointer = new google.maps.LatLng(lat, lng);
    var mapOptions = {
        center: pointer,
        zoom: 11,
        //disableDefaultUI: true,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var target = document.getElementById("map_canvas");
    map = new google.maps.Map(target, mapOptions);
}

function getEquipmentsByProvince(evt) {
    if (evt.target.tagName.toLowerCase() == "a") {
        var obj = evt.currentTarget;
        var province = $(obj).attr("href").replace("#", "");
        if (province != $("#hiddenProvinces").val()) {
            // 临时保存上次更新的省份的设备列表
            $("#hiddenProvinces").val(province);
            // 获取该省份下的设备列表
            getEquipments(province);
        } else {
            if (tempList.length > 0) {
                displayEquipments(tempList);
            }
        }
    }
}

function getEquipments(province) {
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "province", "data": province },
        function (data) {
            tempList = data;
            displayEquipments(tempList);
        });
}

function clearMarkers() {
    markers.forEach(function (obj, index, source) {
        obj.remove();
    });
    markers = [];
    if (markerCluster) {
        markerCluster.clearMarkers();
    }
}

function fitBounds(arr) {
    var bounds = new google.maps.LatLngBounds();
    arr.forEach(function (obj, index, source) {
        var myLatlng = new google.maps.LatLng(obj.Latitude, obj.Longitude);
        bounds.extend(myLatlng);
    });
    map.fitBounds(bounds);
}

function hideModal() {
    $("#warningLoading").modal("hide");
}

function displayEquipments(list) {
    clearMarkers();
    if (list.length < 1) {
        $(".col-lg-12").text("There is no equipment in the territory of " + $("#hiddenProvinces").val() + " Province.");
        $("#warningLoading").modal("show");
        setTimeout("hideModal();", 5000);
        return;
    }
    list.forEach(function (obj, index, arr) {
        if (obj.Latitude > 0 && obj.Longitude > 0) {
            var csss = "red";
            if (obj.Voltage != "G0000") csss = "orange";
            markers.push(new CustomMarker(new google.maps.LatLng(obj.Latitude, obj.Longitude), map, {
                marker_id: obj.Id,
                mac_id: obj.Number,
                css: obj.Voltage == "G0000" ? "red" : "orange"
            }));
        }
    });
    markerCluster = new MarkerClusterer(map, markers, mcOptions);
    google.maps.event.addListener(markerCluster, "click", function (c) {
        var m = c.getMarkers();
        var p = [];
        for (var i = 0; i < m.length; i++) {
            p.push(m[i]);
        }
    });
    if (list.length > 1) {
        fitBounds(list);
    } else {
        map.setCenter(markers[0].getPosition());
    }
    displayEquipment(list);
}

var _equipment_ = "<a href=\"#\" class=\"list-group-item\">%number%</a>";

function displayEquipment(list) {
    var html = "";
    list.forEach(function (obj, index, source) {
        html += _equipment_.replace(/%number%/, obj.Number);
    });
    $("#grp_equipments").html(html);
}

function equipmentClick(event) {
    var obj = event.currentTarget;
    var mac = $(obj).text();
    searchDepth = false;
    var founded = searchMarker(mac, markerCluster.getClusters());
    if (!founded) {
        markers.forEach(function (m, i, source) {
            if (m.getParams().mac_id == mac) {
                map.setCenter(m.getPosition());
                m.showHideInfoWindow(true);
                return;
            }
        });
    }
}

var searchDepth = false;

function searchMarker(mac, clusters) {
    var founded = false;
    clusters.forEach(function (v, index, sources) {
        if (!founded) {
            var m = v.getMarkers();
            m.forEach(function (i, ind, sour) {
                if (i.getParams().mac_id == mac) {
                    founded = true;
                    return;
                }
            });
            if (founded) {
                if (m.length > 1) {
                    if (!searchDepth) {
                        searchDepth = true;
                        // Auto click the icon
                        v.getIcon().onIconClick();
                        setTimeout(function () {
                            // 重新查询
                            var clus = markerCluster.getClusters();
                            if (clus && clus.length > 0) {
                                searchMarker(mac, clus);
                            }
                        }, 1000);
                    }
                } else {
                    map.setCenter(m[0].getPosition());
                    m[0].showHideInfoWindow(true);
                }
                return;
            }
        } else return;
    });
    return founded;
}