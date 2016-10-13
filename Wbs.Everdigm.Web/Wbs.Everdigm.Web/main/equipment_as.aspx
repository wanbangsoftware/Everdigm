<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_as.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_as" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: A/S History</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Repair History-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Repair History</span>
            <div class="input-group" style="float: right; margin-top: -7px; display: none;">
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
        <div class="panel-body" style="min-height: 400px;">
            <table class="table table-hover">
                <thead>
                    <tr class="alert-info">
                        <th class="panel-body-td" style="width: 30px; text-align: center;">#</th>
                        <th class="panel-body-td" style="width: 150px;">Time</th>
                        <th class="panel-body-td" style="width: 150px;">User</th>
                        <th class="panel-body-td" style="width: 80px;">Action</th>
                        <th class="panel-body-td">Description</th>
                    </tr>
                </thead>
                <tbody id="tbodyBody">
                </tbody>
                <tfoot>
                    <tr>
                        <td style="height: 2px;" colspan="5" class="panel-body-td"></td>
                    </tr>
                </tfoot>
            </table>
            <nav style="margin-top: 2px !important; margin-bottom: 2px !important; float: right;">
                <ul class="pagination-sm" style="margin: 0px !important;">
                </ul>
            </nav>
        </div>
    </div>
    <!--Service History-->
    <div class="panel panel-default" style="margin-top: 2px; display: none;">
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
    <script src="../js/twbsPagination/jquery.twbsPagination.js"></script>
    <script src="../scripts/main/equipment.as.js"></script>
</asp:Content>
