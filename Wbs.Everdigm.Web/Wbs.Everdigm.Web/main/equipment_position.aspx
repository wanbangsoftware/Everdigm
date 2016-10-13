<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_position.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_position" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
<script type="text/javascript">
    var statLat = "<%=Lat%>", statLng = "<%=Lng%>", statTime = "<%=Dat%>";
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Position</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Map-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <input type="hidden" id="hiddenLatLng" runat="server" />
        <div class="panel-heading">
            <span id="span_map" style="cursor: pointer;">Map</span>
            <span style="float: right;"><code>Longitude:</code><span id="lng">-</span>　<code>Latitude:</code><span id="lat">-</span>　<code>Time:</code><span id="time">-</span></span>
        </div>
        <div class="panel-body" style="min-height: 200px; padding: 2px !important;">
            <div id="map_canvas" style="width: 100%; height: 400px;"></div>
        </div>
    </div>
    <!--Position History-->
    <div class="panel panel-default" style="margin-top: 2px;">
        <div class="panel-heading">
            <span id="span_malf" style="cursor: pointer;" title="click to collapse">Position History</span>
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
        <div class="panel-body" style="min-height: 120px; display: none;">
            <table class="table table-hover position" style="margin-bottom: 0px !important;">
                <thead>
                    <tr class="alert-info">
                        <th class="panel-body-td" style="width: 180px;">Type</th>
                        <th class="panel-body-td" style="width: 160px;">Receive time</th>
                        <th class="panel-body-td" style="width: 160px;">GPS time</th>
                        <th class="panel-body-td">Address</th>
                    </tr>
                </thead>
                <tbody>
                    <tr data-latlng="">
                        <td class="panel-body-td" colspan="4">Loading data...</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="5" class="panel-body-td" style="padding: 0px !important;"></td>
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
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyA3ZyjLQqMHZ7jtuVmCxbK11r86K2UuNLM&sensor=false"></script>
    <script type="text/javascript" src="../js/google.map.api.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.position.js"></script>
</asp:Content>
