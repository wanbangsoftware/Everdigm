(function ($) {

    "use strict";

    var options = {
        //events_source: '/api/events.php',
        events_source: [
            {
                "id": 293,
                "title": "Event Important",
                "url": "#",
                "class": "event-important",
                //"class": "event-info",
                //"class": "event-warning",
                //"class": "event-special",
                //"class": "event-success",
                "start": 1472074682000, // Milliseconds
                "end": 1473094682000 // Milliseconds
            },{
                "id": 299,
                "title": "Event Inverse",
                "url": "#",
                "class": "event-inverse",
                "start": 1473074682000, // Milliseconds
                "end": 1473394682000 // Milliseconds
            }
        ],
        weekbox: false,
        view: 'month',
        tmpl_path: '../bootstrap-calendar/tmpls/',
        tmpl_cache: false,
        //modal: "#events-modal",
        modal_title: function(e) { return e.title },
        //day: '2013-03-12',
        time_start: '05:00',
        time_end: '23:30',
        onAfterEventsLoad: function (events) {
            if (!events) {
                return;
            }
            //var list = $('#eventlist');
            //list.html('');

            //$.each(events, function (key, val) {
            //    $(document.createElement('li'))
			//		.html('<a href="' + val.url + '">' + val.title + '</a>')
			//		.appendTo(list);
            //});
        },
        onAfterViewLoad: function (view) {
            $('#datetime').text(this.getTitle());
            $('.btn-group button').removeClass('active');
            $('button[data-calendar-view="' + view + '"]').addClass('active');
        },
        classes: {
            months: {
                general: 'label'
            }
        }
    };

    var calendar = $('#calendar').calendar(options);

    $('.btn-group button[data-calendar-nav]').each(function () {
        var $this = $(this);
        $this.click(function (e) {
            calendar.navigate($this.data('calendar-nav'));
            e.preventDefault();
        });
    });

    $('.btn-group button[data-calendar-view]').each(function () {
        var $this = $(this);
        $this.click(function (e) {
            calendar.view($this.data('calendar-view'));
            e.preventDefault();
        });
    });

    //$('#events-modal .modal-header, #events-modal .modal-footer').click(function (e) {
    //    //e.preventDefault();
    //    //e.stopPropagation();
    //});
}(jQuery));