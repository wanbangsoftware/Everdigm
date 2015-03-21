<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_testing_content.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_testing_content" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="alert alert-danger alert-dismissible fade in" role="alert">
            You are in testing progress of Terminal 
            <strong id="terminalInfo" runat="server" style="cursor: pointer;" data-toggle="popover" data-placement="bottom" data-trigger="focus"></strong>now. Please select testing progress blow...
            <input type="hidden" id="terminalContent" runat="server" />
        </div>
        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingOne">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">#1 Retrieve GPS Position
                        </a>
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                    <div class="panel-body">
                        <button type="button" id="bt_position" data-loading-text="Testing GPS position retrieve progress..." data-complete-text="testing finished!" class="btn btn-default" autocomplete="off">
                            GPS position info
                        </button>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingTwo">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">#2 Retrieve GSM Signal
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
                <div class="panel-heading" role="tab" id="headingThree">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">#3 Monitor data
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
                    <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important;  margin-bottom: 0px !important; height: 120px; overflow: auto;">
                        <!--11:22:04 <code>position data</code> Command is waiting in send queue.<br />
                        11:22:05 <code>0x1000</code> Command has been send to target.<br />
                        11:22:09 <code>0x1000</code> Target received the command.<br />
                        11:22:20 <code>0x1000</code> Command responsed successfully, you can <code>Analyse</code> this data by click <code>here</code>.<br />
                        11:22:38 <code>0xDD00</code> Command has been send to target.<br />
                        11:22:40 <code>0xDD00</code> Target is not online.<br />-->
                    </div>
                </div>
                <div class="modal-footer">
                    <span id="timeUsed">time used 00:00</span>&nbsp;
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/javascript.date.pattern.js"></script>
    <script type="text/javascript" src="../js/jquery.timer.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/testing_content.js"></script>
</body>
</html>
