<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_storage.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_storage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Storage</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Storage History-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Storage History</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <div class="input-group" style="float: left; margin-left: 2px;">
                    <div class="input-daterange input-group" style="float: left; margin-left: 2px;">
                        <input type="text" class="input-md form-control little-input click-input" id="start" runat="server" />
                        <span class="input-group-addon">to </span>
                        <input type="text" class="input-md form-control little-input click-input" id="end" runat="server" />
                    </div>
                    <span class="input-group-btn" style="float: left;">
                        <button class="btn btn-success" type="button">Query</button>
                    </span>
                </div>
            </div>
        </div>
        <div class="panel-body" style="min-height: 120px;">
            <table class="table table-hover">
                <thead>
                    <tr class="alert-info">
                        <th class="panel-body-td" style="width: 30px;text-align: center;">#</th>
                        <th class="panel-body-td" style="width: 80px;">Date</th>
                        <th class="panel-body-td" style="width: 30px;" title="Store Times">Times</th>
                        <th class="panel-body-td" style="width: 120px;">Warehouse</th>
                        <th class="panel-body-td" style="width: 100px;">Receipt No.</th>
                        <th class="panel-body-td" style="width: 40px;">Code</th>
                        <th class="panel-body-td">Status</th>
                    </tr>
                </thead>
                <tbody id="tbodyWorktimes">
                    <tr>
                        <td class="panel-body-td" colspan="7">Loading data...</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td style="height: 2px;" colspan="7" class="panel-body-td"></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../scripts/main/equipment.storage.js"></script>
</asp:Content>
