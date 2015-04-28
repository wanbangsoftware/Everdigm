$(document).ready(function () {
    $("#wrap").on("click", "dl", function () {
        var id = $(this).prop("id");
        document.location = "device.aspx?key=" + id;
        //$("[type=\"hidden\"][id$=\"_id\"]").val(id);
        //$("[type=\"submit\"]").click();
    });
});