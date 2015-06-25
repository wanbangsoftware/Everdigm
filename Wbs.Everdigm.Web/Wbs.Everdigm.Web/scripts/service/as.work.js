$(document).ready(function () {
    $("#query").click(function () { $("#btQuery").click(); });

    $("#save").click(function () { $("#btSave").click(); });

    $("#tbodyBody").on("click", "tr", function (evt) {
        showEdit(evt);
    });

    $(".btn-primary").click(function () {
        $("#hiddenId").val("");
    });

    $("#modalNewWork").on("show.bs.modal", function (e) {
        var v = $("#hiddenId").val();
        $(".modal-title").text(isStringNull(v) ? "Add new work: " : "Edit work detail: ");
        if (isStringNull(v)) {
            $("#title").val("");
            $("#description").val("");
            $("#director").val("");
            //$(".input-daterange:eq(1) input").val("");
        }
    });

    $("[data-toggle=\"popover\"]").popover({
        trigger: "hover",
        html: true,
        title: "Description",
        container: "body",
        placement: "top",
        animation: true
    });
});

// 显示正在编辑的项
function showEdit(evt) {
    // 去除点击到工作详情页面的事件
    if (evt.target.tagName.toLowerCase() != "a") {
        var obj = evt.currentTarget;
        // 显示编辑
        var href = $(obj).children("td:eq(6)").children("a:eq(0)").attr("href");
        href = href.substr(href.indexOf("=") + 1);
        $("#hiddenId").val(href);
        updateDatepicker(new Date($(obj).children("td:eq(2)").text()), new Date($(obj).children("td:eq(3)").text()));
        // 显示待编辑详情
        $("#title").val($(obj).children("td:eq(6)").children("a:eq(0)").text());
        $("#director").val($(obj).children("td:eq(4)").text());
        $("#description").val($(obj).children("td:eq(6)").attr("data-data"));
        $("#modalNewWork").modal("show");
    }
}
// 更新datepicker里的日期范围
function updateDatepicker(startTime, endTime) {
    var $datepicker = $(".input-daterange:eq(1)");
    $datepicker.find("#start1").datepicker("update", startTime.pattern("yyyy/MM/dd"));
    $datepicker.find("#end1").datepicker("update", endTime.pattern("yyyy/MM/dd"));
    $datepicker.datepicker("updateDates");
}