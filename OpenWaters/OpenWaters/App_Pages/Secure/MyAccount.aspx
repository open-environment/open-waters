<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="OpenEnvironment.MyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding: 0px 0px 0px 15px">
        <h2>
            My Account
        </h2>
        <div class="divHelp">
            View and edit your account information.
        </div>
        <p>
            <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        </p>
        <asp:Panel runat="server" DefaultButton="btnSave">
            <div class="row">
                <span class="fldLbl">User Name:</span>
                <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="fldTxt" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="row">
                    <span class="fldLbl">First Name:</span>
                    <asp:TextBox ID="txtFName" runat="server" Width="200px" CssClass="fldTxt" MaxLength="40"></asp:TextBox>
            </div>
            <div class="row">
                <span class="fldLbl">Last Name:</span>
                <asp:TextBox ID="txtLName" runat="server" Width="200px" CssClass="fldTxt" MaxLength="40"></asp:TextBox>
            </div>
            <div class="row">
                    <span class="fldLbl">Email:</span>
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="fldTxt" MaxLength="150"></asp:TextBox>
            </div>
            <div class="row">
                    <span class="fldLbl">Phone:</span>
                    <asp:TextBox ID="txtPhone" runat="server" Width="200px" CssClass="fldTxt" MaxLength="12"></asp:TextBox>
            </div>
            <div class="row">
                <span class="fldLbl">Roles:</span>
                <asp:ListBox ID="lbRoleList" Width="200px" CssClass="fldTxt" runat="server" Height="100%" ></asp:ListBox>
                <br />
            </div>
            <div class="row">
                <span class="fldLbl">Organizations:</span>
                <asp:ListBox ID="lblOrgList" Width="200px" CssClass="fldTxt" runat="server" Height="100%" ></asp:ListBox>
                <br />
            </div>
            <br />
            <div class="btnRibbon">
                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnResetPwd" runat="server" CssClass="btn" Text="Change Password" OnClick="btnResetPwd_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
