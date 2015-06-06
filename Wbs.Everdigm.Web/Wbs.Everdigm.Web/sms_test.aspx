<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sms_test.aspx.cs" Inherits="Wbs.Everdigm.Web.sms_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Hex:<br />
            <input type="text" id="hex" runat="server" /><br />
            Base64String:<br />
            <span id="base64" runat="server"></span>
            <br />
            Base64UrlEncode:<br />
            <span id="base64Encode" runat="server"></span><br />
            <asp:Button ID="Encode" runat="server" Text="Encode" OnClick="Encode_Click" />
        </div>
    </form>
</body>
</html>
