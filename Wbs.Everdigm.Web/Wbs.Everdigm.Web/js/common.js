var _datepatternFMT = "yyyy/MM/dd", _datetimepatternFMT = "yyyy/MM/dd HH:mm:ss", _datepickerFMT = "yyyy/mm/dd";
var _pageSize = 5;

// 字符串格式化
if (!String.prototype.format) {
    String.prototype.format = function () {
        var str = this.toString();
        if (!arguments.length)
            return str;
        var args = typeof arguments[0],
            args = (("string" == args || "number" == args) ? arguments : arguments[0]);
        for (arg in args)
            str = str.replace(RegExp("\\{" + arg + "\\}", "gi"), args[arg]);
        return str;
    }
}

// 判断字符串是否为空
function isStringNull(string) {
    return typeof (string) === "undefined" || string == null || string == "";
}

/*判断数字的正则表达式*/
var _regNumber = /^[1-9]\d*$|^0\d*[1-9]$/;

/*判断价格的正则表达式，带 1-6 位小数点*/
var _regPrice = /^[1-9]\d*$|^[0-9]*\.[0-9]{1,6}$/;

/*判断字符串是否全为数字*/
function isStringNumber(obj) {
    return _regNumber.test(obj);
}

/*判断字符串是否为价格方式*/
function isStringPrice(obj) {
    return _regPrice.test(obj);
}

// 显示警告信息的对象的原始样式
var ___warring_old_class = "";
// 显示警告信息的对象的 ID
var ___warringed_id = "";
/*在指定目标之后显示警告信息*/
function showWarringAfter(endOfObj, txtShow) {
    removeWarring();
    var html = "<span class=\"input-notification error png_bg\" id=\"__warring\">" + txtShow + "</span>";
    ___warring_old_class = $("[id$='" + endOfObj + "']").prop("class");
    ___warringed_id = endOfObj;
    ___warring_old_class += " error-input";
    $("[id$='" + endOfObj + "']").prop("class", ___warring_old_class);
    $("[id$='" + endOfObj + "']").after(html);
    $("#" + endOfObj).focus();
}

/*取消已经显示的警告信息*/
function removeWarring() {
    $("#__warring").remove();
    if ("" != ___warringed_id && ___warring_old_class != "")
        $("[id$='" + ___warringed_id + "']").prop("class", ___warring_old_class.replace(" error-input", ""));
    ___warring_old_class = "";
    ___warringed_id = "";
}

// 检测提交的窗体form是否正常
function checkSubmitedForm() {
    var ret = true;
    // 检测提交的form里，标识必填项是否都有信息
    $(".important-input").each(function () {
        var val = $(this).val();
        var id = $(this).prop("id");
        if (isStringNull(val)) {
            ret = false;
            showWarringAfter(id, "value cannot be blank");
            // 跳出 each 循环
            return false;
        }
    });
    return ret;
}
/*识别不同的浏览器*/
function getTargetElement(evt) {
    var elem
    if (evt.target) {
        elem = (evt.target.nodeType == 3) ? evt.target.parentNode : evt.target
    }
    else {
        elem = evt.srcElement
    }
    return elem
}

/*获取点击事件的目标 Element*/
function getEventTargetElement(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    if ("" == evt)
        return null;
    else
        return getTargetElement(evt);
}

/*显示指定 url 的模态窗口*/
function showDialogWindows(url, _onWindowClose, winWidth, winHeight, useHost) {
    var width = (typeof winWidth != 'undefined') ? winWidth : "400";
    var height = (typeof winHeight != 'undefined') ? winHeight : "500";

    var obj;
    if ((typeof useHost == 'undefined') || !useHost) {
        obj = new Object;
        obj.window = window;
    } else {
        obj = window;
    }
    var timestamp = (new Date()).getTime();
    if (url.indexOf("?") >= 0)
        url += "&";
    else
        url += "?";
    url += "timestamp=" + timestamp;
    var str = "";
    if (canShowModalDialog()) {
        // IE
        str = window.showModalDialog(url, obj,
            "dialogWidth:" + width + "px;dialogHeight:" + height + "px;status:no;center:yes;help:no;");
        var ret = windowDialogReturn(str);
        _onWindowClose(ret);
    } else {
        // not IE
        var _screenHeight = $(window).height(), _screenWidth = $(window).width();
        var _top = (_screenHeight - parseInt(height)) / 2;
        var _left = (_screenWidth - parseInt(width)) / 2;
        var pop = window.open(url, obj, "height=" + height + ",width=" + width +
            ",top=" + _top + ",left=" + _left + ",toolbar=no,menubar=no,center=yes,scrollbars=no,resizable=no,location=no,status=no");
        pop.onbeforeunload = function () {
            str = window.returnValue;
            var ret = windowDialogReturn(str);
            _onWindowClose(ret);
        };
    }
}
function canShowModalDialog() {
    return window.showModalDialog;
}
function windowDialogReturn(value) {
    var ret = { names: "", ids: "" };
    if (null == value || value.indexOf("表") >= 0) value = "";
    if (!isStringNull(value)) {
        var index = value.indexOf(",");
        ret.names = value.substr(index + 1);
        ret.ids = value.substr(0, index);
    }
    return ret;
}
// 弹出窗口关闭事件
function windowPopupCloseEvent(value) {
    if (canShowModalDialog()) {
        // IE
        window.returnValue = value;
    } else {
        // not IE
        if (parent.window.opener) {
            parent.window.opener.returnValue = value;
        }
    }
}
/*获取选中的 checkbox 的 id 列表*/
function selectedCheckBox(pre) {
    var ret = "";
    $("input[type='checkbox'][id^=\"" + pre + "\"]").each(function () {
        if ($(this).prop("checked")){
            var id=$(this).prop("id");
            ret += "" == ret ? id : ("," + id);
        }
    });
    return ret;
}
/*按照指定的选择框状态全选或取消指定的选择框列表*/
function selectCheckBox(mster, selectIdsPre) {
    if ($(mster).prop("checked"))
        $("input[id^='" + selectIdsPre + "']").prop("checked", true);
    else
        $("input[id^='" + selectIdsPre + "']").prop("checked", false);
}

// 将SQL数据库中的DateTime字符串的JSON转换成Date
function convertDateTimeToJavascriptDate(dateTimeString) {
    var dotIndex = dateTimeString.indexOf(".");
    if (dotIndex > 0)
        dateTimeString = dateTimeString.substr(0, dotIndex);
    dateTimeString = dateTimeString.replace(/T/, " ").replace(/-/g, "/");
    return new Date(dateTimeString);
}

/*
    通过jQuery的ajax方法post数据到服务器
*/
function GetJsonData(url, data, funSucceed, funFailed) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: url,
        //提交的数据{ "cmd": cmd, "jsondata": jsondata } 传的数据必须为json对象，而不是字符串
        data: data,
        success: function (data, textStatus) {
            funSucceed(data);
        },
        error: function (msg) {
            if (typeof (failed) != "undefined") {
                funFailed(msg);
            }
        }
    });
}

/*判断错误提示窗体容器是否存在，不存在时则动态创建一个*/
function createErrorDiv() {
    if ($("#dialog-modal").length < 1) {
        var errDiv = "<div id=\"dialog-modal\" title=\"提示消息：\">" +
            "<p id=\"p_content\"></p>" +
            "</div>";
        $("#main-content").append(errDiv);
    }
}

/*显示错误提示窗体*/
function showError(msg, msgTitle, fn) {
    if (fn == null) {
        if (msgTitle != null) {
            if (typeof msgTitle === "string") { }
            else { fn = msgTitle; msgTitle = null; }
        }
    }
    if (navigator.userAgent.indexOf("MSIE 6.0") != -1) {
        alert(msg);
        if (fn != null)
            fn();
    }
    else {
        createErrorDiv();
        $("#dialog-modal").html("<p>" + msg + "</p>").dialog({
            title: msgTitle == null ? "Error: " : msgTitle,
            modal: true,
            width: 550,
            dialogClass: "no-close",
            show: true,//{ effect: "slideDown", duration: 100 },
            hide: true,//{ effect: "slideUp", duration: 100 },
            buttons: [{
                text: "OK",
                click: function () {
                    $(this).dialog("close");
                    if (fn != null)
                        fn();
                }
            }]
        });
    }
}
/*
    显示提示信息
*/
function showDialog(title, text, clicked) {
    BootstrapDialog.show({
        title: title,
        message: text,
        buttons: [{
            label: "OK",
            cssClass: "btn-primary",
            action: function (dialogItself) {
                dialogItself.close();
            }
        }],
        onhidden: function (dialogItself) {
            if ("undefined" != typeof (clicked)) {
                clicked();
            }
        }
    });
}
/*
    显示提示信息并显示确认按钮
*/
function confirmDialog(title, text, confirmed, canceled) {
    BootstrapDialog.show({
        title: title,
        message: text,
        buttons: [{
            icon: "glyphicon glyphicon-ok-circle",
            label: "OK",
            cssClass: "btn-warning",
            action: function (dialogItself) {
                // 点击了确定按钮之后发起远程处理方法并关闭本窗口
                dialogItself.close();
                if ("undefined" != typeof (confirmed)) {
                    confirmed();
                }
            }
        }, {
            label: "Cancel", icon: "glyphicon glyphicon-ban-circle",
            action: function (dialogItself) {
                dialogItself.close();
                if ("undefined" != typeof (canceled)) {
                    canceled();
                }
            }
        }]
    });
}
/*获取指定控件本身的 HTML 字符串*/
(function ($) {
    $.fn.outerHTML = function () {
        return $(this).clone().wrap('<div></div>').parent().html();
    }
})(jQuery);
