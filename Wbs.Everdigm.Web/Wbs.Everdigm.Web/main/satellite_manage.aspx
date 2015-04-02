<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="satellite_manage.aspx.cs" Inherits="Wbs.Everdigm.Web.main.satellite_manage" %>

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
                    <h3>Terminal: Satellite Model</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Add" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <asp:Button ID="btQuery" CssClass="hidding" runat="server" Text="Query" OnClick="btQuery_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr>
                                <td class="td_right" style="width: 10% !important;">Card No.:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtNumber" />
                                    <input type="hidden" runat="server" id="hidID" />
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" /></th>
                                    <th style="width: 40px; text-align: center;">#</th>
                                    <th style="width: 100px;">Card Number</th>
                                    <th style="width: 100px;">Registered</th>
                                    <th style="width: 80px;">Bound?</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td></td>
                                    <td style="text-align: center;">1</td>
                                    <td>306312</td>
                                    <td>2015/03.31</td>
                                    <td>false</td>
                                    <td></td>
                                </tr>
                            </tbody>
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
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
</body>
</html>
