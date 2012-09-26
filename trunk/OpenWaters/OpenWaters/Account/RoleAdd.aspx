<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="OpenEnvironment.RoleAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>        
        Add New Role
    </h2>
	<div class="divHelp">	
		This page allows application administrators to add a new application role. 
	</div>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    </p>
    <div class="row">
        <span class="fldLbl">Role Name:</span> 
        <asp:TextBox ID="txtRoleName" runat="server" CssClass="fldDefault" MaxLength="25"></asp:TextBox>
    </div>
    <div class="row">
        <span class="fldLbl">Role Description:</span> 
        <asp:TextBox ID="txtRoleDesc" runat="server" CssClass="fldDefault" Width="300px" MaxLength="100"></asp:TextBox>
    </div>
    <div class="btnRibbon">
        <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Go Back" 
            onclick="btnBack_Click" />
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" 
            onclick="btnSave_Click" />
    </div>

</asp:Content>
