$(document).ready(function () {
    // 导出到excel
    $("#toExcel").click(function () {
        // 导出全部设备到excel供下载
        exportToExcel({ "type": "equipment", "cmd": "equipments2excel" });
    });
});