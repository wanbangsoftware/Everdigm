$(document).ready(function () {

    $("#tbodyBody").on("click", "tr", function (e) {
        lineClick(e);
    }).children("tr").css("cursor", "pointer");

    $("[id^=\"deadLine\"]").datepicker({
        format: "yyyy/mm/dd",
        weekStart: 0,
        todayBtn: true,
        todayHighlight: true,
        autoclose: true
    });

    $("#rentalConfirm").click(function () { confirmRental(); });
    $("#rentalEditor").click(function () { confirmEdit();});

    $(".label-default").click(function () {
        // 打开新窗口来选择客户信息
        showDialogWindows("./customer_pop.aspx", function (data) {
            if (typeof (data) == "undefined" || null == data) { } else {
                if (data.names != "") {
                    if (!isStringNull(data.names)) {
                        //查询这个id的客户详细信息
                        fetchCustomer(data.names);
                    }
                }
            }
        }, 750, 360);
    });

    $("input[id^=\"option\"]").change(function () {
        var id = $(this).prop("id");
        var checked = $(this).prop("checked");
        if (checked) {
            if (id.indexOf("Extend") > 0) {
                $("#divExtend").removeClass("hidden");
                $("#divReclaim").removeClass("hidden").addClass("hidden");
            } else {
                $("#divExtend").removeClass("hidden").addClass("hidden");
                $("#divReclaim").removeClass("hidden");
            }
        }
    });
    $("#cbRepair").change(function () {
        var checked = $(this).prop("checked");
        $(this).next().attr("class", checked ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-ban-circle");
        $(this).next().next().text(checked ? " back to repair" : " do not need repairs");
    });
});

function lineClick(evt) {
    var obj = evt.currentTarget;
    var title = $(obj).children("td:eq(7)").attr("title");
    var _id = $(obj).children("td:eq(2)").children("a:eq(0)").attr("href").replace("#", "");
    if (title == "Inventory") {
        $("#hiddenRentalId").val(_id);
        // 库存的，打开租赁出库界面
        $("#labelRentalOut").text("Rental: " + $(obj).children("td:eq(2)").children("a:eq(0)").html());
        $("#modalRentalOut").modal("show");
    } else if (title == "Rental") {
        // 已租赁出去的，打开延期或回收界面
        $("#hiddenEditId").val(_id);
        $("#labelRentalOver").text("Extend/Reclaim: " + $(obj).children("td:eq(2)").children("a:eq(0)").html());
        $("#modalRentalOver").modal("show");
    }
}

function fetchCustomer(id) {
    GetJsonData("../ajax/query.ashx", { "type": "customer", "cmd": "customer", "data": id }, function (data) {
        if (data.length > 0) {
            var c = data[0];
            $("#hiddenCustomer").val(c.id);
            $("#rentalConfirm").removeClass("disabled");
            $("#popupTbody").children("tr:eq(1)").children("td:eq(1)").text(c.Name);
            $("#popupTbody").children("tr:eq(2)").children("td:eq(1)").text(c.Code);
            $("#popupTbody").children("tr:eq(2)").children("td:eq(3)").text(c.Phone);
        }
    });
}

function confirmRental() {
    $("#spanRentalConfirm").text("");
    var deadLine = $("#deadLine").val();
    if (isStringNull(deadLine)) {
        $("#spanRentalConfirm").text("Please set the deadline value.");
        return;
    }
    $("#confirmRental").click();
}

function confirmEdit() {
    $("#spanRentalOver").text("");
    var id = $("input:radio:checked").prop("id");
    if (id.indexOf("Extend") > 0) {
        // 延长租赁期限
        var deadLine = $("#deadLineExtend").val();
        if (isStringNull(deadLine)) {
            $("#spanRentalOver").text("Please set the extend deadline.");
            return;
        }
        $("#btRentalEdit").click();
    } else if (id.indexOf("Reclaim") > 0) {
        // 回收
        var wh = parseInt($("#hiddenWarehouse").val());
        if (wh < 1) {
            $("#spanRentalOver").text("Please choose the warehouse.");
            return;
        }
        $("#btRentalEdit").click();
    }
}