var currentTestingCommand = "", currentTestCommandTitle = "";
var isInTestProgress = false;
// 计时器
var _timer = null;
// 获取服务器上命令发送状态的时间间隔
var _timerInterval = 1000;
var _MAX_GSM_ = 150, _MAX_SAT_ = 4800;
// 一个命令发送之后最大尝试获取状态的次数30次，超过这个次数后显示发送结果为失败或超时等
var _timerMaxtimes = 150, _timerTimes = 0;
// 发送命令之后的命令的id，通过此id查询后续命令的发送状态
var _lastCommandId = 0, _lastCommandStatus = -1;

/*
命令发送状态的定义
*/
var _command_send_failed = 9, _command_returned = 10,
    _command_gsm_begin = 0,
    _command_sat_begin = 2, _command_sat_handled = 3, _command_sat_sent = 6, _command_sat_received = 8;


var cmdStatus = "%time% Command <code>%code%</code> %desc%<br />";

$(document).ready(function () {

});

// 启动计时器
function prepareTimer(timerFunction) {
    if (null == _timer) {
        _timer = $.timer(_timerInterval, function () {
            timerFunction();
        });
    }
    else {
        _timer.resume();
    }
}
// 测试过程完毕
function progressComplete() {
    isInTestProgress = false;
    _timer.pause();
    _timerTimes = 0;
    _lastCommandStatus = -1;
}


function getCommandIcon(code) {
    var ret = "";
    switch (code) {
        case "signal": ret = "glyphicon-signal"; break;
        case "position": ret = "glyphicon-map-marker"; break;
        case "monitor": ret = "glyphicon-dashboard"; break;
        case "fault": ret = "glyphicon-flag"; break;
        case "worktime": ret = "glyphicon-time"; break;
    }
    return ret;
}
// 获取命令的发送状态
function commandHistoryStatus(stat) {
    var ret = { classs: "", text: "" };
    switch (stat) {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
            ret.classs = "label-info"; ret.text = "In progress...";
            break;
        case 10:
            ret.classs = "label-success text-muted"; ret.text = "Success"; break;
        case 11: ret.classs = "label-warning text-danger"; ret.text = "Timedout"; break;
        case 12: ret.classs = "label-warning text-danger"; ret.text = "EPOS Fail"; break;
        case 13: ret.classs = "label-warning text-danger"; ret.text = "Security Blocked"; break;
        case 16: ret.classs = "label-danger text-danger"; ret.text = "Firmware cannot handle"; break;
        case 17: ret.classs = "label-danger text-danger"; ret.text = "Eng. not start"; break;
        case 18: ret.classs = "label-success text-danger"; ret.text = "Not need return"; break;
        default: ret.classs = "label-danger text-danger"; ret.text = "Fail"; break;
    }
    return ret;
}

var _his_Item = "<div style=\"margin-bottom: 1px;\">[%time%] <code>%cmd%</code>, response <span class=\"command-status label %class%\">%text%</span>.</div>";

// 显示命令历史记录
function showHistoryList(list) {
    var html = "";
    for (var i in list) {
        var obj = list[i];
        var stat = commandHistoryStatus(obj.Status);
        var time = convertDateTimeToJavascriptDate(obj.ScheduleTime);
        html += _his_Item.replace(/%time%/, time.pattern(_datetimepatternFMT))
            .replace(/%cmd%/, obj.Command).replace(/%class%/, stat.classs).replace(/%text%/, stat.text);
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
    html += cmdStatus.replace(/%time%/, time).replace(/%code%/, currentTestCommandTitle).replace(/%desc%/, desc);
    content.html(html);
}

// 计算已用时间
function calculateTimeused() {
    var used = _timerTimes * _timerInterval;
    var time = new Date(new Date(used).toUTCString().substr(0, 25));
    $("#timeUsed").html(time.pattern(_timerMaxtimes == _MAX_GSM_ ? "mm:ss" : "H:mm:ss"));
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

function timerOnTime(functionOnTime, functionTimeout) {
    _timerTimes++;
    if (_timerTimes > _timerMaxtimes) {
        showWarningLine(new Date().pattern("HH:mm:ss"), currentTestingCommand, "Timedout, maybe you should try again.");
        // 大于最大次数之后，停止timer并设置命令发送状态为未发送命令状态以便再发送其他命令
        progressComplete();
        functionTimeout(_timerTimes);
    } else {
        // 循环读取服务器上改命令发送记录的状态并显示
        if (_timerTimes % 5 == 0) {
            // 每隔5秒查询一次命令状态
            functionOnTime(_timerTimes);
            //getCommandStatus();
        }
    }
    calculateTimeused();
}
// 发送命令
function getCommandStatus(functionOnComplete) {
    GetJsonData("../ajax/command.ashx", { "type": "query", "cmd": currentTestingCommand, "data": _lastCommandId },
           function (data) {
               // 如果是卫星方式等待发送/卫星服务已处理/卫星服务已发送
               if (data.status == _command_sat_begin || data.status == _command_sat_handled ||
                   data.status == _command_sat_sent || data.status == _command_sat_received) {
                   _timerMaxtimes = _MAX_SAT_;
                   $("#satWarning").show();
               } else {
                   if (_timerMaxtimes != _MAX_GSM_) {
                       _timerMaxtimes = _MAX_GSM_;
                       $("#satWarning").hide();
                   }
               }
               if (_lastCommandStatus != data.status) {
                   showWarningMessage(data);
                   _lastCommandStatus = data.status;
               }
               if (data.status < 0 || data.status >= _command_send_failed) {
                   progressComplete();
                   functionOnComplete(data);
                   // 状态获取失败
                   //if (data.status == _command_returned) {
                   //    // 发送成功
                   //}
               }
           });
}

// 设置发送按钮的发送状态 true=sending, false=standby
function setButtonsSendingState(sending) {
    var btn = $(".btn:eq(0)");
    var html = "<span class=\"glyphicon " +
        (!sending ? "glyphicon-repeat" : "glyphicon-repeat glyphicon-refresh-animate") + "\"></span> Send" + (!sending ? "" : "ing...");
    btn.html(html);
    //if (sending)
    //    btn.addClass("disabled");
    //else
    //    btn.removeClass("disabled");
}

function queryCommandHistory(queryType) {
    $(".bs-callout:eq(1)").html("Loading data...");
    var id = $("#hidKey").val();
    var inputs = $(".input-daterange .input-md");
    var start = $(inputs[0]).val();
    var end = $(inputs[1]).val();
    var cmd = $("select").val();
    GetJsonData("../ajax/command.ashx", { "type": queryType, "cmd": cmd, "data": id, "start": start, "end": end },
        function (data) {
            if (data.hasOwnProperty("desc")) {
                showAlertModal(data.desc);
                $(".btn-info").addClass("disabled");
            } else {
                showHistoryList(data);
            }
        });
}