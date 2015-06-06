<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_list.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_list" %>

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
            <div class="content-box">
                <div class="content-box-header">
                    <h3>Terminal: Terminal List</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">Number:
                                    <input type="text" id="txtNumber" class="text-input little-input" runat="server" />
                                    Sim Card:
                                    <input type="text" id="txtSimcard" class="text-input little-input" runat="server" />
                                    <input type="hidden" runat="server" id="hidPageIndex" />
                                    <asp:Button ID="btQuery" runat="server" Text="Query" CssClass="button" OnClick="btQuery_Click" />
                                    |
                                    <input type="button" id="btAdd" class="button" value="Add" />
                                    <input type="hidden" runat="server" id="hidID" />
                                    <input type="hidden" runat="server" id="hidBoundSatellite" />
                                    <asp:Button ID="btBoundSatellite" runat="server" CssClass="hidding" OnClick="btBoundSatellite_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="Delete" CssClass="button" OnClick="btDelete_Click" />
                                    <asp:Button ID="btUnbindEquipment" runat="server" CssClass="hidding" OnClick="btUnbindEquipment_Click" />
                                </td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" /></th>
                                    <th style="width: 40px; text-align: center;">ID</th>
                                    <th style="width: 100px;">Number</th>
                                    <th style="width: 100px;">Sim card</th>
                                    <th style="width: 180px;">Satellite</th>
                                    <th style="width: 70px;">Firmware</th>
                                    <th style="width: 30px; text-align: center;">Rev</th>
                                    <th style="width: 100px; text-align: center;">Type</th>
                                    <th style="width: 70px;">Register</th>
                                    <th style="width: 80px; text-align: center;">Has bound?</th>
                                    <th style="width: 140px;">Equipment</th>
                                    <th style="width: 70px;">Link</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="13">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td style="text-align: center;">
                                        <input type="checkbox" id="cb_1" /></td>
                                    <td style="text-align: center;">1</td>
                                    <td>2015010011</td>
                                    <td>13999999999</td>
                                    <td>13999999999</td>
                                    <td>X10C104</td>
                                    <td style="text-align: center;">12</td>
                                    <td style="text-align: center;">DX</td>
                                    <td>2015-01-08</td>
                                    <td style="text-align: center;">-</td>
                                    <td>DX225LCA-00012</td>
                                    <td>OFF</td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1. Click Equipment number(if exist) to unbind terminal.<br />
                            2. Click Add to bind satellite device.
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
    <script type="text/javascript" src="../scripts/main/terminals.js"></script>
</body>
</html>
