$(document).ready(function () {
    $(".input-daterange").each(function () {
        var inputs = $(this).children(".input-md");
        var now = new Date();
        var then = now.dateAfter(-5, 1);
        $(inputs[0]).val(then.pattern(_datepatternFMT));
        $(inputs[1]).val(now.pattern(_datepatternFMT));
    });
    $(".input-daterange").datepicker({
        format: _datepickerFMT,
        weekStart: 0,
        autoclose: true
    });
    $(".date-test").val(new Date().pattern(_datepatternFMT));
    $(".date-test").datepicker({
        format: _datepickerFMT,
        weekStart: 0,
        autoclose: true
    });

    $("#functionBar a").each(function () {
        var href = $(this).attr("href");
        if (href != "#") {
            if (href.indexOf("?key") < 0) {
                $(this).attr("href", href + "?key=" + $("#hidKey").val());
            }
        }
    });
});