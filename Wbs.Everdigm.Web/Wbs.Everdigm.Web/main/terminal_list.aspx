<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_list.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <link href="../css/right.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content">
            <input type="hidden" value="" id="cookieName" runat="server" />
            <div class="content-box">
                <div class="content-box-header">
                    <h3>Terminal: Terminal List</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">Satellite:
                                    <select runat="server" id="selSate">
                                        <option value="-1" selected="selected">Ignore</option>
                                        <option value="0">Not bind</option>
                                        <option value="1">Bound</option>
                                    </select>
                                    Equipment:
                                    <select runat="server" id="selBind">
                                        <option value="0" selected="selected">Not bind</option>
                                        <option value="1">Bound</option>
                                    </select>
                                    <input type="hidden" runat="server" id="hidJson" />
                                    <asp:DropDownList runat="server" ID="ddlEquipmentType"></asp:DropDownList>
                                    <select runat="server" id="selModel">
                                        <option value="">Model:</option>
                                    </select>
                                    Query of:
                                    <input type="text" id="txtNumber" class="text-input little-input" runat="server" />
                                    <input type="hidden" runat="server" id="hidPageIndex" />
                                    <asp:Button ID="btQuery" runat="server" Text="Query" CssClass="button" OnClick="btQuery_Click" />
                                    |
                                    <input type="button" id="btAdd" class="button" value="Add" />
                                    <input type="hidden" runat="server" id="hidID" />
                                    <input type="hidden" runat="server" id="hidBoundSatellite" />
                                    <asp:Button ID="btBoundSatellite" runat="server" CssClass="hidding" OnClick="btBoundSatellite_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="Delete" CssClass="button" OnClick="btDelete_Click" />
                                    <asp:Button ID="bt_Test" runat="server" Text="Set to test" CssClass="button" OnClick="bt_Test_Click" />
                                    <asp:Button ID="btUnbindEquipment" runat="server" CssClass="hidding" OnClick="btUnbindEquipment_Click" />
                                </td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" /></th>
                                    <th style="width: 40px; text-align: center;">ID</th>
                                    <th style="width: 100px;">Number</th>
                                    <th style="width: 180px;">Satellite</th>
                                    <th style="width: 70px;">Firmware</th>
                                    <th style="width: 30px; text-align: center;">Rev</th>
                                    <th style="width: 120px; text-align: center;">Type</th>
                                    <th style="width: 70px;">Register</th>
                                    <th style="width: 80px; text-align: center;">Has bound?</th>
                                    <th style="width: 140px;">Equipment</th>
                                    <th style="width: 70px;">Link</th>
                                    <th style="width: 100px;">Sim card</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="13">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                            </tbody>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1. Click Equipment number(if exist) to unbind terminal.<br />
                            2. Click Add to bind satellite device.
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/terminals.js"></script>
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
            changeBind($("#selBind").getSelectedIndex());
            $("#selBind").change(function () {
                var sel = $(this).getSelectedIndex();
                changeBind(sel);
            });
            $("#ddlEquipmentType").change(function () {
                var selTypeId = $(this).val();
                $("#selModel").clearAll();
                $("#selModel").addOption("Model:", "");
                if (!isStringNull(selTypeId)) {
                    var models = eval("(" + $("#hidJson").val() + ")");
                    $(models).each(function (index, obj) {
                        if (obj.Type == selTypeId) {
                            $("#selModel").addOption(obj.Name, obj.id);
                        }
                    });
                }
            });
        });
        function changeBind(bind) {
            if (bind == 0) {
                $("#ddlEquipmentType").hide();
                $("#selModel").hide();
            } else {
                $("#ddlEquipmentType").show();
                $("#selModel").show();
            }
        }
    </script>
</body>
</html>
