﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_command.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_command" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Command</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigatorContentPlaceHolder" runat="server">
    <!-- Nav tabs -->

    <ul class="nav nav-tabs" role="tablist" id="functionBar">
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Command<span class="caret"></span>
            </a>
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
        <li role="presentation" style="float: right; cursor: pointer !important;" title="close">
            <a href="./equipment_inquiry.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">&times;</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Send Command-->
    <div class="panel panel-default" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Send command</span>
            <ul class="nav nav-tabs" role="tablist" id="commandBar" style="float: right; margin-top: -11px;">
                <li role="presentation" class="dropdown active">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>Select command:</span><span class="caret"></span></a>
                    <ul id="menuCommands" runat="server" class="dropdown-menu" role="menu" aria-labelledby="dropTypes">
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#">No Items</a></li>
                    </ul>
                    <input type="hidden" id="cmdInfo" value="" />
                </li>
                <li role="presentation" style="margin-top: 4px;">
                    <button class="btn btn-info" type="button"><span class="glyphicon glyphicon-repeat"></span> Send</button>
                </li>
            </ul>
        </div>
        <div class="panel-body">
            <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; margin-bottom: 0px !important; height: 150px; overflow: auto;">
                11:22:04 <code>position data</code> Command is waiting in send queue.<br />
                11:22:05 <code>0x1000</code> Command has been send to target.<br />
                11:22:09 <code>0x1000</code> Target received the command.<br />
                11:22:20 <code>0x1000</code> Command responsed successfully, you can <code>Analyse</code> this data by click <code>here</code>.<br />
                11:22:38 <code>0xDD00</code> Command has been send to target.<br />
                11:22:40 <code>0xDD00</code> Target is not online.<br />
            </div>
        </div>
    </div>
    <!--Command History-->
    <div class="panel panel-default" style="margin-top: 2px;">
        <div class="panel-heading">
            <span>Command History</span>
            <div class="input-group" style="float: right; margin-top: -7px;">
                <select style="float: left; width: 200px;" class="form-control">
                    <option>Select command:</option>
                </select>
                <div class="input-daterange input-group" id="datepicker" style="float: left; margin-left: 2px;">
                    <input type="text" class="input-md form-control little-input click-input" runat="server" id="start" name="start" />
                    <span class="input-group-addon">to </span>
                    <input type="text" class="input-md form-control little-input click-input" runat="server" id="end" name="end" />
                </div>
                <span class="input-group-btn" style="float: left;">
                    <button class="btn btn-success" type="button">Query</button>
                </span>
            </div>
        </div>
        <div class="panel-body">
            <div class="bs-callout" style="margin-top: 2px; margin-bottom: 0px; height: 150px; overflow: auto;">
                Loading data...
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/jquery.timer.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.command.js"></script>
</asp:Content>
