var AsHistory = new Array();
$(document).ready(function () {
    _pageSize = 10;
    QueryAs();
});

function QueryAs() {
    ShowWarningMessage("<tr><td class=\"panel-body-td\" colspan=\"5\"><img src=\"../images/loading_orange.gif\" />loading data...</td></tr>");
    var device = $("#equipment_id").text();
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "as", "data": device }, function (data) {
        if (data.hasOwnProperty("status")) {
            ShowWarningMessage("<tr><td class=\"panel-body-td\" colspan=\"5\">" + data.desc + "</td></tr>");
        } else {
            if (data.length < 1) {
                ShowWarningMessage("<tr><td class=\"panel-body-td\" colspan=\"5\">No data exists.</td></tr>");
            } else {
                AsHistory = data;
                ShowAsHistoryPagging();
            }
        }
    }, function (err) {
        ShowWarningMessage("<tr><td class=\"panel-body-td\" colspan=\"5\">fetching data failed, please try again.</td></tr>");
    });
}
var isAsFirstInitializing = true;
function ShowAsHistoryPagging() {
    var len = AsHistory.length;
    var pages = parseInt(len / _pageSize) + (len % _pageSize > 0 ? 1 : 0);
    if (pages < 1) pages = 1;
    // 分页
    if (isAsFirstInitializing) {
        isAsFirstInitializing = false;
    } else {
        // 不是刷新页面首次加载时，需要销毁分页UI待后面重建
        $(".pagination-sm:eq(0)").twbsPagination("destroy");
    }

    $(".pagination-sm:eq(0)").twbsPagination({
        totalPages: pages,
        visiblePages: 5,
        first: "First",
        prev: "Prev",
        next: "Next",
        last: "Last",
        onPageClick: function (event, page) {
            var l = jLinq.from(AsHistory).skipTake((page - 1) * _pageSize, _pageSize);
            ShowAsHistory(l);
        }
    });
    // 初始化第一页
    var l = jLinq.from(AsHistory).skipTake(0, _pageSize);
    ShowAsHistory(l);
}
var item = "<tr><td class=\"panel-body-td\" style=\"text-align: center;\">#cnt#</td><td class=\"panel-body-td\">#time#</td><td class=\"panel-body-td\">#name#</td><td class=\"panel-body-td\">#action#</td><td class=\"panel-body-td\">#description#</td></tr>";
function ShowAsHistory(list) {
    var html = "";
    var cnt = 0;
    for (var i in list) {
        var obj = list[i];
        cnt++;
        html += item.replace("#cnt#", cnt).replace("#time#", obj.Time).replace("#name#", obj.User).replace("#action#", obj.Action).replace("#description#", obj.Description);
    }
    ShowWarningMessage(html);
}
function ShowWarningMessage(html) {
    $(".table:eq(0) tbody").html(html);
}