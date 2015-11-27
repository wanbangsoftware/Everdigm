<%@ Page Title="" Language="C#" MasterPageFile="~/mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="device.aspx.cs" Inherits="Wbs.Everdigm.Web.mobile.device" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <link href="../bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolderLeft" runat="server">
    <a href="javascript:history.go(-1);" class="backto"><span class="glyphicon glyphicon-arrow-left"></span></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TitleContentPlaceHolderRight" runat="server">
    <a href="javascript:location.reload();" class="forwordto"><span class="glyphicon glyphicon-refresh"></span></a>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="nav">
        <ul class="items">
            <li style="width: 50% !important;"><a class="current" id="equipmentId" runat="server">DH300LCA-20002</a></li>
        </ul>
    </div>
    <dl class="invest-type">
        <dt>
            <span class="text-success iconleft signal cell-engine" style="font-size: 1.5em;"></span>Engine ON
        </dt>
        <dd>
            <span style="color:#F28B19;"><span class="glyphicon glyphicon-scale"></span> Work time: 1,024:30(Hr)</span>
            <div class="total-num"><span class="text-warning"><span class="glyphicon glyphicon-calendar"></span> Today: 3:30(Hr)</span></div>
        </dd>
        <dd>
            <span class="text-info"><span class="glyphicon glyphicon-calendar"></span> Yesterday: 3:30(Hr)</span>
        </dd>
    </dl>
    <dl class="invest-type">
        <dt>
            <span class="iconleft fa fa-lock lock-full" style="font-size: 1.5em;"></span><span class="lock-full">Security</span>
        </dt>
        <dd>
            <span class="lock-full"><span class="fa fa-lock"></span> Full lock: 2015/04/03 22:10</span>
        </dd>
    </dl>
    <dl class="invest-type">
        <dt>
            <span class="iconleft fa fa-cloud" style="font-size: 1.5em;"></span>Wireless Communication
        </dt>
        <dd>
            <span class="text-info"><span class="glyphicon glyphicon-comment"></span> Recent data: GPS period data</span>
            <div class="total-num"><span class="label label-info">TCP</span></div>
        </dd>
        <dd>
            <span class="text-info"><span class="glyphicon glyphicon-calendar"></span> Recent active time: 2015/04/03 22:10</span>
        </dd>
    </dl>
    <dl class="invest-type">
        <dt>
            <span class="iconleft fa fa-globe text-success" style="font-size: 1.5em;"></span>Position
            <em class="status"><span class="glyphicon glyphicon-calendar"></span> 2015/04/03 22:10</em>
        </dt>
        <dd>
            <span class="text-muted"><span class="glyphicon glyphicon-map-marker"></span> Asian Highway 3, Songino Khairkhan, Ulaanbaatar, Mongolia</span>
        </dd>
        <dd>
            <span id="spanLat" class="hidden" runat="server"></span>
            <span id="spanLon" class="hidden" runat="server"></span>
            <img id="imgPosition" alt="map" width="300" height="300" />
        </dd>
    </dl>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript">
        function initialize() {
            var lat = parseFloat($("[id$=\"spanLat\"]").text());
            var lon = parseFloat($("[id$=\"spanLon\"]").text());
            $("#imgPosition").attr("src", "static_google_map.aspx?width=300&height=300&lat=" + $("[id$=\"spanLat\"]").text() + "&lon=" + $("[id$=\"spanLon\"]").text());
            //var point = new google.maps.LatLng(lat, lon);//(37.556016, 121.247957);
            //var mapOptions = {
            //    center: point,
            //    zoom: 14,
            //    mapTypeId: google.maps.MapTypeId.ROADMAP
            //};
            //var map = new google.maps.Map(document.getElementById("map_canvas"),
            //    mapOptions);

            //var marker = new google.maps.Marker({
            //    position: new google.maps.LatLng(lat, lon),
            //    map: map,
            //    title: "I'm here!"
            //});
        }
        $(document).ready(function () {
            initialize();
        });
    </script>
</asp:Content>
