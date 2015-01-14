<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SearchEmployees.aspx.cs" Inherits="ETC.EEG.CoolMeeting.AdminPages.SearchEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    管理员任务 > 搜索员工
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>搜索会议</legend>
        <table class="formtable">
            <tr>
                <td>姓名：</td>
                <td>
                    <asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="20" />
                </td>
                <td>账号名：</td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" />
                </td>
                <td>状态：</td>
                <td>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1" Selected="True">已审批</asp:ListItem>
                        <asp:ListItem Value="0">待审批</asp:ListItem>
                        <asp:ListItem Value="-1">已关闭</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="6" class="command">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="clickbutton" OnClick="btnSearch_Click"/>
                    <input type="reset" class="clickbutton" value="重置" />
                </td>
            </tr>
        </table>
    </fieldset>
    <div id="divNoResults" runat="server">
        <h3>没有搜索到满足条件的记录！</h3>
    </div>
    <div id="divResults" runat="server">
        <h3>查询结果</h3>
        <div class="pager-header">
            <div class="header-info">
                共<span class="info-number"><asp:Label ID="lblTotalResults" runat="server" /></span>条结果，
                分成<span class="info-number"><asp:Label ID="lblTotalPages" runat="server" /></span>页显示，
                当前第<span class="info-number"><asp:Label ID="lblCurrentPage" runat="server" /></span>页
            </div>
            <div class="header-nav">
                <asp:Button ID="btnFirst" runat="server" Text="首页" CssClass="clickbutton" OnClick="btnFirst_Click" />
                <asp:Button ID="btnPrev" runat="server" Text="上页" CssClass="clickbutton" OnClick="btnPrev_Click" />
                <asp:Button ID="btnNext" runat="server" Text="下页" CssClass="clickbutton" OnClick="btnNext_Click" />
                <asp:Button ID="btnLast" runat="server" Text="末页" CssClass="clickbutton" OnClick="btnLast_Click" />
                跳到第<asp:TextBox ID="txtPageNumber" runat="server" CssClass="nav-number" />页
                <asp:Button ID="btnGo" runat="server" Text="跳转" CssClass="clickbutton" OnClick="btnGo_Click"/>
            </div>
        </div>
        <asp:Repeater ID="repEmployees" runat="server" OnItemCommand="repEmployees_ItemCommand">
            <HeaderTemplate>
                <table class="listtable">
                    <tr class="listheader">
                        <th>姓名</th>
                        <th>用户名</th>
                        <th>所属部门</th>
                        <th>联系电话</th>
                        <th>电子邮件</th>
                        <th>操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("EmployeeName") %></td>
                    <td><asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' /></td>
                    <td><%# Eval("RelatedDepartment.DepartmentName") %></td>
                    <td><%# Eval("Phone") %></td>
                    <td><a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></td>
                    <td>
                        <asp:LinkButton ID="btnClose" runat="server" Text="关闭账号" CssClass="clickbutton" 
                            OnClientClick="return confirm('确实永久关闭此员工账号吗？');"
                            CommandArgument='<%# Eval("EmployeeID") %>' CommandName="CloseAccount"/>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
