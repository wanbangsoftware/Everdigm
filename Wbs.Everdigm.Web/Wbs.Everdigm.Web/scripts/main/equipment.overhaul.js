$(document).ready(function () {
    $("#tbodyBody a").click(function (e) {
        $("#title").html("Inspection & repair for: " + $(this).text());
        $("#hidRepairId").val($(this).prop("id").replace("a_", ""));
    });

    $("#btRepairSave").click(function () {
        $("#btRepairComplete").click();
    });
});