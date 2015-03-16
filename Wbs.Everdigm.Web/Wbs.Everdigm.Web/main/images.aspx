<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="images.aspx.cs" Inherits="Wbs.Everdigm.Web.main.images" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Choose image:</title>
    <link href="../css/body.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
    <style type="text/css">
        body {
            margin: 20px;
        }

        .selectimg {
            cursor: pointer;
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListView ID="lvImages" runat="server">
                <ItemTemplate>
                    <asp:Image ID="imgs" CssClass="selectimg" runat="server" ImageUrl='<%#"../images/" + Eval("Name")%>' />
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/images.js"></script>
</body>
</html>
