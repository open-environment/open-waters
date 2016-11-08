<%@ Page Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="AssessEdit.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.AssessEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //handles css for tab clicking
        $(document).ready(function () {
            $(".tabs-menu a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("current");
                $(this).parent().siblings().removeClass("current");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });
    </script>
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the assessment report - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function GetConfirmationSubmit() {
            var reply = confirm("WARNING: This will submit the report to EPA - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <h1>
        Assessment Report Details
    </h1>
    <asp:HiddenField ID="hdnReportIDX" runat="server" />
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <table style="width: 100%">
        <tr>
            <td style="width: 100%; vertical-align:top">
                <div class="row">
                    <span class="fldLbl">Report Name:</span>
                    <asp:TextBox ID="txtRptName" MaxLength="100" runat="server" Width="300px"  CssClass="fldTxt"></asp:TextBox>
                </div>
                <div class="row">
                    <span class="fldLbl">Date From:</span>
                    <asp:TextBox ID="txtRptFrom" MaxLength="12" runat="server" Width="100px"  CssClass="fldTxt"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="true" AutoCompleteValue="false"
                        Enabled="True" TargetControlID="txtRptFrom" MaskType="Date" Mask="99/99/9999">
                    </ajaxToolkit:MaskedEditExtender>                    
                    <span class="fldLbl" style="width:75px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date To:</span>
                    <asp:TextBox ID="txtRptTo" MaxLength="12" runat="server" Width="100px"  CssClass="fldTxt"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true" AutoCompleteValue="false"
                        Enabled="True" TargetControlID="txtRptTo" MaskType="Date" Mask="99/99/9999">
                    </ajaxToolkit:MaskedEditExtender>
                </div>
                <asp:Panel ID="pnlTabs" runat="server" Visible="false">
                <div id="tabs-container"  >
                    <ul class="tabs-menu" >
                        <li class="current"><a href="#tab-1">Assessment Units</a></li>
                        <li><a href="#tab-2">Assessments</a></li>
                        <li><a href="#tab-3">Actions</a></li>
                        <li><a href="#tab-4">Priorities</a></li>
                    </ul>
                    <div class="tab" style="clear: both;width:100%; min-height:400px">
                        <div id="tab-1" class="tab-content" style="width:90%">
                            <!-- ******************************** ASSESSMENT UNIT TAB ********************************-->
                            <div class="divHelp">Individual assessment units defined by your organization.</div>
                            <asp:GridView ID="grdAssessUnits" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" 
                                AlternatingRowStyle-CssClass="alt" style="float:left" OnRowCommand="grdAssessUnits_RowCommand" ShowHeaderWhenEmpty="true" EmptyDataText="No Assessment Units entered."> 
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" 
                                                CommandArgument='<% #Eval("ATTAINS_ASSESS_UNIT_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ASSESS_UNIT_ID" HeaderText="Unit ID" SortExpression="ASSESS_UNIT_ID" />
                                    <asp:BoundField DataField="ASSESS_UNIT_NAME" HeaderText="Unit Name" SortExpression="ASSESS_UNIT_NAME" />
                                    <asp:BoundField DataField="LOCATION_DESC" HeaderText="Location Description" SortExpression="LOCATION_DESC" />
                                    <asp:BoundField DataField="AGENCY_CODE" HeaderText="Agency Code" SortExpression="AGENCY_CODE" />
                                    <asp:BoundField DataField="STATE_CODE" HeaderText="State Code" SortExpression="STATE_CODE" />
                                    <asp:BoundField DataField="ACT_IND" HeaderText="Status" SortExpression="ACT_IND" />
                                    <asp:BoundField DataField="WATER_TYPE_CODE" HeaderText="Water Type" SortExpression="WATER_TYPE_CODE" />
                                    <asp:BoundField DataField="WATER_SIZE" HeaderText="Water Size" SortExpression="WATER_SIZE" />
                                    <asp:BoundField DataField="WATER_UNIT_CODE" HeaderText="Unit" SortExpression="WATER_UNIT_CODE" />
                                    <asp:BoundField DataField="USE_CLASS_CODE" HeaderText="Use Class Code" SortExpression="USE_CLASS_CODE" />
                                    <asp:BoundField DataField="USE_CLASS_NAME" HeaderText="Use Class Code" SortExpression="USE_CLASS_NAME" />
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddAssessUnit" runat="server" CssClass="btn" Text="Add Assessment Unit"  Style="float:left;" OnClick="btnAddAssessUnit_Click" />
                        </div>
                        <div id="tab-2" class="tab-content">
                            <!-- ******************************** ASSESSMENTS TAB ********************************-->
                            <div class="divHelp">Water quality assessments for a particular cycle.</div>
                            <asp:GridView ID="grdAssess" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" 
                                AlternatingRowStyle-CssClass="alt" style="float:left" OnRowCommand="grdAssess_RowCommand" ShowHeaderWhenEmpty="true" EmptyDataText="No Assessments entered."> 
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" 
                                                CommandArgument='<% #Eval("ATTAINS_ASSESS_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ASSESS_UNIT_NAME" HeaderText="Assessment Unit" SortExpression="ASSESS_UNIT_NAME" />
                                    <asp:BoundField DataField="REPORTING_CYCLE" HeaderText="Reporting Cycle" SortExpression="REPORTING_CYCLE" />
                                    <asp:BoundField DataField="REPORT_STATUS" HeaderText="Report Status" SortExpression="REPORT_STATUS" />
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddAssess" runat="server" CssClass="btn" Text="Add Assessment"  Style="float:left;" OnClick="btnAddAssess_Click" />
                        </div>
                        <div id="tab-3" class="tab-content">
                            <!-- ******************************** ACTIONS TAB ********************************-->
                            <div class="divHelp">Actions taken to restore or protect water quality.</div>
                            <asp:GridView ID="grdAction" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" 
                                AlternatingRowStyle-CssClass="alt" style="float:left" OnRowCommand="grdAction_RowCommand" ShowHeaderWhenEmpty="true" EmptyDataText="No Actions entered."> 
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" 
                                                CommandArgument='<% #Eval("ATTAINS_ACTION_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ACTION_IDX" SortExpression="ACTION_IDX" />
                                    <asp:BoundField DataField="ACTION_TYPE" HeaderText="Action Type" SortExpression="ACTION_TYPE" />
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddAction" runat="server" CssClass="btn" Text="Add Action"  Style="float:left;" OnClick="btnAddAction_Click" />
                        </div>
                        <div id="tab-4" class="tab-content">
                            <!-- ******************************** PRIORITIES TAB ********************************-->
                            <div class="divHelp">Identification of your organization's priorities.</div>
                            <asp:GridView ID="grdPriority" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" 
                                AlternatingRowStyle-CssClass="alt" style="float:left" OnRowCommand="grdPriority_RowCommand" ShowHeaderWhenEmpty="true" EmptyDataText="No Actions entered."> 
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" 
                                                CommandArgument='<% #Eval("ATTAINS_PRIORITY_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PRIORITY_IDX" SortExpression="PRIORITY_IDX" />
                                    <asp:BoundField DataField="PRIORITY_NAME" HeaderText="Priority Name" SortExpression="PRIORITY_NAME" />
                                </Columns>
                            </asp:GridView>
                            <asp:Button ID="btnAddPriority" runat="server" CssClass="btn" Text="Add Priority"  Style="float:left;" OnClick="btnAddPriority_Click" />

                        </div>

                    </div>

                </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" Style="float:left;" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" CausesValidation="false" Style="float:left;" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" CausesValidation="false" Style="float:right;" OnClick="btnDelete_Click" OnClientClick="return GetConfirmation();" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" CausesValidation="false" Style="float:right;" OnClick="btnSubmit_Click" OnClientClick="return GetConfirmationSubmit();" />
        </div>

</asp:Content>