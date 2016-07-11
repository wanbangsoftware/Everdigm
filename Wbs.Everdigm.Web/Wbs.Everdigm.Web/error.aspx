<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="Wbs.Everdigm.Web.error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>You got an error</title>
    <link rel="shortcut icon" href="images/favicon.ico" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="bootstrap3/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap3/bootstrap3-dialog/css/bootstrap-dialog.min.css" rel="stylesheet" />
    <script type="text/javascript">
        var _dialog_content = "<%=DialogContent%>";
        var _dialog_title = "<%=DialogTitle%>";
    </script>
    <style type="text/css">
        .content {
            color: #505050;
            margin: 0 auto;
            width: 100%;
        }

        DIV.howtocode {
            FONT-SIZE: 10px;
            BACKGROUND: #f0faf9;
            COLOR: #000000;
            margin-left: 0.1cm;
            PADDING-LEFT: 0.1cm;
            BORDER-LEFT: #4E7FD9 2px solid;
            overflow: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <h4>Exception(trace/debug) message:</h4>
            <div class="howtocode">
                <pre id="pre_content" runat="server"></pre>
            </div>
        </div>
    </form>
    <script src="js/jquery-2.1.4.min.js"></script>
    <script src="bootstrap3/js/bootstrap.min.js"></script>
    <script src="bootstrap3/bootstrap3-dialog/js/bootstrap-dialog.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            BootstrapDialog.alert({
                title: _dialog_title,
                message: _dialog_content,
                type: BootstrapDialog.TYPE_DANGER,
            });
        });
    </script>
</body>
</html>
