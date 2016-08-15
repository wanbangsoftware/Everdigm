$(document).ready(function () {
    $("#btSave").click(function () {
        var t = $("[id$=\"selectedType\"]").val();
        var m=$("[id$=\"selectedModel\"]").val();
        var w=$("[id$=\"hidWarehouse\"]").val();
        var n=$("[id$=\"number\"]").val();
        var o = $("[id$=\"old\"]").val();
        var f = $("[id$=\"hidFunctional\"]").val();
        var of = $("[id$=\"oldFunc\"]").val();
        if ((t != "0") ||
            (m != "0") ||
            (w != "0") ||
            (f != of) ||
            (n != o)) {
            $("[id$=\"btSaveInfo\"]").click();
        }
    });
    $("a[href=\"#bind\"]").click(function () {
        // 打开新窗口来选择终端
        showDialogWindows("./terminals.aspx?key=" + $("[id$=\"oldFunc\"]").val(), function (data) {
            if (typeof (data) == "undefined" || null == data) { } else {
                if (data.ids != "") {
                    if (!isStringNull(data.names)) {
                        $("[id$=\"newTerminal\"]").val(data.names);
                        // 保存
                        $("[id$=\"btSaveInfo\"]").click();
                    }
                }
            }
        }, 600, 550);
    });
    $("a[href=\"#unbind\"]").click(function () {
        $("#analyseModal").modal("show");
    });
    $("#satWarning").click(function () { $("[id$=\"btUnbind\"]").click(); });
    $("#btDel").click(function () { $("#warningDelete").modal("show"); });
    $("#satDelete").click(function () { $("[id$=\"btDelete\"]").click(); });
});