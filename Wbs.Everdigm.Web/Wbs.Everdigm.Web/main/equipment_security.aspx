<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_security.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_security" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Security</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Send Command-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Security command</span>
            <ul class="nav nav-tabs" role="tablist" id="commandBar" style="float: right; margin-top: -11px;">
                <li role="presentation" class="dropdown active">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Select command:</span><span class="caret"></span></a>
                    <ul id="menuCommands" runat="server" class="dropdown-menu" role="menu" aria-labelledby="dropTypes">
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Cannot send any command</a></li>
                    </ul>
                    <input type="hidden" id="cmdInfo" value="" />
                    <input type="hidden" id="cmdTitle" value="" />
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
            <span>Security Command History</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <select style="float: left; width: 200px;" class="form-control">
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
    <!--命令发送进度-->
    <div class="modal fade" id="analyseModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">Testing progress</h4>
                </div>
                <div class="modal-body">
                    <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important;  margin-bottom: 0px !important; height: 220px; overflow: auto;">
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="satWarning" type="button" class="btn btn-warning" style="display:none;">
                        <span class="glyphicon glyphicon-time"></span> Satellite mode may take more time to wait the result
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
    <script type="text/javascript" src="../scripts/main/equipment.security.js"></script>
</asp:Content>
