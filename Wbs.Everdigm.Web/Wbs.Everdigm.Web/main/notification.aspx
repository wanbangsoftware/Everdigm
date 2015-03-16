<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notification.aspx.cs" Inherits="Wbs.Everdigm.Web.main.notification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <link href="../css/right.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/invalid.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="../css/style.css" type="text/css" media="screen" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <script type="text/javascript">
        var _maxTimes = parseInt("<%=_maxTimes%>");
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="url" runat="server" />
        <input type="hidden" id="topest" runat="server" />
        <div style="width: 50%; position: absolute; left: 25%; top: 10%; z-index: 1001;">
            <div class="content-box">
                <div class="content-box-header">
                    <h3 id="notification_title" runat="server">Title</h3>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" style="vertical-align: middle; padding: 10px;" id="div_msg">
                        <div class="notification error" id="notification_content" runat="server">
                            <div>测试错误页面信息</div>
                        </div>
                        <span id="decount"></span>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/notification.js"></script>
</body>
</html>
