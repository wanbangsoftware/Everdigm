<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="as_tracker.aspx.cs" Inherits="Wbs.Everdigm.Web.service.as_tracker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
    <link href="../mobile/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" id="hidKey" runat="server" />
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>A/S: Tracker</strong>
            </div>
            <div class="panel-body">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active">
                        <button class="btn btn-info" type="button" style="margin-top: 3px; margin-right: 3px; margin-bottom: 3px;">
                            <i class="fa fa-anchor"></i> <span runat="server" id="aTrackerId"></span>
                        </button>
                    </li>
                    <li role="presentation">
                        <button class="btn btn-info" type="button" style="margin-top: 3px; margin-right: 3px;">
                            <i class="fa fa-car"></i> <span runat="server" id="aTrackerVehicle"></span>
                        </button>
                    </li>
                    <li role="presentation">
                        <div class="input-group" style="margin-top: 3px;">
                            <div class="input-daterange input-group" style="float: left; margin-left: 2px;">
                                <input type="text" class="input-md form-control little-input click-input" runat="server" />
                                <span class="input-group-addon">to </span>
                                <input type="text" class="input-md form-control little-input click-input" runat="server" />
                            </div>
                            <span class="input-group-btn" style="float: left;">
                                <button class="btn btn-success" type="button">Query</button>
                            </span>
                        </div>
                    </li>
                    <li role="presentation">
                        <button class="btn btn-warning" type="button" style="margin-top: 3px; margin-left: 3px; margin-right: 3px;">
                            <span class="signal cell-triangle-ruler"></span> Distance
                            <span id="distance" class="badge">0</span> km
                        </button>
                    </li>
                    <li role="presentation">
                        <button class="btn btn-warning" type="button" style="margin-top: 3px; margin-right: 3px;">
                            <span class="glyphicon glyphicon-map-marker"></span> point(s)
                            <span id="points" class="badge"></span>
                        </button>
                    </li>
                    <li role="presentation">
                        <button class="btn btn-primary" id="animation" type="button" style="margin-top: 3px;">
                            <span id="animationFlag" class="glyphicon glyphicon-play"></span> Animate
                            <span id="animationIndex"></span>
                        </button>
                    </li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content" style="border: 1px solid #fff !important;">
                    <div class="row">
                        <div class="col-sm-8">
                            <div id="map_canvas" style="width: 100%; height: 500px;">
                            </div>
                        </div>
                        <div class="col-sm-4" style="height: 500px; overflow: auto;">
                            <!--Equipment basic informations-->
                            <table class="table table-hover" style="margin-top: 2px !important;">
                                <thead>
                                    <tr class="alert-info">
                                        <th class="panel-body-td" style="width: 30px;">#</th>
                                        <th class="panel-body-td" style="width: 150px;">Receive time</th>
                                        <th class="panel-body-td">Type</th>
                                    </tr>
                                </thead>
                                <tbody id="tbodyBody">
                                    <tr data-latlng="">
                                        <td class="panel-body-td" colspan="3">Loading data...</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.json-2.4.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/jlinq-3.0.1.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-slider.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script type="text/javascript" src="../bootstrap3/bootstrap-datepicker-1.3.0/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../bootstrap3/models/js/bootstrap-dialog.min.js"></script>
    <script type="text/javascript" src="../bootstrap3/models/js/bootstrap-typeahead.js"></script>
    <script type="text/javascript" src="../js/javascript.date.pattern.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../js/jquery.timer.js"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyA3ZyjLQqMHZ7jtuVmCxbK11r86K2UuNLM&sensor=false"></script>
    <script type="text/javascript" src="../js/google.map.api.js"></script>
    <script type="text/javascript" src="../scripts/service/as.common.js"></script>
    <script type="text/javascript" src="../scripts/service/as.tracker.js"></script>
</body>
</html>
