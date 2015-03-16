$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./department_list.aspx"; });

    $("#txtDepartment").click(function () {
        showDialogWindows("./department_pop.aspx?key=0", function (data) {
            $("#txtDepartment").val(data.names);
            $("#hidDepartment").val(data.ids);
        });
    });

    // department pop window *********************************Start
    $("#btClose").click(function () { close(); });
    $("#btConfirm").click(function () {
        var uIds = "", uNames = "";
        // get the checked user's name and id
        $("input[type=\"checkbox\"][id!=\"all\"]").each(function () {
            if ($(this).prop("checked")) {
                var id = $(this).next().attr("href").replace("#", "");
                var name = $(this).next().html();
                uIds += "" == uIds ? id : (";" + id);
                uNames += "" == uNames ? name : ("; " + name);
            }
        });
        windowPopupCloseEvent(uIds + "," + uNames);
        $("#btClose").click();
    });
    $("#all").change(function () {
        if ($(this).prop("checked"))
            $("input[type=\"checkbox\"][id!=\"all\"]").prop("checked", true);
        else
            $("input[type=\"checkbox\"][id!=\"all\"]").prop("checked", false);
    });
    $("#popDepartment a").click(function (e) {
        var pre = $(this).prev();
        if (null != pre) {
            var html = pre.outerHTML();
            if (null != html && html.indexOf("checkbox") >= 0) {
                if (pre.prop("checked"))
                    pre.prop("checked", false);
                else
                    pre.prop("checked", true);

                e.preventDefault();
            }
        }
    });
    // department pop window *********************************End
});

function OnClientTreeNodeChecked(evt) {
    var obj = getEventTargetElement(evt);
    if (null == obj)
        return;
    if (obj.tagName) {
        if (obj.tagName.toUpperCase() == "A") {
            var msg = obj.hash.replace("#", "") + "," + obj.innerHTML;
            windowPopupCloseEvent(msg);
            close();
            return false;
        }
    }
}