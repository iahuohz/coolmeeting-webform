<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="MyMeetings.aspx.cs" Inherits="ETC.EEG.CoolMeeting.MemberPages.MyMeetings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    个人中心 > 我的会议
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Repeater ID="repReservations" runat="server">
        <HeaderTemplate>
            <table class="listtable">
                <caption>我要参加的会议:</caption>
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
                        NavigateUrl='<%# "MeetingDetails.aspx?meetingid=" + Eval("MeetingID") %>' CssClass="clickbutton" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
