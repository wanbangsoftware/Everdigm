﻿$(document).ready(function () {
    $("#modalNewTracker").on("hide.bs.modal", function (e) {
        $("#hiddenId").val("");
        $("#number").val("").attr("disabled", false);
        $("#vehicle").val("");
        $("#director").val("");
    }).on("show.bs.modal", function (e) {
        var v = $("#hiddenId").val();
        $(".modal-title").text(isStringNull(v) ? "Add new tracker: " : "Edit tracker info: ");
    });

    $(".btn-success:eq(0)").click(function () {
        checkAddEdit();
    });

    $("#tbodyBody").on("click", "tr", function (e) {
        lineClick(e);
    }).children("tr").css("cursor", "pointer");
});

function checkAddEdit() {
    var value = $("#number").val();
    if (isStringNull(value))
    {
        alert("Tracker number cannot be blank.");
        return;
    }
    $("#btSave").click();
}

function lineClick(evt) {
    var name = evt.target.tagName.toLowerCase();
    if (name != "a") {
        var can = true;
        if (name == "span") {
            var span = $(evt.target).parent();
            var clazz = span.attr("class");
            if (!clazz.match("gray$")) {
                can = false;
                var a = span.parent();
                var href = a.attr("href");
                var start = href.substr(0, 6);
                if (start == "#sett_") {
                    var warning = clazz.match("warning$");
                    // 根据在线状态不同显示不同的warning
                    var modalClass = warning ? "btn-warning" : "btn-primary";
                    $("#alertModalLabelTrackerSetting").parent().removeClass(warning ? "btn-primary" : "btn-warning").addClass(modalClass);
                    if (warning) {
                        $("#divWarning").show();
                    }
                    else {
                        $("#divWarning").hide();
                    }
                    // 打开设置对话框
                    $("#alertTrackerSetting").modal("show");
                } else {
                    // 打开聊天对话框
                }
            }
        }
        if (can) {
            var obj = evt.currentTarget;
            // 显示编辑
            var href = $(obj).children("td:eq(1)").children("a:eq(0)").attr("href");
            href = href.substr(href.indexOf("=") + 1);
            $("#hiddenId").val(href);

            // 显示号码
            $("#number").val($(obj).children("td:eq(1)").children("a:eq(0)").text()).attr("disabled", true);
            $("#vehicle").val($(obj).children("td:eq(8)").text());
            $("#director").val($(obj).children("td:eq(9)").text());
            $("#modalNewTracker").modal("show");
        }
    }
}