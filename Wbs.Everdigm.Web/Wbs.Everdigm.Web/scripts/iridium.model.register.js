var printProgressTimer = null;
var maxWaitPrintTimes = 150;// 150 sec
var curWaitPrintTimes = 0;
var curIMEI = "";
var printType = "iridium";
var isPrinting = false;

$(document).ready(function () {
    $("#query").click(function () {
        $("#btQuery").click();
    });

    $(".date-picker-picker").datepicker({
        format: "yyyy/mm/dd",
        weekStart: 0,
        todayBtn: true,
        todayHighlight: true,
        autoclose: true
    }).on("show.bs.modal", function (event) { event.stopPropagation(); })
    .on("hide.bs.modal", function (event) { event.stopPropagation(); });

    $("#aDownload").click(function () {
        showWarningDialog("Coming soon... just a sec");
    });

    $("#openModal").click(function () {
        var n = $("#txtQueryNumber").val();
        if (!isStringNull(n)) {
            if (n.length < 10) {
                showWarningDialog("Please input a real IMEI number.");
            } else {
                $("#btSave").click();
            }
        } else {
            showWarningDialog("Number cannot be blank.");
        }
    });
    
    $("#modalManufacturing").on("show.bs.modal", function (event) {
        var a = $(event.relatedTarget);
        curIMEI = a.data("whatever");
        $("#spanImei").text(curIMEI);

        var manuDate = a.parent().next().next().next().next().text();
        if (manuDate.charAt(0) == '-') { manuDate = new Date().pattern("yyyy-MM-dd"); }
        $(".date-picker-picker").datepicker("update", manuDate);
        $(".progress-bar").css("width", "0%");
        curWaitPrintTimes = 0;
    });

    $("#print").click(function () {
        if (!isPrinting) {
            disableButtons(true);
            $(".progress-bar").css("width", "0%");
            curWaitPrintTimes = 0;
            $("#spanPrintStatus").text();
            $("#spanPrintStatusText").text(printStatus(1));
            requestPrint();
        } else {
            $("#modalPrinting").modal("show");
        }
    });

    $("#save").click(function () {
        disableButtons(true);
        savePrintInformation();
    });
    $("#imgLoading").hide();
});

function disableButtons(disable) {
    if (disable) {
        $("#imgLoading").show();
        $("#print").attr("disabled", true);
        $("#save").attr("disabled", true);
    } else {
        $("#imgLoading").hide();
        $("#print").removeAttr("disabled");
        $("#save").removeAttr("disabled");
    }
}

function showWarningDialog(text) {
    $("#spanWarning").text(text);
    $("#modalWarning").modal("show");
}

function showPrintProgress() {
    if (null == printProgressTimer) {
        printProgressTimer = $.timer(1000, function () {
            curWaitPrintTimes++;
            $("#spanPrintStatus").text(curWaitPrintTimes + "s");
            var percentage = parseInt(curWaitPrintTimes / maxWaitPrintTimes * 100);
            $(".progress-bar").css("width", percentage + "%");
            if (curWaitPrintTimes >= maxWaitPrintTimes) {
                $("#spanPrintStatusText").text("Print fail: timeout.");
                stopTimer();
            } else {
                if (curWaitPrintTimes % 5 == 4) {
                    // 获取打印状态
                    requestPrintStatus();
                }
            }
        });
    }
}

function stopTimer() {
    isPrinting = false;
    disableButtons(false);
    printProgressTimer.stop();
    printProgressTimer = null;
}

function savePrintInformation() {
    var obj = {};
    obj.CardNo = curIMEI;
    obj.FWVersion = $("#fwVersion").val();
    obj.PcbNumber = $("#pcbNumber").val();
    obj.ManufactureDate = $("#manufDate").val();
    obj.RatedVoltage = $("#rateVoltate").val();
    obj.Manufacturer = $("#manuf").val();
    GetJsonData("ajax/print.ashx", { "type": printType, "cmd": "save", "data": $.toJSON(obj), "timestamp": new Date().getTime() }, function (data) {
        if (data.State != 0) {
            showWarningDialog(data.Data);
        } else {
            // 可以打印了
            disableButtons(false);
        }
    });
}

function requestPrint() {
    var obj = {};
    obj.CardNo = curIMEI;
    GetJsonData("ajax/print.ashx", { "type": printType, "cmd": "request", "data": $.toJSON(obj), "timestamp": new Date().getTime() }, function (data) {
        if (data.State != 0) {
            showWarningDialog(data.Data);
        } else {
            isPrinting = true;
            $("#modalPrinting").modal("show");
            showPrintProgress();
        }
    });
}

function printStatus(state) {
    switch (state) {
        case 1: return "Waiting for printer...";
        case 2: return "Printing...";
        case 3: return "Print success!";
        default: return "???";
    }
}

function requestPrintStatus() {
    var obj = {};
    obj.CardNo = curIMEI;
    GetJsonData("ajax/print.ashx", { "type": printType, "cmd": "status", "data": $.toJSON(obj), "timestamp": new Date().getTime() }, function (data) {
        if (data.State >= 0) {
            $("#spanPrintStatusText").text(printStatus(data.State));
            if (data.State >= 3) {
                stopTimer();
            }
        } else { showWarningDialog(data.Data); }
    });
}