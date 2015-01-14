<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ETC.EEG.CoolMeeting.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    用户管理 > 注册
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>员工信息</legend>
        <table class="formtable" style="width: 50%">
            <tr>
                <td>姓名：</td>
                <td>
                    <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valEmployeeName" runat="server"
                        ControlToValidate="txtEmployeeName" ErrorMessage="*" CssClass="error" Display="Dynamic"/>
                </td>
            </tr>
            <tr>
                <td>账户名：</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valUserName" runat="server"
                        ControlToValidate="txtUserName" ErrorMessage="*" CssClass="error" Display="Dynamic"/>
                </td>
            </tr>
            <tr>
                <td>密码：</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" placeholder="请输入5位以上的密码"/>
                    <asp:RequiredFieldValidator ID="valPassword1" runat="server" 
                        ControlToValidate="txtPassword" ErrorMessage="*" CssClass="error" Display="Dynamic"/>
                    <asp:RegularExpressionValidator ID="valPassword2" runat="server"
                        ControlToValidate="txtPassword" ValidationExpression="\S{5}\S*" 
                        ErrorMessage="密码不能少于5位" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>确认密码：</td>
                <td>                    
                    <asp:TextBox ID="txtConfirm" runat="server" MaxLength="20" TextMode="Password" />
                    <asp:CompareValidator ID="valConfirm" runat="server"
                        ControlToValidate="txtConfirm" ControlToCompare="txtPassword"
                        ErrorMessage="两次密码输入不一致" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>所属部门：</td>
                <td>
                    <asp:DropDownList ID="ddlDepartments" runat="server" />
                </td>
            </tr>
            <tr>
                <td>联系电话：</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valPhont" runat="server"
                        ControlToValidate="txtPhone" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>电子邮件：</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valEmail" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="6" class="command">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="clickbutton" OnClick="btnSubmit_Click"/>
                    <input type="reset" class="clickbutton" value="重置" />
                </td>
            </tr>
            <tr>
                <td colspan="6" class="command">
                    <asp:Label ID="lblResult" runat="server" CssClass="error"/>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
