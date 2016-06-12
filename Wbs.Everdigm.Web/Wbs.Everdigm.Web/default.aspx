<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Wbs.Everdigm.Web._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Everdigm Terminal Control System</title>
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="/login_img/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="box">
                <div class="top">
                    <img src="./login_img/top.png" alt="" />
                </div>
                <!--top-->
                <div class="middle">
                    <div class="tab">
                        <div class="tab_top">
                            <img src="./login_img/input_top.png" alt="" /></div>
                        <div class="tab_bottom">
                            <div class="tab_tab">
                                <table cellpadding="0" cellspacing="0" border="0" width="300px" align="center">
                                    <tr>
                                        <td width="160" height="30" align="right" style="padding-right: 5px;">User Name</td>
                                        <td width="140" align="right">
                                            <input type="text" size="20" maxlength="10" runat="server" id="username" /></td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" style="padding-right: 5px;">Password</td>
                                        <td align="right">
                                            <input type="password" size="20" maxlength="10" runat="server" id="password" /></td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right"></td>
                                        <td align="left">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="submit" runat="server" CssClass="btn" Text="Login" OnClick="submit_Click" />
                                                    </td>
                                                    <td align="right">
                                                        <a href="apks/">Download app</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!--middle-->
                <div class="bottom"><span style="float:left; margin-left: 2px;">Resolution 1280x800(recommended), IE9+/Chrome/Opera/FireFox</span>
                    <span style="float:right; margin-right: 2px;">COPYRIGHT © WANBANGSOFTWARE CO.,LTD.</span>
                </div>
                <!--bottom-->
            </div>
            <!--box-->
        </div>
        <!--container-->
    </form>
    <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="js/common.js"></script>
    <script type="text/javascript" src="scripts/default.js"></script>
</body>
</html>
