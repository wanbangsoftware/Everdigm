<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account_add.aspx.cs" Inherits="Wbs.Everdigm.Web.main.account_add" %>

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
                    <h3>System: Account - Add/Edit</h3>
                    <div class="clear"></div>
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="table_header">
                                    <asp:Button ID="btSave" runat="server" Text="Save" CssClass="button" OnClientClick="return checkSubmitedForm();" OnClick="btSave_Click" />
                                    <input type="button" class="button" id="btReturn" value="Back" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid #ccc; margin-top: 2px;">
                            <tr class="table_header">
                                <td colspan="4">Base informations:</td>
                            </tr>
                            <tr>
                                <td class="td_right">Name:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtName" />
                                    <input type="hidden" runat="server" id="hidID" />
                                </td>
                                <td class="td_right">Account Number:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input important-input" id="txtCode" title="use for login" /></td>
                            </tr>
                            <tr>
                                <td class="td_right">Password:</td>
                                <td class="td_left">
                                    <span style="color: #ff6a00;">default is 123456</span>
                                </td>
                                <td class="td_right">Email:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtEmail" /></td>
                            </tr>
                            <tr>
                                <td class="td_right">Phone:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtPhone" />
                                </td>
                                <td class="td_right">Landline Number:</td>
                                <td class="td_left">
                                    <input type="text" runat="server" class="text-input" id="txtLindline" /></td>
                            </tr>
                            <tr class="table_header">
                                <td colspan="4">System informations:</td>
                            </tr>
                            <tr>
                                <td class="td_right">Department:</td>
                                <td class="td_left" style="height: 30px;">
                                    <input type="text" class="text-input click-input" runat="server" id="txtDepartment" readonly="readonly" />
                                    <input type="hidden" id="hidDepartment" runat="server" />
                                </td>
                                <td class="td_right">System role:</td>
                                <td class="td_left">
                                    <input type="text" class="text-input click-input" runat="server" id="txtRole" readonly="readonly" />
                                    <input type="hidden" id="hidRole" runat="server" />
                                </td>
                            </tr>
                            <tr class="table_header">
                                <td colspan="4">Privacy informations(<span style="color: #0026ff;">for find password</span>):</td>
                            </tr>
                            <tr>
                                <td class="td_right">Question:</td>
                                <td class="td_left" style="height: 30px;">
                                    <input type="text" class="text-input" runat="server" id="txtQuestion" />
                                </td>
                                <td class="td_right">Answer:</td>
                                <td class="td_left">
                                    <input type="text" class="text-input" runat="server" id="txtAnswer" />
                                </td>
                            </tr>
                        </table>
                        <p>
                            <b>Explanation</b>:<br />
                            1、新注册用户的密码统一默认 123456，用户登录之后可以自行修改，系统管理员可以重置用户的密码为默认密码；<br />
                            2、部门所属留空时新用户会被划分到系统默认部门中，如没有设定默认部门，则部门信息为空，设定默认部门后会自动划分；<br />
                            3、角色所属留空时新用户会被划分到系统默认角色属下，如系统中没有默认角色，则角色信息将为空，设定默认角色之后会自动划分；<br />
                            4、同一个用户可以拥有多个角色身份。
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
    <script type="text/javascript" src="../scripts/main/account.js"></script>
</body>
</html>
