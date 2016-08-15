<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminals.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminals" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Select Terminal:</title>
    <base target="_self" />
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
            <asp:Button ID="btQuery" CssClass="hidding" runat="server" Text="Query" OnClick="btQuery_Click" />
            <div class="panel-heading">
                <span class="input-group-btn">
                    <button class="btn btn-primary" type="button"><span class="glyphicon glyphicon-floppy-open"></span><span>Select & Bind</span></button>
                </span>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content">
                        <!--Equipment list-->
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="in-tab-title-b bg-primary" style="width: 30px;">#</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 50px; text-align: center;">ID</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 100px; text-align: left;">Number</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 80px; text-align: left;">Sim card</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 150px; text-align: left;">Satellite</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 80px; text-align: left;">Type</th>
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
                                    <td class="in-tab-txt-b">
                                        <input type="radio" name="satId" id="cb_1" /></td>
                                    <td class="in-tab-txt-b">1</td>
                                    <td class="in-tab-txt-b" style="text-align: left;">2015050001</td>
                                    <td class="in-tab-txt-b">89007423</td>
                                    <td class="in-tab-txt-b">300234062919250</td>
                                    <td class="in-tab-txt-b">Loader</td>
                                    <td class="in-tab-txt-b"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
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

            $("[id^=\"cb_\"]").prop("disabled", true);

            $("#tbodyBody").on("click", "tr", function () {
                $(this).children("td:eq(0)").children("input:eq(0)").prop("checked", true);
            });

            $(".btn-primary").click(function () {
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
