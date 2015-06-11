$(document).ready(function () {
    $("#inDate").val(new Date().pattern("yyyy/MM/dd"));
    $("input[id$=\"Date\"]").datepicker({
        format: "yyyy/mm/dd",
        weekStart: 0,
        todayBtn: true,
        todayHighlight: true,
        autoclose: true
    });

    $(".btn-primary").click(function () {
        newInStorageSave();
    });
});

function showWarning(content) {
    $("#warningText").html(content);
    $("#modalWarning").modal("show");
}

// 新品入库
function newInStorageSave() {
    var obj = {};

    // functional
    var value = $("#hidFunctional").val();
    if (isStringNull(value)) {
        showWarning("Please select the functional.");
        return;
    }
    obj.Functional = value;

    // model
    value = $("#dropModel span:eq(0)").text();
    var model = getModel(value);
    if (model.length < 1) {
        showWarning("Please select equipment type/model.");
        return;
    }
    obj.Model = model[0].id;

    // number
    value = $("#number").val();
    if (isStringNull(value)) {
        showWarning("Please input equipment number.");
        return;
    }
    obj.Number = value;

    // warehouse
    value = $("#dropWarehouse span:eq(0)").text();
    var house = getWarehouse(value);
    if (house.length < 1) {
        showWarning("Please select warehouse.");
        return;
    }
    obj.Warehouse = house[0].id;

    // CC date
    value = $("#ccDate").val();
    if (isStringNull(value)) {
        showWarning("Please select the C.C. date.");
        return;
    }
    obj.CCDate = value;
    obj.InDate = $("#inDate").val();

    // 新品入库时，出入库记录默认为1
    obj.StoreTimes = 1;
    $("#hidNewInstorage").val($.toJSON(obj));

    // 查找服务器上是否有相同的设备号码存在
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "query", "data": $("#hidNewInstorage").val() },
        function (data) {
            if (data.length > 0) {
                showWarning("There has a same device exist.");
            } else {
                $("#btSave").click();
            }
        });
}