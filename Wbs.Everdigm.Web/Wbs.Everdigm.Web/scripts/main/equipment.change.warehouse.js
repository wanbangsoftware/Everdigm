$(document).ready(function () {
    // 仓库信息点击事件
    $("[id$=\"tbodyBody\"]").on("click", "a", function () {
        var href = $(this).attr("href");
        if (href.indexOf("#") >= 0) {
            var type = href.replace(/#/, "");
            switch (type) {
                case "h":
                    // 更改仓库信息
                    var tr = $(this).parent().parent();
                    $("#equipmentWarehouseInfoBar li:eq(0) a").html("<strong>Number: </strong>" + tr.children("td:eq(2)").text());
                    $("#equipmentWarehouseInfoBar li:eq(1) a").html("<strong>Warehouse: </strong>" + $(this).text());
                    $("#equipmentWarehouseConfirmBar li:eq(0) a").html("<strong>Number: </strong>" + tr.children("td:eq(2)").text());
                    $("#equipmentWarehouseConfirmBar li:eq(1) a").html("<strong>Warehouse: </strong>" + $(this).text());
                    var id = $(this).prop("id").substr(2);
                    var status = tr.children("td:eq(6)").html();
                    $("[id$=hidWarehouseEquipmentId]").val(id);
                    $("#modalWarehousingProduct").modal("show");
                    break;
            }
        }
    });
    $("#query").click(function () {
        $("[id$=\"btQuery\"]").click();
    });
    // 转库按钮
    $("[id$=changeWarehouseSave]").click(function () { changeWarehouse(); });
});

function changeWarehouse() {
    var obj = {};
    // warehouse
    var value = $("#dropWarehouseWarehousing span:eq(0)").text();
    var house = getWarehouse(value);
    if (house.length < 1) {
        alert("Please select the target warehouse.");
        return;
    }
    value = $("#equipmentWarehouseInfoBar li:eq(1) a").text();
    if (value.indexOf(house[0].Name) >= 0) {
        alert("You have not change the warehouse.");
        return;
    }
    obj.Warehouse = house[0].id;
    $("[id$=hidWarehouseTo]").val($.toJSON(obj));
    $("[id$=btSaveChangeWarehouse]").click();
}
// 转库完毕确认
function confirmWarehouse() {
    $("[id$=btConfirmWarehouse]").click();
}