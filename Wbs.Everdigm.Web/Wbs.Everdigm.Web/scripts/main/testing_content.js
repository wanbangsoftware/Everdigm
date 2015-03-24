
//var currentTestingCommand = "";
//var isInTestProgress = false;
//// 计时器
//var _timer = null;
//// 获取服务器上命令发送状态的时间间隔
//var _timerInterval = 1000;
//// 一个命令发送之后最大尝试获取状态的次数30次，超过这个次数后显示发送结果为失败或超时等
//var _timerMaxtimes = 150, _timerTimes = 0;
//// 发送命令之后的命令的id，通过此id查询后续命令的发送状态
//var _lastCommandId = 0, _lastCommandStatus = -1;

$(document).ready(function () {
    $(".alert").alert();

    $("#analyseModal").on("hidden.bs.modal", function () {
        $("#bt_" + currentTestingCommand).button("reset");
        $("[id^=\"bt_\"]").prop("disabled", false);
    });

    $("[id^=\"bt_\"]").on("click", function () {
        $("[id^=\"bt_\"]").prop("disabled", true);
        var $btn = $(this).button("loading");
        var cmd = $(this).prop("id").replace("bt_", "");

        $("#analyseModal").modal("show");
        if (!isInTestProgress) {
            currentTestingCommand = cmd;
            $(".modal-title").text("Testing progress of: " + cmd + ", please wait...");
            sendTerminalCommand(cmd);
        }
    })

    $("#terminalInfo").popover({
        animation: true,
        trigger: "hover",
        html: true,
        //title:"Base Information",
        content: $("#terminalContent").val()
    });
});

function sendTerminalCommand(cmd) {
    $(".modal-header").removeClass("btn-danger").addClass("btn-primary");
    var ter = $("#terminalInfo").html();
    GetJsonData("../ajax/command.ashx", { "type": "terminal", "cmd": cmd, "data": ter },
            function (data) {
                if (data.status == 0) {
                    // 标记已进入测试环节，再点击其他按钮不会再发命令
                    isInTestProgress = true;
                    _lastCommandId = parseInt(data.desc);
                    prepareTimer(timerOnTime);
                    data.desc = "will be sent by server.";
                    showWarningMessage(data);
                } else {
                    showWarningMessage(data);
                }
            });
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
// 计算已用时间
function calculateTimeused() {
    var used = _timerTimes * _timerInterval;
    var time = new Date(used);
    $("#timeUsed").html("time used " + time.pattern("mm:ss"));
    var max = _timerInterval * _timerMaxtimes;
    if (used > max * 0.7) {
        $(".modal-header").removeClass("btn-warning").addClass("btn-danger");
        return;
    }
    if (used > max * 0.5) {
        $(".modal-header").removeClass("btn-primary").addClass("btn-warning");
        return;
    }
}

function timerOnTime() {
    _timerTimes++;
    if (_timerTimes > _timerMaxtimes) {
        showWarningLine(new Date().pattern("HH:mm:ss"), currentTestingCommand, "Timedout, maybe you should try again.");
        // 大于最大次数之后，停止timer并设置命令发送状态为未发送命令状态以便再发送其他命令
        progressComplete();
    } else {
        // 循环读取服务器上改命令发送记录的状态并显示
        if (_timerTimes % 5 == 0) {
            // 每隔5秒查询一次命令状态
            getCommandStatus();
        }
    }
    calculateTimeused();
}

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