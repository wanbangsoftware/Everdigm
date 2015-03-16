<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_delivery.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_delivery" %>

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
                <div class="content-box-header">
                    <h3>Equipment: Delivery</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:DropDownList CssClass="text-input" ID="ddlType" runat="server"></asp:DropDownList>
                                    <select class="text-input" id="selModel">
                                        <option value="">Model:</option>
                                    </select>
                                    Number:
                                    <input type="text" runat="server" style="text-transform: uppercase;" maxlength="20" class="text-input" id="txtEquipment" />
                                    <input type="hidden" runat="server" id="hidEquipmentId" />
                                    <input type="button" class="button" id="queryEquipment" value="Query" />
                                    <span style="margin-left: 3px; margin-right: 3px;">|</span>
                                    <asp:Button ID="btSave" runat="server" Text="Confirm deliver" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr class="table_header"><td colspan="4">Equipment informations:</td></tr>
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
                                <td class="td_right td-temp">Situation:</td>
                                <td class="td_left" colspan="3">-</td>
                            </tr>
                            <tr id="_c_" style="display:none;">
                                <td class="td_right td-temp">Customer code:<input type="hidden" id="hidCustomerId" runat="server" /></td>
                                <td class="td_left">
                                    <input type="text" class="text-input important-input" id="txtCustomerCode" />
                                </td>
                                <td class="td_right">Contract:</td>
                                <td class="td_left">
                                    <input type="text" class="text-input important-input" id="txtContract" />
                                </td>
                            </tr>
                            <tr id="_c_1_" style="display:none;">
                                <td class="td_right td-temp">Situation:</td>
                                <td class="td_left">
                                    <asp:DropDownList ID="ddlSituation" runat="server" CssClass="text-input important-input"></asp:DropDownList>
                                </td>
                                <td class="td_right"></td>
                                <td class="td_left"></td>
                            </tr>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1. Find equipment informations first.<br />
                            2. If the situation is <span style="color: blue;">storage in inventory</span>, 
                                the button <span class="button">Deliver</span> will set to enable style.<br />
                            3. Input customer code, contract no., and click button to finish the deliver operation.
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
    <script type="text/javascript" src="../scripts/main/equipment.delivery.js"></script>
</body>
</html>
