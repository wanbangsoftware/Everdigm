<%@ Page Title="" Language="C#" MasterPageFile="~/main/EquipmentInfo.Master" AutoEventWireup="true" CodeBehind="equipment_work.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_work" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <link href="../bootstrap3/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
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
    <script type="text/javascript">
        var MacId = "<%=MacId%>";
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">Equipment: Work</asp:Content>
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
        <li role="presentation" class="active">
            <a href="#" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Work<span class="caret"></span></a>
        </li>
        <li role="presentation">
            <a href="equipment_as.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">A/S History</a>
        </li>
        <li role="presentation">
            <a href="equipment_storage.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Storage History</a>
        </li>
        <li role="presentation">
            <a href="equipment_setting.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">Setting</a>
        </li>
        <li role="presentation" style="float: right; cursor: pointer !important;" title="close">
            <a href="./equipment_inquiry.aspx" class="dropdown-toggle" aria-haspopup="true" aria-expanded="false">&times;</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!--Daily working time-->
    <div class="panel panel-default" id="printDailyWorkTime" style="margin-top: 2px; margin-bottom: 2px;">
        <div class="panel-heading">
            <span>Daily working time</span>(recently 10 days)
            <span id="toExcel" class="label label-primary" style="cursor: pointer;"><i class="fa fa-file-excel-o"></i> to Excel</span>
            <span id="printLabel" class="label label-primary" style="cursor: pointer;"><span class="glyphicon glyphicon-print" aria-hidden="true"></span> Print</span>
            <input type="hidden" id="hiddenLastDate" runat="server" />
            <div class="input-group" style="float: right; margin-top: -7px;">
                <div class="input-daterange input-group" style="float: left; margin-left: 2px;">
                    <input type="text" class="input-md form-control little-input click-input" id="start" />
                    <span class="input-group-addon">to </span>
                    <input type="text" class="input-md form-control little-input click-input" id="end" />
                    <span class="input-group-btn" style="float: left;">
                        <button class="btn btn-success" type="button">Query</button>
                    </span>
                </div>
            </div>
        </div>
        <div class="panel-body" style="min-height: 120px;">
            <div id="runtimeChart" style="height: 300px; width: 100%;">Loading data...</div>
        </div>
    </div>
    <!--Position History-->
    <div class="panel panel-default" style="margin-top: 2px;" id="divWorkTime" runat="server">
        <div class="panel-heading">
            <span>Work time: </span><span id="reportTime"></span>
        </div>
        <div class="panel-body" style="min-height: 120px;">
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th colspan="4" class="alert-info">Total working time</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><span id="lblTotalHour">0</span></td>
                            <td style="width: 60px;">hours</td>
                            <td><span id="lblTotalMin">0</span></td>
                            <td style="width: 60px;">minutes</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th colspan="6" class="alert-info">Walk mode(Track)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">I</td>
                            <td id="imgTravelSpeed1" class="per">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblTravelSpeedHour1">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblTravelSpeedMin1">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td>II</td>
                            <td class="per" id="imgTravelSpeed2">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td>
                                <span id="lblTravelSpeedHour2">0</span>
                            </td>
                            <td>h</td>
                            <td>
                                <span id="lblTravelSpeedMin2">0</span>
                            </td>
                            <td>m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th colspan="6" class="alert-info">Walk mode(Wheeled)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">Work</td>
                            <td class="per" id="imgWorkingTime">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour"><span id="lblWorkingTimeHour">0</span></td>
                            <td class="h">h</td>
                            <td class="minute"><span id="lblWorkingTimeMin">0</span></td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td>Travel</td>
                            <td class="per" id="imgTravelTime">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td><span id="lblTravelTimeHour">0</span></td>
                            <td>h</td>
                            <td><span id="lblTravelTimeMin">0</span></td>
                            <td>m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th colspan="6" class="alert-info">Auto Idle</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">ON</td>
                            <td class="per" id="imgAutoIdlemode">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblAutoIdlemodeHour">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblAutoIdlemodeMin">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th colspan="6" class="alert-info">Power mode</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name" title="Power">PWR</td>
                            <td class="per" id="imgPowerMode1">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblPowerModeHour1">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblPowerModeMin1">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td title="Standard">STD</td>
                            <td class="per" id="imgPowerMode2">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblPowerModeHour2">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblPowerModeMin2">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th class="alert-info" colspan="6">Work mode</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">Grub</td>
                            <td class="per" id="imgWorkMode1">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblWorkModeHour1">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblWorkModeMin1">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">Trench</td>
                            <td class="per" id="imgWorkMode2">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblWorkModeHour2">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblWorkModeMin2">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th class="alert-info" colspan="6">RPM 1700</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">&uarr;</td>
                            <td class="per" id="imgEngSpeed1">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblEngSpeedHour1">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblEngSpeedMin1">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">&darr;</td>
                            <td class="per" id="imgEngSpeed2">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblEngSpeedHour2">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblEngSpeedMin2">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th class="alert-info" colspan="6">Hyd oil temperature</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">96&#176;C+</td>
                            <td class="per" id="imgOprHydoiltemp6">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprHydoiltempHour6">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprHydoiltempMin6">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">86~95<br>
                            </td>
                            <td class="per" id="imgOprHydoiltemp5">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprHydoiltempHour5">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprHydoiltempMin5">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">76~85</td>
                            <td class="per" id="imgOprHydoiltemp4">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprHydoiltempHour4">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprHydoiltempMin4">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">51~75</td>
                            <td class="per" id="imgOprHydoiltemp3">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprHydoiltempHour3">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprHydoiltempMin3">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">31~50</td>
                            <td class="per" id="imgOprHydoiltemp2">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprHydoiltempHour2">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprHydoiltempMin2">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">30&#176;C-</td>
                            <td class="per" id="imgOprHydoiltemp1">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprHydoiltempHour1">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprHydoiltempMin1">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4">
                <table class="tbtb">
                    <thead>
                        <tr>
                            <th class="alert-info" colspan="6">Coolant temperature</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="name">106&#176;C</td>
                            <td class="per" id="imgOprWateremp6">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprWaterempHour6">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprWaterempMin6">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">96~105<br>
                            </td>
                            <td class="per" id="imgOprWateremp5">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprWaterempHour5">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprWaterempMin5">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">86~95</td>
                            <td class="per" id="imgOprWateremp4">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprWaterempHour4">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprWaterempMin4">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">61~85</td>
                            <td class="per" id="imgOprWateremp3">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprWaterempHour3">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprWaterempMin3">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">41~60</td>
                            <td class="per" id="imgOprWateremp2">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprWaterempHour2">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprWaterempMin2">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                        <tr>
                            <td class="name">40&#176;C</td>
                            <td class="per" id="imgOprWateremp1">
                                <div class="progress pers">
                                    <div class="progress-bar bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    </div>
                                </div>
                            </td>
                            <td class="hour">
                                <span id="lblOprWaterempHour1">0</span>
                            </td>
                            <td class="h">h</td>
                            <td class="minute">
                                <span id="lblOprWaterempMin1">0</span>
                            </td>
                            <td class="m">m</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="warningLoading" tabindex="-1" role="dialog" aria-labelledby="deletelLabel" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="warningLabel">Loading...</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12" id="loadingContent">
                            <img alt="" src="../images/loading.gif"/><span style="margin-left: 10px;">Loading data, please wait...</span>
                        </div>
                        <div class="col-lg-12" id="warningContent">
                            <span style="margin-left: 10px;" id="warningContentText">Loading data, please wait...</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/CanvasJS/canvasjs.min.js"></script>
    <script src="../js/html2canvas.min.js"></script>
    <script type="text/javascript" src="../scripts/main/equipment.work.js"></script>
</asp:Content>
