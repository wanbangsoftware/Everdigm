$(document).ready(function () {
    $("#username").keydown(function (e) {
        if (e.keyCode == 13) {
            $("#password").focus();
            return false;
        }
    });
    // 提交
    $("input[type=\"submit\"]").click(function (e) {
        var u = $("#username").val();
        if (isStringNull(u)) {
            $("#username").focus();
            return false;
        }
        var p = $("#password").val();
        if (isStringNull(p)) {
            $("#password").focus();
            return false;
        }
        return true;
    });

    $("#username").focus();
});