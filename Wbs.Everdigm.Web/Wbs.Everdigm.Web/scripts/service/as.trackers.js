$(document).ready(function () {
    $("#modalNewTracker").on("hide.bs.modal", function (e) {
        $("#hiddenId").val("");
        $("#number").val("").attr("disabled", false);
        $("#vehicle").val("");
        $("#director").val("");
    }).on("show.bs.modal", function (e) {
        var v = $("#hiddenId").val();
        $(".modal-title").text(isStringNull(v) ? "Add new tracker: " : "Edit tracker info: ");
    });

    $(".btn-success").click(function () {
        checkAddEdit();
    });

    $("#tbodyBody").on("click", "tr", function (e) {
        lineClick(e);
    }).children("tr").css("cursor", "pointer");
});

function checkAddEdit() {
    var value = $("#number").val();
    if (isStringNull(value))
    {
        alert("Tracker number cannot be blank.");
        return;
    }
    $("#btSave").click();
}

function lineClick(evt) {
    if (evt.target.tagName.toLowerCase() != "a") {
        var obj = evt.currentTarget;
        // 显示编辑
        var href = $(obj).children("td:eq(1)").children("a:eq(0)").attr("href");
        href = href.substr(href.indexOf("=") + 1);
        $("#hiddenId").val(href);

        // 显示号码
        $("#number").val($(obj).children("td:eq(1)").children("a:eq(0)").text()).attr("disabled", true);
        $("#vehicle").val($(obj).children("td:eq(6)").text());
        $("#director").val($(obj).children("td:eq(7)").text());
        $("#modalNewTracker").modal("show");
    }
}