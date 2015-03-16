<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_authority.aspx.cs" Inherits="Wbs.Everdigm.Web.main.role_authority" %>

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
        <div id="main-content">
            <div class="content-box">
                <div class="content-box-header">
                    <h3>System: Role - Permission manage</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="90%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save Permissions" CssClass="button" OnClientClick="return checkButton();" OnClick="btSave_Click" />
                                    <input type="button" class="button" value="Back" id="btReturn" />
                                    <input type="hidden" id="hidMethod" runat="server" />
                                    <input type="hidden" id="hidID" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table width="90%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th id="thTitle" runat="server">System Menus</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td>
                                        <div id="divRoleAuthority" style="height: 350px; overflow: auto; margin-top: -5px;">
                                            <asp:TreeView ID="tvMenus" runat="server" ShowLines="True" Font-Size="14px" LineImagesFolder="../tree_images" ExpandDepth="6">
                                                <HoverNodeStyle BackColor="#66CCFF" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px" />
                                                <SelectedNodeStyle BackColor="#66CCFF" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px" />
                                            </asp:TreeView>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/role.js"></script>
</body>
</html>
