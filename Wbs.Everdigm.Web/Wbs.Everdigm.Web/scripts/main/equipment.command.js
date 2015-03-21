// 计时器
var _timer = null;
// 获取服务器上命令发送状态的时间间隔
var _timerInterval = 5000;
// 一个命令发送之后最大尝试获取状态的次数30次，超过这个次数后显示发送结果为失败或超时等
var _timerMaxtimes = 30;
// 发送命令之后的命令的id，通过此id查询后续命令的发送状态
var _lastCommandId = 0;

$(document).ready(function () {
    $(".btn").click(function () {
        var index = $(".btn").index($(this));
        if (index == 0) {
            // 发送命令
            sendCommand();
        } else {
            // 查询命令历史记录
            queryCommandHistory();
        }
    });

    $(".dropdown-menu").on("click", "a", function () {
        var text = $(this).text();
        var cmd = $(this).attr("href").replace(/#/, "");
        var parent=$(this).parent().parent();
        parent.prev().children("span:eq(0)").text(text);
        parent.next().val(cmd);
    });
    initializeCommandList();
    queryCommandHistory();
});

function showAlertModal(title) {
    $(".modal-body").html(title);
    $("#alertModal").modal("show");
}

function initializeCommandList() {
    $("select").empty();
    var html = "<option value=\"\">Select command:</option>";
    $(".dropdown-menu a").each(function () {
        var text = $(this).text();
        var cmd = $(this).attr("href").replace(/#/, "");
        html+="<option value=\"" + cmd + "\">" + text + "</option>";
    });
    $("select").html(html);
}

// 设置发送按钮的发送状态 true=sending, false=standby
function setButtonsSendingState(sending) {
    var btn = $(".btn:eq(0)");
    var html = "<span class=\"glyphicon " +
        (!sending ? "glyphicon-repeat" : "glyphicon-repeat glyphicon-refresh-animate") + "\"></span> Send" + (!sending ? "" : "ing...");
    btn.html(html);
    if (sending) btn.addClass("disabled");
    else
        btn.removeClass("disabled");
}
// 发送命令之前的准备工作
function prepareTimer() {
    var btn = $(".btn:eq(0)");
    setButtonsSendingState(true);
    if (null == _timer) {
        _timer = $.timer(_timerInterval, function () {
            setButtonsSendingState(false);
            _timer.pause();
        });
    }
    else {
        _timer.resume();
    }
}

function sendCommand() {
    var id = $("#hidKey").val();
    var cmd = $("#cmdInfo").val();
    if (isStringNull(cmd)) {
        alert("Please select a command first.");
        return;
    } else {
        GetJsonData("../ajax/command.ashx", { "type": "command", "cmd": cmd, "data": id },
            function (data) {
                if (data.status == 0) {
                    _lastCommandId = data.id;
                    prepareTimer();
                } else {
                    showAlertModal(data.desc);
                }
            });
    }
}

function appendHistory() { }

function queryCommandHistory() {
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();
    var cmd = $("#cmdInfo").val();
    GetJsonData("../ajax/command.ashx", { "type": "history", "cmd": cmd, "data": id, "start": start, "end": end },
        function (data) {
            if (data.hasOwnProperty("desc")) {
                showAlertModal(data.desc);
            } else {
                showHistoryList(data);
            }
        });
}

var hisItem = "[%time%] Send command <code>%cmd%</code>, response <span class=\"command-status %class%\">%text%</span>.<br />";
// 获取命令的发送状态
function historyStatus(stat) {
    var ret = { classs: "", text: "" };
    switch (stat) {
        case 6: ret.classs = "bg-success text-muted"; ret.text = "Success"; break;
        case 7: ret.classs = "bg-info text-danger"; ret.text = "Timedout"; break;
        default: ret.classs = "bg-danger text-danger"; ret.text = "Fail"; break;
    }
    return ret;
}
// 显示命令历史记录
function showHistoryList(list) {
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var stat = historyStatus(obj.u_sms_status);
        var time = convertDateTimeToJavascriptDate(obj.u_sms_schedule_time);
        html += hisItem.replace(/%time%/, time.pattern(_datetimepatternFMT))
            .replace(/%cmd%/, obj.u_sms_command).replace(/%class%/, stat.classs).replace(/%text%/, stat.text);
    }
    if (isStringNull(html)) { html = "No records.";}
    $(".bs-callout:eq(1)").html(html);
}