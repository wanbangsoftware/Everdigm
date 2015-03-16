var alarmArray = new Array();
var faultArray = new Array();
var cmdAlarm = "alarms", cmdFault = "faults";

$(document).ready(function () {
    query(cmdAlarm);
    query(cmdFault);

    $(".btn-success").click(function () {
        var index = $(".btn-success").index($(this));
        if (index == 0) { query(cmdAlarm); }
        else { query(cmdFault); }
    });
});
// 查询
function query(type) {
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();
    GetJsonData("../ajax/query.ashx", {
        "type": "equipment", "cmd": type, "data": id, "start": start, "end": end
    }, function (data) {
        if (type == cmdAlarm) {
            alarmArray = data;
            showAlarmPagging();
        } else {
            //for (var i = 0; i <= 10; i++)
            //    faultArray = $.merge(faultArray, data);
            faultArray = data;
            showFaultPagging();
        }
    });
}
var isAlarmFirstInitializing = true;
// 报警列表分页显示
function showAlarmPagging() {
    var len = alarmArray.length;
    var pages = parseInt(len / _pageSize) + (len % _pageSize > 0 ? 1 : 0);
    if (pages < 1) pages = 1;
    // 分页
    if (isAlarmFirstInitializing) {
        isAlarmFirstInitializing = false;
    } else {
        // 不是刷新页面首次加载时，需要销毁分页UI待后面重建
        $(".pagination-sm:eq(0)").twbsPagination("destroy");
    }

    $(".pagination-sm:eq(0)").twbsPagination({
        totalPages: pages,
        visiblePages: 5,
        first: "First",
        prev: "Prev",
        next: "Next",
        last: "Last",
        onPageClick: function (event, page) {
            var l = jLinq.from(alarmArray).skipTake((page - 1) * _pageSize, _pageSize);
            showAlarms(l);
        }
    });
    // 初始化第一页
    var l = jLinq.from(alarmArray).skipTake(0, _pageSize);
    showAlarms(l);
}
var isFaultFirstInitializing = true;
// EPOS故障信息分页显示
function showFaultPagging() {
    var len = faultArray.length;
    var pages = parseInt(len / _pageSize) + (len % _pageSize > 0 ? 1 : 0);
    if (pages < 1) pages = 1;
    // 分页
    if (isFaultFirstInitializing) {
        isFaultFirstInitializing = false;
    } else {
        // 不是刷新页面首次加载时，需要销毁分页UI待后面重建
        $(".pagination-sm:eq(1)").twbsPagination("destroy");
    }

    $(".pagination-sm:eq(1)").twbsPagination({
        totalPages: pages,
        visiblePages: 5,
        first: "First",
        prev: "Prev",
        next: "Next",
        last: "Last",
        onPageClick: function (event, page) {
            var l = jLinq.from(faultArray).skipTake((page - 1) * _pageSize, _pageSize);
            showFaults(l);
        }
    });
    // 初始化第一页
    var l = jLinq.from(faultArray).skipTake(0, _pageSize);
    showFaults(l);
}


var alarmItem = "<tr>" +
                "   <td class=\"panel-body-td\">%date%</td>" +
                "   <td class=\"panel-body-td\">terminal alarm</td>" +
                "   <td class=\"panel-body-td\">%alarm%</td>" +
                "</tr>";
// 显示终端报警信息列表
function showAlarms(list) {
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var time = convertDateTimeToJavascriptDate(obj.Time);
        html += alarmItem.replace(/%date%/, time.pattern(_datetimepatternFMT))
            .replace(/%alarm%/, obj.Alarm);
    }
    if (isStringNull(html)) {
        html = "<tr><td class=\"panel-body-td\" colspan=\"3\">No records exists.</td></tr>";
    }
    $(".table:eq(0) tbody").html(html);
}
var faultItem = "<tr>" +
                "   <td class=\"panel-body-td\">%time%</td>" +
                "   <td class=\"panel-body-td\" style=\"text-align: center;\">%count%</td>" +
                "   <td class=\"panel-body-td\">0x%code%</td>" +
                "   <td class=\"panel-body-td textoverflow\" data-toggle=\"popover\" data-placement=\"top\" data-trigger=\"focus\" data-content=\"%codedesc%\">%codedesc%</td>" +
                "   <td class=\"panel-body-td\">0x%fmi%</td>" +
                "   <td class=\"panel-body-td textoverflow\" data-toggle=\"popover\" data-placement=\"top\" data-trigger=\"focus\" data-content=\"%fmidesc%\">%fmidesc%</td>" +
                "</tr>";
// 显示EPOS错误列表
function showFaults(list) {
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var time = convertDateTimeToJavascriptDate(obj.ReceiveTime);
        html += faultItem.replace(/%time%/, time.pattern(_datetimepatternFMT))
            .replace(/%count%/, obj.Count).replace(/%code%/, obj.CodeHex)
            .replace(/%codedesc%/g, obj.CodeName).replace(/%fmi%/, obj.FMIHex)
            .replace(/%fmidesc%/g, obj.FMIName);
    }
    if (isStringNull(html)) {
        html = "<tr><td class=\"panel-body-td\" colspan=\"6\">No records exists.</td></tr>";
    }
    $(".table:eq(1) tbody").html(html);
    $("[data-toggle=\"popover\"]").popover({
        animation: true,
        placement: "top",
        trigger: "hover",
        title: "Description"
    });
}