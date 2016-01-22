var printProgressTimer = null;
var maxWaitPrintTimes = 150;// 150 sec
var curWaitPrintTimes = 0;
var printType = "terminal";
var isPrinting = false;
var startTimeTicks = 0;
var historyQueryInterval = 10000;

$(document).ready(function () {
    $(".alert").alert();

    $("#analyseModal").on("hidden.bs.modal", function () {
        $("#bt_" + currentTestingCommand).button("reset");
        $("[id^=\"bt_\"]").prop("disabled", false);
    });

    $("[id^=\"bt_\"]").on("click", function () {
        $("[id^=\"bt_\"]").prop("disabled", true);
        var text = $(this).text();
        var $btn = $(this).button("loading");
        var cmd = $(this).prop("id").replace("bt_", "");

        $("#analyseModal").modal("show");
        if (!isInTestProgress) {
            currentTestingCommand = cmd;
            currentTestCommandTitle = text;
            $(".modal-title:eq(0)").text("Testing " + text + ", please wait...");
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
    $("#modalPrinting").on("hide.bs.modal", function (event) {
        //event.stopPropagation();
        $("#printLabel").button("reset");
    });
    $("#printLabel").on("click", function () {
        if (!isPrinting) {
            // 打印标签
            $(this).button("loading");

            $(".progress-bar").css("width", "0%");
            curWaitPrintTimes = 0;
            $("#spanPrintStatus").text();
            $("#spanPrintStatusText").text(printStatus(1));
            requestPrint();
        } else {
            // 如果处于打印过程中则显示打印过程
            $("#modalPrinting").modal("show");
        }
    });
});

function sendTerminalCommand(cmd) {
    $("#analyseModal.modal-header").removeClass("btn-danger").addClass("btn-primary");
    var ter = $("#terminalInfo").html();
    var force = $("input:radio[name =\"options\"]:checked").val();
    GetJsonData("../ajax/command.ashx", { "type": "terminal", "cmd": cmd, "by": force, "data": ter }, function (data) {
        if (data.status == 0) {
            // 标记已进入测试环节，再点击其他按钮不会再发命令
            isInTestProgress = true;
            _lastCommandId = parseInt(data.desc);
            prepareTimer(function () {
                timerOnTime(function () {
                    getCommandStatus(function (obj) {
                    });
                }, function () { })
            });
            data.desc = "will be sent by server" + (force == "sms" ? "(force to SMS)" : "") + ".";
            showWarningMessage(data);
        } else {
            showWarningMessage(data);
        }
    }, function (data) {
        // 执行失败
        showWarningMessage(data);
    });
}

function showPrintProgress() {
    if (null == printProgressTimer) {
        printProgressTimer = $.timer(1000, function () {
            curWaitPrintTimes++;
            $("#spanPrintStatus").text(curWaitPrintTimes + "s");
            var percentage = parseInt(curWaitPrintTimes / maxWaitPrintTimes * 100);
            $(".progress-bar").css("width", percentage + "%");
            if (curWaitPrintTimes >= maxWaitPrintTimes) {
                $("#spanPrintStatusText").text("打印失败：超时未处理");
                stopTimer();
            } else {
                if (curWaitPrintTimes % 5 == 4) {
                    // 获取打印状态
                    requestPrintStatus();
                }
            }
        });
    }
}

function stopTimer() {
    isPrinting = false;
    printProgressTimer.stop();
    printProgressTimer = null;
    $("#printLabel").button("reset");
}

// 请求打印一个终端标签
function requestPrint() {
    var ter = $("#terminalInfo").html();
    var obj = {};
    obj.Number = ter;
    GetJsonData("../ajax/print.ashx", { "type": printType, "cmd": "request", "data": $.toJSON(obj), "timestamp": new Date().getTime() }, function (data) {
        if (data.State != 0) {
            showWarningDialog(data.Data);
        } else {
            isPrinting = true;
            $("#modalPrinting").modal("show");
            showPrintProgress();
        }
    });
}

function printStatus(state) {
    switch (state) {
        case 1: return "正在等待打印机处理...";
        case 2: return "标签正在打印中...";
        case 3: return "标签已打印完毕";
        default: return "未知的标签打印状态";
    }
}

function requestPrintStatus() {
    var ter = $("#terminalInfo").html();
    var obj = {};
    obj.Number = ter;
    GetJsonData("../ajax/print.ashx", { "type": printType, "cmd": "status", "data": $.toJSON(obj), "timestamp": new Date().getTime() }, function (data) {
        if (data.State >= 0) {
            $("#spanPrintStatusText").text(printStatus(data.State));
            if (data.State >= 3) {
                stopTimer();
            }
        } else { showWarningDialog(data.Data); }
    });
}

// 获取终端回报的历史纪录
function queryDataHistory() {
    //Sim card: 89001483<br />Satellite: <br />Equipment: DX500LCA-10039<br />Link: BLIND
    var ter = $("#terminalCardNumber").val();
    GetJsonData("../ajax/query.ashx", { "type": printType, "cmd": "history", "data": ter, "time": startTimeTicks }, function (data) {
        if (startTimeTicks <= 0) {
            // 第一次的时候获取的是服务器的当前时间
            startTimeTicks = data.Time;
            setTimeout("queryDataHistory();", historyQueryInterval);
        } else {
            showTerminalHistory(data);
        }
    });
}
var hisItem = "%time% <code>%cmd%</code> %content%<br />";
function showTerminalHistory(data) {
    var html = "<code>history data</code> will display in here.<br />";
    var d = $(data.Data);
    if (d.length > 0) {
        $(d).each(function (index, item) {
            var time = convertDateTimeToJavascriptDate(item.receive_time).pattern("HH:mm:ss");
            html += hisItem.replace(/%time%/, time).replace(/%cmd%/, item.command_id).replace(/%content%/, item.message_content);
        });
    }
    $(".bs-callout-info").html(html);
    setTimeout("queryDataHistory();", historyQueryInterval);
}