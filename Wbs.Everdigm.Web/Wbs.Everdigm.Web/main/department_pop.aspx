<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="department_pop.aspx.cs" Inherits="Wbs.Everdigm.Web.main.department_pop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <base target="_self" />
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link href="../css/body.css" rel="stylesheet" />
    <link href="../css/left_frame.css" rel="stylesheet" />
    <style type="text/css">
        .button {
            font-family: Verdana, Arial, sans-serif "宋体";
            display: inline-block;
            background: #0053a2 url('/images/bg-button-green.gif') top left repeat-x !important;
            border: 1px solid #0053a2 !important;
            padding: 4px 7px 4px 7px !important;
            color: #fff !important;
            font-size: 12px !important;
            cursor: pointer;
        }

            .button:hover {
                text-decoration: underline;
            }

            .button:active {
                padding: 5px 7px 3px 7px !important;
            }

        .table_header {
            height: 30px;
            border: 1px solid #dddada;
            background: #f0f0f0;
            padding: 3px;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" id="tableTable" runat="server">
            <tr class="table_header">
                <td style="width: 120px; padding-left: 5px;">
                    <input id="btConfirm" type="button" value="Select" class="button" />
                    <input id="btClose" type="button" value="Close" class="button" />
                </td>
                <td style="width: 20px;">
                    <input type="checkbox" id="all" />
                </td>
                <td>select all</td>
            </tr>
        </table>
        <div style="padding-left: 10px;" id="popDepartment">
            <asp:TreeView ID="tvDepartments" runat="server" ShowLines="True" ExpandDepth="3" Font-Size="14px" LineImagesFolder="/tree_images">
                <HoverNodeStyle BackColor="#66CCFF" BorderColor="#0099FF" BorderStyle="Solid"
                    BorderWidth="1px" />
                <SelectedNodeStyle BackColor="#66CCFF" BorderColor="#0099FF"
                    BorderStyle="Solid" BorderWidth="1px" />
            </asp:TreeView>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/department.js"></script>
</body>
</html>
