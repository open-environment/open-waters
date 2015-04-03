<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="OpenEnvironment.Verify" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Account Verification
    </h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    </p>
    <asp:Panel ID="pnl1" runat="server" DefaultButton="btnSave" Visible="false">
        <p>
            Enter your permanent password below.
        </p>

        <div class="row">
            <span class="fldLbl">Set Password</span>
            <asp:TextBox ID="txtPwd" runat="server" CssClass="fldTxt" Width="200px" TextMode="Password"  MaxLength="50"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Confirm Password</span>
            <asp:TextBox ID="txtPwd2" runat="server" CssClass="fldTxt" Width="200px" TextMode="Password"  MaxLength="50"></asp:TextBox>
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save" />
        </div>
    </asp:Panel>
</asp:Content>
