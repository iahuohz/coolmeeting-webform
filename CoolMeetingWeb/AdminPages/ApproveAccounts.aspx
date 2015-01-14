<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="ApproveAccounts.aspx.cs" Inherits="ETC.EEG.CoolMeeting.AdminPages.ApproveAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    管理员任务 > 注册审批
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Repeater ID="repAccounts" runat="server" OnItemCommand="repAccounts_ItemCommand">
        <HeaderTemplate>
            <table class="listtable">
                <caption>所有待审批注册信息：</caption>
                <tr class="listheader">
                    <th>姓名</th>
                    <th>账号名</th>
                    <th>联系电话</th>
                    <th>电子邮件</th>
                    <th>操作</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("EmployeeName") %></td>
                <td><asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' /></td>
                <td><%# Eval("Phone") %></td>
                <td><a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></td>
                <td>
                    <asp:LinkButton ID="btnApprove" runat="server" Text="通过"  CssClass="clickbutton"
                        CommandName="Approve" CommandArgument='<%# Eval("EmployeeID") %>'/>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CssClass="clickbutton" 
                        CommandName="Delete" CommandArgument='<%# Eval("EmployeeID") %>'
                        OnClientClick="return confirm('确实删除此员工数据吗？');"/>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
