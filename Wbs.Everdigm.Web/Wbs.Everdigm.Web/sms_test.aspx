<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sms_test.aspx.cs" Inherits="Wbs.Everdigm.Web.sms_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        #hex {
            width: 617px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Hex:<br />
            <input type="text" id="hex" runat="server" /><br />
            Hex data:<br />
            <span id="hexdata" runat="server"></span><br />
            Base64String:<br />
            <span id="base64" runat="server"></span>
            <br />
            Base64UrlEncode:<br />
            <span id="base64Encode" runat="server"></span><br />
            <asp:Button ID="Encode" runat="server" Text="Encode" OnClick="Encode_Click" />
            <asp:Button ID="Decode" runat="server" Text="Decode" OnClick="Decode_Click" />
        </div>
    </form>
</body>
</html>
