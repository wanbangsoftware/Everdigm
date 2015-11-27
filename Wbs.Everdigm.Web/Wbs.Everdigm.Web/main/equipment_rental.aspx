<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_rental.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_rental" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
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
            <div class="panel-heading"><strong>Equipment: Rental</strong></div>
            <div class="panel-body">
                <asp:Panel runat="server" ID="panelQuery" DefaultButton="btQuery">
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
                                <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="number">
                                <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" Width="0" Height="0" OnClick="btQuery_Click" />
                                <span class="input-group-btn">
                                    <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                                </span>
                            </div>
                            <!-- /input-group -->
                        </li>
                    </ul>
                </asp:Panel>
                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content">
                        <!--Equipment list-->
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="bg-primary"></th>
                                    <th colspan="7" class="in-tab-title-rb bg-primary">Equipment Information</th>
                                    <th colspan="5" class="in-tab-title-rb bg-primary">Rental Information</th>
                                    <th colspan="2" class="in-tab-title-rb bg-primary">Customer</th>
                                    <th class="in-tab-title-b bg-primary">Storage</th>
                                </tr>
                                <tr>
                                    <th class="in-tab-title-b bg-warning" style="width: 30px;">#</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 30px;">Type</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 90px;">Model</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important; width: 80px;">Functional</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: right !important; width: 80px;">SMH</th>
                                    <th class="in-tab-title-b bg-warning textoverflow" style="width: 50px;">Eng.</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important;">Location</th>
                                    <th class="in-tab-title-rb bg-warning" style="width: 50px;">Status</th>
                                    <th class="in-tab-title-b bg-warning textoverflow" colspan="3">Rentail date</th>
                                    <th class="in-tab-title-b bg-warning textoverflow" style="width: 50px;">Days</th>
                                    <th class="in-tab-title-rb bg-warning textoverflow" style="width: 100px;">Hr/Day</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 100px;">Number</th>
                                    <th class="in-tab-title-rb bg-warning" style="text-align: left;">Name</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 80px;">Warehouse</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="15">
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
                                    <td class="in-tab-txt-rb">W</td>
                                    <td class="in-tab-txt-b">2015/08/12</td>
                                    <td class="in-tab-txt-b" style="width: 15px;">to</td>
                                    <td class="in-tab-txt-b">2015/10/12</td>
                                    <td class="in-tab-txt-b">30</td>
                                    <td class="in-tab-txt-rb">4 hr 30 min</td>
                                    <td class="in-tab-txt-b">23553523</td>
                                    <td class="in-tab-txt-rb">23553523</td>
                                    <td class="in-tab-txt-b textoverflow">Warehouse1</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalRentalOut" tabindex="-1" role="dialog" aria-labelledby="labelRentalOut" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <asp:Panel runat="server" ID="panelRentalOut">
                    <div class="modal-content">
                        <div class="modal-header custom-modal-header btn-warning">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="labelRentalOut">Rental</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <table class="table table-hover">
                                        <tbody id="popupTbody">
                                            <tr>
                                                <td class="popup-td" colspan="4">Customer:
                                                <span class="label label-default" style="cursor: pointer;">select customer</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="popup-td">Name:</td>
                                                <td class="popup-td" colspan="3" style="text-align: left;"></td>
                                            </tr>
                                            <tr>
                                                <td class="popup-td" style="width: 10%;">Code:</td>
                                                <td class="popup-td" style="width: 35%;"></td>
                                                <td class="popup-td" style="width: 10%;">Phone:</td>
                                                <td class="popup-td" style="width: 35%;"></td>
                                            </tr>
                                            <tr>
                                                <td class="popup-td" style="vertical-align: middle;">Deadline:</td>
                                                <td class="popup-td" colspan="3">
                                                    <input class="form-control" runat="server" style="z-index: 9999 !important; width: 120px; margin-top: 3px;" id="deadLine" name="deadLine" placeholder="DeadLine">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <span class="label label-warning" id="spanRentalConfirm"></span>
                            <input type="hidden" id="hiddenCustomer" runat="server" />
                            <input type="hidden" id="hiddenRentalId" runat="server" />
                            <asp:Button runat="server" ID="confirmRental" CssClass="hidden" OnClick="confirmRental_Click" />
                            <button id="rentalConfirm" type="button" class="btn btn-warning disabled">
                                <span class="glyphicon glyphicon-ok"></span>Confirm
                            </button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="modal fade" id="modalRentalOver" tabindex="-1" role="dialog" aria-labelledby="labelRentalOver" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <asp:Panel runat="server" ID="panelRentalEdit" DefaultButton="btRentalEdit">
                    <div class="modal-content">
                        <div class="modal-header custom-modal-header btn-success">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="labelRentalOver">Extend/Reclaim:</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="btn-group" data-toggle="buttons">
                                        <label class="btn btn-success active">
                                            <input type="radio" runat="server" name="options" id="optionExtend" autocomplete="off" checked>
                                            Extend
                                        </label>
                                        <label class="btn btn-success">
                                            <input type="radio" runat="server" name="options" id="optionReclaim" autocomplete="off">
                                            Reclaim
                                        </label>
                                    </div>
                                </div>
                                <div class="col-lg-12" id="divExtend" style="padding-top: 10px;">
                                    <div class="col-lg-2" style="vertical-align: middle; margin-top: 10px;">
                                        Extend to:
                                    </div>
                                    <div class="col-lg-9">
                                        <input class="form-control" runat="server" style="z-index: 9999 !important; width: 150px; margin-top: 3px;" id="deadLineExtend" placeholder="new deadline">
                                    </div>
                                </div>
                                <div class="col-lg-12 hidden" id="divReclaim" style="padding-top: 10px;">
                                    <div class="row">
                                        <div class="col-lg-2" style="vertical-align: middle; margin-top: 10px;">
                                            Warehouse:
                                        </div>
                                        <div class="col-lg-9">
                                            <ul class="nav nav-tabs" role="tablist" style="border-bottom: 0px;">
                                                <li role="presentation" class="dropdown" id="ddWarehouse">
                                                    <a id="dropWarehouse" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                                                    </a>
                                                    <ul id="menuWarehouse" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouse">
                                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                                    </ul>
                                                    <input type="hidden" id="hiddenWarehouse" runat="server" value="0" />
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-2" style="vertical-align: middle; margin-top: 10px;">
                                            Repairs?:
                                        </div>
                                        <div class="col-lg-9">
                                            <div class="btn-group" data-toggle="buttons">
                                                <label class="btn btn-success">
                                                    <input type="checkbox" autocomplete="off" id="cbRepair" runat="server"><span class="glyphicon glyphicon-ban-circle"></span><span> do not need repairs</span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="hidden" id="hiddenEditId" runat="server" />
                            <span class="label label-warning" id="spanRentalOver"></span>
                            <asp:Button runat="server" ID="btRentalEdit" CssClass="hidden" OnClick="btRentalEdit_Click" />
                            <button id="rentalEditor" type="button" class="btn btn-success">
                                <span class="glyphicon glyphicon-ok"></span>Confirm
                            </button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </asp:Panel>
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
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script src="../scripts/main/equipment.rental.js"></script>
</body>
</html>
