<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_setting.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Setting</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigatorContentPlaceHolder" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" id="functionBar">
        <li role="presentation">
            <a href="equipment_command.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Command</a>
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
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Setting<span class="caret"></span></a>
        </li>
        <li role="presentation" style="float: right; cursor: pointer !important;" title="close">
            <a href="./equipment_inquiry.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">&times;</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <table class="table table-hover" style="margin-top: 2px !important;">
        <tr>
            <td colspan="4" class="alert-info">Change Equipment Information</td>
        </tr>
        <tr>
            <td colspan="4">
                <ul class="nav nav-tabs" style="border-bottom: 0px;" role="tablist">
                    <li role="presentation" class="dropdown" id="ddType">
                        <a id="dropType" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Type:</span><span class="caret"></span>
                        </a>
                        <ul id="menuType" class="dropdown-menu" role="menu" aria-labelledby="dropType">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="selectedType" runat="server" value="0" />
                    </li>
                    <li role="presentation" class="dropdown" id="ddModel">
                        <a id="dropModel" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Model:</span><span class="caret"></span>
                        </a>
                        <ul id="menuModel" class="dropdown-menu" role="menu" aria-labelledby="dropModel">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Choose a Type First</a></li>
                        </ul>
                        <input type="hidden" id="selectedModel" runat="server" value="0" />
                    </li>
                    <li role="presentation" class="tablist-item-input">
                        <input class="form-control" runat="server" id="number" style="width: 150px;" placeholder="number" maxlength="5" />
                        <input type="hidden" runat="server" id="old" />
                    </li>
                    <li role="presentation" class="tablist-item-input">
                        <a><span id="fullNumber"></span></a>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="alert-info">Change Storage Information</td>
        </tr>
        <tr>
            <td colspan="4">
                <ul class="nav nav-tabs" style="border-bottom: 0px;" role="tablist">
                    <li role="presentation" class="dropdown" id="ddWarehouses">
                        <a id="dropWarehouses" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                        </a>
                        <ul id="menuWarehouses" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouses">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="hidWarehouse" runat="server" value="0" />
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="popup-td">
                <asp:Button ID="btSaveInfo" runat="server" CssClass="hidden" Text="Save" OnClick="btSaveInfo_Click" />
                <button class="btn btn-primary" id="btSave" type="button"><span class="glyphicon glyphicon-floppy-open"></span><span> Save changes</span></button>
                <button class="btn btn-warning" type="button">Leave blank to avoid modification</button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.setting.js"></script>
</asp:Content>
