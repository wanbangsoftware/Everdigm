/*
    通过jQuery的ajax方法post数据到服务器
*/
function GetJsonData(url, data, funSucceed, funFailed) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: url,
        //提交的数据{ "cmd": cmd, "jsondata": jsondata } 传的数据必须为json对象，而不是字符串
        data: data,
        success: function (data, textStatus) {
            funSucceed(data);
        },
        error: function (msg) {
            if (typeof (failed) != "undefined") {
                funFailed(msg);
            }
        }
    });
}