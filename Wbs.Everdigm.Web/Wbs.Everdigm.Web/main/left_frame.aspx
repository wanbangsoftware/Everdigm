<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="left_frame.aspx.cs" Inherits="Wbs.Everdigm.Web.main.left_frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/body.css" rel="stylesheet" />
    <link href="../css/left_frame.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-left: 10px;">
            <label style="font-size:14px;">Welcome <a id="name" runat="server">Admin</a></label>
            <asp:TreeView ID="tvSystemMenu" runat="server" ShowLines="true" Font-Size="13px" LineImagesFolder="../tree_images" NodeStyle-CssClass="node" Font-Names="Arial" ExpandDepth="1">
                <HoverNodeStyle BackColor="#CCCCCC" CssClass="node" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px" />

                <NodeStyle CssClass="node" />
                <SelectedNodeStyle BackColor="#66CCFF" CssClass="node" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px" />
            </asp:TreeView>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
</body>
</html>
