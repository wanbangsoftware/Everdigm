var datepattern = "yyyy/MM/dd";
$(document).ready(function () {

    if (isStringNull($("#start").val())) {
        var date = new Date();
        var date1 = date.dateAfter(-5, 1);
        $("#start").val(date1.pattern(datepattern));
        $("#end").val(date.pattern(datepattern));
    }
    $(".input-daterange").datepicker({
        format: "yyyy/mm/dd",
        weekStart: 0,
        autoclose: true
    });

    updateDatePicker(new Date($("#start").val()), new Date($("#end").val()));

});

function updateDatePicker(start, end, objStart, objEnd) {
    var $datepicker = $(".input-daterange");
    $datepicker.find(null == objStart ? "#start" : objStart).datepicker("update", start.pattern(datepattern));
    $datepicker.find(null == objEnd ? "#end" : objEnd).datepicker("update", end.pattern(datepattern));
    $datepicker.datepicker("updateDates");
}
