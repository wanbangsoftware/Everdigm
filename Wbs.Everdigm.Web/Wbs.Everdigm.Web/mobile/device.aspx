﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="device.aspx.cs" Inherits="Wbs.Everdigm.Web.mobile.device" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <a href="javascript:history.go(-1);" class="backto"><span class="glyphicon glyphicon-arrow-left"></span></a>
    <div class="reg-log">Everdigm</div>
    <a href="javascript:location.reload();" class="forwordto"><span class="glyphicon glyphicon-refresh"></span></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="nav">
        <ul class="items">
            <li><a class="current">Device information</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FootContentPlaceHolder" runat="server">
</asp:Content>
