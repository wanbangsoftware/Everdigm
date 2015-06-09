$(document).ready(function () {
    $("#btSave").click(function () {
        var t = $("[id$=\"selectedType\"]").val();
        var m=$("[id$=\"selectedModel\"]").val();
        var w=$("[id$=\"hidWarehouse\"]").val();
        var n=$("[id$=\"number\"]").val();
        var o=$("[id$=\"old\"]").val();
        if ((t != "0") ||
            (m != "0") ||
            (w != "0") ||
            (n != o)) {
            $("[id$=\"btSaveInfo\"]").click();
        }
    });
});