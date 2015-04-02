var _t_ = false, _e_ = false;

$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./terminal_list.aspx"; });
    $("#tbodyBody").on("click", "tr", function () {
        $(this).children("td:eq(0)").children("input:eq(0)").prop("checked", true);
        setCheckedId();
    });
    $("input[id^=\"radio_\"]").click(function () {
        setCheckedId();
    });
    // query equipment
    $("#txtEquipment").autocomplete({
        source: function (request, response) {
            var obj = {};
            var mod = $("#selModel").val();
            obj.Model = isStringNull(mod) ? 0 : parseInt(mod);
            obj.Number = $("#txtEquipment").val();
            GetJsonData("../ajax/query.ashx",
                { "type": "equipment", "cmd": "notbind", "data": $.toJSON(obj) },
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

});

function setCheckedId() {
    var id = "";
    $("input[id^=\"radio_\"]").each(function () {
        if ($(this).is(':checked'))
        {
            id = $(this).prop("id").replace("radio_", "");
            $("#hidEquipmentId").val(id);
            $("#btSave").prop("disabled", false);
            return false;
        }
    });
}
