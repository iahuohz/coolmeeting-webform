<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="AdminRooms.aspx.cs" Inherits="ETC.EEG.CoolMeeting.AdminPages.AdminRooms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    管理员任务 > 管理会议室
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Repeater ID="repRooms" runat="server">
        <HeaderTemplate>
            <table class="listtable">
                <caption>所有会议室:</caption>
                <tr class="listheader">
                    <th>门牌编号</th>
                    <th>会议室名称</th>
                    <th>容纳人数</th>
                    <th>当前状态</th>
                    <th>操作</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("RoomCode") %></td>
                <td><%# Eval("RoomName") %></td>
                <td><%# Eval("Capacity") %></td>
                <td>
                    <%# 
                        ETC.EEG.CoolMeeting.Model.MeetingRoomStatusDescription.Status[Convert.ToInt32(Eval("Status"))]
                    %>
                </td>
                <td>
                    <asp:HyperLink ID="linkDetails" runat="server" Text="查看详情" 
                        NavigateUrl='<%# "~/AdminPages/EditRoom.aspx?roomid=" + Eval("RoomID") %>'/>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
