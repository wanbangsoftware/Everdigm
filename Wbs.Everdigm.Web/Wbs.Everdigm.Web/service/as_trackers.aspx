﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="as_trackers.aspx.cs" Inherits="Wbs.Everdigm.Web.service.as_trackers" %>

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
                <strong>A/S: Trackers</strong>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active"><a href="#" role="tab" data-toggle="tab">Tracker</a></li>
                    <li role="presentation" class="tablist-item-input">
                        <div class="input-group">
                            <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="license plate" maxlength="15">
                            <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" OnClick="btQuery_Click" />
                            <span class="input-group-btn">
                                <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                            </span>
                        </div>
                        <!-- /input-group -->
                    </li>
                    <li role="presentation" style="width: 100px; margin-top: 3px; padding-left: 5px;">
                        <div class="input-group">
                            <span class="input-group-btn">
                                <button class="btn btn-primary" type="button" data-toggle="modal" data-target="#modalNewTracker"><span class="glyphicon glyphicon-plus-sign"></span><span>Add New</span></button>
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
                                    <th class="in-tab-title-b bg-primary" style="width: 50px;">#</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 80px; text-align: left;">Number</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 130px;">Last Receive</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 130px;">Charging lose</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 130px;">Battery use up</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 130px;">Parking timeout</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 100px;">Vehicle</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 60px;">Director</th>
                                    <th class="in-tab-title-b bg-primary" style="text-align: left;">Address</th>
                                    <th class="in-tab-title-b bg-primary"></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="10">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td class="in-tab-txt-b">1</td>
                                    <td class="in-tab-txt-b" style="text-align: left;">95471420</td>
                                    <td class="in-tab-txt-b">2015/06/11 19:53:12</td>
                                    <td class="in-tab-txt-b">2015/06/11 19:53:12</td>
                                    <td class="in-tab-txt-b">2015/06/11 19:53:12</td>
                                    <td class="in-tab-txt-b">2015/06/11 19:53:12</td>
                                    <td class="in-tab-txt-b">12345678</td>
                                    <td class="in-tab-txt-b">12345678</td>
                                    <td class="in-tab-txt-b textoverflow" style="text-align: left;" title="Asian Highway 3, Songino Khairkhan, Ulaanbaatar, Mongolia">Asian Highway 3, Songino Khairkhan, Ulaanbaatar, Mongolia</td>
                                    <td class="in-tab-txt-b"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-转移仓库界面-->
        <div class="modal fade" id="modalNewTracker" tabindex="-1" style="height: 500px;" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:Panel runat="server" DefaultButton="btSave">
                        <div class="modal-header custom-modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title"><strong>Add new tracker: </strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12 show-grid">
                                    <table class="table table-hover">
                                        <tbody id="popupTbody">
                                            <tr>
                                                <td class="popup-td" style="vertical-align: middle;">Number:</td>
                                                <td class="popup-td">
                                                    <input type="text" class="form-control" style="width: 150px;" runat="server" id="number" placeholder="number" maxlength="10">
                                                </td>
                                                <td class="popup-td" style="vertical-align: middle;">Vehicle:</td>
                                                <td class="popup-td">
                                                    <input type="text" class="form-control" style="width: 150px;" runat="server" id="vehicle" placeholder="vehicle" maxlength="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="popup-td" style="vertical-align: middle;">Director:</td>
                                                <td class="popup-td">
                                                    <input type="text" class="form-control" style="width: 150px;" runat="server" id="director" placeholder="director" maxlength="10">
                                                </td>
                                                <td class="popup-td"></td>
                                                <td class="popup-td"></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btSave" runat="server" CssClass="hidden" OnClick="btSave_Click" />
                            <input type="hidden" id="hiddenId" runat="server" />
                            <button type="button" class="btn btn-success"><span class="glyphicon glyphicon-ok"></span>Save</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </asp:Panel>
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
    <script type="text/javascript" src="../scripts/service/as.trackers.js"></script>
</body>
</html>
