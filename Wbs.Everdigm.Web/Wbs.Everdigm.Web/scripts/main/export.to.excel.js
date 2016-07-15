var excelId;
var excelStatusInterval = 3000;

// 显示警告信息
function showWarning(warning, title, content, shown) {
    $("#warningLabel").text(title);
    if (warning) {
        // 显示警告信息
        $(".modal-header").removeClass("btn-primary").addClass("btn-warning");
        $("#loadingContent").hide();
        $("#warningContent").show();
        $("#warningContentText").text(content);
    } else {
        // 显示数据加载提示信息
        $(".modal-header").removeClass("btn-warning").addClass("btn-primary");
        $("#loadingContent").show();
        $("#warningContent").hide();
        $("#loadingContentText").text(content);
    }
    $("#warningLoading").modal(shown);
}

// 导出到excel
function exportToExcel(params) {
    showWarning(false, "Exporting...", "Prepare data & export into excel file, please wait...", "show");

    GetJsonData("../ajax/query.ashx", params, function (data) {
        if (data.status == 0) {
            excelId = data.data;
            // 每3秒一次轮询excel处理情况
            setTimeout("getExportExcelStatus();", excelStatusInterval);
        } else {
            showWarning(true, "Operation fail", "Fail to export data into excel.", "show");
        }
    });
}

// 等待处理结果
function getExportExcelStatus() {
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "worktime2excelquery", "data": excelId }, function (data) {
        if (data.status > 0) {
            // 下载excel文件
            document.location = data.data;
            showWarning(false, "", "", "hide");
        } else if (data.status < 0) {
            showWarning(true, "Operation fail", data.desc);
        } else {
            setTimeout("getExportExcelStatus();", excelStatusInterval);
        }
    });
}