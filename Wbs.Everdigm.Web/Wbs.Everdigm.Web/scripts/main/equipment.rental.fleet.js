$(document).ready(function () {
    $("#query").click(function () {
        $("[id$=\"btQuery\"]").click();
    });
    $("[id$=tbodyBody]").on("click", "tr", function (e) {
        // 2手、租赁入库
        displayOldOutStoredEquipment(e.currentTarget);
    }).children("tr").css("cursor", "pointer");

    // 2手、租赁入库确认按钮
    $("#oldInStorageSave").click(function () { oldInStorageSave(); });
});

function displayOldOutStoredEquipment(obj) {
    var tbody = $("#oldEquipmentInfo");
    if (null != obj) {
        // 显示设备号码
        tbody.children("tr:eq(0)").children("td:eq(1)").html($(obj).children("td:eq(2)").children("a:eq(0)").text());
        // 状态
        tbody.children("tr:eq(1)").children("td:eq(1)").html($(obj).children("td:eq(7)").html());
        // Functional
        tbody.children("tr:eq(1)").children("td:eq(3)").html($(obj).children("td:eq(3)").html());
        // Location
        tbody.children("tr:eq(2)").children("td:eq(1)").html($(obj).children("td:eq(6)").html());
        $("#oldInStorageSave").removeClass("disabled");
        // 保存id
        var href = $(obj).children("td:eq(2)").children("a:eq(0)").attr("href");
        href = href.substr(href.indexOf("=") + 1);
        $("[id$=hidOldInstorage]").val(href);
    } else {
        tbody.children("tr:eq(0)").children("td:eq(1)").html("");
        tbody.children("tr:eq(1)").children("td:eq(1)").html("");
    }
    $("#modalOldProduct").modal("show");
}

// 已出库的设备入库（租赁、2手）
function oldInStorageSave() {
    // warehouse
    value = $("#dropWarehouseOld span:eq(0)").text();
    var house = getWarehouse(value);
    if (house.length < 1) {
        alert("Please select warehouse.");
        return;
    }
    $("[id$=btSaveOldInStorage]").click();
}
