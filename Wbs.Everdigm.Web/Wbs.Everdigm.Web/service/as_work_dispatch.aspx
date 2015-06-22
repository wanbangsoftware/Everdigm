<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="as_work_dispatch.aspx.cs" Inherits="Wbs.Everdigm.Web.service.as_work_dispatch" %>

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

        body.modal-open .datepicker {
            z-index: 1200 !important;
        }

        body.modal-open .dropdown-menu {
            z-index: 1200 !important;
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
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>AS: Work Dispatch</strong>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active">
                        <div class="input-group" style="margin-top: 3px; margin-bottom: 3px;">
                            <div class="input-daterange input-group" style="float: left;">
                                <span class="input-group-addon">Schedule:</span>
                                <input type="text" class="input-md form-control little-input click-input" id="start" runat="server" />
                                <span class="input-group-addon">to </span>
                                <input type="text" class="input-md form-control little-input click-input" id="end" runat="server" />
                            </div>
                            <span class="input-group-btn" style="float: left;">
                                <asp:Button ID="btQuery" runat="server" CssClass="hidden" OnClick="btQuery_Click" />
                                <button class="btn btn-success" id="query" type="button"><span class="glyphicon glyphicon-search"></span>Query</button>
                            </span>
                        </div>
                    </li>
                    <li role="presentation" style="width: 100px; margin-top: 3px; padding-left: 5px;">
                        <div class="input-group">
                            <span class="input-group-btn">
                                <button class="btn btn-primary" type="button" data-toggle="modal" data-target="#modalNewWork"><span class="glyphicon glyphicon-plus-sign"></span><span>Add New Work</span></button>
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
                                    <th class="in-tab-title-b bg-primary" style="width: 90px;">Published</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 90px;">Start</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 90px;">End</th>
                                    <th class="in-tab-title-b bg-primary textoverflow" style="width: 100px; text-align: left;">Director</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 50px;">Works</th>
                                    <th class="in-tab-title-b bg-primary" style="text-align: left;">Title</th>
                                    <th class="in-tab-title-b bg-primary"></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="8">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-转移仓库界面-->
        <div class="modal fade" id="modalNewWork" tabindex="-1" style="height: 500px;" role="dialog" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:Panel runat="server" DefaultButton="btSave">
                        <div class="modal-header custom-modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title"><strong>Add new work: </strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12 show-grid">
                                    <table class="table table-hover">
                                        <tbody id="popupTbody">
                                            <tr>
                                                <td class="popup-td" style="vertical-align: middle; width: 10%;">Schedule:</td>
                                                <td class="popup-td" colspan="3">
                                                    <div class="input-daterange input-group" style="float: left; margin-left: 2px;">
                                                        <input type="text" class="input-md form-control little-input click-input" id="start1" runat="server" />
                                                        <span class="input-group-addon">to </span>
                                                        <input type="text" class="input-md form-control little-input click-input" id="end1" runat="server" />
                                                    </div>
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
                                            <tr>
                                                <td class="popup-td" style="vertical-align: middle;">Title:</td>
                                                <td class="popup-td" colspan="3">
                                                    <input type="text" class="form-control" runat="server" id="title" placeholder="title" maxlength="100">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="popup-td">Description:</td>
                                                <td class="popup-td" colspan="3">
                                                    <textarea runat="server" class="form-control" id="description" placeholder="description" rows="3"></textarea>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btSave" runat="server" CssClass="hidden" OnClick="btSave_Click" />
                            <input type="hidden" id="hiddenId" runat="server" />
                            <button type="button" class="btn btn-success" id="save"><span class="glyphicon glyphicon-ok"></span>Save</button>
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
    <script type="text/javascript" src="../scripts/service/as.common.js"></script>
    <script type="text/javascript" src="../scripts/service/as.work.js"></script>
</body>
</html>
