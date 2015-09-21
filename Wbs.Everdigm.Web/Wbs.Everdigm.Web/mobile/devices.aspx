<%@ Page Title="" Language="C#" MasterPageFile="~/mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="devices.aspx.cs" Inherits="Wbs.Everdigm.Web.mobile.devices" %>

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
    <input type="hidden" id="_id" runat="server" />
    <asp:Button ID="Submit" CssClass="hidden" OnClick="Submit_Click" runat="server" />
    <div class="nav">
        <ul class="items">
            <li><a class="current">Device list</a></li>
        </ul>
    </div>
    <dl class="invest-type" data-url="#1">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_hex.png">
            </span>DX225LC-20037
            <em class="status">Excavator</em>
        </dt>
        <dd>
            <span class="text-success"><span class="signal cell-engine"></span></span>
            <span class="text-light-gray"><span class="signal cell-engine"></span></span>
            <span class="text-custom-warning"><span class="glyphicon glyphicon-lock"></span></span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 224 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="signal cell-signal-3"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-info">tcp</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
    <dl class="invest-type" data-url="#2">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_adt.png">
            </span>DX225LC-20036
            <em class="status">Ariticulated Dump Truck</em>
        </dt>
        <dd>
            <span class="text-success"><span class="glyphicon glyphicon-ok-circle"></span> Engine On</span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 324 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="glyphicon glyphicon-signal"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-success">udp</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
    <dl class="invest-type" data-url="#3">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_cpt.png">
            </span>DX225LC-20035
            <em class="status">Concrete Pump Track</em>
        </dt>
        <dd>
            <span class="text-success"><span class="glyphicon glyphicon-ok-circle"></span> Engine On</span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 424 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="glyphicon glyphicon-signal"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-warning">sms</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
    <dl class="invest-type" data-url="#4">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_drl.png">
            </span>DX225LC-20034
            <em class="status">Rock Drill</em>
        </dt>
        <dd>
            <span class="text-danger"><span class="glyphicon glyphicon-ok-circle"></span> Engine Off</span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 524 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="glyphicon glyphicon-signal"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-primary">satellite</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
    <dl class="invest-type" data-url="#5">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_fl.png">
            </span>DX225LC-20033
            <em class="status">Fork Lift</em>
        </dt>
        <dd>
            <span class="text-success"><span class="glyphicon glyphicon-ok-circle"></span> Engine On</span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 624 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="glyphicon glyphicon-signal"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-danger">blind</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
    <dl class="invest-type disabled" data-url="#6">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_cdrl.png">
            </span>DX225LC-20032
            <em class="status">Core Drill</em>
        </dt>
        <dd>
            <span class="text-danger"><span class="glyphicon glyphicon-ok-circle"></span> Engine Off</span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 724 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="glyphicon glyphicon-signal"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-default">off</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
    <dl class="invest-type" data-url="#7">
        <dt>
            <span class="iconleft">
                <img class="img-rounded ex" src="../images/equipments/icon_wld.png">
            </span>DX225LC-20031
            <em class="status">Wheel Loader</em>
        </dt>
        <dd>
            <span class="text-danger"><span class="glyphicon glyphicon-ok-circle"></span> Engine Off</span>
            <em class="status"><span class="glyphicon glyphicon-time"></span> 1724 hr 32 min</em>
        </dd>
        <dd>
            <span class="text-warning"><span class="glyphicon glyphicon-signal"></span> rssi: 15(-113dBm)</span>
            <div class="total-num"><span class="label label-danger">trouble</span></div>
        </dd>
        <dd class="desc"><span class="glyphicon glyphicon-globe"></span> 山东省烟台市开发区衡山路1号老咔叽卡级的发1的离开对方</dd>
    </dl>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FootContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/device.list.js"></script>
</asp:Content>
