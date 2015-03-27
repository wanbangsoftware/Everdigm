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
        var parent = $(this).parent().parent();
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
        html += "<option value=\"" + cmd + "\">" + text + "</option>";
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

function timerOnTime() {
    _timerTimes++;
    if (_timerTimes > _timerMaxtimes) {
        progressComplete();
        showWarningLine(new Date().pattern("HH:mm:ss"), currentTestingCommand, "Timedout, maybe you should try again.");
    } else {
        // 循环读取服务器上改命令发送记录的状态并显示
        if (_timerTimes % 5 == 0) {
            // 每隔5秒查询一次命令状态
            getCommandStatus();
        }
    }
}

function sendCommand() {
    var id = $("#hidKey").val();
    var cmd = $("#cmdInfo").val();
    if (isStringNull(cmd)) {
        showAlertModal("Please select a command first.");
        return;
    } else {
        GetJsonData("../ajax/command.ashx", { "type": "security", "cmd": cmd, "data": id },
            function (data) {
                if (data.status == 0) {
                    currentTestingCommand = cmd;
                    _lastCommandId = parseInt(data.desc);
                    //var btn = $(".btn:eq(0)");
                    setButtonsSendingState(true);
                    prepareTimer(timerOnTime);
                    data.desc = "will be sent by server.";
                    showWarningMessage(data);
                } else {
                    //showAlertModal(data.desc);
                    showWarningMessage(data);
                }
            });
    }
}

function progressComplete() {
    _timer.pause();
    _lastCommandStatus = -1;
    setButtonsSendingState(false);
}

function getCommandStatus() {
    GetJsonData("../ajax/command.ashx", { "type": "query", "cmd": currentTestingCommand, "data": _lastCommandId },
           function (data) {
               if (_lastCommandStatus != data.status) {
                   showWarningMessage(data);
                   _lastCommandStatus = data.status;
               }
               if (data.status < 0 || data.status >= 6) {
                   // 状态获取失败
                   progressComplete();
                   if (data.status == 7) {
                       // 发送成功
                   }
               }
           });
}

function queryCommandHistory() {
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();
    var cmd = $("select").val();
    GetJsonData("../ajax/command.ashx", { "type": "sechistory", "cmd": cmd, "data": id, "start": start, "end": end },
        function (data) {
            if (data.hasOwnProperty("desc")) {
                showAlertModal(data.desc);
            } else {
                showHistoryList(data);
            }
        });
}

var hisItem = "<div style=\"margin-bottom: 1px;\">[%time%] Command <code>%cmd%</code>, response <span class=\"command-status label %class%\">%text%</span>.</div>";
// 获取命令的发送状态
function historyStatus(stat) {
    var ret = { classs: "", text: "" };
    switch (stat) {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
            ret.classs = "label-info"; ret.text = "In progress...";
            break;
        case 7: ret.classs = "label-success text-muted"; ret.text = "Success"; break;
        case 8: ret.classs = "label-warning text-danger"; ret.text = "Timedout"; break;
        default: ret.classs = "label-danger text-danger"; ret.text = "Fail"; break;
    }
    return ret;
}
// 显示命令历史记录
function showHistoryList(list) {
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var stat = historyStatus(obj.Status);
        var time = convertDateTimeToJavascriptDate(obj.ScheduleTime);
        html += hisItem.replace(/%time%/, time.pattern(_datetimepatternFMT))
            .replace(/%cmd%/, obj.Dommand).replace(/%class%/, stat.classs).replace(/%text%/, stat.text);
    }
    if (isStringNull(html)) { html = "No records."; }
    $(".bs-callout:eq(1)").html(html);
}


//var cmdStatus = "%time% <code>%code%</code> %desc%<br />";

function showWarningMessage(obj) {
    var time = convertDateTimeToJavascriptDate(obj.date).pattern("HH:mm:ss");
    showWarningLine(time, currentTestingCommand, obj.desc);
}

function showWarningLine(time, code, desc) {
    var content = $(".bs-callout-warning");
    var html = content.html();
    html += cmdStatus.replace(/%time%/, time).replace(/%code%/, code).replace(/%desc%/, desc);
    content.html(html);
}