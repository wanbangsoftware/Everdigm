<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="terminal_testing.aspx.cs" Inherits="Wbs.Everdigm.Web.main.terminal_testing" %>

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
    <link href="../jquery-ui-1.11.2.custom/jquery-ui.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content">
            <div class="content-box">
                <div class="content-box-header">
                    <h3>Terminal: Testing</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">Terminal No./Sim Card No：
                                    <input type="text" class="text-input confirm-input" id="txtNumber" runat="server" maxlength="11" style="text-transform: uppercase;" />
                                    <input type="button" id="btQuery" class="button" value="Query" />
                                </td>
                            </tr>
                        </table>
                        <table id="tabTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr>
                                <td colspan="4">
                                    <div class="content-box-1">
                                        <div class="content-box-header-1">
                                            <ul class="content-box-tabs" id="ul_nav">
                                                <li>
                                                    <a href="#tab1" class="default-tab">Help</a>
                                                </li>
                                            </ul>
                                            <div class="clear"></div>
                                        </div>
                                        <div class="content-box-content-1" id="divContent">
                                            <div class="tab-content default-tab" id="tab1">
                                                <p>
                                                    <b>Explanation</b>:<br />
                                                    1. This place only test for unbond terminals.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../jquery-ui-1.11.2.custom/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/tabs.configuration.js"></script>
    <script type="text/javascript" src="../scripts/main/testing.js"></script>
</body>
</html>
