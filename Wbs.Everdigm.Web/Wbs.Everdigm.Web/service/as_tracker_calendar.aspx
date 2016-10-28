<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="as_tracker_calendar.aspx.cs" Inherits="Wbs.Everdigm.Web.service.as_tracker_calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap-calendar/components/bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap-calendar/components/bootstrap3/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../bootstrap-calendar/css/calendar.min.css" rel="stylesheet" />
    <style type="text/css">
        .custom-modal-header {
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" id="hidKey" runat="server" />
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>A/S: Tracker Daily Work for <span id="spanTrackerNumber" runat="server">89100004</span></strong> (<span id="datetime"></span>) 
                <span id="spanTrackerToMap" class="label label-primary" style="cursor: pointer;"><span class="glyphicon glyphicon-map-marker" aria-hidden="true"></span>Map</span>
                <div class="pull-right form-inline" style="margin-top: -7px !important;">
                    <div class="btn-group">
                        <button class="btn btn-primary" data-calendar-nav="prev">&lt;&lt; Prev</button>
                        <button class="btn" data-calendar-nav="today">Today</button>
                        <button class="btn btn-primary" data-calendar-nav="next">Next &gt;&gt;</button>
                    </div>
                    <div class="btn-group">
                        <button class="btn btn-warning" data-calendar-view="year">Year</button>
                        <button class="btn btn-warning active" data-calendar-view="month">Month</button>
                        <button class="btn btn-warning" data-calendar-view="week">Week</button>
                        <button class="btn btn-warning" data-calendar-view="day">Day</button>
                    </div>
                </div>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content" style="padding: 5px !important;">
                        <div id="calendar"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="events-modal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4>Event</h4>
                    </div>
                    <div class="modal-body" style="height: 400px">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../js/jquery-2.1.4.min.js"></script>
    <script src="../bootstrap-calendar/components/bootstrap3/js/bootstrap.min.js"></script>
    <script src="../bootstrap-calendar/components/underscore/underscore-min.js"></script>
    <script src="../bootstrap-calendar/js/calendar.js"></script>
    <script src="../scripts/service/as.tracker.calendar.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#spanTrackerToMap").click(function () {
                document.location = "as_tracker.aspx?key=" + $("#hidKey").val();
            });
            //var calendar = $("#calendar").calendar({
            //    tmpl_path: "../bootstrap-calendar/tmpls/",
            //    language: 'zh-CN',
            //    modal: "events-modal",
            //    //events_source: function () { return []; }
            //events_source: [
            //    {
            //        "id": 293,
            //        "title": "Event 1",
            //        "url": "http://example.com",
            //        "class": "event-important",
            //        "start": 1473074682000, // Milliseconds
            //        "end": 1473074682000 // Milliseconds
            //    }
            //]
            //});
        });
    </script>
</body>
</html>
