<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_in_storage.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_in_storage" %>

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
            <div class="panel-heading">
                <strong>Equipment: Check in</strong>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!--默认查询新品库存列表-->
                <input type="hidden" id="hidQueryType" runat="server" value="N" />
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active"><a href="#new" role="tab" data-toggle="tab">New Product</a></li>
                    <li role="presentation"><a href="#second-lease" role="tab" data-toggle="tab">Rental Fleet</a></li>
                    <li role="presentation"><a href="#change-warehouse" role="tab" data-toggle="tab">Change Warehouse</a></li>
                    <li role="presentation" class="dropdown" id="ddTypes">
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
                    <li role="presentation" style="width: 100px; margin-top: 3px; padding-left: 5px;">
                        <div class="input-group">
                            <span class="input-group-btn">
                                <button class="btn btn-primary" id="openModal" type="button" data-toggle="modal" data-target="#modalNewProduct"><span class="glyphicon glyphicon-floppy-open"></span> <span>Storage</span></button>
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
                                    <th colspan="6" class="in-tab-title-rb bg-primary">Equipment Information</th>
                                    <th colspan="5" class="in-tab-title-rb bg-primary">Storage Information</th>
                                    <th colspan="6" class="in-tab-title-b bg-primary">Terminal Information</th>
                                </tr>
                                <tr>
                                    <th class="in-tab-title-b bg-warning">#</th>
                                    <th class="in-tab-title-b bg-warning">Type</th>
                                    <th class="in-tab-title-b bg-warning">Model</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: right !important;">SMH</th>
                                    <th class="in-tab-title-b bg-warning">Eng.</th>
                                    <th class="in-tab-title-b bg-warning" style="text-align: left !important;">Location</th>
                                    <th class="in-tab-title-rb bg-warning">Status</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">In Date</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">In Type</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">Out Date</th>
                                    <th class="in-tab-title-b bg-warning textoverflow">Out Type</th>
                                    <th class="in-tab-title-rb bg-warning">Warehouse</th>
                                    <th class="in-tab-title-b bg-warning">Signal</th>
                                    <th class="in-tab-title-b bg-warning">Link</th>
                                    <th class="in-tab-title-b bg-warning">Received</th>
                                    <th class="in-tab-title-b bg-warning">Ter. NO.</th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="18">
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
                                    <td class="in-tab-txt-b textoverflow">DL215-9-21442</td>
                                    <td class="in-tab-txt-b">2430</td>
                                    <td class="in-tab-txt-b">OFF</td>
                                    <td class="in-tab-txt-b textoverflow">山东省烟台市</td>
                                    <td class="in-tab-txt-rb">W</td>
                                    <td class="in-tab-txt-b">2014-09-21</td>
                                    <td class="in-tab-txt-b">S</td>
                                    <td class="in-tab-txt-b">2014-10-22</td>
                                    <td class="in-tab-txt-b">S</td>
                                    <td class="in-tab-txt-rb textoverflow">Warehouse1</td>
                                    <td class="in-tab-txt-b">ON</td>
                                    <td class="in-tab-txt-b">
                                        <img src="../images/img_connect_gprs.png" /></td>
                                    <td class="in-tab-txt-b textoverflow">2014-09-22</td>
                                    <td class="in-tab-txt-b textoverflow">20140921221</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-新品入库界面-->
        <div class="modal fade" id="modalNewProduct" tabindex="-1" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header bg-primary">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="NewStorageIn"><strong>New Product: </strong></h4>
                    </div>
                    <div class="modal-body" style="height: 200px;">
                        <div class="col-sm-12 show-grid" id="continue">
                            <div class="col-sm-12 show-grid">
                                <table class="table table-hover">
                                    <tbody>
                                        <tr>
                                            <td class="popup-td right">Type:</td>
                                            <td class="popup-td">
                                                <div role="presentation" class="dropdown" id="ddType">
                                                    <a id="dropType" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Type:</span><span class="caret"></span>
                                                    </a>
                                                    <ul id="menuType" class="dropdown-menu" role="menu" aria-labelledby="dropType">
                                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <td class="popup-td right">Model:</td>
                                            <td class="popup-td">
                                                <div role="presentation" class="dropdown" id="ddModel">
                                                    <a id="dropModel" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Model:</span><span class="caret"></span>
                                                    </a>
                                                    <ul id="menuModel" class="dropdown-menu" role="menu" aria-labelledby="dropModel">
                                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Choose a Type First</a></li>
                                                    </ul>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">C.C. date:
                                            </td>
                                            <td class="popup-td">
                                                <input class="form-control" runat="server" style="z-index: 9999 !important;" id="ccDate" name="ccDate" placeholder="C.C. date">
                                            </td>
                                            <td class="popup-td right">In date:</td>
                                            <td class="popup-td">
                                                <input class="form-control" runat="server" style="z-index: 9999 !important;" id="inDate" name="inDate" placeholder="In Store date">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">Warehouse:</td>
                                            <td class="popup-td">
                                                <div role="presentation" class="dropdown" id="ddWarehouse">
                                                    <a id="dropWarehouse" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                                                    </a>
                                                    <ul id="menuWarehouse" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouse">
                                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <td class="popup-td right"></td>
                                            <td class="popup-td"></td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">Number:</td>
                                            <td class="popup-td">
                                                <input class="form-control" id="number" placeholder="number" maxlength="5" />
                                            </td>
                                            <td class="popup-td right"></td>
                                            <td class="popup-td"><span id="fullNumber"></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <span style="color: #ff0000;" id="spanWarningNewInstorage"></span>
                        <asp:Button ID="btSaveNewInStorage" CssClass="hidden" runat="server" Text="Button" OnClick="btSaveNewInStorage_Click" Width="0" Height="0" />
                        <input type="hidden" id="hidNewInstorage" runat="server" />
                        <button type="button" class="btn btn-success" id="newInStorageSave"><span class="glyphicon glyphicon-ok"></span>Store in!</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--小模态框-二手/租赁入库界面-->
        <div class="modal fade" id="modalOldProduct" tabindex="-1" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header bg-success">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Rental Fleet storage: </strong></h4>
                    </div>
                    <div class="modal-body" style="height: 230px;">
                        <div class="col-sm-12 show-grid">
                            <ul class="nav nav-tabs col-sm-12 show-grid" role="tablist">
                                <!--输入查询2手或租赁出去的设备信息-->
                                <li role="presentation" class="dropdown" id="ddTypeOld">
                                    <a id="dropTypeOld" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Type:</span><span class="caret"></span>
                                    </a>
                                    <ul id="menuTypeOld" class="dropdown-menu" role="menu" aria-labelledby="dropTypeOld">
                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                    </ul>
                                </li>
                                <li role="presentation" class="dropdown" id="ddModelOld">
                                    <a id="dropModelOld" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Model:</span><span class="caret"></span>
                                    </a>
                                    <ul id="menuModelOld" class="dropdown-menu" role="menu" aria-labelledby="dropModelOld">
                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Choose a Type First</a></li>
                                    </ul>
                                </li>
                                <li role="presentation" class="tablist-item-input">
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtQueryOld" placeholder="number" data-provide="typeahead">
                                        <span class="input-group-btn">
                                            <button class="btn btn-warning" type="button" id="queryOld"><span class="glyphicon glyphicon-search"></span></button>
                                        </span>
                                    </div>
                                </li>
                            </ul>
                            <div class="col-sm-12" style="margin-top: 3px;">
                                <table class="table table-hover">
                                    <tbody id="oldEquipmentInfo">
                                        <tr>
                                            <td class="popup-td right">Equipment:</td>
                                            <td class="popup-td"></td>
                                            <td class="popup-td" colspan="2"><input type="checkbox" id="cbRepair" runat="server" />Need nspection & repair</td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">Status:</td>
                                            <td class="popup-td" colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">Location:</td>
                                            <td class="popup-td" colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">Store In:</td>
                                            <td class="popup-td">
                                                <div role="presentation" class="dropdown" id="ddWarehouseOld">
                                                    <a id="dropWarehouseOld" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                                                    </a>
                                                    <ul id="menuWarehouseOld" class="dropdown-menu" role="menu" aria-labelledby="dropWarehousOlde">
                                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                                    </ul>
                                                </div>
                                            </td>
                                            <td class="popup-td right"></td>
                                            <td class="popup-td"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btSaveOldInStorage" CssClass="hidden" runat="server" Text="Button" OnClick="btSave2Hand_Click" />
                        <input type="hidden" id="hidOldInstorage" value="" runat="server" />
                        <input type="hidden" id="hidOldInstorageId" value="" />
                        <button type="button" class="btn btn-success disabled" id="oldInStorageSave"><span class="glyphicon glyphicon-ok"></span>Store in!</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!--小模态框-转移仓库界面-->
        <div class="modal fade" id="modalWarehousingProduct" tabindex="-1" style="height: 500px;" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header bg-danger">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Change Warehouse: </strong></h4>
                    </div>
                    <div class="modal-body" style="height: 120px; ">
                        <div class="col-sm-12 show-grid">
                            <ul class="nav nav-tabs" id="equipmentWarehouseInfoBar">
                                <li role="presentation"><a href="#" role="tab">Number: </a></li>
                                <li role="presentation"><a href="#" role="tab">Warehouse: </a></li>
                            </ul>
                            <ul class="nav nav-tabs">
                                <li role="presentation"><a href="#" role="tab">Change to: </a></li>
                                <li role="presentation" class="dropdown" id="ddWarehouseWarehousing">
                                    <a id="dropWarehouseWarehousing" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Change to:</span><span class="caret"></span>
                                    </a>
                                    <ul id="menuWarehouseWarehousing" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouseWarehousing">
                                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btSaveChangeWarehouse" CssClass="hidden" runat="server" Text="Change warehouse" OnClick="btSaveChangeWarehouse_Click" />
                        <input type="hidden" id="hidWarehouseTo" value="" runat="server" />
                        <input type="hidden" id="hidWarehouseEquipmentId" value="" runat="server" />
                        <button type="button" class="btn btn-success" id="changeWarehouseSave"><span class="glyphicon glyphicon-ok"></span>Transfer!</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-转移仓库确定界面-->
        <div class="modal fade" id="modalWarehousingConfirm" tabindex="-1" style="height: 500px;" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Change Warehouse(Confirm): </strong></h4>
                    </div>
                    <div class="modal-body" style="height: 300px; overflow: auto;">
                        <ul class="nav nav-tabs" id="equipmentWarehouseConfirmBar">
                            <li role="presentation"><a href="#" role="tab">Number: </a></li>
                            <li role="presentation"><a href="#" role="tab">Warehouse: </a></li>
                        </ul>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btConfirmWarehouse" CssClass="hidden" runat="server" Text="Change warehouse" OnClick="btConfirmWarehouse_Click" />
                        <input type="hidden" id="hidConfirmWarehouse" value="" runat="server" />
                        <button type="button" class="btn btn-success" id="confirmWarehouseSave"><span class="glyphicon glyphicon-ok"></span>Confirm</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment_in_storage.js"></script>
</body>
</html>
