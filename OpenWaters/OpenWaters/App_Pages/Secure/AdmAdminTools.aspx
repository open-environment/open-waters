<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="AdmAdminTools.aspx.cs" Inherits="OpenEnvironment.AdmAdminTools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        Transaction ID: 
        <asp:TextBox ID="txtTransID" runat="server" CssClass="fldTxt"></asp:TextBox>
        <br />

    </div>
    <div class="btnRibbon">
        <asp:Button ID="btnGetStatus" runat="server" Text="Get Status" CssClass="btn" OnClick="btnGetStatus_Click" />
        <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn" OnClick="btnDownload_Click" />
        <asp:Button ID="btnTransHistory" runat="server" Text="Get Transaction History" CssClass="btn" OnClick="btnTransHistory_Click" />
    </div>
</asp:Content>
