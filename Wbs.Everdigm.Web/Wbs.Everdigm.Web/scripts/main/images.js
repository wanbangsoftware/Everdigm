$(document).ready(function () {
    $("img").click(function () {
        var msg = $(this).attr("src") + ",";
        windowPopupCloseEvent(msg);
        close();
    });
});