
var queryedCustomers = new Array();

$(document).ready(function () {
    $("#queryEquipment").click(function () {
        queryEquipment($("#txtEquipment").val(), function (data) {
            if (data.length > 0) {
                showEquipmentInfo(data[0]);
                showEquipmentSituation(data[0]);
            } else {
                showError("No equipment like \"" + $("#txtEquipment").val() + "\"", "no record");
                showEquipmentInfo(null);
                showEquipmentSituation(null);
            }
        });
    });
    // query equipment which in store in warehouse
    $("#txtEquipment").autocomplete({
        source: function (request, response) {
            var obj = {};
            var mod = $("#selModel").val();
            obj.Model = isStringNull(mod) ? 0 : parseInt(mod);
            obj.Number = $("#txtEquipment").val();
            GetJsonData("../ajax/query.ashx",
                { "type": "equipment", "cmd": "query", "data": $.toJSON(obj) },
                function (data) {
                    response($.map(data, function (item) {
                        var model = getModel(item.Model)[0];
                        var number = model.Code + item.Number;
                        return {
                            label: number,
                            value: number
                        }
                    }));
                });
        },
        minLength: 4,
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
    // query customer code
    $("#txtCustomerCode").autocomplete({
        source: function (request, response) {
            GetJsonData("../ajax/query.ashx",
                { "type": "customer", "cmd": "query", "data": $("#txtCustomerCode").val() },
                function (data) {
                    queryedCustomers = data;
                    response($.map(data, function (item) {
                        var number = item.Name+"," + item.Code;
                        return {
                            label: number,
                            value: item.Code
                        }
                    }));
                });
        },
        select: function (event, ui) {
            var v = jLinq.from(queryedCustomers).equals("Code", ui.item.value).select();
            if (v.length > 0) {
                $("#hidCustomerId").val(v[0].id);
            }
        },
        minLength: 4,
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
});

function showEquipmentSituation(obj) {
    var status = null == obj ? null : getStatusCode(obj.Status)[0];
    var situation = null == obj ? null : getStatusStatus(status.Status)[0];
    if (null != obj && situation.IsInventory == true) {
        $("#_e_3_ td:eq(1)").html(null == obj ? "-" : (situation.Name + "(" + status.Name + ")"));
        $("#btSave").prop("disabled", false);
        $("[id^=\"_c_\"]").show();
    }
    else {
        $("#_e_3_ td:eq(1)").html(null == obj ? "-" : ("<span style=\"color: #ff0000;\">" +
                situation.Name + "(" + status.Name + ")</span> Cannot change to deliver status"));
        $("#btSave").prop("disabled", true);
        $("[id^=\"_c_\"]").hide();
    }
}
