<%@ Page Title="Open Waters - Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenEnvironment._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="brdr-green" style="width: 500px; margin: 100px auto">
        <div class="ie9roundedgradient" style="width:100%" >
            <div class="rndPnlTop-green">
                Login to Open Waters
            </div>
        </div>
        <div style="padding: 10px 10px;">
            <asp:Label ID="lblTestWarn" runat="server" CssClass="fldErrLg" Text="" Visible="false" Style="margin-top:20px"></asp:Label>
            <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="true" OnLoggedIn="LoginUser_LoggedIn" OnLoginError="LoginUser_LoginError">
                <LayoutTemplate>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" ValidationGroup="LoginUserValidationGroup" />
                    <p>
                        <asp:Label ID="lblUserName" runat="server" CssClass="fldLbl" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="fldTxt" Width="280px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="lblPassword" runat="server" CssClass="fldLbl" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="fldTxt" Width="280px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <asp:Button CssClass="btn" ID="LoginButton" style="margin-left:150px" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                    &nbsp;&nbsp;<asp:HyperLink ID="lblPwd" runat="server" NavigateUrl="~/Account/ResetPassword.aspx" Text="(Forgot password)"></asp:HyperLink>
                </LayoutTemplate>
            </asp:Login>
        </div>
    </div>
</asp:Content>
