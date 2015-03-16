<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="privacy_setting.aspx.cs" Inherits="Wbs.Everdigm.Web.main.privacy_setting" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content">
            <div class="content-box">
                <div class="content-box-header">
                    <h3>Personal: Privacy Setting</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save my Question" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <input id="btReturn" type="button" value="Back" class="button" style="display:none;" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr>
                                <td class="td_right" style="width: 10% !important;">Question:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input medium-input important-input" id="txtQuestion" />
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="td_right">Answer:</td>
                                <td class="td_left">
                                    <input type="password" runat="server" class="text-input medium-input important-input" id="txtAnswer" />
                                </td>
                                <td class="td_right">&nbsp;</td>
                                <td class="td_left">&nbsp;</td>
                            </tr>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            You can use this question and answer to find back your password when you forget it.
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/account.js"></script>
</body>
</html>
