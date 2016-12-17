<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="SetOrg.aspx.cs" Inherits="OpenEnvironment.SetOrg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdlURL" runat="server" />
    <p>
        Use the dropdown above to select the organization you wish to work with.
    </p>
    <asp:Button ID="btnReturn" runat="server" CssClass="btn" Text="Return" OnClick="btnReturn_Click" />
</asp:Content>
