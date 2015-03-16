<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_history.aspx.cs" Inherits="Wbs.Everdigm.Web.main.user_history" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <link href="../css/right.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" value="" id="cookieName" runat="server" />
        <div id="main-content">
            <div class="content-box">
                <div class="content-box-header">
                    <h3>System: Accounts' History</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header1" style="width: 60px; text-align: right;">Name:</td>
                                <td class="table_header" style="width: 80px; border-left: 0px; border-right: 0px;">
                                    <input type="text" id="txtName" class="text-input little-input" runat="server" />
                                </td>
                                <td class="table_header1" style="border-left: 0px; width: 50px; text-align: right;">Time:</td>
                                <td class="table_header" style="border-left: 0px; border-right: 0px; width: 200px;">
                                    <input type="hidden" runat="server" id="hidPageIndex" />
                                    <div class="input-daterange input-group" id="datepicker" style="float: left">
                                        <input type="text" class="input-md form-control text-input click-input" runat="server" id="start" name="start" />
                                        <span class="input-group-addon">to </span>
                                        <input type="text" class="input-md form-control text-input click-input" runat="server" id="end" name="end" />
                                    </div>
                                </td>
                                <td class="table_header1" style="border-left: 0px;">
                                    <asp:Button ID="btQuery" runat="server" Text="Query" CssClass="button" OnClick="btQuery_Click" />
                                </td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 60px; text-align: center;">ID</th>
                                    <th style="width: 150px;">Time</th>
                                    <th style="width: 150px;">User</th>
                                    <th style="width: 250px;">Action</th>
                                    <th style="width: 120px;">IP</th>
                                    <th>Summary</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <div class="pagging" style="float: right; margin: 0px;" id="divPagging" runat="server">
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
                            Display recently 5 days history default.
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/javascript.date.pattern.js"></script>
    <script type="text/javascript" src="../bootstrap3/bootstrap-datepicker-1.3.0/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/account.js"></script>
    <script type="text/javascript" src="../scripts/main/account_history.js"></script>
</body>
</html>
