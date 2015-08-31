<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer_pop.aspx.cs" Inherits="Wbs.Everdigm.Web.main.customer_pop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <base target="_self" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <link href="../css/right.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
    <link href="../css/pagging.css" rel="stylesheet" />
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

        #tbodyBody tr {
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" value="" id="cookieName" runat="server" />
            <input type="hidden" runat="server" value="0" id="hidPageIndex" />
            <input type="hidden" runat="server" id="hidTotalPages" value="0" />
            <div class="panel-heading">
                <table width="100%" cellpadding="0" cellspacing="0" id="tableTable" runat="server">
                    <tr class="table_header">
                        <td style="width: 120px; padding-left: 5px;">
                            <input id="btSelect" type="button" value="Select" class="button" />
                            <input id="btClose" type="button" value="Close" class="button" />
                        </td>
                        <td style="width: 50px; text-align: right;">Name:</td>
                        <td style="width: 100px;">
                            <input type="text" runat="server" id="txtName" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btQuery" CssClass="button" Text="Query" OnClick="btQuery_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content">
                        <!--Equipment list-->
                        <table class="table table-hover" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th class="in-tab-title-b bg-primary" style="width: 20px; text-align: center;">#</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 20px; text-align: center;">ID</th>
                                    <th class="in-tab-title-b bg-primary" style="text-align: left;">Name</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 80px; text-align: left;">Code</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 80px; text-align: left;">Phone</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 150px; text-align: left;">Fax</th>
                                    <th class="in-tab-title-b bg-primary"></th>
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
                                <tr>
                                    <td class="in-tab-txt-b" style="text-align: center;">
                                        <input type="radio" name="satId" id="cb_1" />
                                    </td>
                                    <td class="in-tab-txt-b" style="text-align: center;">1</td>
                                    <td class="in-tab-txt-b" style="text-align: left;">Leekwok</td>
                                    <td class="in-tab-txt-b">89007423</td>
                                    <td class="in-tab-txt-b">89007423</td>
                                    <td class="in-tab-txt-b">89007777</td>
                                    <td class="in-tab-txt-b"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.json-2.4.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/jlinq-3.0.1.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-slider.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script type="text/javascript" src="../bootstrap3/bootstrap-datepicker-1.3.0/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../bootstrap3/models/js/bootstrap-dialog.min.js"></script>
    <script type="text/javascript" src="../bootstrap3/models/js/bootstrap-typeahead.js"></script>
    <script type="text/javascript" src="../js/javascript.date.pattern.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btClose").click(function () { close(); });
            $("[id^=\"cb_\"]").prop("disabled", true);

            $("#tbodyBody").on("click", "tr", function () {
                $(this).children("td:eq(0)").children("input:eq(0)").prop("checked", true);
            });

            $("#btSelect").click(function () {
                if ($("input:radio:checked").length > 0) {
                    var id = $("input:radio:checked").prop("id").replace("cb_", "");
                    var msg = "," + id;
                    windowPopupCloseEvent(msg);
                    close();
                } else { alert("You should choose a customer first.");}
            });
        });
    </script>
</body>
</html>
