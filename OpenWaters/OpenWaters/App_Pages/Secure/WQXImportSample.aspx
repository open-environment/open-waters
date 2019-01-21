<%@ Page Title="Open Waters - Import Samples" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportSample.aspx.cs" Inherits="OpenEnvironment.WQXImportSample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
<style>
    #gridwrap { height: calc(100vh - 320px); }
    .grd td { padding: 1px; font-size:9pt; border-bottom-color:#999; color: #333; }
</style>
    <h1>
        Confirm Sample Data to Import
    </h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="pnlFilter" runat="server" CssClass="fltBox" >
        <div class="fltTitle" style="background-color:#eb5656">Note: You must click the 'Import Selected Rows' button to complete the import process!</div>
        <div class="fltMain">
            <div class="row">
                <asp:CheckBox ID="chkWQXImport" runat="server" Text="Automatically submit imported data to EPA/WQX" CssClass="fldTxt" Checked="true" />
                <asp:Panel ID="pnlActivityID" Visible="false" runat="server" CssClass="row" >
                    <span class="row">You are trying to import activity IDs that already exist for this organization. How would you like to handle this conflict?</span>
                    <br />
                    <asp:DropDownList ID="ddlActivityReplaceType" runat="server" CssClass="fldTxt">
                        <asp:ListItem Value="R" Text="Delete any matching Activity IDs and reimport (recommended)" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="U" Text="Update existing activities, appending results to previous"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </div>
            <div class="row">
                <asp:Button ID="btnImport" runat="server" CssClass="btn" Style="font-size:12pt" Text="Import Selected Rows" OnClick="btnImport_Click" />        
            </div>
        </div>
    </asp:Panel>
    <div id="gridwrap" style="width:100%; overflow:auto">
        <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AllowPaging="true" PageSize="1000"  AutoGenerateColumns="False" OnRowDataBound="grdImport_RowDataBound" 
            Width="100%" Style="word-wrap: break-word; " OnPageIndexChanging="grdImport_PageIndexChanging"  >
            <Columns>
                <asp:TemplateField HeaderText="Include in Import"   HeaderStyle-Width="60px">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkImport" runat="server" Checked='<%# VerifyCheck(Eval("IMPORT_STATUS_CD")) %>' Enabled="false" />
                        <asp:HiddenField ID="hdnTempSampleIDX" Value='<%# Eval("TEMP_SAMPLE_IDX") %>' runat="server" />
                        <asp:HiddenField ID="hdnTempResultIDX" Value='<%# Eval("TEMP_RESULT_IDX") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IMPORT_STATUS_CD" HeaderText="Import Status" SortExpression="IMPORT_STATUS_CD"  />
                <asp:BoundField DataField="IMPORT_STATUS_DESC" HeaderText="Import Errors" SortExpression="IMPORT_STATUS_DESC" />
                <asp:BoundField DataField="PROJECT_ID" HeaderText="Project ID" SortExpression="PROJECT_ID" />
                <asp:BoundField DataField="MONLOC_ID" HeaderText="Mon Loc ID" SortExpression="MONLOC_ID" />
                <asp:BoundField DataField="ACTIVITY_ID" HeaderText="Activity ID" SortExpression="ACTIVITY_ID" />
                <asp:BoundField DataField="ACT_TYPE" HeaderText="Activity Type" SortExpression="ACT_TYPE" />
                <asp:BoundField DataField="ACT_MEDIA" HeaderText="Activity Media" SortExpression="ACT_MEDIA" />
                <asp:BoundField DataField="ACT_SUBMEDIA" HeaderText="Sub-Media" SortExpression="ACT_SUBMEDIA" />
                <asp:BoundField DataField="ACT_START_DT" HeaderText="Start Date" SortExpression="ACT_START_DT" />
                <asp:BoundField DataField="ACT_END_DT" HeaderText="End Date" SortExpression="ACT_END_DT" />
                <asp:BoundField DataField="ACT_TIME_ZONE" HeaderText="Time Zone" SortExpression="ACT_TIME_ZONE"  />
                <asp:BoundField DataField="RELATIVE_DEPTH_NAME" HeaderText="Relative Depth" SortExpression="RELATIVE_DEPTH_NAME" />
                <asp:BoundField DataField="ACT_DEPTHHEIGHT_MSR" HeaderText="Activity Depth" SortExpression="ACT_DEPTHHEIGHT_MSR" />
                <asp:BoundField DataField="ACT_DEPTHHEIGHT_MSR_UNIT" HeaderText="Depth Unit" SortExpression="ACT_DEPTHHEIGHT_MSR_UNIT" />
                <asp:BoundField DataField="TOP_DEPTHHEIGHT_MSR" HeaderText="Top Depth" SortExpression="TOP_DEPTHHEIGHT_MSR" />
                <asp:BoundField DataField="TOP_DEPTHHEIGHT_MSR_UNIT" HeaderText="Top Depth Unit" SortExpression="TOP_DEPTHHEIGHT_MSR_UNIT" />
                <asp:BoundField DataField="BOT_DEPTHHEIGHT_MSR" HeaderText="Bot Depth" SortExpression="BOT_DEPTHHEIGHT_MSR" />
                <asp:BoundField DataField="BOT_DEPTHHEIGHT_MSR_UNIT" HeaderText="Bot Depth Unit" SortExpression="BOT_DEPTHHEIGHT_MSR_UNIT" />
                <asp:BoundField DataField="ACT_COMMENT" HeaderText="Activity Comment" SortExpression="ACT_COMMENT" />
                <asp:BoundField DataField="BIO_ASSEMBLAGE_SAMPLED" HeaderText="Assemblage Sampled" SortExpression="BIO_ASSEMBLAGE_SAMPLED" />
                <asp:BoundField DataField="BIO_DURATION_MSR" HeaderText="Duration" SortExpression="BIO_DURATION_MSR" />
                <asp:BoundField DataField="BIO_DURATION_MSR_UNIT" HeaderText="Duration Unit" SortExpression="BIO_DURATION_MSR_UNIT" />
                <asp:BoundField DataField="BIO_SAMP_COMPONENT" HeaderText="Bio Samp Component" SortExpression="BIO_SAMP_COMPONENT" />
                <asp:BoundField DataField="BIO_SAMP_COMPONENT_SEQ" HeaderText="Samp Component Seq" SortExpression="BIO_SAMP_COMPONENT_SEQ" />
                <asp:BoundField DataField="SAMP_COLL_METHOD_ID" HeaderText="Collection Method" SortExpression="SAMP_COLL_METHOD_ID" />
                <asp:BoundField DataField="SAMP_COLL_METHOD_CTX" HeaderText="Collection Method Context" SortExpression="SAMP_COLL_METHOD_CTX" />
                <asp:BoundField DataField="SAMP_COLL_EQUIP" HeaderText="Equipment" SortExpression="SAMP_COLL_EQUIP" />
                <asp:BoundField DataField="SAMP_COLL_EQUIP_COMMENT" HeaderText="Equipment Comment" SortExpression="SAMP_COLL_EQUIP_COMMENT" />
                <asp:BoundField DataField="SAMP_PREP_ID" HeaderText="Samp Prep" SortExpression="SAMP_PREP_ID" />
                <asp:BoundField DataField="SAMP_PREP_CTX" HeaderText="Samp Prep Context" SortExpression="SAMP_PREP_CTX" />
                <asp:BoundField DataField="RESULT_DETECT_CONDITION" HeaderText="Detect Condition" SortExpression="RESULT_DETECT_CONDITION" />
                <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME" />
                <asp:BoundField DataField="METHOD_SPECIATION_NAME" HeaderText="Method Speciation" SortExpression="METHOD_SPECIATION_NAME" />
                <asp:BoundField DataField="RESULT_SAMP_FRACTION" HeaderText="Samp Fraction" SortExpression="RESULT_SAMP_FRACTION" />
                <asp:BoundField DataField="RESULT_MSR" HeaderText="Result" SortExpression="RESULT_MSR"    />
                <asp:BoundField DataField="RESULT_MSR_UNIT" HeaderText="Unit" SortExpression="RESULT_MSR_UNIT" />
                <asp:BoundField DataField="RESULT_MSR_QUAL" HeaderText="Qualifier" SortExpression="RESULT_MSR_QUAL"     />
                <asp:BoundField DataField="RESULT_STATUS" HeaderText="Result Status" SortExpression="RESULT_STATUS"     />
                <asp:BoundField DataField="STATISTIC_BASE_CODE" HeaderText="Statistic Base" SortExpression="STATISTIC_BASE_CODE"  />
                <asp:BoundField DataField="RESULT_VALUE_TYPE" HeaderText="Result Value Type" SortExpression="RESULT_VALUE_TYPE"  />
                <asp:BoundField DataField="WEIGHT_BASIS" HeaderText="Weight Basis" SortExpression="WEIGHT_BASIS" />
                <asp:BoundField DataField="TIME_BASIS" HeaderText="Time Basis" SortExpression="TIME_BASIS" />
                <asp:BoundField DataField="TEMP_BASIS" HeaderText="Temp Basis" SortExpression="TEMP_BASIS" />
                <asp:BoundField DataField="PARTICLESIZE_BASIS" HeaderText="Partical Size" SortExpression="PARTICLESIZE_BASIS" />
                <asp:BoundField DataField="PRECISION_VALUE" HeaderText="Precision" SortExpression="PRECISION_VALUE" />
                <asp:BoundField DataField="BIAS_VALUE" HeaderText="Bias" SortExpression="BIAS_VALUE" />
                <asp:BoundField DataField="RESULT_COMMENT" HeaderText="Result Comment" SortExpression="RESULT_COMMENT" />
                <asp:BoundField DataField="BIO_INTENT_NAME" HeaderText="Bio Intent" SortExpression="BIO_INTENT_NAME" />
                <asp:BoundField DataField="BIO_SUBJECT_TAXONOMY" HeaderText="Taxonomy" SortExpression="BIO_SUBJECT_TAXONOMY" />
                <asp:BoundField DataField="FREQ_CLASS_CODE" HeaderText="Freq Class" SortExpression="FREQ_CLASS_CODE" />
                <asp:BoundField DataField="FREQ_CLASS_UNIT" HeaderText="Freq Class Unit" SortExpression="FREQ_CLASS_UNIT" />
                <asp:BoundField DataField="ANAL_METHOD_ID" HeaderText="Anal Method" SortExpression="ANAL_METHOD_ID" />
                <asp:BoundField DataField="ANAL_METHOD_CTX" HeaderText="Anal Method Context" SortExpression="ANAL_METHOD_CTX" />
                <asp:BoundField DataField="LAB_NAME" HeaderText="Lab Name" SortExpression="LAB_NAME" />
                <asp:BoundField DataField="ANAL_START_DT" HeaderText="Analysis Start Date" SortExpression="ANAL_START_DT" />
                <asp:BoundField DataField="ANAL_END_DT" HeaderText="Analysis End Date" SortExpression="ANAL_END_DT" />
                <asp:BoundField DataField="LAB_COMMENT_CODE" HeaderText="Lab Comment" SortExpression="LAB_COMMENT_CODE" />
                <asp:BoundField DataField="DETECTION_LIMIT" HeaderText="Detection Limit" SortExpression="DETECTION_LIMIT" />
                <asp:BoundField DataField="LAB_REPORTING_LEVEL" HeaderText="Lab Reporting Level" SortExpression="LAB_REPORTING_LEVEL" />
                <asp:BoundField DataField="PQL" HeaderText="PQL" SortExpression="PQL" />
                <asp:BoundField DataField="LOWER_QUANT_LIMIT" HeaderText="Lower Quant Limit" SortExpression="LOWER_QUANT_LIMIT" />
                <asp:BoundField DataField="UPPER_QUANT_LIMIT" HeaderText="Upper Quant Limit" SortExpression="UPPER_QUANT_LIMIT" />
                <asp:BoundField DataField="DETECTION_LIMIT_UNIT" HeaderText="Detection Limit Unit" SortExpression="DETECTION_LIMIT_UNIT" />
                <asp:BoundField DataField="LAB_SAMP_PREP_START_DT" HeaderText="Lab Prep Start Date" SortExpression="LAB_SAMP_PREP_START_DT" />
                <asp:BoundField DataField="DILUTION_FACTOR" HeaderText="Dilution Factor" SortExpression="DILUTION_FACTOR" />
            </Columns>
        </asp:GridView>
    </div>
    <div class="btnRibbon">
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel Import" OnClick="btnCancel_Click"  />
        <asp:Button ID="btnSample" runat="server" CssClass="btn" Text="View Imported Samples" Visible="false" OnClick="btnSample_Click"  />
    </div>
</asp:Content>
