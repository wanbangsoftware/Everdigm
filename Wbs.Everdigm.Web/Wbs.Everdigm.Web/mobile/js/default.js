$(document).ready(function () {
    $("#account").keyup(function () {
        this.value = this.value.replace(/[^0-9]/g, "");
    });

    $(".btn").click(function () {
        clearErrorMsgUp();
        var _id = $("#account").val();
        if ("" == _id) {
            loginError("Account can not be blank.");
            return;
        }
        var _pd = $("#password").val();
        if ("" == _pd) {
            loginError("Password can not be blank.");
            return;
        }
        tryLogin(_id, CryptoJS.MD5(_pd));
    });
});

function tryLogin(usr, pwd) {
    GetJsonData("../../ajax/query.ashx",
        { "type": "customer", "cmd": "login", "data": usr + "," + pwd }, function (data) {
            if (data.status == 0) {
                location.href = "devices.aspx";
            } else {
                loginError(data.desc);
            }
        });
}

function clearErrorMsgUp() {
    var obj = $("#errorMsgUp");
    if (obj.prop("class").indexOf("none") < 0) {
        obj.addClass("none");
    }
}
function loginError(msg) {
    $("#errorMsgUp").html(msg).removeClass("none");
}