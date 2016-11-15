$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./terminal_list.aspx"; });
    $("#btAdd").click(function () { document.location = "./terminal_register.aspx"; });

    $(".text-input").on("keyup", function (e) {
        if (!isStringNumber($(this).val()))
            $(this).val("");
    });

    $("#tbodyBodies td").on("click", function (e) {
        var cursor = $(this).css("cursor");
        var text = $(this).text();
        if (cursor == "pointer" && text.indexOf("add") < 0) {
            var sate = $(this).text().replace(/[^0-9]/ig, "");
            var id = $(this).children("a:eq(0)").attr("href").replace("#del_", "");
            var stopped = $(this).parent().children("td:eq(9)").text();
            if (stopped.indexOf("stopped") >= 0) { return; }
            else {
                $("#hidBoundSatellite").val(id);
                $("#warningStoppingContentText").html("Are you sure wanna <font color='#FF0000'>STOP</font> using this satellite(<font color='#0000FF'>" + sate + "</font>)?");
                $("#warningStopping").modal("show");
            }
        }
    });

    // 为终端添加卫星模块
    $("#tbodyBodies a").on("click", function (e) {
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
                $("#hidBoundSatellite").val(id);
                var tnumber = $(this).parent().prev().children("a:eq(0)").text();
                var sate = $(this).parent().text().replace(/[^0-9]/ig, "");
                $("#warningContentText").html("Are you sure wanna unbind this satellite(<font color='#FF0000'>" + sate + "</font>) & terminal(<font color='#FF0000'>" + tnumber + "</font>)?");
                $("#warningLoading").modal("show");
                e.stopPropagation();
                //if (confirm("Are you sure to unbind the satellite and terminal?")) {
                //    $("#hidBoundSatellite").val(id);
                //    $("#btBoundSatellite").click();
                //}
            } else if (type == "unbind") {
                // unbind terminal & equeipment
                if (confirm("Are you sure to unbind this equipment and terminal?")) {
                    $("#hidBoundSatellite").val(id);
                    $("#btUnbindEquipment").click();
                }
            }
        }
    });

    $("#satUnbind").click(function () {
        $("#btBoundSatellite").click();
    });

    $("#satStopping").click(function () { $("#btnSatelliteStopping").click(); });
});