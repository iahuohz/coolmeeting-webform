<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="ETC.EEG.CoolMeeting.AdminPages.AddRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    管理员任务 > 添加会议室
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>会议室信息</legend>
        <table class="formtable">
            <tr>
                <td>门牌号:</td>
                <td>
                    <asp:TextBox ID="txtRoomCode" runat="server" MaxLength="10" placeholder="例如：201"/>
                    <asp:RequiredFieldValidator ID="valRoomCode" runat="server" 
                        ControlToValidate="txtRoomCode" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>会议室名称:</td>
                <td>
                    <asp:TextBox ID="txtRoomName" runat="server" MaxLength="20" placeholder="例如：第一会议室" />
                    <asp:RequiredFieldValidator ID="valRoomName" runat="server"
                        ControlToValidate="txtRoomName" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>最多容纳人数：</td>
                <td>
                    <asp:TextBox ID="txtCapacity" runat="server" placeholder="填写一个正整数"/>
                    <asp:RequiredFieldValidator ID="valCapacity1" runat="server"
                        ControlToValidate="txtCapacity" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RangeValidator ID="valCapacity2" runat="server"
                        ControlToValidate="txtCapacity" MinimumValue="1" MaximumValue="300" Type="Integer"
                        ErrorMessage="请填写1~300之间的正整数" CssClass="error" Display="Dynamic"/>
                </td>
            </tr>
            <tr>
                <td>当前状态：</td>
                <td>
                    <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                        <asp:ListItem Value="0">禁用</asp:ListItem>
                        <asp:ListItem Value="-1">已删除</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>备注：</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                        MaxLength="200" Rows="5" placeholder="200字以内的文字描述" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="command">
                    <asp:Button ID="btnSubmit" runat="server" Text="确定" CssClass="clickbutton" OnClick="btnSubmit_Click"/>
                    <input type="reset" value="重置" class="clickbutton" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
