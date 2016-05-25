<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_checkout.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../mobile/css/style.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
    <link href="../bootstrap3/css/bootstrap-select.min.css" rel="stylesheet" />
    <style type="text/css">
        .datepicker {
            z-index: 1200 !important;
        }

        body.modal-open .datepicker {
            z-index: 1200 !important;
        }

        body.modal-open .dropdown-menu {
            z-index: 1200 !important;
        }

        .modal {
            overflow: visible;
        }

        .modal-body {
            overflow-y: visible;
        }

        .custom-modal-header {
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" value="" id="cookieName" runat="server" />
            <input type="hidden" runat="server" value="0" id="hidPageIndex" />
            <input type="hidden" runat="server" id="hidTotalPages" value="0" />
            <!-- Default panel contents -->
            <div class="panel-heading"><strong>Equipment: Check out</strong></div>
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
                    <li role="presentation" class="dropdown" id="ddWarehouses">
                        <a id="dropWarehouses" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                        </a>
                        <ul id="menuWarehouses" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouses">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="hidQueryWarehouse" runat="server" value="0" />
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
                                    <th colspan="3" class="in-tab-title-rb bg-primary">Terminal Information</th>
                                    <th colspan="3" class="in-tab-title-b bg-primary">Storage Information</th>
                                </tr>
                                <tr>
                                    <th class="in-tab-title-b bg-warning">#</th>
                                    <th class="in-tab-title-b bg-warning">Type</th>
                                    <th class="in-tab-title-b bg-warning">Model</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important;">Functional</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: right !important;">SMH</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">Eng.</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important;">Location</th>
                                    <th class="in-tab-title-rb bg-warning">Status</th>
                                    <th class="in-tab-title-b bg-warning">Link</th>
                                    <th class="in-tab-title-b bg-warning">Received</th>
                                    <th class="in-tab-title-rb bg-warning">Ter. NO.</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">In Date</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">In Type</th>
                                    <th class="in-tab-title-b bg-warning">Warehouse</th>
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
                                    <td class="in-tab-txt-b">EX.</td>
                                    <td class="in-tab-txt-b textoverflow" style="text-align: left !important;">DL215-9-21442</td>
                                    <td class="in-tab-txt-b">Electric</td>
                                    <td class="in-tab-txt-b">2430</td>
                                    <td class="in-tab-txt-b">OFF</td>
                                    <td class="in-tab-txt-b textoverflow">山东省烟台市</td>
                                    <td class="in-tab-txt-rb">W</td>
                                    <td class="in-tab-txt-b">
                                        <img src="../images/img_connect_gprs.png" /></td>
                                    <td class="in-tab-txt-b textoverflow">2014-09-22</td>
                                    <td class="in-tab-txt-rb textoverflow">20140921221</td>
                                    <td class="in-tab-txt-b">2014-09-21</td>
                                    <td class="in-tab-txt-b">S</td>
                                    <td class="in-tab-txt-rb textoverflow">Warehouse1</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <p>
                    Click model number to open checkout dialog.
                </p>
            </div>
        </div>
        <!--小模态框-出库界面-->
        <div class="modal fade" id="modalCheckout" tabindex="-1" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header bg-primary">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong id="titleCheckout">Check out: </strong></h4>
                    </div>
                    <div class="modal-body" style="height: 200px;">
                        <div class="col-sm-12 show-grid" id="continue">
                            <div class="col-sm-12 show-grid">
                                <table class="table table-hover">
                                    <tbody id="popupTbody">
                                        <tr>
                                            <td class="popup-td" colspan="2" style="width: 50%;">
                                                <div class="input-group" style="width: 180px;">
                                                    <input type="text" class="form-control" id="number" placeholder="Customer number" maxlength="10" data-provide="typeahead"/>
                                                    <span class="input-group-btn">
                                                        <button class="btn btn-warning" type="button" id="queryCustomer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </span>
                                                </div>
                                            </td>
                                            <td class="popup-td" colspan="2" style="width: 50%;">
                                                <asp:DropDownList ID="ddlOuttype" runat="server" CssClass="form-control selectpicker" Width="180px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td" style="width: 10%;">Name:</td>
                                            <td class="popup-td" style="width: 40%;"></td>
                                            <td class="popup-td" style="width: 10%;">Phone:</td>
                                            <td class="popup-td"></td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td">Fax:</td>
                                            <td class="popup-td"></td>
                                            <td class="popup-td"></td>
                                            <td class="popup-td"></td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td" style="vertical-align: middle;">Date:</td>
                                            <td class="popup-td" colspan="3" style="text-align: left;">    
                                                <input class="form-control" runat="server" style="z-index: 9999 !important; width: 120px; margin-top: 3px;" id="outTime" name="outTime" placeholder="outdoor date" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <span style="color: #ff0000;" id="spanWarningNewInstorage"></span>
                        <asp:Button ID="btCheckoutStorage" CssClass="hidden" runat="server" Text="Button" OnClick="btCheckoutStorage_Click" />
                        <input type="hidden" id="hidCheckEquipmentId" runat="server" />
                        <input type="hidden" id="hidCheckCustomerId" runat="server" />
                        <button type="button" class="btn btn-success disabled" id="newCheckoutSave"><span class="glyphicon glyphicon-ok"></span>Checkout!</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
    <script type="text/javascript" src="../bootstrap3/js/bootstrap-select.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.checkout.js"></script>
</body>
</html>
