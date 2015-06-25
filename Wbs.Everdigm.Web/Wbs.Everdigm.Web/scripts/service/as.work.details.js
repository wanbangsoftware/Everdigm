var equipmentList = new Array();
var terminalList = new Array();
var excelId = -1;

$(document).ready(function () {

    $("#save").click(function () {
        performSave();
    });
    $("#tbodyBody").children("tr").css("cursor", "pointer").click(function () {
        showModalExcel();
    });
    $("#equipmentInfo").hide();

    $("#equipment").typeahead({
        items: 5,
        minLength: 4,
        source: function (query, process) {
            GetJsonData("../ajax/query.ashx", { "type": "equipment", "cmd": "number", "data": query },
                function (data) {
                    if (data.length > 0) {
                        equipmentList = data;
                        var ret = $.map(data, function (item) { return item.Number; });
                        process(ret);
                    }
                });
        },
        updater: function (item) {
            var obj = jLinq.from(equipmentList).equals("Number", item).select();
            $("#hiddenEquipment").val(obj[0].Id);
            $("#equipmentInfo").children("td:eq(1)").text(obj[0].Terminal);
            $("#equipmentInfo").children("td:eq(3)").text(obj[0].Sim);
            if (!isStringNull(obj[0].Terminal)) {
                $("#option2").click();
                $("#equipmentInfo").show();
            } else { $("#equipmentInfo").hide(); }
            return item;
        }
    });

    $("#terminal").typeahead({
        items: 5,
        minLength: 4,
        source: function (query, process) {
            GetJsonData("../ajax/query.ashx", { "type": "terminal", "cmd": "book", "data": query },
                function (data) {
                    if (data.length > 0) {
                        terminalList = data;
                        var ret = $.map(data, function (item) {
                            return item.Number + "," + item.Sim;
                        });
                        process(ret);
                    }
                });
        },
        updater: function (item) {
            var obj = jLinq.from(terminalList).equals("Number", item.substr(0, 10)).select();
            $("#hiddenTerminal").val(obj[0].Id);
            return item;
        }
    });
});

function showWarning(title) {
    $("#warning").text(title);
}

function performSave() {
    showWarning("");
    $("#equipment").removeClass("caution");
    // 确定选择的工作类别
    var type = $("input:radio[name =\"options\"]:checked").val();
    $("#hiddenType").val(type);

    type = $("#hiddenEquipment").val();
    if (isStringNull(type)) {
        $("#equipment").addClass("caution");
        showWarning("Cannot dispatch work without equipment.");
        return;
    }
    $("#btSave").click();
}

function showDialogWaring(text) {
    $(".col-sm-12:eq(1)").html(text);
    $("#modalShowFile").modal(isStringNull(text) ? "hide" : "show");
}

function showModalExcel() {
    // 从服务器上生成pdf之后返回文件url
    GetJsonData("../ajax/work.ashx", { "type": "work", "cmd": "detail", "data": $("#hidKey").val() }, function (data) {
        if (data.status < 0) {
            // 文件转换失败，显示提示信息
            showDialogWaring(data.desc);
        } else {
            showDialogWaring("<span class=\"glyphicon glyphicon-repeat glyphicon-refresh-animate text-primary\"></span> Preparing file, wait for a moment please...");
            excelId = parseInt(data.data);
            setTimeout("getModalExcel();", 3000);
        }
    });
}

// 获取服务器处理excel文档的结果
function getModalExcel() {
    // 获取服务器上处理excel的结果
    GetJsonData("../ajax/work.ashx", { "type": "work", "cmd": "excel", "data": excelId }, function (data) {
        if (data.status < 0) {
            // 文件转换失败，显示提示信息
            showDialogWaring(data.desc);
        } else if (data.status >= 1) {
            showDialogWaring("");
            // 定时5秒后获取服务器处理结果
            var pdf_link = data.data;
            var iframe = "<div class=\"iframe-container\"><iframe border=\"0\" src=\"" + pdf_link + "\"></iframe></div>";
            $.createModal({
                title: "Work dispatch preview",
                message: iframe,
                closeButton: true,
                scrollable: true
            });
        } else {
            // 如果服务器没处理完则继续3秒后读取状态
            setTimeout("getModalExcel();", 3000);
        }
    });
}

/*
* This is the plugin
*/
(function (a) {
    a.createModal = function (b) {
        defaults = {
            title: "",
            message: "Your Message Goes Here!",
            closeButton: true,
            scrollable: false
        };
        var b = a.extend({}, defaults, b);
        var c = (b.scrollable === true) ? 'style="max-height: 420px; overflow-y: hidden;"' : "";
        html = '<div class="modal" id="myModal">';
        html += '<div class="modal-dialog modal-lg">';
        html += '<div class="modal-content">';
        html += '<div class="modal-header custom-modal-header bg-primary">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>';
        if (b.title.length > 0) {
            html += '<h4 class="modal-title">' + b.title + "</h4>";
        }
        html += "</div>";
        html += '<div class="modal-body" ' + c + ">";
        html += b.message;
        html += "</div>";
        html += '<div class="modal-footer">';
        if (b.closeButton === true) {
            html += '<button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>';
        }
        html += "</div>";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        a("body").prepend(html);
        a("#myModal").modal().on("hidden.bs.modal", function () {
            a(this).remove();
        })
    }
})(jQuery);
