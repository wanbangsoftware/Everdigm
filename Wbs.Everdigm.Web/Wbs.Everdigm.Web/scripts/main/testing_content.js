﻿
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
                    prepareTimer(function () {
                        timerOnTime(function () {
                            getCommandStatus(function (obj) {
                            });
                        }, function () { })
                    });
                    data.desc = "will be sent by server.";
                    showWarningMessage(data);
                } else {
                    showWarningMessage(data);
                }
            });
}