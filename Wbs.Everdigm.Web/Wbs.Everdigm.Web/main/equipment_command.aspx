<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_command.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_command" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Command</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigatorContentPlaceHolder" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" id="functionBar">
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Command<span class="caret"></span>
            </a>
        </li>
        <li role="presentation">
            <a href="equipment_security.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Security</a>
        </li>
        <li role="presentation">
            <a href="equipment_alarm.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Alarm</a>
        </li>
        <li role="presentation">
            <a href="equipment_position.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Map</a>
        </li>
        <li role="presentation">
            <a href="equipment_work.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Work</a>
        </li>
        <li role="presentation">
            <a href="equipment_as.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">A/S History</a>
        </li>
        <li role="presentation">
            <a href="equipment_storage.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Storage History</a>
        </li>
        <li role="presentation">
            <a href="equipment_setting.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Setting</a>
        </li>
        <li role="presentation" style="float: right; cursor: pointer !important;" title="close">
            <a href="./equipment_inquiry.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">&times;</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Send Command-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Send command</span>
            <ul class="nav nav-tabs" role="tablist" id="commandBar" style="float: right; margin-top: -11px;">
                <li role="presentation" class="dropdown active">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Select command:</span><span class="caret"></span></a>
                    <ul id="menuCommands" runat="server" class="dropdown-menu" role="menu" aria-labelledby="dropTypes">
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                    </ul>
                    <input type="hidden" id="cmdInfo" value="" />
                </li>
                <li role="presentation" style="margin-top: 4px;">
                    <button class="btn btn-info" type="button"><span class="glyphicon glyphicon-repeat"></span>Send</button>
                </li>
            </ul>
        </div>
        <div class="panel-body">
            <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; height: 150px; overflow: auto;">
            </div>
        </div>
    </div>
    <!--Command History-->
    <div class="panel panel-default" style="margin-top: 2px;">
        <div class="panel-heading">
            <span>Command History</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <select style="float: left; width: 250px;" class="form-control">
                    <option>Select command:</option>
                </select>
                <div class="input-daterange input-group" id="datepicker" style="float: left; margin-left: 2px;">
                    <input type="text" class="input-md form-control little-input click-input" runat="server" id="start" name="start" />
                    <span class="input-group-addon">to </span>
                    <input type="text" class="input-md form-control little-input click-input" runat="server" id="end" name="end" />
                </div>
                <span class="input-group-btn" style="float: left;">
                    <button class="btn btn-success" type="button">Query</button>
                </span>
            </div>
        </div>
        <div class="panel-body">
            <div class="bs-callout" style="margin-top: 2px; margin-bottom: 0px; height: 150px; overflow: auto;">
                Loading data...
            </div>
        </div>
    </div>
    <div class="modal fade" id="alertModal" tabindex="-1" role="dialog" aria-labelledby="alertModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-warning">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="alertModalLabel">Warning</h4>
                </div>
                <div class="modal-body">
                    ...
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="alertModalWorktime" tabindex="-1" role="dialog" aria-labelledby="alertModalLabelWork" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-warning">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="alertModalLabelWork">Loader Work Time initialize</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-8">
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon1" style="width: 90px;">Hours:</span>
                                <input type="text" id="txtHour" class="form-control" placeholder="hour" aria-describedby="basic-addon1">
                            </div>
                        </div>
                        <div class="col-lg-8" style="margin-top: 5px;">
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon2" style="width: 90px;">Minutes:</span>
                                <input type="text" id="txtMinute" class="form-control" placeholder="minute" aria-describedby="basic-addon2">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="initializeWorktime" data-dismiss="modal"><span class="glyphicon glyphicon-ok"></span>Send</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!--命令发送进度-->
    <div class="modal fade" id="analyseModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">Testing progress</h4>
                </div>
                <div class="modal-body">
                    <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; height: 220px; overflow: auto;">
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="satWarning" type="button" class="btn btn-warning" style="display:none;">
                        <span class="glyphicon glyphicon-time"></span> Satellite mode may take more time to wait
                    </button>
                    <button class="btn btn-primary" type="button">
                        time used: <span class="badge" id="timeUsed">00:00</span>
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/jquery.timer.js"></script>
    <script type="text/javascript" src="../scripts/main/command.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.command.js"></script>
</asp:Content>
