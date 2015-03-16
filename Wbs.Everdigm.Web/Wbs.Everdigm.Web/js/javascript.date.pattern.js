/**
* 对Date的扩展，将 Date 转化为指定格式的String
* 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符
* 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
* eg:
* (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423
* (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04
* (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04
* (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04
* (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18
*/
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时
        "H+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
// 计算当前日期在本年度的周数  
Date.prototype.getWeekOfYear = function (weekStart) { // weekStart：每周开始于周几：周日：0，周一：1，周二：2 ...，默认为周日  
    weekStart = (weekStart || 0) - 0;
    if (isNaN(weekStart) || weekStart > 6)
        weekStart = 0;

    var year = this.getFullYear();
    var firstDay = new Date(year, 0, 1);
    var firstWeekDays = 7 - firstDay.getDay() + weekStart;
    var dayOfYear = (((new Date(year, this.getMonth(), this.getDate())) - firstDay) / (24 * 3600 * 1000)) + 1;
    return Math.ceil((dayOfYear - firstWeekDays) / 7) + 1;
}
// 计算当前日期在本月份的周数  
Date.prototype.getWeekOfMonth = function (weekStart) {
    weekStart = (weekStart || 0) - 0;
    if (isNaN(weekStart) || weekStart > 6)
        weekStart = 0;

    var dayOfWeek = this.getDay();
    var day = this.getDate();
    return Math.ceil((day - dayOfWeek - 1) / 7) + ((dayOfWeek >= weekStart) ? 1 : 0);
}
// 获取星期几
Date.prototype.getDayOfWeek = function () {
    var day_of_week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日"];
    return day_of_week[this.getDay()];
}
/* 功能 : 返回与某日期相距N天(N个24小时)的日期
* 参数 : num number类型 可以为正负整数或者浮点数,默认为1;
* type 0(秒) or 1(天),默认为秒
* 返回 : 新的PowerDate类型
*/
Date.prototype.dateAfter = function (num, type) {
    num = (num == null ? 1 : num);
    if (typeof (num) != "number") throw new Error(-1, "dateAfterDays(num,type)的num参数为数值类型.");
    type = (type == null ? 0 : type);
    var arr = [1000, 86400000];
    var dd = this.valueOf();
    dd += num * arr[type];
    return new Date(dd);
}
//通过当前时间计算当前周数
Date.prototype.getWeekNumber = function () {
    var d = new Date(this.getFullYear(), this.getMonth(), this.getDate(), 0, 0, 0);
    var DoW = d.getDay();
    d.setDate(d.getDate() - (DoW + 6) % 7 + 3); // Nearest Thu
    var ms = d.valueOf(); // GMT
    d.setMonth(0);
    d.setDate(4); // Thu in Week 1
    return Math.round((ms - d.valueOf()) / (7 * 864e5)) + 1;
}
// 获取当前日期所在月的周数
Date.prototype.getMonthWeeks = function () {
    var first = new Date(this.getFullYear(), this.getMonth(), 1);
    var last = new Date(this.getFullYear(), this.getMonth() + 1, 0, 23, 59, 59);
    var days = first.getDay() + last.getDate();
    return Math.ceil(days / 7);
}
//根据当前日期所在年和周数返回周一的日期
Date.prototype.RtnMonByWeekNum = function (weekNum) {
    if (typeof (weekNum) != "number")
        throw new Error(-1, "RtnByWeekNum(weekNum)的参数是数字类型.");
    var date = new Date(this.getFullYear(), 0, 1);
    var week = date.getDay();
    week = (week == 0 ? 7 : week);
    return date.dateAfter(weekNum * 7 - week - 7 + 7, 1);
}

//根据当前日期所在年和周数返回周日的日期
Date.prototype.RtnSunByWeekNum = function (weekNum) {
    if (typeof (weekNum) != "number")
        throw new Error(-1, "RtnByWeekNum(weekNum)的参数是数字类型.");
    var date = new Date(this.getFullYear(), 0, 1);
    var week = date.getDay();
    week = (week == 0 ? 7 : week);
    return date.dateAfter(weekNum * 7 - week - 2 + 7, 1);
}
