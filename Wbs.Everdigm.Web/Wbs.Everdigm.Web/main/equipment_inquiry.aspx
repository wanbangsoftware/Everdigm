﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_inquiry.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_inquiry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/font-awesome-animation.min.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../mobile/css/style.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" value="" id="cookieName" runat="server" />
            <input type="hidden" runat="server" value="0" id="hidPageIndex" />
            <input type="hidden" runat="server" id="hidTotalPages" value="0" />
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>Equipment: Inquiry</strong>
                <span id="toExcel" class="label label-primary" style="cursor: pointer;"><i class="fa fa-file-excel-o"></i> Export to Excel</span>
            </div>
            <div class="panel-body">
                <!--默认查询新品库存列表-->
                <input type="hidden" id="hidQueryType" runat="server" value="N" />
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active dropdown" id="ddTypes">
                        <a id="dropTypes" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Type:</span><span class="caret"></span>
                        </a>
                        <ul id="menuTypes" class="dropdown-menu" role="menu" aria-labelledby="dropTypes">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="selectedTypes" runat="server" value="0" />
                    </li>
                    <li role="presentation" class="dropdown" id="ddModels">
                        <a id="dropModels" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Model:</span><span class="caret"></span>
                        </a>
                        <ul id="menuModels" class="dropdown-menu" role="menu" aria-labelledby="dropModels">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Choose a Type First</a></li>
                        </ul>
                        <input type="hidden" id="selectedModels" runat="server" value="0" />
                    </li>
                    <li role="presentation" class="dropdown" id="ddCustomers">
                        <a id="dropCustomers" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Customer:</span><span class="caret"></span>
                        </a>
                        <ul id="menuCustomers" class="dropdown-menu" role="menu" aria-labelledby="dropCustomers">
                            <li role="presentation"><a role="menuitem" tabindex="1" href="#">Ignore(default)</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="2" href="#">Bound</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="3" href="#">None</a></li>
                        </ul>
                        <input type="hidden" id="hiddenCustomer" runat="server" value="1" />
                    </li>
                    <!--<li role="presentation" class="dropdown" id="ddWarehouses">
                        <a id="dropWarehouses" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                        </a>
                        <ul id="menuWarehouses" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouses">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="hidQueryWarehouse" runat="server" value="0" />
                    </li>-->
                    <li role="presentation" class="dropdown" id="ddTerminals">
                        <a id="dropTerminals" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Terminal:</span><span class="caret"></span>
                        </a>
                        <ul id="menuTerminals" class="dropdown-menu" role="menu" aria-labelledby="dropTerminals">
                            <li role="presentation"><a role="menuitem" tabindex="1" href="#">Ignore(default)</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="2" href="#">Installed</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="3" href="#">None</a></li>
                        </ul>
                        <input type="hidden" id="hiddenTerminal" runat="server" value="2" />
                    </li>
                    <li role="presentation" class="tablist-item-input">
                        <div class="input-group">
                            <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="number" />
                            <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" Width="0" Height="0" OnClick="btQuery_Click" />
                            <span class="input-group-btn">
                                <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                            </span>
                        </div>
                        <!-- /input-group -->
                    </li>
                </ul>

                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content">
                        <!--Equipment list-->
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="bg-primary"></th>
                                    <th colspan="7" class="in-tab-title-rb bg-primary">Equipment Information</th>
                                    <!--<th colspan="2" class="in-tab-title-rb bg-primary">RENT/SOLD SMH</th>-->
                                    <th colspan="2" class="in-tab-title-rb bg-primary">Customer</th>
                                    <th colspan="5" class="in-tab-title-rb bg-primary">Terminal Information</th>
                                    <th class="in-tab-title-b bg-primary">Storage</th>
                                </tr>
                                <tr>
                                    <th class="in-tab-title-b bg-warning" style="width: 20px;">#</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 20px;">Type</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 20px;">Model</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important; width: 80px;">Functional</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: right !important; width: 80px;">SMH</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 20px;">ENG</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important;">Location</th>
                                    <th class="in-tab-title-rb bg-warning" style="width: 20px;">Status</th>
                                    <!--<th class="in-tab-title-b bg-warning textoverflow">Days</th>
                                    <th class="in-tab-title-rb bg-warning textoverflow">Hr/Day</th>-->
                                    <th class="in-tab-title-b bg-warning">Number</th>
                                    <th class="in-tab-title-rb bg-warning">Name</th>
                                    <!--<th class="in-tab-title-b bg-warning">Signal</th>-->
                                    <th class="in-tab-title-b bg-warning" style="width: 30px;">Link</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 20px;"></th>
                                    <th class="in-tab-title-b bg-warning" style="width: 90px;">Last received</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 70px;">Terminal</th>
                                    <th class="in-tab-title-rb bg-warning" style="width: 20px;">SAT</th>
                                    <!--<th class="in-tab-title-b bg-warning textoverflow">In Date</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">In Type</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">Out Date</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">Out Type</th>-->
                                    <th class="in-tab-title-b bg-warning">Warehouse</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="16">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td class="in-tab-txt-b">1</td>
                                    <td class="in-tab-txt-b">EX.</td>
                                    <td class="in-tab-txt-b textoverflow" style="text-align: left !important;">DL215-9-21442</td>
                                    <td class="in-tab-txt-b">Loader</td>
                                    <td class="in-tab-txt-b">2430</td>
                                    <td class="in-tab-txt-b">OFF</td>
                                    <td class="in-tab-txt-b textoverflow">山东省烟台市</td>
                                    <td class="in-tab-txt-b">W</td>
                                    <!--<td class="in-tab-txt-b">30</td>
                                    <td class="in-tab-txt-rb">4 hr 30 min</td>-->
                                    <td class="in-tab-txt-b">23553523</td>
                                    <td class="in-tab-txt-b">23553523</td>
                                    <td class="in-tab-txt-rb">23553523</td>
                                    <td class="in-tab-txt-b">
                                        <img src="../images/img_connect_gprs.png" /></td>
                                    <td class="in-tab-txt-b textoverflow">2014-09-22</td>
                                    <td class="in-tab-txt-rb textoverflow">20140921221</td>
                                    <td class="in-tab-txt-rb textoverflow">Warehouse1</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="warningLoading" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-primary">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="warningLabel">Loading...</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12" id="loadingContent">
                                <img alt="" src="../images/loading.gif" /><span style="margin-left: 10px;" id="loadingContentText">Loading data, please wait...</span>
                            </div>
                            <div class="col-lg-12" id="warningContent">
                                <span style="margin-left: 10px;" id="warningContentText"></span>
                            </div>
                        </div>
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
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script src="../scripts/main/export.to.excel.js"></script>
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script src="../scripts/main/equipment.inquiry.js"></script>
</body>
</html>
