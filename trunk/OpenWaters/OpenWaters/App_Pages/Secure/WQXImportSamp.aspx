<%@ Page Title="Open Waters - Import Samples" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportSamp.aspx.cs" Inherits="OpenEnvironment.WQXImportSamp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <h1>
        Confirm Sample Data to Import
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
    <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"  AutoGenerateColumns="False" OnRowDataBound="grdImport_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="Include in Import">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkImport" runat="server" Checked='<%# VerifyCheck(Eval("IMPORT_STATUS_CD")) %>' Enabled="true" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TEMP_SAMPLE_IDX" HeaderText="ImportSampleID" SortExpression="TEMP_SAMPLE_IDX" />
            <asp:BoundField DataField="TEMP_RESULT_IDX" HeaderText="ImportResultID" SortExpression="TEMP_RESULT_IDX" />
            <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
            <asp:BoundField DataField="PROJECT_ID" HeaderText="Project ID" SortExpression="PROJECT_ID" />
            <asp:BoundField DataField="MONLOC_ID" HeaderText="Mon Loc ID" SortExpression="MONLOC_ID" />
            <asp:BoundField DataField="ACTIVITY_ID" HeaderText="Activity ID" SortExpression="ACTIVITY_ID" />
            <asp:BoundField DataField="ACT_START_DT" HeaderText="Activity Start Date" SortExpression="ACT_START_DT" />
            <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME" />
            <asp:BoundField DataField="RESULT_MSR" HeaderText="Result" SortExpression="RESULT_MSR" />
            <asp:BoundField DataField="RESULT_MSR_UNIT" HeaderText="Unit" SortExpression="RESULT_MSR_UNIT" />
            <asp:BoundField DataField="IMPORT_STATUS_CD" HeaderText="Import Status" SortExpression="IMPORT_STATUS_CD" />
            <asp:BoundField DataField="IMPORT_STATUS_DESC" HeaderText="Import Errors" SortExpression="IMPORT_STATUS_DESC" />
        </Columns>
    </asp:GridView>
    <div class="btnRibbon">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel Import" OnClick="btnCancel_Click"  />
        <asp:Button ID="btnSample" runat="server" CssClass="btn" Text="View Imported Samples" Visible="false" OnClick="btnSample_Click"  />
    </div>
</asp:Content>
