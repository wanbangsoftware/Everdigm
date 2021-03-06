﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_new_product.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_new_product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <style type="text/css">
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
                <strong>Equipment: New Product</strong>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!--默认查询新品库存列表-->
                <input type="hidden" id="hidQueryType" runat="server" value="N" />
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active"><a href="#new" role="tab" data-toggle="tab">New Product</a></li>
                </ul>

                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content">
                        <!--Equipment list-->
                        <table class="table table-hover" style="margin-top: 2px !important;">
                            <tr>
                                <td colspan="4" class="alert-info">Equipment Information</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <ul class="nav nav-tabs" style="border-bottom: 0px;" role="tablist">
                                        <li role="presentation" class="dropdown" id="ddFunctional">
                                            <a id="dropFunctional" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Functional:</span><span class="caret"></span>
                                            </a>
                                            <ul id="menuFunctional" class="dropdown-menu" role="menu" runat="server" aria-labelledby="dropFunctional">
                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                            </ul>
                                            <input type="hidden" id="hidFunctional" runat="server" value="0" />
                                        </li>
                                        <li role="presentation" class="dropdown" id="ddType">
                                            <a id="dropType" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Type:</span><span class="caret"></span>
                                            </a>
                                            <ul id="menuType" class="dropdown-menu" role="menu" aria-labelledby="dropType">
                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                            </ul>
                                            <input type="hidden" id="selectedType" runat="server" value="0" />
                                        </li>
                                        <li role="presentation" class="dropdown" id="ddModel">
                                            <a id="dropModel" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Model:</span><span class="caret"></span>
                                            </a>
                                            <ul id="menuModel" class="dropdown-menu" role="menu" aria-labelledby="dropModel">
                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Choose a Type First</a></li>
                                            </ul>
                                            <input type="hidden" id="selectedModel" runat="server" value="0" />
                                        </li>
                                        <li role="presentation" class="tablist-item-input">
                                            <input class="form-control" runat="server" id="number" style="width: 150px; margin-top: 3px;" placeholder="number" maxlength="10" />
                                        </li>
                                        <li role="presentation" class="tablist-item-input">
                                            <a><span id="fullNumber"></span></a>
                                        </li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="alert-info">Storage Information</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <ul class="nav nav-tabs" style="border-bottom: 0px;" role="tablist">
                                        <li role="presentation" class="dropdown" id="ddWarehouses">
                                            <a id="dropWarehouse" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                                            </a>
                                            <ul id="menuWarehouse" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouse">
                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                                            </ul>
                                            <input type="hidden" id="hidWarehouse" runat="server" value="0" />
                                        </li>
                                        <li role="presentation" class="tablist-item-input">
                                            <input class="form-control" runat="server" style="z-index: 9999 !important; width: 120px; margin-top: 3px;" id="ccDate" name="ccDate" placeholder="C.C. date" />
                                        </li>
                                        <li role="presentation" class="tablist-item-input">
                                            <input class="form-control" runat="server" style="z-index: 9999 !important; width: 120px; margin-top: 3px; margin-left: 5px;" id="inDate" name="inDate" placeholder="In Store date" />
                                        </li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="popup-td">
                                    <input type="hidden" id="hidNewInstorage" runat="server" />
                                    <asp:Button ID="btSave" CssClass="hidden" runat="server" OnClick="btSave_Click" />
                                    <button class="btn btn-primary" type="button"><span class="glyphicon glyphicon-floppy-open"></span><span>Save</span></button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalWarning" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-warning">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="myModalLabel">Warning</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <span id="warningText"></span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
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
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.base.js"></script>
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.new.product.js"></script>
</body>
</html>
