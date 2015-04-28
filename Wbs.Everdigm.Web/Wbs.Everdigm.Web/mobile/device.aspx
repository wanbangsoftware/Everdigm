<%@ Page Title="" Language="C#" MasterPageFile="~/mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="device.aspx.cs" Inherits="Wbs.Everdigm.Web.mobile.device" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <link href="../bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <a href="javascript:history.go(-1);" class="backto"><span class="glyphicon glyphicon-arrow-left"></span></a>
    <div class="reg-log">Everdigm Mobile</div>
    <a href="javascript:location.reload();" class="forwordto"><span class="glyphicon glyphicon-refresh"></span></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="nav">
        <ul class="items">
            <li><a class="current" id="equipmentId" runat="server">DH300LCA-20002</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FootContentPlaceHolder" runat="server">
</asp:Content>
