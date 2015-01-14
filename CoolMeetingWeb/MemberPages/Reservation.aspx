<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="Reservation.aspx.cs" Inherits="ETC.EEG.CoolMeeting.MemberPages.Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #divfrom {
            float: left;
            width: 150px;
        }

        #divto {
            float: left;
            width: 150px;
        }

        #divoperator {
            float: left;
            width: 50px;
            padding: 60px 5px;
        }

            #divoperator input[type="button"] {
                margin: 10px 0;
            }

        #selDepartments {
            display: block;
            width: 100%;
        }

        #lbEmployees {
            display: block;
            width: 100%;
            height: 200px;
        }

        #selSelectedEmployees {
            display: block;
            width: 100%;
            height: 225px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    会议预定 > 预定会议
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>会议信息</legend>
        <table class="formtable">
            <tr>
                <td>会议名称：</td>
                <td>
                    <asp:TextBox ID="txtMeetingName" runat="server" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="valMeetingName" runat="server"
                        ControlToValidate="txtMeetingName" ErrorMessage="*" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>预计参加人数：</td>
                <td>                    
                    <asp:TextBox ID="txtNumberofParticipants" runat="server" placeholder="填写一个正数"/>
                    <asp:RequiredFieldValidator ID="valNumberofParticipants1" runat="server"
                        ControlToValidate="txtNumberofParticipants" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RangeValidator ID="valNumberofParticipants2" runat="server"
                        ControlToValidate="txtNumberofParticipants" MinimumValue="1" MaximumValue="300" Type="Integer"
                        ErrorMessage="请填写1~300之间的正整数" CssClass="error" Display="Dynamic"/>
                </td>
            </tr>
            <tr>
                <td>预计开始时间：</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" placeholder="例如：2013-10-22" />
                    <asp:RequiredFieldValidator ID="valStartDate1" runat="server" 
                        ControlToValidate="txtStartDate" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RangeValidator ID="valStartDate2" runat="server"
                        ControlToValidate="txtStartDate" ErrorMessage="日期格式错误" CssClass="error" Display="Dynamic" 
                        Type="Date"/>
                    <asp:TextBox ID="txtStartTime" runat="server" TextMode="Time" placeholder="例如：17:35"/>
                    <asp:RequiredFieldValidator ID="valStartTime1" runat="server"
                        ControlToValidate="txtStartTime" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RangeValidator ID="valStartTime2" runat="server"
                        ControlToValidate="txtStartTime" ErrorMessage="时间格式错误" CssClass="error" Display="Dynamic"
                        Type="String" MinimumValue="00:00" MaximumValue="23:59" />
                </td>
            </tr>
            <tr>
                <td>预计结束时间：</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" placeholder="例如：2013-10-22" />
                    <asp:RequiredFieldValidator ID="valEndDate1" runat="server" 
                        ControlToValidate="txtEndDate" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RangeValidator ID="valEndDate2" runat="server"
                        ControlToValidate="txtEndDate" ErrorMessage="日期格式错误" CssClass="error" Display="Dynamic" 
                        Type="Date"/>
                    <asp:TextBox ID="txtEndTime" runat="server" TextMode="Time" placeholder="例如：19:35"/>
                    <asp:RequiredFieldValidator ID="valEndTime1" runat="server"
                        ControlToValidate="txtEndTime" ErrorMessage="*" CssClass="error" Display="Dynamic" />
                    <asp:RangeValidator ID="valEndTime2" runat="server"
                        ControlToValidate="txtEndTime" ErrorMessage="时间格式错误" CssClass="error" Display="Dynamic"
                        Type="String" MinimumValue="00:00" MaximumValue="23:59" />
                </td>
            </tr>
            <tr>
                <td>选择会议室：</td>
                <td>
                    <asp:DropDownList ID="ddlRooms" runat="server" />
                </td>
            </tr>
            <tr>
                <td>会议说明：</td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td>选择参会人员：</td>
                <td>
                    <asp:UpdatePanel ID="up" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDepartments" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnRemove" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divfrom">
                                <asp:DropDownList ID="ddlDepartments" runat="server" Width="100%" AutoPostBack="true" 
                                    ClientIDMode="Static" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged"/>
                                <asp:ListBox ID="lbEmployees" runat="server" SelectionMode="Multiple" 
                                    ClientIDMode="Static" Width="100%" Height="200" />
                            </div>
                            <div id="divoperator">
                                <asp:Button ID="btnAdd" runat="server" Text="&gt;" CssClass="clickbutton" OnClick="btnAdd_Click" ValidationGroup="g2"/>
                                <asp:Button ID="btnRemove" runat="server" Text="&lt;" CssClass="clickbutton" OnClick="btnRemove_Click" ValidationGroup="g2"/>
                            </div>
                            <div id="divto">
                                <asp:ListBox ID="lbSelectedEmployees" runat="server" SelectionMode="Multiple" 
                                    ClientIDMode="Static" Width="100%" Height="225" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>                    
                </td>
            </tr>
            <tr>
                <td class="command" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="预定会议" CssClass="clickbutton" OnClick="btnSubmit_Click" />
                    <input type="reset" class="clickbutton" value="重置" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
