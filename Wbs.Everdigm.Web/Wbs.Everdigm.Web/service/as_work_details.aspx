<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="as_work_details.aspx.cs" Inherits="Wbs.Everdigm.Web.service.as_work_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../bootstrap3/bootstrap-datepicker-1.3.0/css/datepicker3.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/pagging.css" rel="stylesheet" />
    <style type="text/css">
        body {
            margin: 5px;
        }

        .textoverflow {
            max-width: 150px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .custom-modal-header {
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        .caution {
            border-color: #d9534f;
        }

        .iframe-container {
            padding-bottom: 60%;
            padding-top: 30px;
            height: 0;
            overflow: hidden;
        }

            .iframe-container iframe,
            .iframe-container object,
            .iframe-container embed {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" id="hidKey" runat="server" />
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>A/S: Work Detail</strong>
            </div>
            <div class="panel-body">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active">
                        <a href="#">Work detail</a>
                    </li>
                    <li role="presentation" style="margin-top: 3px; padding-left: 2px;">
                        <button class="btn btn-success" type="button" data-toggle="modal" data-target="#modalAddEquipment"><span class="glyphicon glyphicon-plus"></span><span>Add Equipment</span></button>
                    </li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content" style="border: 1px solid #fff !important;">
                    <div class="row">
                        <div class="col-sm-9">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th class="in-tab-title-b bg-primary" style="width: 30px; text-align: center;">#</th>
                                        <th class="in-tab-title-b bg-primary" style="width: 140px;">Equipment</th>
                                        <th class="in-tab-title-b bg-primary" style="width: 50px;">Type</th>
                                        <th class="in-tab-title-b bg-primary" style="width: 90px;">Terminal(booked)</th>
                                        <th class="in-tab-title-b bg-primary" style="width: 70px;">Sim</th>
                                        <th class="in-tab-title-b bg-primary" style="width: 70px;">Satellite</th>
                                        <th class="in-tab-title-b bg-primary" style="text-align: left;">Detail</th>
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
                        <div class="col-sm-3">
                            <!--Equipment basic informations-->
                            <table class="table table-hover" id="workinfo" runat="server">
                                <tr class="alert-info">
                                    <th class="panel-body-td" colspan="2">Work description</th>
                                </tr>
                                <tr>
                                    <td class="panel-body-td" style="width: 100px;">Title:</td>
                                    <td class="panel-body-td">kj</td>
                                </tr>
                                <tr>
                                    <td class="panel-body-td">Published:</td>
                                    <td class="panel-body-td">kj</td>
                                </tr>
                                <tr>
                                    <td class="panel-body-td">Schedule:</td>
                                    <td class="panel-body-td">kj</td>
                                </tr>
                                <tr>
                                    <td class="panel-body-td">Director:</td>
                                    <td class="panel-body-td">kj</td>
                                </tr>
                                <tr>
                                    <td class="panel-body-td">Equipment:</td>
                                    <td class="panel-body-td">0</td>
                                </tr>
                                <tr>
                                    <td class="panel-body-td">Description:</td>
                                    <td class="panel-body-td">aa</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-添加设备和终端界面-->
        <div class="modal fade" id="modalAddEquipment" tabindex="-1" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:Panel runat="server" DefaultButton="btSave">
                        <div class="modal-header custom-modal-header bg-primary">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title"><strong>Add equipment into work: </strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12 show-grid">
                                    <table class="table table-hover">
                                        <tr>
                                            <td class="popup-td" style="vertical-align: middle; text-align: right;">Equipment:</td>
                                            <td class="popup-td" colspan="3">
                                                <input type="hidden" id="hiddenEquipment" runat="server" />
                                                <input type="text" class="form-control" data-provide="typeahead" id="equipment" placeholder="equipment" maxlength="20">
                                            </td>
                                        </tr>
                                        <tr id="equipmentInfo">
                                            <td class="popup-td" style="text-align: right; width: 50px;">Terminal:</td>
                                            <td class="popup-td"></td>
                                            <td class="popup-td" style="text-align: right; width: 50px;">Sim:</td>
                                            <td class="popup-td"></td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td" style="vertical-align: middle; text-align: right;">Book:</td>
                                            <td class="popup-td" colspan="3">
                                                <input type="hidden" id="hiddenTerminal" runat="server" />
                                                <input type="text" class="form-control" data-provide="typeahead" id="terminal" placeholder="terminal to install or displace" maxlength="10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td" style="vertical-align: middle; text-align: right;">Work type:</td>
                                            <td class="popup-td" colspan="3">
                                                <input type="hidden" id="hiddenType" runat="server" />
                                                <div class="btn-group" data-toggle="buttons">
                                                    <label class="btn btn-primary active">
                                                        <input type="radio" name="options" value="0" id="option1" autocomplete="off" checked>Install Terminal
                                                    </label>
                                                    <label class="btn btn-primary" style="margin-left: 1px;">
                                                        <input type="radio" name="options" value="1" id="option2" autocomplete="off">Displace Terminal
                                                    </label>
                                                    <label class="btn btn-primary" style="margin-left: 1px;">
                                                        <input type="radio" name="options" value="2" id="option3" autocomplete="off">Equipment Service
                                                    </label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td" style="vertical-align: middle; text-align: right;">Detail:</td>
                                            <td class="popup-td" colspan="3">
                                                <textarea runat="server" class="form-control" id="detail" placeholder="detail" rows="3"></textarea>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <label class="label label-warning" id="warning"></label>
                            <asp:Button ID="btSave" runat="server" CssClass="hidden" OnClick="btSave_Click" />
                            <button type="button" class="btn btn-success" id="save"><span class="glyphicon glyphicon-ok"></span>Save</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <!--小模态框--显示打印-->
        <div class="modal fade" id="modalShowFile" role="dialog" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-warning">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Warning</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12 show-grid">
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
    <script type="text/javascript" src="../scripts/service/as.work.details.js"></script>
</body>
</html>
