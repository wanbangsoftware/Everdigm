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

        .list-group img {
            width: auto;
            height: 30px;
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <div id="terminal_list">
            <div class="btn btn-primary custom-modal-header" style="width: 100%; margin-bottom: 1px;">终端</div>
            <div style="width: 100%; padding: 1px;">
                <div class="input-group" style="margin: 1px 1px 1px 2px;">
                    <input type="text" id="txtQueryNumber" runat="server" class="form-control" placeholder="number" />
                    <span class="input-group-btn">
                        <button class="btn btn-warning" type="button" id="query"><span class="glyphicon glyphicon-search"></span></button>
                    </span>
                </div>
            </div>
            <div class="list-group">
                <a href="#" class="list-group-item">
                    <img alt="#" src="../images/provinces/icon_all.png" />All</a>
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
