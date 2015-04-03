<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  CodeBehind="Register.aspx.cs" Inherits="OpenEnvironment.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        <asp:Label ID="lblTitle" runat="server">Create a New Account</asp:Label>
    </h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    </p>
    <asp:Panel ID="pnl1" runat="server" DefaultButton="btnSave">
        <p>
            Use the form below to create a new account.
        </p>

        <div class="row">
            <span class="fldLbl">User ID</span>
            <asp:TextBox ID="txtUserID" runat="server" MaxLength="25" Width="200px" CssClass="fldTxt" ></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">First Name</span>
            <asp:TextBox ID="txtFName" runat="server" CssClass="fldTxt" Width="200px" MaxLength="40"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl" >Last Name</span>
            <asp:TextBox ID="txtLName" runat="server" CssClass="fldTxt" Width="200px" MaxLength="40"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Email</span>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="fldTxt" Width="200px"  MaxLength="150"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Phone</span>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="fldTxt" Width="100px" MaxLength="12"></asp:TextBox>
            <span class="fldLbl" style="width:10px" ></span>
            <span class="fldLbl" style="width:75px" >Extention</span>
            <asp:TextBox ID="txtPhoneExt" runat="server" CssClass="fldTxt"  Width="50px" MaxLength="4"></asp:TextBox>
        </div>
        <asp:Panel ID="pnlBeta" runat="server"  Visible="false" CssClass="row">
            <span class="fldLbl">Beta Invite Code</span>
            <asp:TextBox ID="txtBetaKey" runat="server" CssClass="fldTxt" Width="200px"  MaxLength="10"></asp:TextBox>
        </asp:Panel>
        <br />
        <div class="smallnote row">
            By signing up for Open Waters, you agree to the <a href="../App_Pages/Public/Terms.aspx" target="_blank">Terms of Service</a>.
        </div>
        <div class="btnRibbon">
            <asp:Button ID="btnBack" runat="server" CssClass="btn" OnClick="btnBack_Click" Text="Go Back" />
            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl2" runat="server" Visible="false" >
        <div class="row">
            <h3>Your Account is Registered</h3>
            <h4 class="failureNotification">Email Verification Required!</h4>
            You will need to verify your account. Please <b><u>check your email</u></b> and click the link in the email to verify your account.
            <div class="btnRibbon">
                <asp:Button ID="btnBack2" runat="server" CssClass="btn" OnClick="btnBack_Click" Text="Return to Login Page" />
            </div>

        </div>
    </asp:Panel>
</asp:Content>
