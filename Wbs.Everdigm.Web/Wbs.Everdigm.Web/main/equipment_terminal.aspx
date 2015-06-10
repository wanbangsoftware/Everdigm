<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_terminal.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_terminal" %>

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
    <link href="../jquery-ui-1.11.2.custom/jquery-ui.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content">
            <div class="content-box">
                <input type="hidden" value="" id="cookieName" runat="server" />
                <input type="hidden" runat="server" id="hidPageIndex" />
                <div class="content-box-header">
                    <h3>Terminal: Bind Terminal and Equipment</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">Equipment:
                                    <asp:DropDownList CssClass="text-input" ID="ddlType" runat="server"></asp:DropDownList>
                                    <select class="text-input" id="selModel">
                                        <option value="">Model:</option>
                                    </select>
                                    <input type="text" runat="server" placeholder="number" style="text-transform: uppercase;" maxlength="15" class="text-input" id="txtEquipment" />
                                    <input type="hidden" runat="server" id="hidEquipmentId" />
                                    <asp:Button ID="btQuery" Text="Query" CssClass="button" runat="server" OnClick="btQuery_Click" />
                                    <asp:Button ID="btSave" Enabled="false" runat="server" Text="Save" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <input type="button" class="button" id="btReturn" value="Back to terminal list" />
                                    <input type="hidden" runat="server" id="hidTerminalId" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" id="terminalinfo" runat="server" cellspacing="0" style="width: 100%; border: 1px solid #ccc; margin-top: 2px;">
                            <tr class="table_header">
                                <td colspan="6">
                                    Selected terminal(<span id="terType" runat="server"></span>):
                                    <input type="hidden" id="hiddenType" runat="server" />
                                </td>
                            </tr>
                            <tr id="_t_1_">
                                <td class="td_right td-temp">Terminal:</td>
                                <td class="td_left1">-</td>
                                <td class="td_right">Sim No.</td>
                                <td class="td_left1">-</td>
                                <td class="td_right td-temp">Satellite:</td>
                                <td class="td_left1">-</td>
                            </tr>
                            <tr id="_t_2_">
                                <td class="td_right">Firmware:</td>
                                <td class="td_left1">-</td>
                                <td class="td_right td-temp">Online Time:</td>
                                <td class="td_left1">-</td>
                                <td class="td_right">Link:</td>
                                <td class="td_left1">-</td>
                            </tr>
                        </table>
                        <table style="width: 100%; border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="text-align: center; width: 20px;"></th>
                                    <th style="text-align: center; width: 30px;">#</th>
                                    <th style="width: 50px;">Type</th>
                                    <th style="width: 120px;">Model</th>
                                    <th style="width: 80px; text-align: right;">SMH</th>
                                    <th style="width: 80px; text-align: center;">Status</th>
                                    <th style="width: 80px;">Terminal</th>
                                    <th style="width: 80px;">Warehouse</th>
                                    <th>Location</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td style="text-align: center;">
                                        <input type="radio" name="satId" id="cb_1" />
                                    </td>
                                    <td style="text-align: center;">1</td>
                                    <td>HEX</td>
                                    <td>DX500LCA-00000</td>
                                    <td style="text-align: right;">1,230.20</td>
                                    <td style="text-align: center;">SOLD</td>
                                    <td>2013030001</td>
                                    <td>UB1(old)</td>
                                    <td>Asian Highway 3, Songi</td>
                                    <td></td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="9">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1. Terminal: Find some terminal not bind on equipment.<br />
                            2. Equipment: Find some equipment not bind terminal.
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.json-2.4.js"></script>
    <script type="text/javascript" src="../js/jlinq-3.0.1.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../jquery-ui-1.11.2.custom/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.terminal.base.js"></script>
    <script type="text/javascript" src="../scripts/main/terminal.bind.equipment.js"></script>
</body>
</html>
