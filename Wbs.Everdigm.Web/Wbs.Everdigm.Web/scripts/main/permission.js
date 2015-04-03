$(document).ready(function () {
    $("#btReturn").click(function () {
        document.location = "./permission_list.aspx";
    });

    $("#btAdd").click(function () { document.location = "./permission_add.aspx";});

    $("#imgImage").click(function () {
        showDialogWindows("./images.aspx", function (ret) {
            $("#imgImage").prop("src", ret.ids);
            $("#hidImage").val(ret.ids);
        });
    });
    // 上级菜单
    $("#txtParent").click(function () {
        showDialogWindows("./permission_pop.aspx", function (ret) {
            $("#txtParent").val(ret.names);
            $("#hidParent").val(ret.ids);
            $("#btQuery").click();
        });
    });
});
// 客户端的树叶点击事件
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