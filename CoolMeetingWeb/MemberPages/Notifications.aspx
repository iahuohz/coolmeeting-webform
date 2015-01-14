<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="ETC.EEG.CoolMeeting.MemberPages.Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    个人中心 > 最新通知
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Repeater ID="repNewMeetings" runat="server">
        <HeaderTemplate>
            <table class="listtable">
                <caption>未来7天我要参加的会议:</caption>
                <tr class="listheader">
                    <th style="width: 300px">会议名称</th>
                    <th>会议室</th>
                    <th>起始时间</th>
                    <th>结束时间</th>
                    <th style="width: 100px">操作</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("MeetingName") %></td>
                <td><%# Eval("Room.RoomName") %></td>
                <td><%# Eval("StartTime") %></td>
                <td><%# Eval("EndTime") %></td>
                <td>
                    <asp:HyperLink ID="linkDetails" runat="server" Text="查看详情" 
                        NavigateUrl="MeetingDetails.aspx" CssClass="clickbutton" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="repCanceledMeetins" runat="server">
        <HeaderTemplate>
            <table class="listtable">
                <caption>已取消的会议:</caption>
                <tr class="listheader">
                    <th style="width: 300px">会议名称</th>
                    <th>会议室</th>
                    <th>起始时间</th>
                    <th>结束时间</th>
                    <th>取消原因</th>
                    <th style="width: 100px">操作</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("MeetingName") %></td>
                <td><%# Eval("RoomName") %></td>
                <td><%# Eval("StartTime") %></td>
                <td><%# Eval("EndTime") %></td>
                <td><%# Eval("Description") %></td>
                <td>
                    <asp:HyperLink ID="linkDetails" runat="server" Text="查看详情" 
                        NavigateUrl="MeetingDetails.aspx" CssClass="clickbutton" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
