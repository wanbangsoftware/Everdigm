$(document).ready(function () {
    tabsRefresh();
	$("#ul_nav").on("click", "a", // When a tab is clicked...
		function () {
			$(this).parent().siblings().find("a").removeClass("current"); // Remove "current" class from all tabs
			$(this).parent().siblings().find("span").hide();// Hide other closers
			$(this).addClass("current"); // Add class "current" to clicked tab
			$(this).parent().find("span").show(); // Show the current closer
			var currentTab = $(this).attr("href"); // Set variable "currentTab" to the value of href of clicked tab
			$(currentTab).siblings().hide(); // Hide all content divs
			$(currentTab).show(); // Show the content div with the id equal to the id of clicked tabgggg
			return false;
		}
	).on("click", "span", function () {
	    var closedTab = $(this).next().attr("href");
	    $(this).parent().removeClass("current");
	    $(this).parent().fadeTo(400, 0, function () { $(this).slideUp(400); $(this).remove(); });
	    $(closedTab).fadeOut("slow", function () { $(closedTab).remove(); tabsRefresh(); });
	    // 查看净水器终端的检测列表并删除相应的检测项
	    if (null != testObjects) {
            // 从检测队列里删除记录
	        testObjects.splice($.inArray(closedTab.replace("#tab_", ""), testObjects), 1);
	    }
		//tabsRefresh();
	});
});

function tabsRefresh() {
    $('.content-box .content-box-content div.tab-content').hide(); // Hide the content divs
    $('ul.content-box-tabs li span').hide(); // Hide the content closers
    $('ul.content-box-tabs li a.default-tab').addClass('current'); // Add the class "current" to the default tab
    $('.content-box-content div.default-tab').show(); // Show the div with class "default-tab"
    $('ul.content-box-tabs li span.default-tab').show(); // Show the span with class "default-tab"
}