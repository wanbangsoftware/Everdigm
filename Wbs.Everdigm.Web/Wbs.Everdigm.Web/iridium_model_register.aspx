<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iridium_model_register.aspx.cs" Inherits="Wbs.Everdigm.Web.iridium_model_register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iridium Model Register</title>
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
                <strong>Satellite: Iridium Model Register</strong>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active"><a href="#" role="tab" data-toggle="tab">Iridium Models</a></li>
                    <li role="presentation" class="tablist-item-input">
                        <div class="input-group">
                            <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="number" maxlength="15" />
                            <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" Width="0" Height="0" OnClick="btQuery_Click" />
                            <span class="input-group-btn">
                                <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                            </span>
                        </div>
                    </li>
                    <li role="presentation" style="width: 100px; margin-top: 3px; padding-left: 5px;">
                        <div class="input-group">
                            <asp:Button ID="btSave" CssClass="hidden" runat="server" Text="Save" OnClick="btSave_Click" />
                            <span class="input-group-btn">
                                <button class="btn btn-primary" id="openModal" type="button"><span class="glyphicon glyphicon-floppy-open"></span><span> Register IMEI</span></button>
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
                                    <th class="in-tab-title-b bg-primary" style="width: 120px; text-align: left;">IMEI</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 150px; text-align: left;">Register date</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 60px;">Bind?</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 100px; text-align: left;">PCB number</th>
                                    <th class="in-tab-title-b bg-primary" style="width: 150px; text-align: left;">Manufacturing date</th>
                                    <th class="in-tab-title-b bg-primary"></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="7">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                            </tbody>
                        </table>
                        <b>Explanation</b>:<br />
                        <b>1:</b> Input Iridium IMEI number and register it.<br />
                        <b>2:</b> Click IMEI number to bind manufacture informations and print LABEL.<br />
                        <b>3:</b> Before doing those steps, you should install the printer driver and <a id="aDownload" style="cursor: pointer;">download and install</a> the printer app .
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-信息提示界面-->
        <div class="modal fade" id="modalWarning" tabindex="-1" style="z-index: 1301;" role="dialog" aria-labelledby="NewStorageIn" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-danger">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Warning: </strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12 show-grid">
                                <span id="spanWarning">Number cannot be blank.</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-信息提示界面-->
        <div class="modal fade" id="modalPrinting" tabindex="-1" style="z-index: 1300;" role="dialog" data-backdrop="static" aria-labelledby="NewStorageIn" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-danger">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Iridium Model Label Print</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12 show-grid">
                                <span id="spanWarningPrinting">Operating Iridium label print process, please wait...</span><br />
                                <span>Please confirm your app installation and start it before this operation.</span><br />
                                <br />
                                <div class="progress">
                                    <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                        <span id="spanPrintStatus" class="sr-only"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 show-grid">
                                <span id="spanPrintStatusText"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框：终端生产界面-->
        <div class="modal fade" id="modalManufacturing" tabindex="-1" role="dialog" data-backdrop="static" aria-labelledby="NewStorageIn" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-primary">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>Manufacture informations for "<span id="spanImei"></span>"</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12 show-grid">
                                <table class="table table-hover">
                                    <tr>
                                        <td class="popup-td right" style="vertical-align: middle; width: 140px;">PCB number:</td>
                                        <td class="popup-td">
                                            <input type="text" id="pcbNumber" runat="server" class="form-control" placeholder="PCB number" maxlength="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="popup-td" style="vertical-align: middle; text-align: right;">Firmware version:</td>
                                        <td class="popup-td">
                                            <input type="text" id="fwVersion" runat="server" class="form-control" placeholder="Firmware version" value="1.00" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="popup-td" style="vertical-align: middle; text-align: right;">Manufacture date:</td>
                                        <td class="popup-td">
                                            <input type="text" id="manufDate" runat="server" class="input-md form-control click-input date-picker-picker" placeholder="Manufacture date" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="popup-td" style="vertical-align: middle; text-align: right;">Rated voltage:</td>
                                        <td class="popup-td">
                                            <input type="text" id="rateVoltate" runat="server" class="form-control" value="9-32VDC 1A =" placeholder="Rated voltage" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="popup-td" style="vertical-align: middle; text-align: right;">Manufacturer:</td>
                                        <td class="popup-td">
                                            <input type="text" id="manuf" runat="server" class="form-control" placeholder="Manufacturer" value="T&R(Korea)" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <label class="label label-danger" id="warning"></label>
                        <img src="images/loading_orange.gif" id="imgLoading" />
                        <button type="button" class="btn btn-primary" id="save"><span class="glyphicon glyphicon-floppy-disk"></span> 1. Save</button>
                        <button type="button" class="btn btn-primary" id="print"><span class="glyphicon glyphicon-print"></span> 2. Print Label</button>
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
    <script src="js/jquery.timer.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="scripts/iridium.model.register.js"></script>
</body>
</html>
