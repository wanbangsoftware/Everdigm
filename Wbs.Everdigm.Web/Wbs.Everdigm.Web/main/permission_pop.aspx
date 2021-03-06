﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="permission_pop.aspx.cs" Inherits="Wbs.Everdigm.Web.main.permission_pop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Choose the parent menu:</title>
    <base target="_self" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="../css/body.css" rel="stylesheet" />
    <link href="../css/left_frame.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-left: 10px;">
            <asp:TreeView ID="tvNavigations" runat="server" ShowLines="True" ExpandDepth="1" Font-Size="14px" LineImagesFolder="../tree_images">
                <HoverNodeStyle BackColor="#CCCCCC" CssClass="node" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px" />
                <SelectedNodeStyle BackColor="#66CCFF" CssClass="node" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px" />
                <NodeStyle CssClass="node" />
            </asp:TreeView>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/permission.js"></script>
</body>
</html>
