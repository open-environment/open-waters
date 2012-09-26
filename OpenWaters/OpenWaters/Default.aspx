<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenEnvironment._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="divBrdr" style="width:500px; margin: 100px auto">
        <div style="color: #cccccc; border-top-left-radius: 30px;  border-top-right-radius: 10px;
            background-color: #003300; font-size: 11pt; font-weight: bold; padding: 6px; text-align: center">
            Login
        </div>
        <div style="padding: 10px 10px;">
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
   <br /><br /><br /> 
</asp:Content>
