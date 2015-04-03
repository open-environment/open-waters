<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="OpenEnvironment.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
        An error has been encountered in the Open Waters application.
        <br />
        <asp:Button ID="btnReturn" runat="server" Text="Return" CssClass="btn" />
    </div>
</asp:Content>
