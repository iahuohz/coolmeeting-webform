<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="SearchReservations.aspx.cs" Inherits="ETC.EEG.CoolMeeting.AdminPages.SearchReservations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    管理员任务 > 搜索会议
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>搜索会议</legend>
        <table class="formtable">
            <tr>
                <td>会议名称：</td>
                <td>
                    <asp:TextBox ID="txtMeetingName" runat="server" MaxLength="20" />
                </td>
                <td>会议室名称：</td>
                <td>
                    <asp:TextBox ID="txtRoomName" runat="server" MaxLength="20" />
                </td>
            </tr>
            <tr>
                <td>预定日期：</td>
                <td colspan="5">从&nbsp;
                    <asp:TextBox ID="txtReserveFromDate" runat="server" placeholder="例如：2013-10-20" />
                    到&nbsp;
                    <asp:TextBox ID="txtReserveToDate" runat="server" placeholder="例如：2013-10-22" />
                </td>
            </tr>
            <tr>
                <td>会议日期：</td>
                <td colspan="5">从&nbsp;<asp:TextBox ID="txtMeetingFromDate" runat="server" placeholder="例如：2013-10-20" />
                    到&nbsp;<asp:TextBox ID="txtMeetingToDate" runat="server" placeholder="例如：2013-10-22" />
                </td>
            </tr>
            <tr>
                <td colspan="6" class="command">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="clickbutton" OnClick="btnSearch_Click" />
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
        <asp:Repeater ID="repEmployees" runat="server">
            <HeaderTemplate>
                <table class="listtable">
                    <tr class="listheader">
                        <th>会议名称</th>
                        <th>会议室名称</th>
                        <th>会议开始时间</th>
                        <th>会议结束时间</th>
                        <th>会议预定时间</th>
                        <th>预定者</th>
                        <th>操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("MeetingName") %></td>
                    <td><%# Eval("Room.RoomName") %></td>
                    <td><%# Eval("StartTime") %></td>
                    <td><%# Eval("EndTime") %></td>
                    <td><%# Eval("ReservationTime") %></td>
                    <td><%# Eval("Reservationist.EmployeeName") %></td>
                    <td>
                        <asp:HyperLink ID="linkDetails" runat="server" Text="查看详情" CssClass="clickbutton" 
                            NavigateUrl='<%# "~/MemberPages/MeetingDetails.aspx?meetingid=" + Eval("MeetingID").ToString() %>'/>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
