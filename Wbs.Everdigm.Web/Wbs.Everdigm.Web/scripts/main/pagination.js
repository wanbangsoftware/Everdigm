// 分页相关的js
$(document).ready(function () {

    $("#hidPageIndex").val($.cookie($("#cookieName").val()));

    $("#btQuery").click(function () { $.cookie($("#cookieName").val(), $("#hidPageIndex").val()); });

    $("input[id^='bt_']").prop("disabled", true);

    $("#divPagging a").on("click", function (e) {
        var p = $(this).html();
        p = "«" == p ? "1" : p;
        p = "»" == p ? "0" : p;
        $("#hidPageIndex").val(p);
        e.preventDefault();
        if ($(this).outerHTML().indexOf("current") < 0)
            $("#btQuery").click();
    });
    $("#divPagging span").on("click", function () {
        var pid = parseInt($("#txtPage").val());
        $("#hidPageIndex").val(pid);
        $("#btQuery").click();
    }).hover(function () {
        $(this).css({ "color": "#990000" });
    }, function () {
        $(this).css({ "color": "#0053a2" });
    }).css({ "color": "#0053a2" });

    $("#txtPage").on("keydown", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#divPagging span").click();
        }
    }).on("keyup", function (e) {
        if (!isStringNumber($(this).val()))
            $(this).val("1");
    });
    $("#cbAll").change(function () {
        selectCheckBox($(this), "cb_");
        setEnabled();
    });
    $("input[id^='cb_']").click(function () {
        setEnabled();
    });
});

/*
    设置页面上相应按钮的状态
*/
function setEnabled() {
    $("#hidID").val(selectedCheckBox("cb_").replace(/cb_/g, ""));
    var v = $("#hidID").val();
    if (v == "") {
        $("[id^='bt_']").prop("disabled", true);
    } else if (v.indexOf(",") >= 0) {
        $("[id^='bt_D']").prop("disabled", false);
        $("[id^='bt_T']").prop("disabled", true);
    } else {
        $("[id^='bt_']").prop("disabled", false);
    }
}