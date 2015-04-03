var currentTestingCommand = "";
var isInTestProgress = false;
// 计时器
var _timer = null;
// 获取服务器上命令发送状态的时间间隔
var _timerInterval = 1000;
// 一个命令发送之后最大尝试获取状态的次数30次，超过这个次数后显示发送结果为失败或超时等
var _timerMaxtimes = 150, _timerTimes = 0;
// 发送命令之后的命令的id，通过此id查询后续命令的发送状态
var _lastCommandId = 0, _lastCommandStatus = -1;

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
