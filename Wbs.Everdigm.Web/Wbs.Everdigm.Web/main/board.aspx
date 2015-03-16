<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="board.aspx.cs" Inherits="Wbs.Everdigm.Web.main.board" %>

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
                    <h3>System: Permission</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save" CssClass="button" />
                                    <input type="button" class="button" id="btReturn" value="Back" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr>
                                <td class="td_right">Menu Name: </td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtName" />
                                    <input type="hidden" runat="server" id="hidID" />
                                </td>
                                <td class="td_right">Menu URL: </td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtURL" />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_right">Parent Menu: </td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtUpper" readonly="readonly" style="cursor: pointer;" />
                                    <input type="hidden" runat="server" id="hidUpper" />
                                </td>
                                <td class="td_right">Is Default: </td>
                                <td class="td_left"><input type="checkbox" runat="server" id="cbDefault" /></td>
                            </tr>
                            <tr>
                                <td class="td_right">Image: </td>
                                <td class="td_left" style="height: 30px;">
                                    <img alt="icon" id="imgImage" runat="server" src="/images/index_08.gif" title="click to choose" style="cursor: pointer;" />
                                    <input type="hidden" id="hidImage" runat="server" value="/images/index_08.gif" />
                                </td>
                                <td class="td_right">Description: </td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtDescription" /></td>
                            </tr>
                        </table>
                        <p>
                            <b>Explanation</b>: <br />
                            1. Menu name: the text of the menu show to user.<br />
                            2. Menu URL: <br />
                            3. Parent Menu: click to choose the parent menu<br />
                            4. Is default: 
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
