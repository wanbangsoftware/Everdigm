<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="google_map.aspx.cs" Inherits="Wbs.Everdigm.Web.google_map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Google Map: Reverse Geocoding</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <link href="css/body.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btSave" CssClass="hidding" runat="server" Text="Button" OnClick="btSave_Click" />
        <input type="hidden" id="hidPosId" value="0" runat="server" />
        <input type="hidden" id="hidLatLng" value="" runat="server" />
        <input type="hidden" id="hidAddress" value="" runat="server" />
    </form>
    <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="js/jquery.json-2.4.js"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyA3ZyjLQqMHZ7jtuVmCxbK11r86K2UuNLM&language=en"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            reverseGeocoding();
        });

        function reverseGeocoding() {
            var id = parseInt($("#hidPosId").val());
            if (id < 1) return;

            var input = $("#hidLatLng").val();
            if (null == input || "" == input) return;

            var latlngStr = input.split(",", 2);
            var lat = parseFloat(latlngStr[0]);
            var lng = parseFloat(latlngStr[1]);
            var latlng = new google.maps.LatLng(lat, lng);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ "latLng": latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        var addr = "";
                        var len = results[0].address_components.length;
                        for (var i = 0; i < len; i++) {
                            var obj = results[0].address_components[i];
                            addr += obj.long_name + (i + 1 >= len ? "" : ", ");
                        }
                        $("#hidAddress").val(addr);
                        $("#btSave").click();
                    }
                }
            });
        }
    </script>
</body>
</html>
