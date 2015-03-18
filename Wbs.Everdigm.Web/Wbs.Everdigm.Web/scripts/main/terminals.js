$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./terminal_list.aspx"; });
    $("#btAdd").click(function () { document.location = "./terminal_register.aspx"; });

    $(".text-input").on("keyup", function (e) {
        if (!isStringNumber($(this).val()))
            $(this).val("");
    });

    // 为终端添加卫星模块
    $("#tbodyBody a").on("click", function (e) {
        var href = $(this).attr("href");
        var sharpIndex = href.indexOf("#");
        if (sharpIndex >= 0) {
            href = href.substr(sharpIndex + 1);
            var underscore = href.indexOf("_");
            var type = href.substr(0, underscore);
            var id = href.substr(underscore + 1);
            if (type == "add") {
                // 打开新窗口来添加卫星模块
                showDialogWindows("./terminal_satellite.aspx?key=" + id, function (data) {
                    //alert(data);
                    if (typeof (data) == "undefined" || null == data) { } else {
                        if (data.ids != "" && data.names != "") {
                            $("#hidBoundSatellite").val(data.ids + "," + data.names);
                            $("#btBoundSatellite").click();
                        }
                    }
                }, 500, 300);
            } else if (type == "del") {
                //
                if (confirm("Are you sure to unbound the satellite?")) {
                    $("#hidBoundSatellite").val(id);
                    $("#btBoundSatellite").click();
                }
            }
        }
    });
});