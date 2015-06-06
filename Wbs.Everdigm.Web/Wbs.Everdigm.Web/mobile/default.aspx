<%@ Page MasterPageFile="~/mobile/Mobile.Master" Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Wbs.Everdigm.Web.mobile._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="TitleContentPlaceHolderRight" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="login">
        <div class="link-touch">
            <ul class="items">
                <li>
                    <label class="txt">Account</label>
                    <input type="text" id="account" class="input" placeholder="customer code or phone number" maxlength="15">
                </li>
                <li>
                    <label class="txt">Passowrd</label>
                    <input type="password" id="password" class="input" placeholder="password" maxlength="20">
                </li>
            </ul>
            <div id="errorMsgUp" class="vtip errs none">Please input again</div>
        </div>
        <div id="captchaDiv" class="link-touch verify-num none">
            <span class="verify-code">
                <input type="text" id="captcha" class="input" placeholder="verify code" maxlength="5">
            </span>
            <img id="imgCaptcha" src="#" onclick="return refresh();">
            <div id="errorMsgDown" class="vtip errs none">please input again</div>
        </div>
        <div class="operate-button operate-acc">
            <a class="btn">Sing in</a>
            <a href="#" class="btn-server">Retrieve passowrd</a>
            <span id="span" class="none" runat="server"></span>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FootContentPlaceHolder" runat="server">
    <script type="text/javascript" src="../js/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../js/CryptoJS.v3.1.2/rollups/md5.js"></script>
    <script type="text/javascript" src="js/common.js"></script>
    <script type="text/javascript" src="js/default.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        });
    </script>
</asp:Content>
