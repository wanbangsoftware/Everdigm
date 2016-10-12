<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_list.aspx.cs" Inherits="Wbs.Everdigm.Web.main.account_list" %>

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
            <input type="hidden" value="" id="cookieName" runat="server" />
            <div class="content-box">
                <div class="content-box-header">
                    <h3>System: Account List</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">Name:
                                    <input type="text" id="txtName" class="text-input little-input" runat="server" />
                                    Department:
                                    <input type="text" id="txtDepartment" class="text-input little-input click-input" runat="server" readonly="readonly" />
                                    <input type="hidden" id="hidDepartment" runat="server" />
                                    Role:
                                    <input type="text" id="txtRole" class="text-input little-input click-input" runat="server" readonly="readonly" />
                                    <input type="hidden" id="hidRole" runat="server" />
                                    <input type="hidden" runat="server" id="hidPageIndex" />
                                    <asp:Button ID="btQuery" runat="server" Text="Query" CssClass="button" OnClick="btQuery_Click" />
                                    |
                                    <input type="button" id="btAdd" class="button" value="Add" />
                                    <input type="hidden" runat="server" id="hidID" />
                                    <asp:Button ID="bt_Unlock" runat="server" Text="Unlock/Lock" CssClass="button" OnClick="bt_Unlock_Click" />
                                    <asp:Button ID="bt_Delete" runat="server" Text="Delete" CssClass="button" OnClick="btDelete_Click" />
                                </td>
                            </tr>
                        </table>
                        <table id="tbTable" width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <thead>
                                <tr>
                                    <th style="width: 40px; text-align: center;">
                                        <input type="checkbox" id="cbAll" /></th>
                                    <th style="width: 60px; text-align: center;">ID</th>
                                    <th style="width: 150px;">Name</th>
                                    <th style="width: 100px;">Code</th>
                                    <th style="width: 120px;">Register Date</th>
                                    <th style="width: 50px;">Status</th>
                                    <th>Department</th>
                                    <th>Role</th>
                                    <th>Login Times</th>
                                    <th>Last Login</th>
                                    <th>Last Login IP</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan="12">
                                        <div class="pagging" id="divPagging" runat="server">
                                        </div>
                                        <div class="clear"></div>
                                    </td>
                                </tr>
                            </tfoot>
                            <tbody id="tbodyBody" runat="server">
                                <tr>
                                    <td style="text-align: center;">
                                        <input type="checkbox" id="cb_1" /></td>
                                    <td style="text-align: center;">1</td>
                                    <td>向李果</td>
                                    <td>WB-010</td>
                                    <td>2000-01-01</td>
                                    <td>正常</td>
                                    <td>信息一部</td>
                                    <td>系统管理员</td>
                                    <td>3768</td>
                                    <td>2013-02-04 07:46:32</td>
                                    <td>127.0.0.1</td>
                                    <td>WEB</td>
                                </tr>
                            </tbody>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1、请务必留下一位用户拥有系统管理员角色的用户，否则可能无法再继续管理系统；<br />
                            2、锁定、解锁会直接锁定/解锁选中用户，锁定后该用户将无法再登录系统，解锁后才能恢复正常；<br />
                        </p>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/common.js"></script>
    <script type="text/javascript" src="../scripts/main/pagination.js"></script>
    <script type="text/javascript" src="../scripts/main/account.js"></script>
</body>
</html>
