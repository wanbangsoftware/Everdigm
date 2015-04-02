var customerList = null;
$(document).ready(function () {
    $("#tbodyBody a").click(function (e) {
        $("#titleCheckout").html("Check out for: " + $(this).text());
        $("#hidCheckEquipmentId").val($(this).prop("id").replace("a_", ""));
        $("#newCheckoutSave").removeClass("disabled").addClass("disabled");
    });

    $("#number").typeahead({
        items: 5,
        minLength: 4,
        source: function (query, process) {
            GetJsonData("../ajax/query.ashx", { "type": "customer", "cmd": "query", "data": query },
                function (data) {
                    if (data.length > 0) {
                        customerList = data;
                        var ret = $.map(data, function (item) { return item.Code; });
                        process(ret);
                    }
                });
        }
    });

    $("#queryCustomer").click(function () {
        var num = $("#number").val();
        var item = jLinq.from(customerList).equals("Code", num).select();
        var tbody = $("#popupTbody");
        if (item.length > 0) {
            var obj = item[0];
            tbody.children("tr:eq(1)").children("td:eq(1)").html(obj.Name);
            tbody.children("tr:eq(1)").children("td:eq(3)").html(obj.Phone);
            tbody.children("tr:eq(2)").children("td:eq(1)").html(obj.Fax);
            $("#newCheckoutSave").removeClass("disabled");
            $("#hidCheckCustomerId").val(obj.id);
        } else {
            tbody.children("tr:eq(1)").children("td:eq(1)").html("");
            tbody.children("tr:eq(1)").children("td:eq(3)").html("");
            tbody.children("tr:eq(2)").children("td:eq(1)").html("");
        }
    });

    $("#newCheckoutSave").click(function () {
        var type = $("#ddlOuttype").val();
        if (isStringNull(type)) {
            alert("Please select the type.");
            return;
        }
        $("#btCheckoutStorage").click();
    });
});