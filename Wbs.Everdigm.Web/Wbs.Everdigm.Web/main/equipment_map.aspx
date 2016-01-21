<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_map.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../js/BootSiderMenu/css/BootSideMenu.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/google.custom.marker.css" rel="stylesheet" />
    <style type="text/css">
        html, body {
            height: 100%;
            padding: 0;
        }

        #map_canvas {
            width: 100%;
            height: 100%;
        }

        .custom-modal-header {
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -webkit-border-bottom-left-radius: 0px;
            -webkit-border-bottom-right-radius: 0px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomleft: 0px;
            -moz-border-radius-bottomright: 0px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        .list-group img {
            width: auto;
            height: 30px;
            margin-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var topHeight = "<%=TopHeight%>";
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="map_canvas"></div>
        <div id="provice_list">
            <div class="btn btn-primary custom-modal-header" style="width: 100%; margin-bottom: 1px;">
                Provinces:
                <input type="hidden" value="Ulaanbaatar" id="hiddenProvinces" />
            </div>
            <div class="list-group">
                <a href="#" class="list-group-item">
                    <img alt="#" src="../images/provinces/icon_all.png" />All</a>
                <a href="#Ulaanbaatar" class="list-group-item active">
                    <img alt="" src="../images/provinces/Ulaanbaatar.png" />Ulaanbaatar</a>
                <a href="#Arkhangai" class="list-group-item">
                    <img alt="" src="../images/provinces/Arhangai.png" />Arkhangai</a>
                <a href="#Bayankhongor" class="list-group-item">
                    <img alt="" src="../images/provinces/Bayankhongor.png" />Bayankhongor</a>
                <a href="#Bayan-Ulgii" class="list-group-item">
                    <img alt="" src="../images/provinces/Bayan_Olgii.png" />Bayan-Ulgii</a>
                <a href="#Bulgan" class="list-group-item">
                    <img alt="" src="../images/provinces/Bulgan.png" />Bulgan</a>
                <a href="#Darkhan-Uul" class="list-group-item">
                    <img alt="" src="../images/provinces/DarkhanUul.png" />Darkhan-Uul</a>
                <a href="#Dornod" class="list-group-item">
                    <img alt="" src="../images/provinces/Dornod.png" />Dornod</a>
                <a href="#Dornogovi" class="list-group-item">
                    <img alt="" src="../images/provinces/Dornogovi.png" />Dornogovi</a>
                <a href="#Dundgovi" class="list-group-item">
                    <img alt="" src="../images/provinces/Dundgovi.png" />Dundgovi</a>
                <a href="#Govi-Altai" class="list-group-item">
                    <img alt="" src="../images/provinces/Govaltai.gif" />Govi-Altai</a>
                <a href="#Govisumber" class="list-group-item">
                    <img alt="" src="../images/provinces/Govsumber.gif" />Govisumber</a>
                <a href="#Khentii" class="list-group-item">
                    <img alt="" src="../images/provinces/Khentii.gif" />Khentii</a>
                <a href="#Khovd" class="list-group-item">
                    <img alt="" src="../images/provinces/Khovd.png" />Khovd</a>
                <a href="#Khuvsgul" class="list-group-item">
                    <img alt="" src="../images/provinces/Khuvsgul.png" />Khuvsgul</a>
                <a href="#Orkhon" class="list-group-item">
                    <img alt="" src="../images/provinces/Orkhon.png" />Orkhon</a>
                <a href="#Selenge" class="list-group-item">
                    <img alt="" src="../images/provinces/Selenge.gif" />Selenge</a>
                <a href="#Sukhbaatar" class="list-group-item">
                    <img alt="" src="../images/provinces/Sukhbaatar.png" />Sukhbaatar</a>
                <a href="#Tuv" class="list-group-item">
                    <img alt="" src="../images/provinces/Tov.gif" />Tuv</a>
                <a href="#Umnugovi" class="list-group-item">
                    <img alt="" src="../images/provinces/Ömnögovǐ.jpg" />Umnugovi</a>
                <a href="#Uvs" class="list-group-item">
                    <img alt="" src="../images/provinces/Uvs.gif" />Uvs</a>
                <a href="#Uvurkhangai" class="list-group-item">
                    <img alt="" src="../images/provinces/Uvurkhangay.png" />Uvurkhangai</a>
                <a href="#Zavkhan" class="list-group-item">
                    <img alt="" src="../images/provinces/Zavkhan.gif" />Zavkhan</a>
            </div>
        </div>
        <div id="equipment_list">
            <div class="btn btn-primary custom-modal-header" style="width: 100%; margin-bottom: 1px;">
                Equipments:
            </div>
            <div class="list-group" id="grp_equipments">
                <a href="#" class="list-group-item">Loading data...</a>
            </div>
        </div>
        <div class="modal fade" id="warningLoading" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-warning">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="deletelLabel">Warning!!</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                Loading data, please wait...
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script src="../js/BootSiderMenu/js/BootSideMenu.js"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyA3ZyjLQqMHZ7jtuVmCxbK11r86K2UuNLM&sensor=false"></script>
    <script src="../js/CustomGoogleMapMarker.js"></script>
    <script src="../js/js.marker.clusterer/src/markerclusterer.js"></script>
    <script src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.map.js"></script>
</body>
</html>
