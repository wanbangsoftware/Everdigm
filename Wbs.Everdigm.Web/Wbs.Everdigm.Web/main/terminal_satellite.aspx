<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_satellite.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_satellite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Select Satellite Number</title>
    <base target="_self" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="../css/right.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
    <style type="text/css">
        .button {
            font-family: Verdana, Arial, sans-serif "宋体";
            display: inline-block;
            background: #0053a2 url('/images/bg-button-green.gif') top left repeat-x !important;
            border: 1px solid #0053a2 !important;
            padding: 4px 7px 4px 7px !important;
            color: #fff !important;
            font-size: 12px !important;
            cursor: pointer;
        }

            .button:hover {
                text-decoration: underline;
            }

            .button:active {
                padding: 5px 7px 3px 7px !important;
            }

        .table_header {
            height: 30px;
            border: 1px solid #dddada;
            background: #f0f0f0;
            padding: 3px;
            vertical-align: middle;
        }
        .hidden {
            display:none;
        }
        #tbodyBody tr {
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content">
            <input type="hidden" value="" id="cookieName" runat="server" />
            <input type="hidden" runat="server" id="hidPageIndex" />
            <input type="hidden" runat="server" id="hidID" />
            <asp:Button ID="btQuery" runat="server" Text="Query" CssClass="hidden" OnClick="btQuery_Click" />
            <div class="content-box" style="width: 95%;">
                <div class="content-box-header" style="padding-left: 8px; padding-right: 8px;">
                    <input id="btConfirm" style="margin-top: 6px;" type="button" value="Select & Bound" class="button" />
                    <input id="btClose" type="button" style="margin-top: 6px; float: right;" value="Close" class="button" />
                    
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table id="tbTable" cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 20px; text-align: center;"></th>
                                    <th style="width: 30px; text-align: center;">#</th>
                                    <th style="width: 100px;">Number</th>
                                    <th style="width: 50px">Bound</th>
                                    <th style="width: 150px">Register date</th>
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
                                    <td style="text-align: center;">
                                        <input type="radio" name="satId" id="cb_1" /></td>
                                    <td style="text-align: center;">111</td>
                                    <td>306314</td>
                                    <td>false</td>
                                    <td>2015-03-20</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <input type="radio" name="satId" id="cb_2" /></td>
                                    <td style="text-align: center;">121</td>
                                    <td>306315</td>
                                    <td>false</td>
                                    <td>2015-03-20</td>
                                    <td></td>
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
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id^=\"cb_\"]").prop("disabled", true);
            $("#btClose").click(function () { close(); });
            $("#tbodyBody").on("click", "tr", function () {
                $(this).children("td:eq(0)").children("input:eq(0)").prop("checked", true);
            });
            $("#btConfirm").click(function () {
                if ($("input:radio:checked").length > 0) {
                    var id = $("input:radio:checked").prop("id").replace("cb_", "");
                    var msg = $("#hidID").val() + "," + id;
                    windowPopupCloseEvent(msg);
                    close();
                }
            });
        });
    </script>
</body>
</html>
