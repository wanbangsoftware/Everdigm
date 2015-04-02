$(document).ready(function () {

    $("#hidPageIndex").val($.cookie($("#cookieName").val()));

    $("#btQuery").click(function () { $.cookie($("#cookieName").val(), $("#hidPageIndex").val()); });

    $("#query").click(function () { $("#btQuery").click(); });

    // 下拉菜单选择事件
    $("[id^=\"dd\"] ul").on("click", "li a", (function () {
        var index = $(this).prop("tabindex");
        if (index > -1) {
            $(this).parent().parent().prev().children("span:eq(0)").text($(this).text());
            $(this).parent().parent().next().val($(this).prop("tabindex"));
        }
    }));

    $("[id^=\"menuType\"]").on("click", "a", function () {
        var parentSuiffix = $(this).parent().parent().parent().prop("id").replace(/ddType/, "");
        typesOnChange($(this).prop("tabindex"), parentSuiffix);
    });

    $("#number").on("keyup", function (e) {
        var v = $(this).val();
        v = v.replace(/\D/g, "");
        $(this).val(v);
        var model = $("#ddModel").children("a").children("span:eq(0)").text();
        if (model.indexOf("-") > 0) {
            $("#fullNumber").html(model + v);
        }
    });

    getEquipmentModels(function () {
        initializeTypesModels();
    });
});

// 初始化页面上的设备类别、型号等信息
function initializeTypesModels() {
    // 初始化设备类别
    // 查询主UI相关
    initializeMenu("Type", "s", equipmentTypesModels.types);
    // 初始化已选中的Type
    var type = parseInt($("#selectedTypes").val());
    if (type > 0) {
        typesOnChange(type, "s");
    }
    // 初始化已选中的Model
    var selectedModel = parseInt($("#selectedModels").val());
    if (selectedModel > 0) {
        var model = jLinq.from(equipmentTypesModels.models).equals("id", selectedModel).select();
        typesOnChange(model[0].Type, "s");
        $("#ddModels").children("a").children("span:eq(0)").text(model[0].Code);
    }
    initializeMenu("Warehouse", "s", equipmentTypesModels.warehouses);
    // 初始化已选中的Warehouse
    var house = parseInt($("#hidQueryWarehouse").val());
    if (house > 0) {
        var houses = jLinq.from(equipmentTypesModels.warehouses).equals("id", house).select();
        $("#ddWarehouses").children("a").children("span:eq(0)").text(houses[0].Name);
    }
    // 新品入库UI
    initializeMenu("Type", "", equipmentTypesModels.types);
    initializeMenu("Warehouse", "", equipmentTypesModels.warehouses);
    // 新品入库默认为库存状态(有3种库存状态：新品、2手、租赁)
    //initializeMenu("Storage", "", jLinq.from(equipmentTypesModels.codes).equals("Status", 2).select());
    // 2手/租赁入库UI
    initializeMenu("Type", "Old", equipmentTypesModels.types);
    initializeMenu("Warehouse", "Old", equipmentTypesModels.warehouses);
    // 2手/租赁入库默认为检修状态
    //initializeMenu("Storage", "Old", jLinq.from(equipmentTypesModels.codes).equals("Status", 4).select());

    // 更改库存
    initializeMenu("Warehouse", "Warehousing", equipmentTypesModels.warehouses);
}
function initializeMenu(type, suffix, list) {
    $("#dd" + type + suffix).children("a").children("span:eq(0)").text(type + ":");
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var selectedType = $("#selected" + type + suffix).val();
        if (obj.id == parseInt(selectedType)) {
            $("#dd" + type + suffix).children("a").children("span:eq(0)").text(obj.Name);
        }
        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" + obj.id + "\" href=\"#\">" + obj.Name + "</a></li>";
    }
    $("#dd" + type + suffix).children("ul").html(html);
}
// 设备类别选择更改事件
function typesOnChange(newType, suffix) {
    $("#ddModel" + suffix).children("a").children("span:eq(0)").text("Model:");
    $("#selectedModel" + suffix).val("0");
    var list = jLinq.from(equipmentTypesModels.models).equals("Type", newType).select();
    var html = "";
    for (var i in list) {
        var obj = list[i];
        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" + obj.id + "\" href=\"#\">" + obj.Code + "</a></li>";
    }
    if (isStringNull(html))
        html = "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#\">No Items</a></li>";
    $("#menuModel" + suffix).html(html);
}