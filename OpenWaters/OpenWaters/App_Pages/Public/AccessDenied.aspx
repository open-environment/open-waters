<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="OpenEnvironment.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Access Denied</h2>
    <div class="divHelp">
        You do not have security access to this page. Please click the link below to 
        return to the dashboard.
    </div>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <asp:Button ID="btnReturn" runat="server" CssClass="btn" 
        Text="Return to Dashboard" onclick="btnReturn_Click" />
</asp:Content>
