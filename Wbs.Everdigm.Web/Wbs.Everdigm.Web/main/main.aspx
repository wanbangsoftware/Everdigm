<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Wbs.Everdigm.Web.main.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <title>Everdigm Terminal Control System</title>
</head>
<frameset rows="<%=TopHeight %>" cols="*" framespacing="0" frameborder="no" border="0" id="main_frame" name="main_frame">
    <frame src="top_frame.aspx" name="top_frame" scrolling="no" noresize="noresize" id="top_frame" />
    <frameset rows="*" cols="<%=MenuWidth %>" framespacing="0" frameborder="no" border="0" name="body_frame" id="body_frame">
        <frame src="left_frame.aspx" name="left_frame" scrolling="auto" noresize="noresize" id="left_frame" />
        <frameset rows="*" cols="11,*" framespacing="0" frameborder="no" border="0">
            <frame src="center_frame.aspx" name="close_frame" scrolling="no" noresize="noresize" id="close_frame" />
		    <frame src="account_history.aspx" name="right_frame" id="right_frame" />
	    </frameset>
    </frameset>
    <frame src="bottom_frame.aspx" name="bottom_frame" scrolling="No" noresize="noresize" id="bottom_frame" />
</frameset>
<noframes>
    <body>
        Your navigator didn't support framset.
    </body>
</noframes>
</html>
