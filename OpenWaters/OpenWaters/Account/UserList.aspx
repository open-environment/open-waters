<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="OpenEnvironment.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="dsUsers" runat="server" 
        ConnectionString="name=OpenEnvironmentEntities" ContextTypeName="OpenEnvironment.App_Logic.DataAccessLayer.OpenEnvironmentEntities"
        DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" 
        EntitySetName="T_OE_USERS">
    </asp:EntityDataSource>
    <h2>        
        Manage Users
    </h2>
	<div class="divHelp">	
		This page lists all application users and allows you to add new users or edit an existing user.
	</div>
    <asp:GridView ID="grdUsers" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt"   
        AllowSorting="True" DataKeyNames="USER_IDX" 
        DataSourceID="dsUsers" AllowPaging="True" PageSize="20" 
    onrowcommand="grdUsers_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" CommandArgument='<% #Eval("USER_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="USER_IDX" HeaderText="ID" SortExpression="USER_IDX" />
                <asp:BoundField DataField="USER_ID" HeaderText="Username" SortExpression="USER_ID" />
                <asp:BoundField DataField="FNAME" HeaderText="First Name" SortExpression="FNAME" />
                <asp:BoundField DataField="LNAME" HeaderText="Last Name" SortExpression="LNAME" />
                <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL" />
                <asp:CheckBoxField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" />
                <asp:BoundField DataField="LASTLOGIN_DT" HeaderText="Last Login" SortExpression="LASTLOGIN_DT" />
                <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE" />
                <asp:BoundField DataField="PHONE_EXT" HeaderText="Phone Ext." SortExpression="PHONE_EXT" />
                <asp:BoundField DataField="CREATE_USERID" HeaderText="Created By" SortExpression="CREATE_USERID" />
                <asp:BoundField DataField="CREATE_DT" HeaderText="Created Date" SortExpression="CREATE_DT" />
                <asp:BoundField DataField="MODIFY_USERID" HeaderText="Modified By" SortExpression="MODIFY_USERID" />
                <asp:BoundField DataField="MODIFY_DT" HeaderText="Modified Date" SortExpression="MODIFY_DT" />
            </Columns>
    </asp:GridView>
</asp:Content>
