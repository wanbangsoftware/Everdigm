var datepattern = "yyyy/MM/dd";
function updateDatePicker(start, end, objStart, objEnd) {
    var $datepicker = $(".input-daterange");
    $datepicker.find(null == objStart ? "input:eq(0)" : objStart).datepicker("update", start.pattern(datepattern));
    $datepicker.find(null == objEnd ? "input:eq(1)" : objEnd).datepicker("update", end.pattern(datepattern));
    $datepicker.datepicker("updateDates");
}

$(document).ready(function () {

    $(".input-daterange").datepicker({
        format: _datepickerFMT,
        weekStart: 0,
        autoclose: true
    });

});
