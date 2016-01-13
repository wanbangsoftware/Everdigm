
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
            $(".modal-title").text("Testing " + text + ", please wait...");
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