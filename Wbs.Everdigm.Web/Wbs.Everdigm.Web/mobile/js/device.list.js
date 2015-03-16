$(document).ready(function () {
    $("#wrap").on("click", "dl", function () {
        var id = $(this).prop("id").replace(/#/,"");
        $("[type=\"hidden\"][id$=\"_id\"]").val(id);
        $("[type=\"submit\"]").click();
    });
});