
$(document).ready(function () {
    $("#btReturn").click(function () {
        document.location = "./role_list.aspx";
    });

    $("#btAdd").click(function () { document.location = "./role_add.aspx"; });

    // 为了不至于和页面上其他元素混乱，所以把TreeView控件放在一个 id 为 _tvList 的 div 中，然后
    // 再查找checkbox
    $("#divRoleAuthority table tr td  input[type=checkbox]").click(function () {
        var cked = $(this).prop("checked");
        var _id = $(this).prop("id").replace(/CheckBox/, "Nodes");
        if (!cked)
            $("#divRoleAuthority div[id=" + _id + "] table tr td  input[type='checkbox']").prop("checked", false);
        else {
            $("#divRoleAuthority div[id=" + _id + "] table tr td  input[type='checkbox']").prop("checked", true);
        }
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