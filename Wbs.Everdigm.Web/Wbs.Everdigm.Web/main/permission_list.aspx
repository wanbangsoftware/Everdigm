﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="permission_list.aspx.cs" Inherits="Wbs.Everdigm.Web.main.permission_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <link href="../css/right.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" value="" id="cookieName" runat="server" />
        <div id="main-content">
            <div class="content-box">
                <div class="content-box-header">
                    <h3>System: Permissions</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    Name:
                                    <input type="text" class="text-input little-input" id="txtName" runat="server" />
                                    Parent:
                                    <input type="text" runat="server" title="click to choose" class="text-input little-big-imput click-input" id="txtParent" readonly="readonly" />
                                    <input type="hidden" runat="server" id="hidParent" />
                                    <input type="hidden" id="hidPageIndex" runat="server" />
                                    <asp:Button ID="btQuery" runat="server" Text="Query" CssClass="button" OnClick="btQuery_Click" />
                                    |
                                    <input type="button" value="Add" class="button" id="btAdd" />
                                    <input type="hidden" id="hidID" runat="server" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="Delete" CssClass="button" OnClick="bt_Delete_Click" />
                                    <asp:Button ID="bt_ToUpper" runat="server" Text="Up" ToolTip="change display order: &uarr;" CssClass="button" OnClick="bt_ToUpper_Click" />
                                    <asp:Button ID="bt_ToDown" runat="server" Text="Down" ToolTip="change display order: &darr;" CssClass="button" OnClick="bt_ToDown_Click" />
                                </td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" /></th>
                                    <th style="width: 40px; text-align: center;">ID</th>
                                    <th style="width: 150px;">Name</th>
                                    <th style="width: 40px;">Image</th>
                                    <th style="width: 60px;">Default</th>
                                    <th>Parent</th>
                                    <th>URL</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="8">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/permission.js"></script>
</body>
</html>
