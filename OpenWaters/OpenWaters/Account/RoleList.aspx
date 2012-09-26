<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="OpenEnvironment.RoleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="dsRoles" runat="server" 
        ConnectionString="name=OpenEnvironmentEntities" ContextTypeName="OpenEnvironment.App_Logic.DataAccessLayer.OpenEnvironmentEntities"
        DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" 
        EntitySetName="T_OE_ROLES">
    </asp:EntityDataSource>
    <h2>        
        Manage Roles
    </h2>
	<div class="divHelp">	
		This page lists all application securty roles. Security Roles define a set of security privileges within the system.
	</div>
    <asp:GridView ID="grdRoles" runat="server" CssClass="grd" 
    PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  
        AllowSorting="True" DataSourceID="dsRoles" 
        AutoGenerateColumns="False" onrowcommand="grdRoles_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" CommandArgument='<% #Eval("ROLE_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ROLE_IDX" HeaderText="Role ID" SortExpression="ROLE_IDX" />
                <asp:BoundField DataField="ROLE_NAME" HeaderText="Role Name" SortExpression="ROLE_NAME" />
                <asp:BoundField DataField="ROLE_DESC" HeaderText="Role Description" SortExpression="ROLE_DESC" />
                <asp:BoundField DataField="CREATE_USERID" HeaderText="Created By" />
                <asp:BoundField DataField="CREATE_DT" HeaderText="Created Date" />
                <asp:BoundField DataField="MODIFY_USERID" HeaderText="Modified By" />
                <asp:BoundField DataField="MODIFY_DT" HeaderText="Modified Date" />
            </Columns>
    </asp:GridView>
    <br />
   	<div class="btnRibbon">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add New" 
            onclick="btnAdd_Click" />
    </div>

</asp:Content>
