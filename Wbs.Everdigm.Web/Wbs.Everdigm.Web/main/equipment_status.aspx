<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_status.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_status" %>

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
            <input type="hidden" value="" id="cookieName" runat="server" />
            <input type="hidden" runat="server" id="hidPageIndex" />
            <div class="content-box">
                <div class="content-box-header">
                    <h3>System: Equipment - Situation</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <asp:Button ID="btQuery" CssClass="hidding" runat="server" Text="Query" OnClick="btQuery_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr>
                                <td class="td_right" style="width: 10% !important;">Name:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtName" />
                                    <input type="hidden" runat="server" id="hidID" />
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="td_right">Code:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtCode" /></td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="td_right">Is Inventory:</td>
                                <td class="td_left">
                                    <label><asp:CheckBox ID="cbIsInventory" runat="server" />&nbsp;Set situation as inventory</label>
                                </td>
                                <td class="td_right">Is Delivered:</td>
                                <td class="td_left">
                                    <label><asp:CheckBox ID="cbIsOutstorage" runat="server" />&nbsp;Set situation as delivered</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Is Overhaul:</td>
                                <td class="td_left">
                                    <label><asp:CheckBox ID="cbIsOverhaul" runat="server" />&nbsp;Set situation as overhaul</label>
                                </td>
                                <td class="td_right">Is Waiting:</td>
                                <td class="td_left">
                                    <label><asp:CheckBox ID="cbIsWaiting" runat="server" />&nbsp;Set situation as waiting storage in</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Is Rental:</td>
                                <td class="td_left">
                                    <label><asp:CheckBox ID="cbIsRental" runat="server" />&nbsp;Set situation as rental</label>
                                </td>
                                <td class="td_right"></td>
                                <td class="td_left"></td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" /></th>
                                    <th style="width: 40px; text-align: center;">ID</th>
                                    <th style="width: 200px;">Name</th>
                                    <th style="width: 180px;">Code</th>
                                    <th>Is it Inventory?</th>
                                    <th>Is it Out Storage?</th>
                                    <th>Is it Overhaul?</th>
                                    <th>Is it Waiting?</th>
                                    <th>Is it Rental?</th>
                                    <th>-</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="10">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                            </tbody>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            <span style="color:#ff0000;">DO NOT CHANGE those CODE if you haven't a full grasp</span><br />
                            <span style="color:#ff0000;">Each situation can only exist one in the system.</span>
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
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
</body>
</html>
