<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="center_frame.aspx.cs" Inherits="Wbs.Everdigm.Web.main.center_frame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/center_frame.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var col = "<%=_cols_%>";
        var row = "<%=_rows_%>";
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a class="index_close_top" title="hide top banner" id="control_top_frame"></a>
        <a class="index_close_left" title="hide left menu" id="control_left_frame"></a>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/center_frame.js"></script>
</body>
</html>
