<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer_new.aspx.cs" Inherits="Wbs.Everdigm.Web.main.customer_new" %>

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
                    <h3>System: Customers - Add/Edit</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <input type="button" class="button" id="btReturn" value="Back" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr class="table_header">
                                <td colspan="4">Customer's informations:</td>
                            </tr>
                            <tr>
                                <td class="td_right">Name:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" maxlength="20" class="text-input important-input" id="txtName" />
                                    <input type="hidden" runat="server" id="hidID" />
                                </td>
                                <td class="td_right">Customer code:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" maxlength="20" class="text-input important-input" id="txtCode" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Phone:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" maxlength="20" class="text-input" id="txtPhone" />
                                </td>
                                <td class="td_right">ID card:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" maxlength="20" class="text-input" id="txtIdCard" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Address:</td>
                                <td class="td_left" colspan="3">
                                    <input type="text" runat="server" maxlength="50" class="text-input" id="txtAddress" /></td>
                            </tr>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/customers.js"></script>
</body>
</html>
