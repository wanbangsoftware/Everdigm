$(document).ready(function () {
    $(".btn-success").click(function () { queryStorageHistory(); });

    getEquipmentModels(function () {
        queryStorageHistory();
    });
});
// 查询设备的出入库历史记录
function queryStorageHistory() {
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();

    GetJsonData("../ajax/query.ashx", {
        "type": "equipment", "cmd": "storage", "data": id, "start": start, "end": end
    }, function (data) {
        showStorageHistory(data);
    });
}

var item = "<tr>" +
           "    <td class=\"panel-body-td\" style=\"text-align: center;\">%cnt%</td>" +
           "    <td class=\"panel-body-td\">%date%</td>" +
           "    <td class=\"panel-body-td\" style=\"text-align: center;\">%times%</td>" +
           "    <td class=\"panel-body-td\">%warehouse%</td>" +
           "    <td class=\"panel-body-td\">%receipt%</td>" +
           "    <td class=\"panel-body-td\" style=\"text-align: center;\">%code%</td>" +
           "    <td class=\"panel-body-td\">%status%</td>" +
           "</tr>";
function showStorageHistory(list) {
    var html = "";
    var cnt = 0;
    for (var i in list) {
        var obj = list[i];
        cnt++;
        var date = new Date(obj.Stocktime.substr(0,obj.Stocktime.indexOf("T"))).pattern(_datepatternFMT);
        var status = getStatusCode(obj.Status)[0];
        var statusA = getStatusStatus(status.Status)[0];
        var house = getWarehouse(obj.Warehouse)[0];
        html += item.replace(/%cnt%/, cnt).replace(/%date%/, date).
            replace(/%times%/, obj.StoreTimes).replace(/%receipt%/, "-").
            replace(/%code%/, (statusA.Code + status.Code)).
            replace(/%status%/, (status.Name + "(" + statusA.Name + ")")).
            replace(/%warehouse%/, house.Name);
    }
    if (isStringNull(html)) {
        html = "<tr><td class=\"panel-body-td\" colspan=\"7\">no record(s).</td></tr>";
    }
    $("#tbodyWorktimes").html(html);
}