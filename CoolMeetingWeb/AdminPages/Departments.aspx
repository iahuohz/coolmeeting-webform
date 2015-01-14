<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="ETC.EEG.CoolMeeting.AdminPages.Departments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    管理员任务 > 部门管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>部门信息</legend>
        部门名称:
        <asp:TextBox ID="txtDepartmentName" runat="server" MaxLength="20" ValidationGroup="Add" />
        <asp:RequiredFieldValidator ID="valDepartmentName" runat="server" ValidationGroup="Add"
            ControlToValidate="txtDepartmentName" ErrorMessage="*" CssClass="error" Display="Dynamic"/>
        <asp:Button ID="btnSubmit" runat="server" Text="添加部门" CssClass="clickbutton" OnClick="btnSubmit_Click" ValidationGroup="Add"/>
    </fieldset>
    <asp:GridView ID="gvDepartments" runat="server" AutoGenerateColumns="false" DataKeyNames="DepartmentID"
        CssClass="listtable" Caption="所有部门："
        OnRowEditing="gvDepartments_RowEditing" 
        OnRowCancelingEdit="gvDepartments_RowCancelingEdit"
        OnRowUpdating="gvDepartments_RowUpdating"
        OnRowDeleting="gvDepartments_RowDeleting">
        <HeaderStyle CssClass="listheader" />
        <Columns>
            <asp:BoundField DataField="DepartmentID" HeaderText="部门编号" ReadOnly="true"/>
            <asp:TemplateField HeaderText="部门名称">
                <ItemTemplate>
                    <%# Eval("DepartmentName") %>
                </ItemTemplate>
                <EditItemTemplate>                    
                    <asp:TextBox ID="txtDepartmentName" runat="server" Text='<%# Eval("DepartmentName") %>' ValidationGroup="Edit"/>
                    <asp:RequiredFieldValidator ID="valDepartmentName" runat="server" ValidationGroup="Edit"
                        ControlToValidate="txtDepartmentName" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="编辑"/>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="删除" 
                        OnClientClick="return confirm('确实要删除该部门吗？');"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" Text="更新" ValidationGroup="Edit"/>
                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
