
/*一些查询页面上常用的方法*/
$(document).ready(function () {

    $("#tbodyBody tr").hover(function () {
        $(this).css("background", "#f5f5f5");
    }, function () {
        $(this).css("background", "");
    });

    // tbody a click
    $("#tbodyBody a").on("click", function (e) {
        var href = $(this).attr("href");
        var sharpIndex = href.indexOf("#");
        if (sharpIndex >= 0) {
            href = href.substr(sharpIndex + 1);
            var type = href.substr(0, 1);
            var canQuery = true;
            switch (type)
            {
                case "d":
                    // department query
                    $("#hidDepartment").val(href.replace(/d/, ""));
                    $("#txtDepartment").val($(this).html());
                    break;
                case "r":
                    // role query
                    $("#txtRole").val($(this).html());
                    $("#hidRole").val(href.replace(/r/, ""));
                    break;
                case "p":
                    // permission query
                    $("#txtParent").val($(this).html());
                    $("#hidParent").val(href.replace(/p/, ""));
                    break;
                default:
                    canQuery = false;
                    break;
            }
            if (canQuery) {
                $("#btQuery").click();
            }
        }
    });
});