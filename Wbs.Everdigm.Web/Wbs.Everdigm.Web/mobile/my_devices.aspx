<%@ Page Title="" Language="C#" MasterPageFile="~/mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="my_devices.aspx.cs" Inherits="Wbs.Everdigm.Web.mobile.my_devices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <link href="../bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/links.css" rel="stylesheet" />
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolderLeft" runat="server">
    <a href="javascript:history.go(-1);" class="backto"><span class="glyphicon glyphicon-arrow-left"></span></a>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="TitleContentPlaceHolderRight" runat="server">
    <a href="javascript:location.reload();" class="forwordto"><span class="glyphicon glyphicon-refresh"></span></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="nav">
        <ul class="items">
            <li style="width: 100% !important;"><a class="current" id="account" runat="server">Wecome Name!</a></li>
        </ul>
    </div>
    <div id="equipmentItems" runat="server">
        <dl class="invest-type" id="0">
            <dt>
                <span class="iconleft">
                    <img class="img-rounded ex" src="../images/excavator.png">
                </span>DX225LC-20037
            <em class="status">Excavator</em>
            </dt>
            <dd>
                <span class="text-success"><span class="glyphicon glyphicon-ok-circle"></span>Engine On</span>
                <em class="status"><span class="glyphicon glyphicon-time"></span>224:32(Hr)</em>
            </dd>
            <dd>
                <span class="text-warning"><span class="glyphicon glyphicon-signal"></span>CSQ: 15(rssi: -113dBm)</span>
                <div class="total-num"><span class="label label-info">TCP</span></div>
            </dd>
            <dd class="desc"><span class="glyphicon glyphicon-globe"></span>山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
        </dl>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FootContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/device.list.js"></script>
</asp:Content>
