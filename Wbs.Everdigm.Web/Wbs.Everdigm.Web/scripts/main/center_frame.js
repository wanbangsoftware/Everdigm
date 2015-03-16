$(document).ready(function () {
    $("#control_left_frame").click(function () {
        var frame = parent.document.getElementById("body_frame");
        var curCols = frame.cols;
        frame.cols = curCols == col ? "0,*" : col;
        $(this).prop("title", (curCols == col ? "show" : "hide") + " left menu");
        $(this).prop("class", ("index_close_left" + (curCols == col ? "_1" : "")));
    });
});