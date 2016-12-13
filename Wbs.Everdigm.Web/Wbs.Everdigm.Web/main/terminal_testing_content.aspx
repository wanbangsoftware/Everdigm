<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_testing_content.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_testing_content" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <style type="text/css">
        .custom-modal-header {
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
        html { overflow-x:hidden; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="alert alert-warning" style="padding: 5px !important;" role="alert">
            You are testing terminal 
            <strong id="terminalInfo" runat="server" style="cursor: pointer;" data-toggle="popover" data-placement="bottom" data-trigger="focus"></strong>&nbsp;now. Command send type: 
            <input type="hidden" id="terminalContent" runat="server" />
            <input type="hidden" id="terminalCardNumber" runat="server" />
            <div class="btn-group" data-toggle="buttons">
                <label class="btn btn-warning active">
                    <input type="radio" name="options" value="normal" autocomplete="off" checked />
                    Normal (auto detect)
                </label>
                <label class="btn btn-warning">
                    <input type="radio" name="options" value="sms" autocomplete="off" />
                    Force to SMS
                </label>
            </div>
            <button type="button" id="printLabel" style="float: right;" data-loading-text="Printing..." class="btn btn-primary" autocomplete="off">
                Print Label
            </button>
        </div>
        <div class="bs-callout bs-callout-info" style="font-size: 12px !important; margin-bottom: 5px !important; margin-top: -15px; height: 120px; overflow: auto;">
            <code>history data</code> will display in here.<br />
        </div>
        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingOne">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Retrieve GPS Position
                        </a>
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                    <div class="panel-body">
                        <button type="button" id="bt_position" data-loading-text="Testing GPS position retrieve progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Position info
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingTwo">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">Retrieve GSM Signal
                        </a>
                    </h4>
                </div>
                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                    <div class="panel-body">
                        <button type="button" id="bt_signal" data-loading-text="Testing GSM signal retrieve progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            GSM signal
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingFifteen">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFifteen" aria-expanded="false" aria-controls="collapseFifteen">Retrieve Worktime(Loader/Electric)
                        </a>
                    </h4>
                </div>
                <div id="collapseFifteen" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFifteen">
                    <div class="panel-body">
                        <button type="button" id="bt_ld_worktime" data-loading-text="Testing [worktime retrieve] progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Retrieve Worktime(Loader/Electric)
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingTwelve">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwelve" aria-expanded="false" aria-controls="collapseTwelve">Security: Lock(Loader/Electric)
                        </a>
                    </h4>
                </div>
                <div id="collapseTwelve" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwelve">
                    <div class="panel-body">
                        <button type="button" id="bt_ldlock" data-loading-text="Testing reset to satellite progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Lock(Loader/Electric)
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingThirteen">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThirteen" aria-expanded="false" aria-controls="collapseThirteen">Security: Unlock(Loader/Electric)
                        </a>
                    </h4>
                </div>
                <div id="collapseThirteen" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThirteen">
                    <div class="panel-body">
                        <button type="button" id="bt_ldunlock" data-loading-text="Testing [security unlock] progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Unlock(Loader/Electric)
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingFourteen">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFourteen" aria-expanded="false" aria-controls="collapseFourteen">Security: Worktime Initialize(Loader/Electric)
                        </a>
                    </h4>
                </div>
                <div id="collapseFourteen" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFourteen">
                    <div class="panel-body">
                        <button type="button" id="bt_ld_initial" data-loading-text="Testing [worktime initialize] progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Worktime Initialize(Loader/Electric)
                        </button>
                    </div>
                </div>
            </div>
            <!--
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingThree">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">Monitor data
                        </a>
                    </h4>
                </div>
                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                    <div class="panel-body">
                        <button type="button" id="bt_monitor" data-loading-text="Testing monitor data retrieve progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Monitor data
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingFour">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFour" aria-expanded="false" aria-controls="collapseFour">Security: Enable/Unlock
                        </a>
                    </h4>
                </div>
                <div id="collapseFour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFour">
                    <div class="panel-body">
                        <button type="button" id="bt_enable" data-loading-text="Testing security enable/unlock progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Enable/Unlock
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingFive">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFive" aria-expanded="false" aria-controls="collapseFive">Security: Disable
                        </a>
                    </h4>
                </div>
                <div id="collapseFive" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFive">
                    <div class="panel-body">
                        <button type="button" id="bt_disable" data-loading-text="Testing security disable progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Disable
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingSix">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSix" aria-expanded="false" aria-controls="collapseSix">Security: Partial lock
                        </a>
                    </h4>
                </div>
                <div id="collapseSix" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingSix">
                    <div class="panel-body">
                        <button type="button" id="bt_custom" data-loading-text="Testing partial lock progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Partial lock
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingSeven">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSeven" aria-expanded="false" aria-controls="collapseSeven">Security: Lock
                        </a>
                    </h4>
                </div>
                <div id="collapseSeven" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingSeven">
                    <div class="panel-body">
                        <button type="button" id="bt_full" data-loading-text="Testing full lock progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Security: Lock
                        </button>
                    </div>
                </div>
            </div>-->
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingEight">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseEight" aria-expanded="false" aria-controls="collapseEight">Security: Reset to satellite(Iridium)
                        </a>
                    </h4>
                </div>
                <div id="collapseEight" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingEight">
                    <div class="panel-body">
                        <button type="button" id="bt_reset_sat" data-loading-text="Testing reset to satellite progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Reset to satellite
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingNine">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseNine" aria-expanded="false" aria-controls="collapseNine">Security: Satellite enable
                        </a>
                    </h4>
                </div>
                <div id="collapseNine" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingNine">
                    <div class="panel-body">
                        <button type="button" id="bt_satenable" data-loading-text="Testing satellite enable progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Satellite enable
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingTen">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTen" aria-expanded="false" aria-controls="collapseTen">Security: Satellite disable
                        </a>
                    </h4>
                </div>
                <div id="collapseTen" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTen">
                    <div class="panel-body">
                        <button type="button" id="bt_satdisable" data-loading-text="Testing satellite disable progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Satellite disable
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingEleven">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseEleven" aria-expanded="false" aria-controls="collapseEleven">Security: Reset to GSM(SMS)
                        </a>
                    </h4>
                </div>
                <div id="collapseEleven" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingEleven">
                    <div class="panel-body">
                        <button type="button" id="bt_reset_gsm" data-loading-text="Testing reset to satellite progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            Reset to GSM(SMS)
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Modal -->
    <div class="modal fade" id="analyseModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">Testing progress</h4>
                </div>
                <div class="modal-body">
                    <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; height: 120px; overflow: auto;">
                        <!--11:22:04 <code>position data</code> Command is waiting in send queue.<br />
                        11:22:05 <code>0x1000</code> Command has been send to target.<br />
                        11:22:09 <code>0x1000</code> Target received the command.<br />
                        11:22:20 <code>0x1000</code> Command responsed successfully, you can <code>Analyse</code> this data by click <code>here</code>.<br />
                        11:22:38 <code>0xDD00</code> Command has been send to target.<br />
                        11:22:40 <code>0xDD00</code> Target is not online.<br />-->
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="satWarning" type="button" class="btn btn-warning" style="display: none;">
                        <span class="glyphicon glyphicon-time"></span>Satellite mode may take more time to wait the result
                    </button>
                    <button class="btn btn-primary" type="button">
                        time used: <span class="badge" id="timeUsed">00:00</span>
                    </button>
                </div>
            </div>
        </div>
    </div>
    <!--标签打印对话框-->
    <div class="modal fade" id="modalPrinting" tabindex="-1" style="z-index: 1300;" role="dialog" data-backdrop="static" aria-labelledby="NewStorageIn" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-danger">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title"><strong>标签打印</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12 show-grid">
                            <span id="spanWarningPrinting">正在打印终端标签，请稍候...</span><br />
                            <span>如果打印机长时间没有动作，请检查打印驱动程序是否安装正确</span><br />
                            <br />
                            <div class="progress">
                                <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                                    <span id="spanPrintStatus" class="sr-only"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 show-grid">
                            <span id="spanPrintStatusText"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--终端运转时间初始化对话框-->
    <div class="modal fade" id="alertModalWorktime" tabindex="-1" role="dialog" aria-labelledby="alertModalLabelWork" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header custom-modal-header btn-warning">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="alertModalLabelWork">Loader Work Time initialize</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-8">
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon1" style="width: 90px;">Hours:</span>
                                <input type="text" id="txtHour" class="form-control" placeholder="hour" aria-describedby="basic-addon1" />
                            </div>
                        </div>
                        <div class="col-lg-8" style="margin-top: 5px;">
                            <div class="input-group">
                                <span class="input-group-addon" id="basic-addon2" style="width: 90px;">Minutes:</span>
                                <input type="text" id="txtMinute" class="form-control" placeholder="minute" aria-describedby="basic-addon2" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="initializeWorktime" data-dismiss="modal"><span class="glyphicon glyphicon-ok"></span>Send</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery.json-2.4.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/javascript.date.pattern.js"></script>
    <script type="text/javascript" src="../js/jquery.timer.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/command.base.js"></script>
    <script type="text/javascript" src="../scripts/main/testing_content.js?v=20161213"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var type = $.getUrlParam("type");
            type = null == type ? "normal" : type;
            // 半成品测试的话，只显示2个命令
            $(".panel-default").each(function (index, item) {
                if (type == "semi") {
                    if (index <= 1) {
                        $(item).show();
                    } else {
                        $(item).hide();
                    }
                } else if (type == "finished") {
                    if (index <= 1 || index == 3) {
                        $(item).show();
                    } else {
                        $(item).hide();
                    }
                    $("#printLabel").hide();
                } else {
                    $("#printLabel").hide();
                }
            });

            if (type != "normal") {
                queryDataHistory();
            } else {
                $("#printLabel").hide();
            }
        });
    </script>
</body>
</html>
