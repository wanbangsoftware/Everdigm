$(document).ready(function () {
    $(".btn").click(function () {
        var index = $(".btn").index($(this));
        if (index == 0) {
            // 发送命令
            if (isInTestProgress) {
                $("#analyseModal").modal("show");
            } else {
                sendCommand();
            }
        } else {
            // 查询命令历史记录
            query_command_history();
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
    query_command_history();
});

function query_command_history() {
    queryCommandHistory("sechistory");
}

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
                    isInTestProgress = true;
                    currentTestingCommand = cmd;
                    _lastCommandId = parseInt(data.desc);
                    //var btn = $(".btn:eq(0)");
                    setButtonsSendingState(true);
                    prepareTimer(function () {
                        timerOnTime(function () {
                            getCommandStatus(function (obj) {
                                testProgressComplete();
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
}

function testProgressComplete() {
    setButtonsSendingState(false);
}
