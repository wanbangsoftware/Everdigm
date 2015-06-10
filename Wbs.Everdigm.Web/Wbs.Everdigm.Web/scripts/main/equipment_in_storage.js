$(document).ready(function () {

    $("#queryBar li:lt(3)").each(function () {
        var type = $("#hidQueryType").val();
        var ex = new RegExp(type);
        var href = $(this).children("a").attr("href");
        switch (href)
        {
            case "#new":
                if (ex.test("N")) {
                    //document.location = "./equipment_new_product.aspx";
                    $(this).addClass("active");
                    $("#openModal").children("span:eq(1)").html("New product");
                    $("#openModal").attr("data-target", "#modalNewProduct").show();
                }
                else
                    $(this).removeClass("active");
                break;
            case "#second-lease":
                if (ex.test("S,L")) {
                    $(this).addClass("active");
                    $("#openModal").children("span:eq(1)").html("Check in");
                    $("#openModal").attr("data-target", "#modalOldProduct").show();
                }
                else
                    $(this).removeClass("active");
                break;
            case "#change-warehouse":
                if (ex.test("I")) {
                    $(this).addClass("active");
                    $("#openModal").attr("data-target", "#modalWarehousingProduct").hide();
                }
                else
                    $(this).removeClass("active");
                break;
        }
    });

    $("#queryBar li:lt(3)").on("click", "a", function () {
        var href = $(this).attr("href");
        var cls = $(this).parent().prop("class");
        if (cls.indexOf("active") >= 0) return;
        switch (href) {
            case "#new":
                // 查询新品状态
                $("#hidQueryType").val("N");
                break;
            case "#second-lease":
                // 查询2手/租赁设备的状态
                $("#hidQueryType").val("S,L");
                break;
            case "#change-warehouse":
                // 查询库存设备状态
                $("#hidQueryType").val("I");
                break;
        }
        $("#query").click();
    });

    
    $("#inDate").val(new Date().pattern("yyyy/MM/dd"));

    $("input[id$=\"Date\"]").datepicker({
        format: "yyyy/mm/dd",
        weekStart: 0,
        todayBtn: true,
        todayHighlight: true,
        autoclose: true
    });

    $("#txtQueryOld").typeahead({
        items: 5,
        minLength: 4,
        source: function (query, process) {
            queryOldInStore(function (data) {
                if (data.length > 0) {
                    var mod = getModel(data[0].Model);
                    var ret = $.map(data, function (item) { return mod[0].Code + item.Number; });
                    process(ret);
                }
            });
        }
    });

    // 查询2手/租赁的车以便入库操作
    $("#queryOld").click(function () {
        displayOldOutStoredEquipment(null);
        queryOldInStore(function (data) {
            if (data.length > 0) {
                displayOldOutStoredEquipment(data[0]);
            }
            else {
                alert("no equipment exist like your query condition.");
            }
        });
    });

    // 仓库信息点击事件
    $("#tbodyBody").on("click", "a", function () {
        var href = $(this).attr("href");
        if (href.indexOf("#") >= 0)
        {
            var type = href.replace(/#/, "");
            switch (type)
            {
                case "h":
                    // 更改仓库信息
                    var tr = $(this).parent().parent();
                    $("#equipmentWarehouseInfoBar li:eq(0) a").html("<strong>Number: </strong>" + tr.children("td:eq(2)").text());
                    $("#equipmentWarehouseInfoBar li:eq(1) a").html("<strong>Warehouse: </strong>" + $(this).text());
                    $("#equipmentWarehouseConfirmBar li:eq(0) a").html("<strong>Number: </strong>" + tr.children("td:eq(2)").text());
                    $("#equipmentWarehouseConfirmBar li:eq(1) a").html("<strong>Warehouse: </strong>" + $(this).text());
                    var id = $(this).prop("id").substr(2);
                    var status = tr.children("td:eq(6)").html();
                    //if (status.indexOf("T") >= 0) {
                    //    // 转库中的话，显示转库完毕确认界面
                    //    $("#openModal").attr("data-target", "#modalWarehousingConfirm");
                    //    $("#hidConfirmWarehouse").val(id);
                    //}
                    //else {
                        $("#openModal").attr("data-target", "#modalWarehousingProduct");
                        $("#hidWarehouseEquipmentId").val(id);
                    //}
                    $("#openModal").click();
                    break;
            }
        }
    });

    // 新品入库
    //$("a[href=\"#new\"]").click(function () { $("#openModal").attr("data-target", "#modalNewProduct"); });
    //$("a[href=\"#second-lease\"]").click(function () { $("#openModal").attr("data-target", "#modalOldProduct"); });
    //$("a[href=\"#change-warehouse\"]").click(function () { $("#openModal").prop("data-target", "#modalWarehousingProduct"); });

    // 新品入库确认按钮
    $("#newInStorageSave").click(function () { newInStorageSave(); });
    // 2手、租赁入库确认按钮
    $("#oldInStorageSave").click(function () { oldInStorageSave(); });
    // 转库按钮
    $("#changeWarehouseSave").click(function () { changeWarehouse(); });
    // 转库完毕确认按钮
    $("#confirmWarehouseSave").click(function () { confirmWarehouse(); });
});
// 查询已出库的2手/租赁设备以便入库操作
function queryOldInStore(callback) {
    var model = getModel($("#ddModelOld span:eq(0)").text());
    var obj = {};
    if (model.length > 0) {
        obj.Model = model[0].id;
    }
    obj.Number = $("#txtQueryOld").val();
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "old-in-store", "data": $.toJSON(obj) },
    function (data) {
        callback(data);
    });
}
function displayOldOutStoredEquipment(obj) {
    var tbody = $("#oldEquipmentInfo");
    if (null != obj) {
        $("#hidOldInstorageId").val(obj.id);
        var model = getModel(obj.Model)[0];
        tbody.children("tr:eq(0)").children("td:eq(1)").html(null == obj ? "" : (model.Code + obj.Number));
        //var code = getStatusCode(obj.Status)[0];
        var status = getStatusStatus(obj.Status)[0];
        // 如果不是出库状态的话，显示红色提示，不能入库
        if (status.IsItRental != true) {
            tbody.children("tr:eq(1)").children("td:eq(1)").html("<span style=\"color: #ff0000;\">" +
                status.Name + "</span> Cannot change status");
            $("#oldInStorageSave").addClass("disabled");
        } else {
            tbody.children("tr:eq(1)").children("td:eq(1)").html(status.Name);
            $("#oldInStorageSave").removeClass("disabled");
        }
        // Location
        tbody.children("tr:eq(2)").children("td:eq(1)").html("" == obj.GpsAddress ? "not available" : obj.GpsAddress);
    } else {
        tbody.children("tr:eq(0)").children("td:eq(1)").html("");
        tbody.children("tr:eq(1)").children("td:eq(1)").html("");
    }
}
// 新品入库
function newInStorageSave() {
    var obj = {};
    var value = $("#dropModel span:eq(0)").text();
    var model = getModel(value);//jLinq.from(equipmentTypesModels.models).equals("Code", value).select();
    if (model.length < 1) {
        alert("Please select equipment type/model.");
        return;
    }
    obj.Model = model[0].id;
    obj.CCDate = $("#ccDate").val();
    obj.InDate = $("#inDate").val();
    // store status
    //value = $("#dropStorage span:eq(0)").text();
    //var store = jLinq.from(equipmentTypesModels.codes).equals("Status", 2).equals("Name", value).select();
    //if (store.length < 1) {
    //    alert("Please select store type.");
    //    return;
    //}
    //if (value.indexOf("N") < 0) {
    //    alert("New product cannot be store as lease or second-hands situation.");
    //    return;
    //}
    //obj.Status = store[0].id;

    // warehouse
    value = $("#dropWarehouse span:eq(0)").text();
    var house = getWarehouse(value);//jLinq.from(equipmentTypesModels.warehouses).equals("Name", value).select();
    if (house.length < 1) {
        alert("Please select warehouse.");
        return;
    }
    obj.Warehouse = house[0].id;
    // functional
    value = $("#hiddenFunctional").val();
    if (isStringNull(value)) {
        alert("Please select the functional.");
        return;
    }
    obj.Functional = value;
    // number
    value = $("#number").val();
    if (isStringNull(value)) {
        alert("Please input equipment number.");
        return;
    }
    obj.Number = value;
    // 新品入库时，出入库记录默认为1
    obj.StoreTimes = 1;
    $("#hidNewInstorage").val($.toJSON(obj));

    // 查找服务器上是否有相同的设备号码存在
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "query", "data": $("#hidNewInstorage").val() },
        function (data) {
            if (data.length > 0) {
                $("#spanWarningNewInstorage").text("There has a same device exist.");
            } else {
                $("#btSaveNewInStorage").click();
            }
        });
}
// 已出库的设备入库（租赁、2手）
function oldInStorageSave() {
    var obj = {};
    obj.id = $("#hidOldInstorageId").val();
    // warehouse
    value = $("#dropWarehouseOld span:eq(0)").text();
    var house = getWarehouse(value);
    if (house.length < 1) {
        alert("Please select warehouse.");
        return;
    }
    obj.Warehouse = house[0].id;
    // store status
    //value = $("#dropStorageOld span:eq(0)").text();
    //var store = getStatusCode(value);
    //if (store.length < 1) {
    //    alert("Please select store type.");
    //    return;
    //}
    //obj.Status = store[0].id;
    $("#hidOldInstorage").val($.toJSON(obj));
    $("#btSaveOldInStorage").click();
}

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
    $("#hidWarehouseTo").val($.toJSON(obj));
    $("#btSaveChangeWarehouse").click();
}
// 转库完毕确认
function confirmWarehouse() {
    $("#btConfirmWarehouse").click();
}