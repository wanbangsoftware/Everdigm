$(document).ready(function () {
    $("#query").click(function () {
        $("#btQuery").click();
    });

    $(".btn-primary").click(function () {
        var n = $("#txtQueryNumber").val();
        if (!isStringNull(n)) {
            $("#btSave").click();
        } else {
            $("#modalWarning").modal("show");
        }
    });
});