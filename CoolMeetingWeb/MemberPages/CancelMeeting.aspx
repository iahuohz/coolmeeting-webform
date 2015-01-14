<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="CancelMeeting.aspx.cs" Inherits="ETC.EEG.CoolMeeting.MemberPages.CancelMeeting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    会议预定 > 撤销预定
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
        <legend>撤销预定</legend>
        <table class="formtable">
            <tr>
                <td>会议名称：</td>
                <td><asp:Label ID="lblMeetingName" runat="server" /></td>
            </tr>
            <tr>
                <td>撤销理由：</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" placeholder="200字以内的理由" />
                    <asp:RequiredFieldValidator ID="valDescription" runat="server"
                        ControlToValidate="txtDescription" ErrorMessage="*" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="command" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="确认撤销" CssClass="clickbutton" OnClick="btnSubmit_Click" />
                    <input type="button" class="clickbutton" value="返回" onclick="window.history.back();" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
