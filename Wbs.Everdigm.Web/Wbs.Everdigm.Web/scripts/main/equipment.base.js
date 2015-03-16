
var equipmentTypesModels = new Array();

// 获取设备类别列表
function getEquipmentModels(callback) {
    GetJsonData("../ajax/equipment_models.ashx", { "cmd": "list" },
        function (data) {
            equipmentTypesModels = data;
            if (typeof (callback) != "undefined") {
                callback();
            }
        });
}
// 查找设备类型
function getType(code) {
    var type = typeof (code);
    if(type==="string")
        return jLinq.from(equipmentTypesModels.types).equals("Name", code).select();
    else
        return jLinq.from(equipmentTypesModels.types).equals("id", code).select();
}
// 查找设备型号
function getModel(code) {
    var model = typeof (code);
    if (model === "string")
        return jLinq.from(equipmentTypesModels.models).equals("Code", code).select();
    else
        return jLinq.from(equipmentTypesModels.models).equals("id", code).select();
}
// 查找设备状态码信息
function getStatusCode(name) {
    if (typeof (name) === "string")
        return jLinq.from(equipmentTypesModels.codes).equals("Name", name).select();
    else
        return jLinq.from(equipmentTypesModels.codes).equals("id", name).select();
}
// 查找设备状态
function getStatusStatus(name) {
    if (typeof (name) === "string")
        return jLinq.from(equipmentTypesModels.statuses).equals("Name", name).select();
    else
        return jLinq.from(equipmentTypesModels.statuses).equals("id", name).select();
}
// 查找仓库
function getWarehouse(name) {
    var type = typeof (name);
    if(type==="string")
        return jLinq.from(equipmentTypesModels.warehouses).equals("Name", name).select();
    else
        return jLinq.from(equipmentTypesModels.warehouses).equals("id", name).select();
}