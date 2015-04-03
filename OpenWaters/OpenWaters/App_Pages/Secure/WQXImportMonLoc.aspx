<%@ Page Title="Open Waters - Import Mon Loc" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportMonLoc.aspx.cs" Inherits="OpenEnvironment.WQXImportMonLoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        #container { float:left;
        }
    </style>
    <h1>
        Confirm Monitoring Location Data to Import
    </h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="pnlFilter" runat="server" CssClass="fltBox" >
        <div class="fltTitle" style="background-color:#eb5656">Note: You must click the 'Import Selected Rows' button to complete the import process!</div>
        <div class="fltMain">
            <div class="row">
                <asp:CheckBox ID="chkWQXImport" runat="server" Text="Automatically submit imported data to EPA/WQX" CssClass="fldTxt" Checked="true" />
            </div>
            <div class="row">
                <asp:Button ID="btnImport" runat="server" CssClass="btn" Style="font-size:12pt" Text="Import Selected Rows" OnClick="btnImport_Click" />        
            </div>
        </div>
    </asp:Panel>
    <div style="height: 500px; overflow-x: auto;">
    <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"  AutoGenerateColumns="False" OnRowDataBound="grdImport_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="Include in Import">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkImport" runat="server" Checked='<%# VerifyCheck(Eval("IMPORT_STATUS_CD")) %>' Enabled="true" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TEMP_MONLOC_IDX" HeaderText="ImportID" SortExpression="TEMP_MONLOC_IDX" />
            <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
            <asp:BoundField DataField="MONLOC_ID" HeaderText="Mon Loc ID" SortExpression="MONLOC_ID" />
            <asp:BoundField DataField="MONLOC_NAME" HeaderText="Mon Loc Name" SortExpression="MONLOC_NAME" />
            <asp:BoundField DataField="MONLOC_TYPE" HeaderText="Mon Loc Type" SortExpression="MONLOC_TYPE" />
            <asp:BoundField DataField="MONLOC_DESC" HeaderText="Mon Loc Desc" SortExpression="MONLOC_DESC" />
            <asp:BoundField DataField="HUC_EIGHT" HeaderText="HUC 8" SortExpression="HUC_EIGHT" />
            <asp:BoundField DataField="HUC_TWELVE" HeaderText="HUC 12" SortExpression="HUC_TWELVE" />
            <asp:BoundField DataField="LATITUDE_MSR" HeaderText="Latitude" SortExpression="LATITUDE_MSR" />
            <asp:BoundField DataField="LONGITUDE_MSR" HeaderText="Longitude" SortExpression="LONGITUDE_MSR" />
            <asp:BoundField DataField="COUNTRY_CODE" HeaderText="Country" SortExpression="COUNTRY_CODE" />
            <asp:BoundField DataField="STATE_CODE" HeaderText="State Code" SortExpression="STATE_CODE" />
            <asp:BoundField DataField="COUNTY_CODE" HeaderText="County Code" SortExpression="COUNTY_CODE" />
            <asp:BoundField DataField="IMPORT_STATUS_CD" HeaderText="Import Status" SortExpression="IMPORT_STATUS_CD" />
            <asp:BoundField DataField="IMPORT_STATUS_DESC" HeaderText="Import Errors" SortExpression="IMPORT_STATUS_DESC" />
        </Columns>
    </asp:GridView>
    </div>
    <div class="btnRibbon">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel Import" OnClick="btnCancel_Click"  />
        <asp:Button ID="btnMonLoc" runat="server" CssClass="btn" Text="View Monitoring Locations" Visible="false" OnClick="btnMonLoc_Click"  />
    </div>

</asp:Content>
