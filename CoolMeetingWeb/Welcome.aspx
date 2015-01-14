<%@ Page Title="" Language="C#" MasterPageFile="~/BasePage.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="ETC.EEG.CoolMeeting.Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .welcomeimg {
            width:150px;
            height:150px;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
    欢迎
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div style="text-align:center">
        <img src="../images/welcome1.png" class="welcomeimg"/>
        <img src="../images/welcome2.png" class="welcomeimg"/>
        <img src="../images/welcome3.png" class="welcomeimg"/>
        <img src="../images/welcome4.png" class="welcomeimg"/>
    </div>
</asp:Content>
