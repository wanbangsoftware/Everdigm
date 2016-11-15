<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_list.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_list" %>

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

        .satellite:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <input type="hidden" value="" id="cookieName" runat="server" />
            <input type="hidden" runat="server" id="hidTotalPages" value="0" />
            <input type="hidden" runat="server" id="hidPageIndex" />
            <input type="hidden" runat="server" id="hidJson" />
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>Terminal: Terminal List</strong>
                <span id="toExcel" class="label label-primary" style="cursor: pointer;"><i class="fa fa-file-excel-o"></i> Export all terminals to Excel</span>
            </div>
            <div class="panel-body">
                <!-- Tab panes -->
                <ul class="nav nav-tabs" role="tablist" id="queryBar">
                    <li role="presentation" class="active dropdown" id="ddSatellite">
                        <a id="dropSatellite" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span id="spanSatellite" runat="server">Satellite:</span><span class="caret"></span>
                        </a>
                        <ul id="menuSatellite" class="dropdown-menu" role="menu" aria-labelledby="dropSatellite">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Ignore</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="0" href="#">Not bind</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="1" href="#">Bound</a></li>
                        </ul>
                        <input type="hidden" id="selectedSatellite" runat="server" value="-1" />
                    </li>
                    <li role="presentation" class="dropdown" id="ddEquipment">
                        <a id="dropEquipment" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span id="spanEquipment" runat="server">Equipment:</span><span class="caret"></span>
                        </a>
                        <ul id="menuEquipment" class="dropdown-menu" role="menu" aria-labelledby="dropEquipment">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Ignore</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="0" href="#">Not bind</a></li>
                            <li role="presentation"><a role="menuitem" tabindex="1" href="#">Bound</a></li>
                        </ul>
                        <input type="hidden" id="selectedEquipment" runat="server" value="-1" />
                    </li>
                    <li role="presentation" class="dropdown hidden" id="ddEquipmentType">
                        <a id="dropEquipmentType" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Equipment Type:</span><span class="caret"></span>
                        </a>
                        <ul id="menuEquipmentType" runat="server" class="dropdown-menu" role="menu" aria-labelledby="dropEquipmentType">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="hidEquipmentType" runat="server" value="0" />
                    </li>
                    <li role="presentation" class="tablist-item-input">
                        <div class="input-group">
                            <input type="text" id="txtNumber" runat="server" class="form-control" placeholder="query number" />
                            <asp:Button ID="btQuery" CssClass="hidden" runat="server" Text="Query" Width="0" Height="0" OnClick="btQuery_Click" />
                            <span class="input-group-btn">
                                <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                            </span>
                        </div>
                    </li>
                    <li role="presentation" class="tablist-item-input" style="margin-left: 5px !important;">
                        <button class="btn btn-primary" type="button" id="btAdd"><span class="glyphicon glyphicon-plus"></span>Add</button>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-content">
                        <table class="table table-hover hidden">
                            <tr>
                                <td class="table_header">
                                    <input type="hidden" runat="server" id="hidID" />
                                    <input type="hidden" runat="server" id="hidBoundSatellite" />
                                    <asp:Button ID="btBoundSatellite" runat="server" Text="Bind Satellite" CssClass="hidding" OnClick="btBoundSatellite_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="Delete" CssClass="button" OnClick="btDelete_Click" />
                                    <asp:Button ID="bt_Test" runat="server" Text="Set to test" CssClass="button" OnClick="bt_Test_Click" />
                                    <asp:Button ID="btUnbindEquipment" runat="server" Text="Unbind" CssClass="hidding" OnClick="btUnbindEquipment_Click" />
                                </td>
                            </tr>
                        </table>

                        <!--<th style="width: 40px; text-align: center;" class="in-tab-title-rb bg-primary"><input type="checkbox" id="cbAll" /></th>-->
                        <table id="tbTable" class="table table-hover">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;" class="in-tab-title-rb bg-primary">#</th>
                                    <th style="width: 100px; text-align: left;" class="in-tab-title-rb bg-primary">Number</th>
                                    <th style="width: 180px; text-align: left;" class="in-tab-title-rb bg-primary">Satellite</th>
                                    <th style="width: 70px;" class="in-tab-title-rb bg-primary">Firmware</th>
                                    <th style="width: 30px; text-align: center;" class="in-tab-title-rb bg-primary">Rev</th>
                                    <th style="width: 120px; text-align: left;" class="in-tab-title-rb bg-primary">Type</th>
                                    <th style="width: 70px;" class="in-tab-title-rb bg-primary">Register</th>
                                    <th style="width: 80px; text-align: center;" class="in-tab-title-rb bg-primary">Bound?</th>
                                    <th style="width: 140px;" class="in-tab-title-rb bg-primary">Equipment</th>
                                    <th style="width: 70px;" class="in-tab-title-b bg-primary">Link</th>
                                    <!--th style="width: 100px;" class="in-tab-title-b bg-primary">Sim card</!--th>-->
                                    <th class="in-tab-title-rb bg-primary"></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="11">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBodies" runat="server">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="warningLoading" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-warning">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="warningLabel">Warning!</h4>
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
        <div class="modal fade" id="warningUnbinding" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-warning">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Unbind satellite model & terminal!</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <span style="margin-left: 10px;" id="warningUnbindingContentText"></span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="satUnbind" type="button" class="btn btn-danger">
                            <span class="glyphicon glyphicon-ok"></span> Yes, UNBIND them!
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="warningStopping" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-danger">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Temporarily stop using this satellite model</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <span style="margin-left: 10px;" id="warningStoppingContentText"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <span style="margin-left: 10px; font-style: italic;">The data link will be resume when you re-use it again.</span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSatelliteStopping" runat="server" CssClass="hidden" OnClick="btnSatelliteStopping_Click" />
                        <button id="satStopping" type="button" class="btn btn-danger">
                            <span class="glyphicon glyphicon-ok"></span> Yes, STOP it!
                        </button>
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
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script src="../scripts/main/export.to.excel.js"></script>
    <script type="text/javascript" src="../scripts/main/terminals.js?d=201611151030"></script>
    <script type="text/javascript">
        //得到select项的个数   
        jQuery.fn.size = function () {
            return jQuery(this).get(0).options.length;
        }

        //获得选中项的索引   
        jQuery.fn.getSelectedIndex = function () {
            return jQuery(this).get(0).selectedIndex;
        }

        //获得当前选中项的文本   
        jQuery.fn.getSelectedText = function () {
            if (this.size() == 0) return "下拉框中无选项";
            else {
                var index = this.getSelectedIndex();
                return jQuery(this).get(0).options[index].text;
            }
        }

        //获得当前选中项的值   
        jQuery.fn.getSelectedValue = function () {
            if (this.size() == 0)
                return "下拉框中无选中值";

            else
                return jQuery(this).val();
        }

        //设置select中值为value的项为选中   
        jQuery.fn.setSelectedValue = function (value) {
            jQuery(this).get(0).value = value;
        }

        //设置select中文本为text的第一项被选中   
        jQuery.fn.setSelectedText = function (text) {
            var isExist = false;
            var count = this.size();
            for (var i = 0; i < count; i++) {
                if (jQuery(this).get(0).options[i].text == text) {
                    jQuery(this).get(0).options[i].selected = true;
                    isExist = true;
                    break;
                }
            }
            if (!isExist) {
                alert("下拉框中不存在该项");
            }
        }
        //设置选中指定索引项   
        jQuery.fn.setSelectedIndex = function (index) {
            var count = this.size();
            if (index >= count || index < 0) {
                alert("选中项索引超出范围");
            }
            else {
                jQuery(this).get(0).selectedIndex = index;
            }
        }
        //判断select项中是否存在值为value的项   
        jQuery.fn.isExistItem = function (value) {
            var isExist = false;
            var count = this.size();
            for (var i = 0; i < count; i++) {
                if (jQuery(this).get(0).options[i].value == value) {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
        //向select中添加一项，显示内容为text，值为value,如果该项值已存在，则提示   
        jQuery.fn.addOption = function (text, value) {
            if (this.isExistItem(value)) {
                alert("待添加项的值已存在");
            }
            else {
                jQuery(this).get(0).options.add(new Option(text, value));
            }
        }
        //删除select中值为value的项，如果该项不存在，则提示   
        jQuery.fn.removeItem = function (value) {
            if (this.isExistItem(value)) {
                var count = this.size();
                for (var i = 0; i < count; i++) {
                    if (jQuery(this).get(0).options[i].value == value) {
                        jQuery(this).get(0).remove(i);
                        break;
                    }
                }
            }
            else {
                alert("待删除的项不存在!");
            }
        }
        //删除select中指定索引的项   
        jQuery.fn.removeIndex = function (index) {
            var count = this.size();
            if (index >= count || index < 0) {
                alert("待删除项索引超出范围");
            }
            else {
                jQuery(this).get(0).remove(index);
            }
        }
        //删除select中选定的项   
        jQuery.fn.removeSelected = function () {
            var index = this.getSelectedIndex();
            this.removeIndex(index);
        }
        //清除select中的所有项   
        jQuery.fn.clearAll = function () {
            jQuery(this).get(0).options.length = 0;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#hidPageIndex").val($.cookie($("#cookieName").val()));

            $("#btQuery").click(function () { $.cookie($("#cookieName").val(), $("#hidPageIndex").val()); });

            $("#query").click(function () { $("#btQuery").click(); });

            $("#toExcel").click(function () {
                // 导出全部终端到excel供下载
                exportToExcel({ "type": "equipment", "cmd": "terminals2excel" });
            });
            // 下拉菜单选择事件
            $("[id^=\"dd\"] ul").on("click", "li a", (function () {
                var index = $(this).prop("tabindex");
                //if (index > -1) {
                var title = $(this).parent().parent().prev().children("span:eq(0)");
                var text = $(title).text();
                text = text.substr(0, text.indexOf(":") + 1);
                $(title).text(text + $(this).text());
                //$(this).parent().parent().prev().children("span:eq(0)").text($(this).text());
                $(this).parent().parent().next().val(index);
                //}
            }));
        });
    </script>
</body>
</html>
