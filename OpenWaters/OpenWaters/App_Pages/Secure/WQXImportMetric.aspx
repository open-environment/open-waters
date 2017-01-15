<%@ Page Title="Open Waters - Import Metrics" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportMetric.aspx.cs" Inherits="OpenEnvironment.WQXImportMetric" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <h1>
        Confirm Activity Metrics Data to Import
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
    <div style="height: 500px; width:100%; overflow: auto;">
    <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"  AutoGenerateColumns="False" OnRowDataBound="grdImport_RowDataBound" Style="word-wrap: break-word; " 
         DataKeyNames="TEMP_ACTIVITY_METRIC_IDX">
        <Columns>
            <asp:TemplateField HeaderText="Include in Import">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkImport" runat="server" Checked='<%# VerifyCheck(Eval("IMPORT_STATUS_CD")) %>' Enabled="true"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
            <asp:BoundField DataField="ACTIVITY_ID" HeaderText="Activity ID" SortExpression="ACTIVITY_ID" />
            <asp:BoundField DataField="METRIC_TYPE_ID" HeaderText="Metric Type ID" SortExpression="METRIC_TYPE_ID" />
            <asp:BoundField DataField="METRIC_TYPE_ID_CONTEXT" HeaderText="Metric Type Context" SortExpression="METRIC_TYPE_ID_CONTEXT" />
            <asp:BoundField DataField="METRIC_SCORE" HeaderText="Metric Score" SortExpression="METRIC_SCORE" />
            <asp:BoundField DataField="METRIC_VALUE_MSR" HeaderText="Metric Value" SortExpression="METRIC_VALUE_MSR" />
            <asp:BoundField DataField="METRIC_VALUE_MSR_UNIT" HeaderText="Metric Value Unit" SortExpression="METRIC_VALUE_MSR_UNIT" />
            <asp:BoundField DataField="METRIC_COMMENT" HeaderText="Metric Comment" SortExpression="METRIC_COMMENT" />
            <asp:BoundField DataField="IMPORT_STATUS_CD" HeaderText="Import Status" SortExpression="IMPORT_STATUS_CD" />
            <asp:BoundField DataField="IMPORT_STATUS_DESC" HeaderText="Import Errors" SortExpression="IMPORT_STATUS_DESC" />
        </Columns>
    </asp:GridView>
    </div>
    <div class="btnRibbon">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel Import" OnClick="btnCancel_Click"  />
    </div>

</asp:Content>
