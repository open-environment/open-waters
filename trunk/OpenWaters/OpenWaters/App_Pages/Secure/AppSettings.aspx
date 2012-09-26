<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="AppSettings.aspx.cs" Inherits="OpenEnvironment.AppSettings" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="dsAppSettings" runat="server" ConnectionString="name=OpenEnvironmentEntities" 
    DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_OE_APP_SETTINGS"
        ContextTypeName="OpenEnvironment.App_Logic.DataAccessLayer.OpenEnvironmentEntities" EnableUpdate="True" >
    </asp:EntityDataSource>
    <h2>
        General Application Settings
    </h2>
    <div class="divHelp">
        This page allows administrators to edit global application settings.
    </div>
    <asp:GridView ID="GridView1" runat="server" GridLines="None" CssClass="grd"
    PagerStyle-CssClass="pgr" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt" 
    DataSourceID="dsAppSettings" AutoGenerateEditButton="True" DataKeyNames="SETTING_IDX">
        <Columns>
            <asp:BoundField DataField="SETTING_IDX" HeaderText="ID" SortExpression="SETTING_IDX" />
            <asp:BoundField DataField="SETTING_NAME" HeaderText="Setting Name" SortExpression="SETTING_NAME" />
            <asp:BoundField DataField="SETTING_VALUE" HeaderText="Setting Value" SortExpression="SETTING_VALUE" />
        </Columns>
    </asp:GridView>
</asp:Content>
