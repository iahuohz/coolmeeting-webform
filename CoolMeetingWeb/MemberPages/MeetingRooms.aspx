<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="MeetingRooms.aspx.cs" Inherits="ETC.EEG.CoolMeeting.MemberPages.MeetingRooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    会议预定 > 查看会议室
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
                    <th>描述</th>
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
                    <%# Eval("Description") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
