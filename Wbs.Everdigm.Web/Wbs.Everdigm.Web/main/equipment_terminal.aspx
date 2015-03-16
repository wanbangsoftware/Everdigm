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
    <style type="text/css">
        .td-temp {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content">
            <div class="content-box">
                <div class="content-box-header">
                    <h3>Terminal: Bind Equipment</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" Enabled="false" runat="server" Text="Save" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <input type="button" class="button" id="btReturn" value="Back to terminal list" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr class="table_header"><td colspan="4">Terminal informations:</td></tr>
                            <tr>
                                <td class="td_right">Number:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" style="text-transform: uppercase;" maxlength="15" class="text-input" id="txtTerminal" />
                                    <input type="hidden" runat="server" id="hidTerminalId" />
                                    <input type="button" class="button" id="queryTerminal" value="Query" />
                                </td>
                                <td class="td_right"></td>
                                <td class="td_left"></td>
                            </tr>
                            <tr><td colspan="4" style="background: #f0f0f0; height: 1px;"></td></tr>
                            <tr id="_t_1_">
                                <td class="td_right td-temp">Terminal:</td>
                                <td class="td_left">-</td>
                                <td class="td_right">Sim No.</td>
                                <td class="td_left">-</td>
                            </tr>
                            <tr id="_t_2_">
                                <td class="td_right td-temp">Satellite:</td>
                                <td class="td_left">-</td>
                                <td class="td_right">Firmware:</td>
                                <td class="td_left">-</td>
                            </tr>
                            <tr class="table_header"><td colspan="4">Equipment informations:</td></tr>
                            <tr>
                                <td class="td_right">Type:</td>
                                <td class="td_left" style="height: 30px;">
                                    <asp:DropDownList CssClass="text-input" ID="ddlType" runat="server"></asp:DropDownList>
                                </td>
                                <td class="td_right">Model:</td>
                                <td class="td_left">
                                    <select class="text-input" id="selModel">
                                        <option value="">Model:</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Number:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" style="text-transform: uppercase;" maxlength="20" class="text-input" id="txtEquipment" />
                                    <input type="hidden" runat="server" id="hidEquipmentId" />
                                    <input type="button" class="button" id="queryEquipment" value="Query" />
                                </td>
                                <td class="td_right"></td>
                                <td class="td_left"></td>
                            </tr>
                            <tr><td colspan="4" style="background: #f0f0f0; height: 1px;"></td></tr>
                            <tr id="_e_1_">
                                <td class="td_right td-temp">Type:</td>
                                <td class="td_left">-</td>
                                <td class="td_right">Model:</td>
                                <td class="td_left">-</td>
                            </tr>
                            <tr id="_e_2_">
                                <td class="td_right td-temp">Number:</td>
                                <td class="td_left">-</td>
                                <td class="td_right">Warehouse:</td>
                                <td class="td_left">-</td>
                            </tr>
                            <tr id="_e_3_">
                                <td class="td_right td-temp">Terminal:</td>
                                <td class="td_left">-</td>
                                <td class="td_right"></td>
                                <td class="td_left"></td>
                            </tr>
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
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.terminal.base.js"></script>
    <script type="text/javascript" src="../scripts/main/terminal.bind.equipment.js"></script>
</body>
</html>
