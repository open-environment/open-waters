<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportProject.aspx.cs" Inherits="OpenEnvironment.WQXImportProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <h1>
        Confirm Project Data to Import
    </h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="pnlFilter" runat="server" CssClass="fltBox" >
        <div class="fltTitle" style="background-color:#eb5656">Note: You must click the 'Import Selected Rows' button to complete the import process!</div>
        <div class="fltMain">
            <div class="row">
                <asp:CheckBox ID="chkWQXImport" runat="server" Text="Automatically submit imported data to EPA/WQX" CssClass="fldTxt" Checked="true" />
            </div>
            <div class="row">
                <asp:Button ID="btnImport" runat="server" CssClass="btn" Style="font-size:12pt" Text="Import Selected Rows" OnClick="btnImport_Click"  />        
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
            <asp:BoundField DataField="TEMP_PROJECT_IDX" HeaderText="ImportID" SortExpression="TEMP_PROJECT_IDX" />
            <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
            <asp:BoundField DataField="PROJECT_ID" HeaderText="Project ID" SortExpression="PROJECT_ID" />
            <asp:BoundField DataField="PROJECT_NAME" HeaderText="Project Name" SortExpression="PROJECT_NAME" />
            <asp:BoundField DataField="PROJECT_DESC" HeaderText="Project Desc" SortExpression="PROJECT_DESC" />
            <asp:BoundField DataField="SAMP_DESIGN_TYPE_CD" HeaderText="Sample Design Type" SortExpression="SAMP_DESIGN_TYPE_CD" />
            <asp:BoundField DataField="QAPP_APPROVAL_IND" HeaderText="QAPP Approve Indicator" SortExpression="QAPP_APPROVAL_IND" />
            <asp:BoundField DataField="QAPP_APPROVAL_AGENCY" HeaderText="QAPP Approval Agency" SortExpression="QAPP_APPROVAL_AGENCY" />
            <asp:BoundField DataField="IMPORT_STATUS_CD" HeaderText="Import Status" SortExpression="IMPORT_STATUS_CD" />
            <asp:BoundField DataField="IMPORT_STATUS_DESC" HeaderText="Import Errors" SortExpression="IMPORT_STATUS_DESC" />
        </Columns>
    </asp:GridView>
    <div class="btnRibbon">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel Import" OnClick="btnCancel_Click"  />
        <asp:Button ID="btnProject" runat="server" CssClass="btn" Text="View Projects" Visible="false" OnClick="btnProject_Click"   />
    </div>


</asp:Content>
