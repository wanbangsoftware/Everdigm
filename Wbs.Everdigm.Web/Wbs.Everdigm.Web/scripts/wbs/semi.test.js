$(document).ready(function () {

    //$("#terminal_list").BootSideMenu({ side: "right" });
    $("#terminal_list").BootSideMenu({ side: "left" });

    $(".toggler").tooltip({
        "placement": "right",
        "title": "点击打开侧滑菜单"
    });

    $("#txtQueryNumber").keyup(function (e) {
        $(this).val($(this).val().replace(/[^\d]/g, ""));
        var text = $(this).val();
        if (text.length >= 4) {
            showLoading(true);
            getTerminalList(text);
        }
    });
    showLoading(false);

    $("#divTerminals").on("click", "a", function (e) {
        e.preventDefault();
        var id = $(this).prop("id");
        appendTestTerminal(id);
    });

    $(".nav-tabs").on("click", "a", function (e) {
        e.preventDefault();
        //if (!$(this).hasClass('add-contact')) {
        $(this).tab('show');
        //}
    }).on("click", "span", function () {
        var anchor = $(this).siblings('a');
        $(anchor.attr('href')).remove();
        $(this).parent().remove();
        $(".nav-tabs li").children('a').first().click();
    });
});

function showLoading(shown) {
    if (shown)
        $("#loading").show();
    else
        $("#loading").hide();
}

function getTerminalList(text) {
    GetJsonData("../ajax/query.ashx", { "type": "terminal", "cmd": "normal", "data": text }, function (data) {
        parseTerminals(data);
    });
}

var htmlItem = "<a href=\"#\" class=\"list-group-item\" id=\"%number%\">" +
               "<img alt=\"#\" src=\"../images/wifi_modem.png\" />%number%" +
               "<img alt=\"#\" style=\"margin-left: 10px;\" src=\"../images/sim_card.png\" />%card%</a>";
function parseTerminals(data) {
    showLoading(false);
    var html = "";
    if (data.length > 0) {
        $(data).each(function (i, item) {
            html += htmlItem.replace(/%number%/g, item.Number).replace(/%card%/, item.Sim);
        });
    }
    $("#divTerminals").html(html);
}
var tabItem = "<li role=\"presentation\"><a href=\"#content_%id%\" aria-controls=\"content_%id%\" id=\"tab_%id%\" role=\"tab\" data-toggle=\"tab\">%id%</a><span>&times;</span></li>";
var tabContentItem = "<div role=\"tabpanel\" class=\"tab-pane fade\" id=\"content_%id%\">" +
                     "   <iframe frameborder=\"0\" scrolling=\"auto\" style=\"padding: 10px;\" height=\"500px\" width=\"100%\" " +
                     "src=\"../main/terminal_testing_content.aspx?key=%id%\&type=semi\"></iframe>" +
                     "</div>";
function appendTestTerminal(terminal) {
    var len = $(".nav-tabs a").length;
    if (len >= 7) {
        showWarning("您最多只能同时测试6台终端");
        return;
    }
    var exist = false;
    $(".nav-tabs a").each(function (index, item) {
        var id = $(item).prop("id").replace(/tab_/g, "");
        if (id == terminal) {
            exist = true;
        }
    });
    if (!exist) {
        $(".nav-tabs").append(tabItem.replace(/%id%/g, terminal));
        $('.tab-content').append(tabContentItem.replace(/%id%/g, terminal));
    } else {
        showWarning("终端“" + terminal + "”已经在测试中了");
    }
}

function showWarning(text) {
    $(".warning-content").text(text);
    $("#modalWarningTabs").modal("show");
}