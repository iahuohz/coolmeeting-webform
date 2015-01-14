<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ETC.EEG.CoolMeeting.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    登录
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>登录信息</legend>
        <table class="formtable" style="width: 50%">
            <tr>
                <td>账号名:</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" />
                    <asp:RequiredFieldValidator ID="valUserName" runat="server"
                        ControlToValidate="txtUserName" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>密码:</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="valPassword" runat="server"
                        ControlToValidate="txtPassword" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="command">
                    <asp:Button ID="btnSubmit" runat="server" Text="登录" CssClass="clickbutton" OnClick="btnSubmit_Click"/>
                    <input type="reset" value="重置" class="clickbutton" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <asp:Label ID="lblLoginResult" runat="server" Visible="false" CssClass="error">
                        用户名或密码错误，请重试
                    </asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
