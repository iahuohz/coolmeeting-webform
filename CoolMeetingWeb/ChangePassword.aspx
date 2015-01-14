<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ETC.EEG.CoolMeeting.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    用户管理 > 修改密码
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>密码信息：</legend>
        <table class="formtable">
            <tr>
                <td>原始密码：</td>
                <td>
                    <asp:TextBox ID="txtOrigin" runat="server" TextMode="Password" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valOrigin" runat="server"
                        ControlToValidate="txtOrigin" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>新密码：</td>
                <td>
                    <asp:TextBox ID="txtNew" runat="server" TextMode="Password" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valNew" runat="server"
                        ControlToValidate="txtNew" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="valNew2" runat="server"
                        ControlToValidate="txtNew" ValidationExpression="\S{5}\S*" 
                        ErrorMessage="密码不能少于5位" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>确认新密码：</td>
                <td>
                    <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" MaxLength="20" />
                    <asp:CompareValidator ID="valConfirm" runat="server"
                        ControlToValidate="txtConfirm" ControlToCompare="txtNew"
                        ErrorMessage="两次密码输入不一致" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="command">
                    <asp:Button ID="btnSubmit" runat="server" Text="确认修改" CssClass="clickbutton" OnClick="btnSubmit_Click"/>
                    <input type="reset" value="重置" class="clickbutton" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
