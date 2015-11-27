<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_add.aspx.cs" Inherits="Wbs.Everdigm.Web.main.role_add" %>

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
                    <h3>System: Role - Add/Edit</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <input type="button" class="button" value="Back" id="btReturn" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr>
                                <td class="td_right" style="width: 10% !important;">Role name:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtName" />
                                    <input type="hidden" runat="server" id="hidID" />
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="td_right">Default:</td>
                                <td class="td_left">
                                    
                                    <label><asp:CheckBox ID="cbIsDefault" runat="server" />&nbsp;Set as default role</label>
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="td_right">Administrator:</td>
                                <td class="td_left">
                                    <label><asp:CheckBox ID="cbIsAdmin" runat="server" />&nbsp;Set as administrator</label>
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="td_right">Description:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtDescription" />
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1. Default role: Only one Default role in system. All new account will be set to this role.<br />
                            2. Administrator role: Only one Administrator role in system. the account(s) of this role can access all system pages.
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/role.js"></script>
</body>
</html>
