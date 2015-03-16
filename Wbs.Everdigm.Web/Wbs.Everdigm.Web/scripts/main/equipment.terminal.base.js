$(document).ready(function () {
    $("#ddlType").change(function (e) {
        var v = $(this).children("option:selected").val();
        typesChange(v);
    });
    
    // get equipment types and models
    getEquipmentModels();
});
// 查询设备信息
function queryEquipment(number, callback) {
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "fullnumber", "data": number },
        function (data) {
            callback(data);
        });
}
// 查询终端信息
function queryTerminal(ter, callback) {
    GetJsonData("../ajax/query.ashx", { "type": "terminal", "cmd": "single", "data": ter },
       function (data) {
           callback(data);
       });
}

function typesChange(selected) {
    if (isStringNull(selected)) return;

    var modes = jLinq.from(equipmentTypesModels.models).equals("Type", parseInt(selected)).select();
    $("#selModel").empty();
    $("<option value=\"\">Mode:</option>").appendTo("#selModel");
    for (var i in modes) {
        var mode = modes[i];
        $("<option value=" + mode.id + ">" + mode.Code + "</option>").appendTo("#selModel");
    }
}
// 显示终端信息
function showTerminalInfo(obj) {
    $("#hidTerminalId").val(null == obj ? "" : obj.id);
    $("#_t_1_ td:eq(1)").html(null == obj ? "-" : obj.Number);
    $("#_t_1_ td:eq(3)").html(null == obj ? "-" : obj.Sim);
    $("#_t_2_ td:eq(1)").html(null == obj ? "-" : obj.Satellite);
    $("#_t_2_ td:eq(3)").html(null == obj ? "-" : obj.Firmware);
}
// 显示设备的基本信息
function showEquipmentInfo(obj) {
    $("#hidEquipmentId").val(null == obj ? "" : obj.id);
    var model = null == obj ? null : getModel(obj.Model)[0];
    var type = null == obj ? null : getType(model.Type)[0];
    var warehouse = null == obj ? null : getWarehouse(obj.Warehouse)[0];
    $("#_e_1_ td:eq(1)").html(null == obj ? "-" : type.Name);
    $("#_e_1_ td:eq(3)").html(null == obj ? "-" : model.Code);
    $("#_e_2_ td:eq(1)").html(null == obj ? "-" : model.Code + obj.Number);
    $("#_e_2_ td:eq(3)").html((null == obj || null == warehouse) ? "-" : warehouse.Name);
    $("#_e_3_ td:eq(1)").html(null == obj ? "-" : (null == obj.Terminal ? "not bond" :
        "<span style=\"color: red;\">bonded</span>"));
}