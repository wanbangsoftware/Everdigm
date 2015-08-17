<%@ Page Language="C#" MasterPageFile="~/main/EquipmentStorage.Master" AutoEventWireup="true" CodeBehind="equipment_in_storage.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_in_storage" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <li role="presentation" class="active"><a role="tab" data-toggle="tab">New Product</a></li>
    <li role="presentation"><a href="./equipment_rental_fleet.aspx">Rental Fleet</a></li>
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
    <li role="presentation" style="width: 100px; margin-top: 3px; padding-left: 5px;">
        <div class="input-group">
            <span class="input-group-btn">
                <button class="btn btn-primary" id="openModal" type="button"><span class="glyphicon glyphicon-floppy-open"></span><span> New Product</span></button>
            </span>
        </div>
        <!-- /input-group -->
    </li>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <tfoot>
        <tr>
            <td colspan="18">
                <div class="pagging" id="divPagging" runat="server">
                </div>
                <div class="clear"></div>
            </td>
        </tr>
    </tfoot>
    <tbody id="tbodyBody" runat="server">
        <tr>
            <td class="in-tab-txt-b">1</td>
            <td class="in-tab-txt-b">EX.</td>
            <td class="in-tab-txt-b textoverflow">DL215-9-21442</td>
            <td class="in-tab-txt-b">2430</td>
            <td class="in-tab-txt-b">OFF</td>
            <td class="in-tab-txt-b textoverflow">山东省烟台市</td>
            <td class="in-tab-txt-rb">W</td>
            <td class="in-tab-txt-b">2014-09-21</td>
            <td class="in-tab-txt-b">S</td>
            <td class="in-tab-txt-b">2014-10-22</td>
            <td class="in-tab-txt-b">S</td>
            <td class="in-tab-txt-rb textoverflow">Warehouse1</td>
            <td class="in-tab-txt-b">ON</td>
            <td class="in-tab-txt-b">
                <img src="../images/img_connect_gprs.png" /></td>
            <td class="in-tab-txt-b textoverflow">2014-09-22</td>
            <td class="in-tab-txt-b textoverflow">20140921221</td>
        </tr>
    </tbody>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderDialog" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolderFooter" runat="server">
    <script type="text/javascript" src="../scripts/main/equipment_in_storage.js"></script>
</asp:Content>
