
$(document).ready(function () {
    $(".btn").click(function () {
        var index = $(".btn").index($(this));
        switch (index) {
            case 0: sendCommand(); break;
            case 1: queryCommandHistory("history"); break;
            case 2:
                var cmd = $("#cmdInfo").val();
                var text = $("#txtHour").val();
                text = (null == text || "" == text) ? "0" : text;
                var hour = parseInt(text);
                hour = isNaN(hour) ? 0 : hour;
                text = $("#txtMinute").val();
                text = (null == text || "" == text) ? "0" : text;
                var minute = parseInt(text);
                minute = isNaN(minute) ? 0 : minute;
                //showAlertModal("hour: " + hour + ",minute: " + minute);
                sendCommandTo(cmd, (hour * 60 + minute));
                break;
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
    queryCommandHistory("history");
});

function showAlertModal(title) {
    $(".modal-body:eq(0)").html(title);
    $("#alertModal").modal("show");
}

function initializeCommandList() {
    $("select").empty();
    var html = "<option value=\"\">Select command:</option>";
    $(".dropdown-menu a").each(function () {
        var text = $(this).text();
        var cmd = $(this).attr("href").replace(/#/, "");
        html += "<option value=\"" + cmd + "\" data-icon=\"" + getCommandIcon (cmd)+ "\">" + text + "</option>";
    });
    $("select").html(html);
}

function sendCommand() {
    var cmd = $("#cmdInfo").val();
    if (isStringNull(cmd)) {
        showAlertModal("Please select a command first.");
        return;
    } else {
        if (cmd == "ld_initial") {
            $("#alertModalWorktime").modal("show");
        } else {
            sendCommandTo(cmd, 0);
        }
    }
}

function sendCommandTo(cmd, param) {
    var id = $("#hidKey").val();
    GetJsonData("../ajax/command.ashx", { "type": "equipment", "cmd": cmd, "data": id, "param": param },
            function (data) {
                if (data.status == 0) {
                    currentTestingCommand = cmd;
                    _lastCommandId = parseInt(data.desc);
                    //var btn = $(".btn:eq(0)");
                    setButtonsSendingState(true);
                    prepareTimer(function () {
                        timerOnTime(function () {
                            getCommandStatus(function (obj) {
                                testProgressComplete(obj);
                            });
                        }, function () { testProgressComplete(); })
                    });
                    data.desc = "will be sent by server.";
                    showWarningMessage(data);
                    $("#analyseModal").modal("show");
                } else {
                    //showAlertModal(data.desc);
                    showWarningMessage(data);
                }
            });
}

function testProgressComplete() {
    //_timer.pause();
    //_lastCommandStatus = -1;
    setButtonsSendingState(false);
}
