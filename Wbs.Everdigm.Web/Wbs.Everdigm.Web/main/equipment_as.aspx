<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_as.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: A/S History</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigatorContentPlaceHolder" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" id="functionBar">
        <li role="presentation">
            <a href="equipment_command.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Command</a>
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
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">A/S History<span class="caret"></span></a>
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
    <!--Repair History-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Repair History</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <div class="input-group" style="float: left; margin-left: 2px;">
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
        </div>
        <div class="panel-body" style="min-height: 120px;">
            <table class="table table-hover">
                <tbody id="tbodyWorktimes">
                    <tr>
                        <td class="panel-body-td" style="text-align: center;">
                            
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td style="height: 2px;" class="panel-body-td"></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
    <!--Command History-->
    <div class="panel panel-default" style="margin-top: 2px;">
        <div class="panel-heading">
            <span>Service History:</span>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
</asp:Content>
