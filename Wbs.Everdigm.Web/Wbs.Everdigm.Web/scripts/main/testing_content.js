$(document).ready(function () {
    $(".alert").alert();

    $("[id^=\"bt_0x\"]").on("click", function () {
        $("[id^=\"bt_0x\"]").prop("disabled", true);
        var $btn = $(this).button("loading");

        // business logic...
        //$btn.button('reset');
        $(".modal-body").html("test ok<br />now bye bye.");
        $("#analyseModal").modal('show');
    })

    $("#terminalInfo").popover({
        animation: true,
        trigger: "hover",
        html: true,
        //title:"Base Information",
        content: $("#terminalContent").val()
    });
});