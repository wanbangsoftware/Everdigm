﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_type.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_type" %>

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
                    <h3>System: Equipment - Types</h3>
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
                                <td class="td_right" rowspan="2">Clipart:</td>
                                <td class="td_left" rowspan="2">
                                    <img alt="equipment sketch" style="width: 41px; cursor: pointer;" title="Click to select the clipart" class="img-rounded" id="imgImage" runat="server" src="~/images/equipments/icon_hex.png" />
                                    <input type="hidden" id="hidImage" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Code:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtCode" maxlength="5" style="text-transform: uppercase;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Normal Vehicle:</td>
                                <td class="td_left">
                                    <label style="margin-top: 10px;">
                                        <asp:CheckBox ID="cbNormalVehicle" runat="server" />&nbsp;Identification of normal vehicle
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" />
                                    </th>
                                    <th style="width: 40px; text-align: center;">ID</th>
                                    <th style="width: 300px;">Name</th>
                                    <th style="width: 180px;">Code</th>
                                    <th style="width: 100px;">Vehicle</th>
                                    <th style="width: 100px;">Clipart</th>
                                    <th>-</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="7">
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
                        </p>
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
    <script type="text/javascript">
        $("#imgImage").click(function () {
            showDialogWindows("../mobile/images.aspx", function (ret) {
                $("#imgImage").prop("src", ret.ids);
                $("#hidImage").val(ret.ids);
            });
        });
    </script>
</body>
</html>
