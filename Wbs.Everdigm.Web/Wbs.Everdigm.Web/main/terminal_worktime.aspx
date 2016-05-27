﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_worktime.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_worktime" %>

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
                <strong>运转时间分析</strong>
            </div>
            <div class="panel-body" style="padding-bottom: 0px !important;">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active">
                        <div class="input-daterange input-group" style="margin-right: 2px; margin-bottom: 2px;">
                            <input type="text" class="input-md form-control little-input click-input" placeholder="start" id="start" runat="server" />
                            <span class="input-group-addon">to </span>
                            <input type="text" class="input-md form-control little-input click-input" placeholder="end" id="end" runat="server" />
                        </div>
                    </li>
                    <li role="presentation" class="tablist-item-input">
                        <div class="input-group" style="margin-top: -3px; margin-bottom: 2px;">
                            <input type="text" id="txtQuery" runat="server" class="form-control" placeholder="equipment number" maxlength="20" />
                            <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" OnClick="btQuery_Click" />
                            <span class="input-group-btn">
                                <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                            </span>
                        </div>
                    </li>
                </ul>

                <!-- Tab panes -->
                <div class="tab-content">
                    <div class="tab-content" style="overflow: scroll !important; height: 500px; width: 100% !important;">
                        <!--Equipment list-->
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="bg-primary" style="width: 30px;"></th>
                                    <th colspan="4" class="in-tab-title-rb bg-primary">Server Time<span id="spanTotalServerTime" runat="server"></span></th>
                                    <th colspan="4" class="in-tab-title-rb bg-primary">Work Time<span id="spanTotalWorkTime" runat="server"></span></th>
                                    <th colspan="2" class="in-tab-title-rb bg-primary">Equipment</th>
                                    <th colspan="2" class="in-tab-title-b bg-primary" style="text-align: left;">Content</th>
                                    <th class="in-tab-title-b bg-primary"></th>
                                </tr>
                                <tr>
                                    <th class="in-tab-title-b bg-warning" style="width: 50px;">#</th>
                                    <th class="in-tab-title-b bg-warning textoverflow" style="width: 50px; text-align: left;">服务器时间</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 50px; text-align: left;">间隔</th>
                                    <th class="in-tab-title-b bg-warning textoverflow" style="width: 50px; text-align: right;">小时差(秒)</th>
                                    <th class="in-tab-title-rb bg-warning" style="width: 50px; text-align: right;">补(分)</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 60px; text-align: right;">SMH</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 60px; text-align: right;">SMM</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 50px; text-align: right;">差(分)</th>
                                    <th class="in-tab-title-rb bg-warning" style="width: 50px; text-align: right;">补(分)</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 50px; text-align: left;">CMD</th>
                                    <th class="in-tab-title-rb bg-warning" style="width: 50px; text-align: left;">终端</th>
                                    <th class="in-tab-title-b bg-warning" style="width: 30px; text-align: center;">Eng</th>
                                    <th class="in-tab-title-b bg-warning textoverflow" style="width: 50px; text-align: left;">Full Data</th>
                                    <th class="in-tab-title-b bg-warning"></th>
                                </tr>
                            </thead>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td class="in-tab-title-b">1</td>
                                    <td class="in-tab-title-b textoverflow" style="text-align: left;">2016/06/26 13:00:02</td>
                                    <td class="in-tab-title-b textoverflow" style="text-align: left;">01:01:09</td>
                                    <td class="in-tab-title-b" style="text-align: right;">38</td>
                                    <td class="in-tab-title-rb" style="text-align: right;">1</td>
                                    <td class="in-tab-title-b" style="text-align: right;">1560.13</td>
                                    <td class="in-tab-title-b" style="text-align: right;">169860</td>
                                    <td class="in-tab-title-b" style="text-align: right;">30</td>
                                    <td class="in-tab-title-rb" style="text-align: right;">1</td>
                                    <td class="in-tab-title-b" style="text-align: left;">0x1000</td>
                                    <td class="in-tab-title-rb" style="text-align: left;">89007420</td>
                                    <td class="in-tab-title-b" style="text-align: center;"><span class="text-custom-success" title="Eng. On"><span class="signal cell-engine" style="font-size: 130%;"></span></span></td>
                                    <td class="in-tab-title-b" style="text-align: left;">010000000330360F68C5130400A1970200</td>
                                    <td class="in-tab-title-b"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <!--小模态框-->
        <div class="modal fade" id="modalWarning" tabindex="-1" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-danger">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong id="title">Warning</strong></h4>
                    </div>
                    <div class="modal-body">
                        Please input your query number first(min length: 4).<br />
                        请输入您要查询的设备号码（请至少提供4位长度的字符）。
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
    <script type="text/javascript">
        $(document).ready(function () {
            $(".input-daterange").each(function () {
                var inputs = $(this).children(".input-md");
                if (isStringNull($(inputs[0]).val())) {
                    var now = new Date();
                    now.setDate(1);
                    $(inputs[0]).val(now.pattern(_datepatternFMT));
                }
                if (isStringNull($(inputs[1]).val())) {
                    var now = new Date();
                    $(inputs[1]).val(now.pattern(_datepatternFMT));
                }
            }).datepicker({
                format: _datepickerFMT,
                weekStart: 0,
                autoclose: true
            });
            $("#query").click(function () {
                var number = $("#txtQuery").val();
                if (isStringNull(number) || number.length < 4) {
                    $("#modalWarning").modal("show");
                } else {
                    $("#tbodyBody").html("<tr><td colspan=\"13\"><img src=\"../images/loading_orange.gif\" />Loading data, please wait...</td></tr>");
                    $("#btQuery").click();
                }
            });
        });
    </script>
</body>
</html>
