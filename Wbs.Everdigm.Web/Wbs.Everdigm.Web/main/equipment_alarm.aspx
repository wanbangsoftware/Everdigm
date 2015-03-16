<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_alarm.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_alarm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Alarm</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigatorContentPlaceHolder" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" id="functionBar">
        <li role="presentation">
            <a href="equipment_command.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Command</a>
        </li>
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Alarm<span class="caret"></span></a>
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
        <li role="presentation" style="float: right; cursor: pointer !important;" title="close">
            <a href="./equipment_inquiry.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">&times;</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Alarms-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Alarms</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <div class="input-daterange input-group" style="float: left; margin-left: 2px;">
                    <input type="text" class="input-md form-control little-input click-input" runat="server" />
                    <span class="input-group-addon">to </span>
                    <input type="text" class="input-md form-control little-input click-input" runat="server" />
                </div>
                <span class="input-group-btn" style="float: left;">
                    <button class="btn btn-success" type="button">Query</button>
                </span>
            </div>
        </div>
        <div class="panel-body" style="min-height: 120px;">
            <table class="table table-hover" style="margin-bottom: 0px !important;">
                <thead>
                    <tr class="alert-info">
                        <th class="panel-body-td" style="width: 150px;">Date</th>
                        <th class="panel-body-td" style="width: 120px;">Type</th>
                        <th class="panel-body-td">Alarm Description</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="panel-body-td" colspan="3">loading data...</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td style="padding: 0px !important;" colspan="3" class="panel-body-td"></td>
                    </tr>
                </tfoot>
            </table>
            <nav style="margin-top: 2px !important; margin-bottom: 2px !important; float: right;">
                <ul class="pagination-sm" style="margin: 0px !important;">
                </ul>
            </nav>
        </div>
    </div>
    <!--Malfunctions-->
    <div class="panel panel-default" style="margin-top: 2px;">
        <div class="panel-heading">
            <span>Malfunctions:</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <div class="input-daterange input-group" style="float: left; margin-left: 2px;">
                    <input type="text" class="input-md form-control little-input click-input" runat="server" />
                    <span class="input-group-addon">to </span>
                    <input type="text" class="input-md form-control little-input click-input" runat="server" />
                </div>
                <span class="input-group-btn" style="float: left;">
                    <button class="btn btn-success" type="button">Query</button>
                </span>
            </div>
        </div>
        <div class="panel-body" style="min-height: 120px;">
            <table class="table table-hover" style="margin-bottom: 0px !important;">
                <thead>
                    <tr class="alert-info">
                        <th class="panel-body-td" style="width: 150px;">Date</th>
                        <th class="panel-body-td" style="width: 50px;">CNT</th>
                        <th class="panel-body-td" style="width: 50px;">Code</th>
                        <th class="panel-body-td">Code description</th>
                        <th class="panel-body-td" style="width: 50px;">FMI</th>
                        <th class="panel-body-td">FMI description</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="panel-body-td" colspan="6">loading data...</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td style="padding: 0px !important;" colspan="6" class="panel-body-td"></td>
                    </tr>
                </tfoot>
            </table>
            <nav style="margin-top: 2px !important; margin-bottom: 2px !important; float: right;">
                <ul class="pagination-sm" style="margin: 0px !important;">
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/twbsPagination/jquery.twbsPagination.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.alarm.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[data-toggle=\"tooltip\"]").tooltip();
        });
    </script>
</asp:Content>
