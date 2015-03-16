var timeInteval = 1000;

var intervalHandler = null;

$(document).ready(function () {
    if (_maxTimes > 0) {
        $("#decount").html(_maxTimes + " second(s) to redirect...");
        intervalHandler = setInterval("decount();", timeInteval);
    } else { $("#decount").hide(); }
});

function decount() {
    _maxTimes--;
    $("#decount").html(_maxTimes + " second(s) to redirect...");
    if (_maxTimes <= 0) {
        clearInterval(intervalHandler);
        redirect();
    }
}

function redirect() {
    var url = $("#url").val();
    var topest = $("#topest").val();
    if (!isStringNull(url)) {
        if (isStringNull(topest) || "0" == topest) {
            window.location = url;
        }
        else {
            window.parent.location = url;
        }
    }
}