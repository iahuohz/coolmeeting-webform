<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="MeetingDetails.aspx.cs" Inherits="ETC.EEG.CoolMeeting.MemberPages.MeetingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    会议预定 > 会议详情
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>会议信息</legend>
        <table class="formtable">
            <tr>
                <td>会议名称：</td>
                <td><asp:Label ID="lblMeetingName" runat="server" /></td>
            </tr>
            <tr>
                <td>会议室名称：</td>
                <td><asp:Label ID="lblRoomName" runat="server" /></td>
            </tr>
            <tr>
                <td>预定者：</td>
                <td><asp:Label ID="lblReservationist" runat="server" /></td>
            </tr>
            <tr>
                <td>预计开始时间：</td>
                <td><asp:Label ID="lblStartTime" runat="server" /></td>
            </tr>
            <tr>
                <td>预计结束时间：</td>
                <td><asp:Label ID="lblEndTime" runat="server" /></td>
            </tr>
            <tr>
                <td>会议说明：</td>
                <td>
                    <asp:Label ID="lblDescription" runat="server" />
                </td>
            </tr>
            <tr>
                <td>预计参加人数：</td>
                <td><asp:Label ID="lblNumberofParticipants" runat="server" /></td>
            </tr>
            <tr>
                <td>参会人员</td>
                <td>
                    <asp:Repeater ID="repParticipants" runat="server">
                        <HeaderTemplate>
                            <table class="listtable">
                            <tr class="listheader">
                                <th>姓名</th>
                                <th>联系电话</th>
                                <td>电子邮件</td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("EmployeeName") %></td>
                                <td><%# Eval("Phone") %></td>
                                <td><%# Eval("Email") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
