
$(document).ready(function () {
    $("#btReturn").click(function () { document.location = "./account_list.aspx"; });

    $("input[type='checkbox']").change(function () {
        var cked = $(this).prop("checked");
        var objs = $("[id^='txt']");
        for (var i = 0; i < objs.length; i++) {
            objs[i].type = cked ? "text" : "password";
        }
    });

    // choose department
    $("#txtDepartment").click(function () {
        showDialogWindows("./department_pop.aspx?key=0", function (data) {
            $("#txtDepartment").val(data.names);
            $("#hidDepartment").val(data.ids);
        });
    });

    // choose role
    $("#txtRole").click(function () {
        showDialogWindows("./role_pop.aspx", function (data) {
            $("#txtRole").val(data.names);
            $("#hidRole").val(data.ids);
        });
    });
});

function checkChangePassword()
{
    var ret = checkSubmitedForm();
    if (true != ret)
        return ret;
    
    if ($("#txtNewPassword").val() != $("#txtNewPassword1").val()) {
        ret = false;
        showWarringAfter("txtNewPassword1", "Please re-input your new password.");
    }
    else {
        if ($("input[type='checkbox']").prop("checked"))
            $("input[type='checkbox']").click();

        ret = true;
    }
    return ret;
}