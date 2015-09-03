<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_flow.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_flow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel DefaultButton="btQuery" runat="server">
            <div class="panel panel-default">
                <input type="hidden" value="" id="cookieName" runat="server" />
                <input type="hidden" runat="server" value="0" id="hidPageIndex" />
                <input type="hidden" runat="server" id="hidTotalPages" value="0" />
                <!-- Default panel contents -->
                <div class="panel-heading">
                    <strong>Equipment: Flow statistics</strong> <span id="spanMonthly" runat="server">for 2015/10</span>
                </div>
                <div class="panel-body" style="padding-bottom: 0px !important;">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist" id="queryBar">
                        <li role="presentation" class="active dropdown" id="ddYears">
                            <a id="dropYears" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Year:</span><span class="caret"></span>
                            </a>
                            <ul id="menuYears" class="dropdown-menu" role="menu" aria-labelledby="dropYears" runat="server">
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                            </ul>
                            <input type="hidden" id="selectedYears" runat="server" value="0" />
                        </li>
                        <li role="presentation" class="dropdown" id="ddMonths">
                            <a id="dropMonths" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Month:</span><span class="caret"></span>
                            </a>
                            <ul id="menuMonths" class="dropdown-menu" role="menu" aria-labelledby="dropMonths" runat="server">
                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">-</a></li>
                            </ul>
                            <input type="hidden" id="selectedMonths" runat="server" value="0" />
                        </li>
                        <li role="presentation" class="tablist-item-input">
                            <div class="input-group">
                                <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="sim/satellite NO." maxlength="15">
                                <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" OnClick="btQuery_Click" />
                                <span class="input-group-btn">
                                    <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                                </span>
                            </div>
                            <!-- /input-group -->
                        </li>
                        <li role="presentation" class="tablist-item-input">
                            <a href="#" class="dropdown-toggle" style="width: 300px;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                R: Receive, S: Send, PKG: Package
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div class="tab-content">
                            <!--Equipment list-->
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th class="in-tab-title-b bg-primary" style="width: 50px;"></th>
                                        <th class="in-tab-title-rb bg-primary" colspan="3">Basic Information</th>
                                        <th class="in-tab-title-rb bg-primary" colspan="4">GSM flow statistics</th>
                                        <th class="in-tab-title-b bg-primary" colspan="5">Satellite flow statistics</th>
                                        <th class="in-tab-title-b bg-primary"></th>
                                    </tr>
                                    <tr>
                                        <th class="in-tab-title-b bg-warning">#</th>
                                        <th class="in-tab-title-b bg-warning" style="width: 120px; text-align: left;">Equipment NO.</th>
                                        <th class="in-tab-title-b bg-warning" style="width: 100px; text-align: left;">Terminal NO.</th>
                                        <th class="in-tab-title-rb bg-warning" style="width: 100px; text-align: left;">Sim Card NO.</th>
                                        <th class="in-tab-title-b bg-warning textoverflow" style="width: 80px;">TCP/IP(R)</th>
                                        <th class="in-tab-title-rb bg-warning textoverflow" style="width: 80px;">TCP/IP(S)</th>
                                        <th class="in-tab-title-b bg-warning textoverflow" style="width: 80px;">SMS(R)</th>
                                        <th class="in-tab-title-rb bg-warning textoverflow" style="width: 80px;">SMS(S)</th>
                                        <th class="in-tab-title-b bg-warning textoverflow" style="width: 80px;">Satellite NO.</th>
                                        <th class="in-tab-title-b bg-warning textoverflow" style="width: 80px;">PKG.(R)</th>
                                        <th class="in-tab-title-rb bg-warning textoverflow" style="width: 80px;">Length(R)</th>
                                        <th class="in-tab-title-b bg-warning textoverflow" style="width: 80px;">PKG.(S)</th>
                                        <th class="in-tab-title-b bg-warning textoverflow" style="width: 80px;">Length(S)</th>
                                        <th class="in-tab-title-b bg-warning"></th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <td colspan="14">
                                            <div class="pagging" id="divPagging" runat="server">
                                            </div>
                                            <div class="clear"></div>
                                        </td>
                                    </tr>
                                </tfoot>
                                <tbody id="tbodyBody" runat="server">
                                    <tr>
                                        <td class="in-tab-txt-b">1</td>
                                        <td class="in-tab-txt-b" style="text-align: left;">DX500LCA-10030</td>
                                        <td class="in-tab-txt-b">2015090201</td>
                                        <td class="in-tab-txt-rb">13999999999</td>
                                        <td class="in-tab-txt-b">56.04KB</td>
                                        <td class="in-tab-txt-rb">46B</td>
                                        <td class="in-tab-txt-b">135</td>
                                        <td class="in-tab-txt-rb">23</td>
                                        <td class="in-tab-txt-b textoverflow">111111111111111</td>
                                        <td class="in-tab-txt-b textoverflow">44</td>
                                        <td class="in-tab-txt-rb">256.01KB</td>
                                        <td class="in-tab-txt-b">2</td>
                                        <td class="in-tab-txt-b">102B</td>
                                        <td class="in-tab-txt-b"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.json-2.4.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/jlinq-3.0.1.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-slider.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#query").click(function () { $("#btQuery").click(); });
        });
    </script>
</body>
</html>
