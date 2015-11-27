var MaxTestingCount = 6;
var TestingList = new Array();

$(document).ready(function () {

    $("#btQuery").click(function () {
        if (TestingList.length >= MaxTestingCount) {
            showError("You can only testing " + MaxTestingCount + " terminals before you close any tabs.");
        }
        else {
            GetJsonData("../ajax/query.ashx",
                    { "type": "terminal", "cmd": "single", "data": $("#txtNumber").val() },
                    function (data) {
                        if (data.length > 0) {
                            NewTestTab(data[0]);
                            TestingList[TestingList.length] = data[0];
                        } else {
                            showError("No object like \"" + $("#txtNumber").val() + "\"", "no record");
                        }
                    });
        }
    });

    // 输入框中增加模糊查询
    $("#txtNumber").autocomplete({
        source: function (request, response) {
            GetJsonData("../ajax/query.ashx",
                { "type": "terminal", "cmd": "normal", "data": $("#txtNumber").val() },
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
});

var table_title = "<li><span class=\"ui-icon ui-icon-close\" title=\"close\" style=\"display: none;\">" +
    "</span><a href=\"#%id%\" title=\"check \"%label%\"\">%label%</a></li>";
var table_content = "<div class=\"tab-content\" id=\"%id%\" style=\"display: none;\">" +
            "<iframe frameborder=\"0\" scrolling=\"auto\" width=\"100%\" height=\"330px\" " +
            "src=\"./terminal_testing_content.aspx?key=%label%\"></iframe></div>";

/*添加一个新的 tab */
function NewTestTab(_tar) {
    var id = "tab_" + _tar.id;
    var _t = $("a[href='#" + id + "']");
    // 如果找不到目标则新添加，否则点击一下目标让其显示出来
    if (_t.length < 1) {
        var label = $("#txtNumber").val();
        var tabTitle = table_title.replace(/%id%/g, id).replace(/%label%/g, label);
        $("#ul_nav").append(tabTitle);

        var contents = table_content.replace(/%id%/g, id).replace(/%label%/g, label);
        $("#divContent").append(contents);
        // 点击新增加的 tab
        $("a[href='#" + id + "']").click();
    } else
        _t.click();
}