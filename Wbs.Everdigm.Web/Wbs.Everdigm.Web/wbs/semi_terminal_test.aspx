<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="semi_terminal_test.aspx.cs" Inherits="Wbs.Everdigm.Web.wbs.semi_terminal_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../bootstrap3/css/bootstrap.css" rel="stylesheet" />
    <link href="../js/BootSiderMenu/css/BootSideMenu.css" rel="stylesheet" />
    <link href="../css/body_equipment.css" rel="stylesheet" />
    <style type="text/css">
        html, body {
            height: 100%;
            padding: 0;
        }

        .custom-modal-header {
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -webkit-border-bottom-left-radius: 0px;
            -webkit-border-bottom-right-radius: 0px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            -moz-border-radius-bottomleft: 0px;
            -moz-border-radius-bottomright: 0px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        .nav-tabs > li {
            position: relative;
        }

            .nav-tabs > li > a {
                display: inline-block;
            }

            .nav-tabs > li > span {
                display: none;
                cursor: pointer;
                position: absolute;
                right: 6px;
                top: 0px;
                color: red;
            }

            .nav-tabs > li:hover > span {
                display: inline-block;
            }

        .list-group img {
            width: auto;
            height: 30px;
            margin-right: 5px;
        }

        .list-group-item img {
            width: auto;
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading"><strong id="heading">半成品测试</strong></div>
            <div class="panel-body">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#help" aria-controls="help" role="tab" data-toggle="tab">帮助</a></li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content" style="border-left: 1px solid #ddd; border-right: 1px solid #ddd; border-bottom: 1px solid #ddd;">
                    <div role="tabpanel" class="tab-pane fade in active" id="help" style="padding: 10px;">
                        说明：<br />
                        1、点击页面左侧的侧滑菜单，输入终端号/卡号查询；<br />
                        2、点选查询出来的结果列表里的终端；<br />
                        3、右侧会出现相应的测试界面；<br />
                        4、测试；<br />
                        5、测试完毕后打印标签
                    </div>
                </div>
            </div>
        </div>
        <div id="terminal_list">
            <div class="btn btn-primary custom-modal-header" style="width: 100%; margin-bottom: 1px;">终端</div>
            <div style="width: 100%; padding: 1px;">
                <div class="input-group" style="margin: 1px 1px 1px 2px;">
                    <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="终端号/卡号" />
                    <span class="input-group-btn">
                        <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                    </span>
                </div>
            </div>
            <div class="list-group">
                <a href="#" class="list-group-item" id="loading">
                    <img alt="#" src="../images/loading_orange.gif" />加载中...</a>
                <div id="divTerminals"></div>
            </div>
        </div>
        
        <div class="modal fade" id="modalWarningTabs" role="dialog" data-backdrop="static" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header custom-modal-header btn-warning">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title"><strong>消息通知</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12 show-grid warning-content">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../bootstrap3/js/bootstrap.js"></script>
    <script src="../js/BootSiderMenu/js/BootSideMenu.js"></script>
    <script src="../js/common.js"></script>
    <script src="../scripts/wbs/semi.test.js"></script>
</body>
</html>
