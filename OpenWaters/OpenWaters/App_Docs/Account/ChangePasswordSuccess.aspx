<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePasswordSuccess.aspx.cs" Inherits="OpenEnvironment.Account.ChangePasswordSuccess" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Change Password
    </h2>
    <p>
        Your password has been changed successfully to a permanent password. 

        </p>
    <p>
        Click to login using your new password:
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" 
            style="font-weight: 700">HyperLink</asp:HyperLink>

    </p>
</asp:Content>
