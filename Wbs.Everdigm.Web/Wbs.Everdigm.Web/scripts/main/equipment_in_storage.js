$(document).ready(function () {

    $("#openModal").click(function (e) {
        document.location = "./equipment_new_product.aspx";
    });
    $("#query").click(function () {
        $("[id$=\"btQuery\"]").click();
    });
});