$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./terminal_list.aspx"; });
    $("#btAdd").click(function () { document.location = "./terminal_register.aspx"; });

    $(".text-input").on("keyup", function (e) {
        if (!isStringNumber($(this).val()))
            $(this).val("");
    });
});