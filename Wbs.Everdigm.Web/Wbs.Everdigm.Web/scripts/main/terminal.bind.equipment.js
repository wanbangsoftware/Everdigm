var _t_ = false, _e_ = false;

$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./terminal_list.aspx"; });

    // query terminal
    $("#txtTerminal").autocomplete({
        source: function (request, response) {
            GetJsonData("../ajax/query.ashx",
                { "type": "terminal", "cmd": "notbound", "data": $("#txtTerminal").val() },
                function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Number + "," + item.Sim,
                            value: item.Number
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
    // query equipment
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

    $("#queryTerminal").click(function () {
        _t_ = false;
        queryTerminal($("#txtTerminal").val(), function (data) {
            if (data.length > 0) {
                _t_ = true;
                showTerminalInfo(data[0]);
            } else {
                showError("No terminal like \"" + $("#txtTerminal").val() + "\"", "no record");
                showTerminalInfo(null);
            }
            checkSaveButton();
        });
    });

    $("#queryEquipment").click(function () {
        _e_ = false;
        queryEquipment($("#txtEquipment").val(), function (data) {
            if (data.length > 0) {
                var obj=data[0];
                showEquipmentInfo(obj);
                _e_ = null == obj.Terminal;
            } else {
                showError("No equipment like \"" + $("#txtEquipment").val() + "\"", "no record");
                showEquipmentInfo(null);
            }
            checkSaveButton();
        });
    });
});

function checkSaveButton() {
    $("#btSave").prop("disabled", ((_t_ == true && _e_ == true) ? "" : "disabled"));
}
