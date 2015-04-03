<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipment_summary.aspx.cs" Inherits="Wbs.Everdigm.Web.main.equipment_summary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap3/models/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <!-- Default panel contents -->
            <div class="panel-heading">
                <strong>Equipment: Summary</strong>
            </div>
            <div class="panel-body">
                <div class="panel-group" role="tablist" aria-multiselectable="true">
                    <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingTwo">
                            <h4 class="panel-title">
                                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">Field(total: <span id="totalField" runat="server">20</span>EA):
                                </a>
                            </h4>
                        </div>
                        <div id="collapseTwo" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                            <div class="panel-body">
                                <div class="col-sm-10">
                                    <div class="col-sm-5">
                                        <div class="bs-callout bs-callout-info" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>Sold</h4>
                                            <p>Count of <code style="font-size: 14px;">Sold</code> <a href="./equipment_inquiry.aspx"><code style="font-size: 20px;" id="countSold" runat="server">30</code></a> EA</p>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="bs-callout bs-callout-info" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>Rental</h4>
                                            <p>Count of <code style="font-size: 14px;">Rental</code> <a href="./equipment_inquiry.aspx"><code style="font-size: 20px;" id="countRental" runat="server">40</code></a> EA</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-10">
                                    <div class="col-sm-5">
                                        <div class="bs-callout" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>Working</h4>
                                            <p>Count of <code style="font-size: 14px;">Working</code> <a href="./equipment_inquiry.aspx"><code style="font-size: 20px;" id="countWorking" runat="server">30</code></a> EA</p>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="bs-callout" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>Stay Idle</h4>
                                            <p>Count of <code style="font-size: 14px;">Stay Idle</code> <a href="./equipment_inquiry.aspx"><code style="font-size: 20px;" id="countIdle" runat="server">40</code></a> EA</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingOne">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Inventory(total: <span id="totalInventory" runat="server">70</span>EA):
                                </a>
                            </h4>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                            <div class="panel-body">
                                <div class="col-sm-10">
                                    <div class="col-sm-5">
                                        <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>New product</h4>
                                            <p>Count of <code style="font-size: 14px;">New product</code> <a href="./equipment_in_storage.aspx"><code style="font-size: 20px;" id="countNew" runat="server">30</code></a> EA</p>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="bs-callout bs-callout-warning" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>Rental Fleet</h4>
                                            <p>Count of <code style="font-size: 14px;">Rental Fleet</code> <a href="./equipment_in_storage.aspx"><code style="font-size: 20px;" id="countFleet" runat="server">40</code></a> EA</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingThree">
                            <h4 class="panel-title">
                                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">Inspection & repair(total: <span id="totalRepair" runat="server">10</span>EA):
                                </a>
                            </h4>
                        </div>
                        <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                            <div class="panel-body">
                                <div class="col-sm-10">
                                    <div class="col-sm-10">
                                        <div class="bs-callout bs-callout-danger" style="margin-top: 2px !important; font-size: 12px !important; margin-bottom: 0px !important; overflow: auto;">
                                            <h4>Inspection & repair</h4>
                                            <p>Count of <code style="font-size: 14px;">Inspection & repair</code> <a href="./equipment_overhaul.aspx"><code style="font-size: 20px;" id="countOverhaul" runat="server">30</code></a> EA</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Tab panes -->
                <div class="tab-content" style="border: 1px solid #fff !important;">
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
</body>
</html>
