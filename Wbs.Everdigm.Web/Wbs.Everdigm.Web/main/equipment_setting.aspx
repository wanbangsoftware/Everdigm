<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_setting.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Setting</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigatorContentPlaceHolder" runat="server">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs" role="tablist" id="functionBar">
        <li role="presentation">
            <a href="equipment_command.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Command</a>
        </li>
        <li role="presentation">
            <a href="equipment_security.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Security</a>
        </li>
        <li role="presentation">
            <a href="equipment_alarm.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Alarm</a>
        </li>
        <li role="presentation">
            <a href="equipment_position.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Map</a>
        </li>
        <li role="presentation">
            <a href="equipment_work.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Work</a>
        </li>
        <li role="presentation">
            <a href="equipment_as.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">A/S History</a>
        </li>
        <li role="presentation">
            <a href="equipment_storage.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Storage History</a>
        </li>
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Setting<span class="caret"></span></a>
        </li>
        <li role="presentation" style="float: right; cursor: pointer !important;" title="close">
            <a href="./equipment_inquiry.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">&times;</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <table class="table table-hover" style="margin-top: 2px !important;">
        <tr>
            <td colspan="4" class="alert-info">Change Equipment Information</td>
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
                        <input type="hidden" id="oldFunc" runat="server" />
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
                        <input class="form-control" runat="server" id="number" style="width: 150px;" placeholder="number" maxlength="5" />
                        <input type="hidden" runat="server" id="old" />
                    </li>
                    <li role="presentation" class="tablist-item-input">
                        <a><span id="fullNumber"></span></a>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="alert-info">Change Storage Information</td>
        </tr>
        <tr>
            <td colspan="4">
                <ul class="nav nav-tabs" style="border-bottom: 0px;" role="tablist">
                    <li role="presentation" class="dropdown" id="ddWarehouses">
                        <a id="dropWarehouses" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Warehouse:</span><span class="caret"></span>
                        </a>
                        <ul id="menuWarehouses" class="dropdown-menu" role="menu" aria-labelledby="dropWarehouses">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                        </ul>
                        <input type="hidden" id="hidWarehouse" runat="server" value="0" />
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="alert-info">Change Terminal Information</td>
        </tr>
        <tr id="terminalInfo" runat="server">
            <td class="popup-td">Number:
                <input type="hidden" runat="server" id="oldTerminal" />
                <input type="hidden" runat="server" id="newTerminal" />
            </td>
            <td class="popup-td"><a href="#bind">Click here to bind</a></td>
            <td class="popup-td">Sim card:</td>
            <td class="popup-td">-</td>
        </tr>
        <tr>
            <td colspan="4" class="popup-td">
                <asp:Button ID="btUnbind" runat="server" CssClass="hidden" Text="Unbind" OnClick="btUnbind_Click" />
                <asp:Button ID="btSaveInfo" runat="server" CssClass="hidden" Text="Save" OnClick="btSaveInfo_Click" />
                <asp:Button ID="btDelete" runat="server" CssClass="hidden" OnClick="btDelete_Click" />
                <button class="btn btn-primary" id="btSave" type="button"><span class="glyphicon glyphicon-floppy-open"></span><span> Save changes</span></button>
                <button class="btn btn-danger" id="btDel" type="button"><span class="glyphicon glyphicon-remove"></span><span> Delete equipment</span></button>
                <button class="btn btn-warning" type="button">Leave blank to avoid modification</button>
            </td>
        </tr>
    </table>

    <div class="modal fade" id="analyseModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-warning">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">Warning</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <span>Are you really wanna to unbind this equipment & terminal?</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="satWarning" type="button" class="btn btn-warning">
                        <span class="glyphicon glyphicon-ok"></span> Yes, DO it!
                    </button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="warningDelete" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-danger">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="deletelLabel">Caution</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <span>Are you really wanna to <strong>DELETE</strong> this equipment?</span><br />
                            <span>It's cannot be roll back, and terminal will unbind.</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="satDelete" type="button" class="btn btn-danger">
                        <span class="glyphicon glyphicon-ok"></span> Yes, DELETE it!
                    </button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../scripts/main/equipments.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.setting.js"></script>
</asp:Content>
