

$(document).ready(function () {
    $(".btn-success").click(function () { showWorktimes(); });

    // 初始化查询最近5天内的启动情况
    showWorktimes();
});

var worktimeItem = "<tr><td class=\"panel-body-td\"><img alt=\"%alt%\" src=\"equipment_today_works.aspx?key=%key%&date=%date%&width=600\" /></td></tr>";
var lastQueryDate = "";
function showWorktimes() {
    var date = new Date($("#worktimeDate").val());
    var html = date.pattern(_datepatternFMT);
    // 最近查询的日期跟当前查询的日期不一样的话，才进行查询，否则不动
    if (lastQueryDate != html) {
        lastQueryDate = html;
        html = "";

        for (var i = 1; i <= 5; i++) {
            var d = date.pattern(_datepatternFMT);
            html += worktimeItem.replace(/%alt%/, date.pattern(_datepatternFMT) + " work time").
                replace(/%key%/, $("#hidKey").val()).replace(/%date%/, d);
            date = date.dateAfter(-1, 1);
        }
        $("#tbodyWorktimes").html(html);
    }
    // 查询指定时间内的最新运转时间
    getWorktime();
}
// 获取最新的运转时间信息
function getWorktime() {
    GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "6004", "data": $("#hidKey").val(), "date": $("#worktimeDate").val() },
        function (data) {
            displayWorkTimes(data);
        });
}
// 显示运转时间
function displayWorkTimes(obj) {
    $("#reportTime").html("(" + obj.ReceiveTime.replace(/T/, " ") + ")");

    $("#lblTotalHour").html(parseInt(obj.TotalWorkTime / 60));
    $("#lblTotalMin").html(obj.TotalWorkTime % 60);

    $("#lblTravelSpeedHour1").html(parseInt(obj.TravelSpeedI / 60));
    $("#lblTravelSpeedMin1").html(obj.TravelSpeedI % 60);
    $("#imgTravelSpeed1 .bar").css({ "width": (parseInt(obj.TravelSpeedI / obj.TotalWorkTime * 100) + "%") });

    $("#lblTravelSpeedHour2").html(parseInt(obj.TravelSpeedII / 60));
    $("#lblTravelSpeedMin2").html(obj.TravelSpeedII % 60);
    $("#imgTravelSpeed2 .bar").css({ "width": (parseInt(obj.TravelSpeedII / obj.TotalWorkTime * 100) + "%") });

    $("#lblWorkingTimeHour").html(parseInt(obj.Work / 60));
    $("#lblWorkingTimeMin").html(obj.Work % 60);
    $("#imgWorkingTime .bar").css({ "width": (parseInt(obj.Work / obj.TotalWorkTime * 100) + "%") });

    $("#lblTravelTimeHour").html(parseInt(obj.Travel / 60));
    $("#lblTravelTimeMin").html(obj.Travel % 60);
    $("#imgTravelTime .bar").css({ "width": (parseInt(obj.Travel / obj.TotalWorkTime * 100) + "%") });

    $("#lblAutoIdlemodeHour").html(parseInt(obj.AutoIdleMode / 60));
    $("#lblAutoIdlemodeMin").html(obj.AutoIdleMode % 60);
    $("#imgAutoIdlemode .bar").css({ "width": (parseInt(obj.AutoIdleMode / obj.TotalWorkTime * 100) + "%") });

    $("#lblPowerModeHour1").html(parseInt(obj.PowerMode / 60));
    $("#lblPowerModeMin1").html(obj.PowerMode % 60);
    $("#imgPowerMode1 .bar").css({ "width": (parseInt(obj.PowerMode / obj.TotalWorkTime * 100) + "%") });

    $("#lblPowerModeHour2").html(parseInt(obj.StandardMode / 60));
    $("#lblPowerModeMin2").html(obj.StandardMode % 60);
    $("#imgPowerMode2 .bar").css({ "width": (parseInt(obj.StandardMode / obj.TotalWorkTime * 100) + "%") });

    $("#lblWorkModeHour1").html(parseInt(obj.DiggingMode / 60));
    $("#lblWorkModeMin1").html(obj.DiggingMode % 60);
    $("#imgWorkMode1 .bar").css({ "width": (parseInt(obj.DiggingMode / obj.TotalWorkTime * 100) + "%") });

    $("#lblWorkModeHour2").html(parseInt(obj.TrenchingMode / 60));
    $("#lblWorkModeMin2").html(obj.TrenchingMode % 60);
    $("#imgWorkMode2 .bar").css({ "width": (parseInt(obj.TrenchingMode / obj.TotalWorkTime * 100) + "%") });

    var rpm1700up = (obj.EngSpeed1700 + obj.EngSpeed1800 + obj.EngSpeed1900 + obj.EngSpeed2000);
    $("#lblEngSpeedHour1").html(parseInt(rpm1700up / 60));
    $("#lblEngSpeedMin1").html(rpm1700up % 60);
    $("#imgEngSpeed1 .bar").css({ "width": (parseInt(rpm1700up / obj.TotalWorkTime * 100) + "%") });

    var rpm1700down = (obj.EngSpeed1600 + obj.EngSpeed1200 + obj.EngSpeed1200D);
    $("#lblEngSpeedHour2").html(parseInt(rpm1700down / 60));
    $("#lblEngSpeedMin2").html(rpm1700down % 60);
    $("#imgEngSpeed2 .bar").css({ "width": (parseInt(rpm1700down / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprHydoiltempHour6").html(parseInt(obj.HydOilTemp96 / 60));
    $("#lblOprHydoiltempMin6").html(obj.HydOilTemp96 % 60);
    $("#imgOprHydoiltemp6 .bar").css({ "width": (parseInt(obj.HydOilTemp96 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprHydoiltempHour5").html(parseInt(obj.HydOilTemp86 / 60));
    $("#lblOprHydoiltempMin5").html(obj.HydOilTemp86 % 60);
    $("#imgOprHydoiltemp5 .bar").css({ "width": (parseInt(obj.HydOilTemp86 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprHydoiltempHour4").html(parseInt(obj.HydOilTemp76 / 60));
    $("#lblOprHydoiltempMin4").html(obj.HydOilTemp76 % 60);
    $("#imgOprHydoiltemp4 .bar").css({ "width": (parseInt(obj.HydOilTemp76 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprHydoiltempHour3").html(parseInt(obj.HydOilTemp51 / 60));
    $("#lblOprHydoiltempMin3").html(obj.HydOilTemp51 % 60);
    $("#imgOprHydoiltemp3 .bar").css({ "width": (parseInt(obj.HydOilTemp51 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprHydoiltempHour2").html(parseInt(obj.HydOilTemp31 / 60));
    $("#lblOprHydoiltempMin2").html(obj.HydOilTemp31 % 60);
    $("#imgOprHydoiltemp2 .bar").css({ "width": (parseInt(obj.HydOilTemp31 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprHydoiltempHour1").html(parseInt(obj.HydOilTemp30 / 60));
    $("#lblOprHydoiltempMin1").html(obj.HydOilTemp30 % 60);
    $("#imgOprHydoiltemp1 .bar").css({ "width": (parseInt(obj.HydOilTemp30 / obj.TotalWorkTime * 100) + "%") });


    $("#lblOprWaterempHour6").html(parseInt(obj.CoolantTemp106 / 60));
    $("#lblOprWaterempMin6").html(obj.CoolantTemp106 % 60);
    $("#imgOprWateremp6 .bar").css({ "width": (parseInt(obj.CoolantTemp106 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprWaterempHour5").html(parseInt(obj.CoolantTemp96 / 60));
    $("#lblOprWaterempMin5").html(obj.CoolantTemp96 % 60);
    $("#imgOprWateremp5 .bar").css({ "width": (parseInt(obj.CoolantTemp96 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprWaterempHour4").html(parseInt(obj.CoolantTemp86 / 60));
    $("#lblOprWaterempMin4").html(obj.CoolantTemp86 % 60);
    $("#imgOprWateremp4 .bar").css({ "width": (parseInt(obj.CoolantTemp86 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprWaterempHour3").html(parseInt(obj.CoolantTemp61 / 60));
    $("#lblOprWaterempMin3").html(obj.CoolantTemp61 % 60);
    $("#imgOprWateremp3 .bar").css({ "width": (parseInt(obj.CoolantTemp61 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprWaterempHour2").html(parseInt(obj.CoolantTemp41 / 60));
    $("#lblOprWaterempMin2").html(obj.CoolantTemp41 % 60);
    $("#imgOprWateremp2 .bar").css({ "width": (parseInt(obj.CoolantTemp41 / obj.TotalWorkTime * 100) + "%") });

    $("#lblOprWaterempHour1").html(parseInt(obj.CoolantTemp40 / 60));
    $("#lblOprWaterempMin1").html(obj.CoolantTemp40 % 60);
    $("#imgOprWateremp1 .bar").css({ "width": (parseInt(obj.CoolantTemp40 / obj.TotalWorkTime * 100) + "%") });
}