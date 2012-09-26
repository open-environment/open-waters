<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="OpenEnvironment.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Forgot Password
    </h2>
	<div class="divHelp">If you already have an account and have forgotten your password, please enter your 
        username in the box provided below. A new temporary password will be emailed to you.
	</div>
    <p>
        <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000"></asp:Label>
    </p>
    <fieldset>
        <legend>Reset your Open Waters password</legend>
        <div class="row">
            <asp:Label ID="EmailLabel" runat="server" CssClass="fldLbl" AssociatedControlID="UserID">Username:</asp:Label>
            <br />
            <asp:TextBox ID="UserID" runat="server" Width="200px" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <asp:Label ID="Label1" runat="server" CssClass="fldLbl">Email:</asp:Label>
            <br />
            <asp:TextBox ID="txtEmail2" runat="server" Width="200px" CssClass="fldTxt"></asp:TextBox>
        </div>
    </fieldset>
	<div class="pgBtnPnl">
		<asp:Button ID="btnSubmit" runat="server" Text="Reset Password" CssClass="btn" onclick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn" onclick="btnCancel_Click" />
	</div>
</asp:Content>
