<%@ Page Language="C#" MasterPageFile="~/main/EquipmentStorage.Master" AutoEventWireup="true" CodeBehind="equipment_rental_fleet.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_rental_fleet" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <li role="presentation"><a href="./equipment_in_storage.aspx">New Product</a></li>
    <li role="presentation" class="active"><a>Rental Fleet</a></li>
    <li role="presentation"><a href="./equipment_change_warehouse.aspx">Change Warehouse</a></li>
    <li role="presentation" class="dropdown" id="ddTypes">
        <a id="dropTypes" href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Type:</span><span class="caret"></span>
        </a>
        <ul id="menuTypes" class="dropdown-menu" role="menu" aria-labelledby="dropTypes">
            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
        </ul>
        <input type="hidden" id="selectedTypes" runat="server" value="0" />
        <input type="hidden" value="" id="cookieName" runat="server" />
        <input type="hidden" runat="server" value="0" id="hidPageIndex" />
        <input type="hidden" runat="server" id="hidTotalPages" value="0" />
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
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <tfoot>
        <tr>
            <td colspan="19">
                <div class="pagging" id="divPagging" runat="server">
                </div>
                <div class="clear"></div>
            </td>
        </tr>
    </tfoot>
    <tbody id="tbodyBody" runat="server">
        
    </tbody>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderDialog" runat="server">
    <!--小模态框-二手/租赁入库界面-->
    <div class="modal fade" id="modalOldProduct" tabindex="-1" role="dialog" aria-labelledby="NewStorageIn" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header bg-success">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title"><strong>Rental Fleet storage: </strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12 show-grid">
                            <div class="col-sm-12" style="margin-top: 3px;">
                                <table class="table table-hover">
                                    <tbody id="oldEquipmentInfo">
                                        <tr>
                                            <td class="popup-td right">Equipment:</td>
                                            <td class="popup-td"></td>
                                            <td class="popup-td" colspan="2">
                                                <input type="checkbox" id="cbRepair" runat="server" />Need nspection & repair</td>
                                        </tr>
                                        <tr>
                                            <td class="popup-td right">Status:</td>
                                            <td class="popup-td"></td>
                                            <td class="popup-td right">Functional:</td>
                                            <td class="popup-td"></td>
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
                                                    <input type="hidden" runat="server" id="hiddenOldWarehouse" />
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
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btSaveOldInStorage" CssClass="hidden" runat="server" Text="Button" OnClick="btSaveOldInStorage_Click" />
                    <input type="hidden" id="hidOldInstorage" value="" runat="server" />
                    <button type="button" class="btn btn-success disabled" id="oldInStorageSave"><span class="glyphicon glyphicon-ok"></span>Store in!</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderFooter" runat="server">
    <script type="text/javascript" src="../scripts/main/equipment.rental.fleet.js"></script>
</asp:Content>
