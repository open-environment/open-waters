<%@ Page Title="Open Waters - Reference Data" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="RefData.aspx.cs" Inherits="OpenEnvironment.RefData" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the record - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="MPE1" runat="server" TargetControlID="btnAdd" PopupControlID="pnlModal" CancelControlID="btnClose"   
        BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl">
    </ajaxToolkit:ModalPopupExtender>
    <div style="padding:10px">
        <div class="row">
            <span class="fldLbl">Select Reference Table:</span>
            <asp:DropDownList ID="ddlRef" runat="server" CssClass="fldTxt" AutoPostBack="True" onselectedindexchanged="ddlRef_SelectedIndexChanged">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="--ORGANIZATION--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Tribes" Value="Tribe"></asp:ListItem>
                <asp:ListItem Text="--PROJECTS--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Sampling Design Type" Value="SamplingDesignType"></asp:ListItem>
                <asp:ListItem Text="--MONITORING LOCATION--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="County" Value="County"></asp:ListItem>
                <asp:ListItem Text="Country" Value="Country"></asp:ListItem>
                <asp:ListItem Text="Horizontal Collection Method" Value="HorizontalCollectionMethod"></asp:ListItem>
                <asp:ListItem Text="Horizontal Reference Datum" Value="HorizontalCoordinateReferenceSystemDatum"></asp:ListItem>
                <asp:ListItem Text="Monitoring Location Type" Value="MonitoringLocationType"></asp:ListItem>
                <asp:ListItem Text="State" Value="State"></asp:ListItem>
                <asp:ListItem Text="Vertical Collection Method" Value="VerticalCollectionMethod"></asp:ListItem>
                <asp:ListItem Text="Vertical Reference Datum" Value="VerticalCoordinateReferenceSystemDatum"></asp:ListItem>
                <asp:ListItem Text="Well Formation Type" Value="WellFormationType"></asp:ListItem>
                <asp:ListItem Text="Well Type" Value="WellType"></asp:ListItem>
                <asp:ListItem Text="--ACTIVITY--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Activity Media" Value="ActivityMedia"></asp:ListItem>
                <asp:ListItem Text="Activity Media Subdivision" Value="ActivityMediaSubdivision"></asp:ListItem>
                <asp:ListItem Text="Activity Type" Value="ActivityType"></asp:ListItem>
                <asp:ListItem Text="Activity Relative Depth" Value="ActivityRelativeDepth"></asp:ListItem>
                <asp:ListItem Text="Assemblage" Value="Assemblage"></asp:ListItem>
                <asp:ListItem Text="Biological Intent" Value="BiologicalIntent"></asp:ListItem>
                <asp:ListItem Text="Metric Type" Value="MetricType"></asp:ListItem>
                <asp:ListItem Text="Net Type" Value="NetType"></asp:ListItem>
                <asp:ListItem Text="Sample Collection Equipment" Value="SampleCollectionEquipment"></asp:ListItem>
                <asp:ListItem Text="Sample Collection Method" Value="SampleCollectionMethod"></asp:ListItem>
                <asp:ListItem Text="Sample Container Color" Value="SampleContainerColor"></asp:ListItem>
                <asp:ListItem Text="Sample Container Type" Value="SampleContainerType"></asp:ListItem>
                <asp:ListItem Text="Sample Prep Method" Value="SamplePrepMethod"></asp:ListItem>
                <asp:ListItem Text="Sample Tissue Anatomy" Value="SampleTissueAnatomy"></asp:ListItem>
                <asp:ListItem Text="Thermal Preservative Used" Value="ThermalPreservativeUsed"></asp:ListItem>
                <asp:ListItem Text="TimeZone" Value="TimeZone"></asp:ListItem>
                <asp:ListItem Text="Toxicity Test Type" Value="ToxicityTestType"></asp:ListItem>
                <asp:ListItem Text="--RESULTS--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Analytical Method" Value="AnalyticalMethod"></asp:ListItem>
                <asp:ListItem Text="Cell Form" Value="CellForm"></asp:ListItem>
                <asp:ListItem Text="Cell Shape" Value="CellShape"></asp:ListItem>
                <asp:ListItem Text="Characteristic" Value="Characteristic"></asp:ListItem>
                <asp:ListItem Text="Detection Quantitation Limit Type" Value="DetectionQuantitationLimitType"></asp:ListItem>
                <asp:ListItem Text="Frequency Class Descriptor" Value="FrequencyClassDescriptor"></asp:ListItem>
                <asp:ListItem Text="Habit" Value="Habit"></asp:ListItem>
                <asp:ListItem Text="Laboratory" Value="Laboratory"></asp:ListItem>
                <asp:ListItem Text="Measure Unit" Value="MeasureUnit"></asp:ListItem>
                <asp:ListItem Text="Method Speciation" Value="MethodSpeciation"></asp:ListItem>
                <asp:ListItem Text="Result Detection Condition" Value="ResultDetectionCondition"></asp:ListItem>
                <asp:ListItem Text="Result Laboratory Comment" Value="ResultLaboratoryComment"></asp:ListItem>
                <asp:ListItem Text="Result Measure Qualifier" Value="ResultMeasureQualifier"></asp:ListItem>
                <asp:ListItem Text="Result Sample Fraction" Value="ResultSampleFraction"></asp:ListItem>
                <asp:ListItem Text="Result Status" Value="ResultStatus"></asp:ListItem>
                <asp:ListItem Text="Result Temperature Basis" Value="ResultTemperatureBasis"></asp:ListItem>
                <asp:ListItem Text="Result Time Basis" Value="ResultTimeBasis"></asp:ListItem>
                <asp:ListItem Text="Result Value Type" Value="ResultValueType"></asp:ListItem>
                <asp:ListItem Text="Result Weight Basis" Value="ResultWeightBasis"></asp:ListItem>
                <asp:ListItem Text="StatisticalBase" Value="StatisticalBase"></asp:ListItem>
                <asp:ListItem Text="Taxon" Value="Taxon"></asp:ListItem>
                <asp:ListItem Text="Voltinism" Value="Voltinism"></asp:ListItem>
            </asp:DropDownList>
            <span class="fldLbl" style="padding-left:20px; width: 90px">Search Data:</span>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="fldTxt" placeholder="Search..." style="width: 200px; " />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
        </div>
        <div class="row">
            <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        </div>

        <asp:GridView ID="grdRef" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="REF_DATA_IDX" DataSourceID="dsRefGen" onpageindexchanging="grdRef_PageIndexChanging" onrowcommand="grdRef_RowCommand" style="float:left" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("REF_DATA_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                            CommandArgument='<%# Eval("REF_DATA_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" ToolTip="Save" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" ToolTip="Cancel" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="VALUE" HeaderText="Value" SortExpression="VALUE" ControlStyle-Width="98%" />
                <asp:BoundField DataField="TEXT" HeaderText="Description" SortExpression="TEXT" ControlStyle-Width="98%" />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsRefGen" runat="server"  EnableUpdate="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_DATA" Where="it.[Table] = @tbl AND ((it.[Value] like '%' + @value + '%' OR @value IS NULL) or (it.[Text] like '%' + @value + '%' OR @value IS NULL))">  
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlRef" DbType="String" DefaultValue="" Name="tbl" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="txtSearch" DbType="String" DefaultValue="" Name="value" PropertyName="Text" />
            </WhereParameters>
        </asp:EntityDataSource>

        <asp:GridView ID="grdChar" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="True" Visible="false" 
            AutoGenerateColumns="False" DataKeyNames="CHAR_NAME" DataSourceID="dsChar" onpageindexchanging="grdChar_PageIndexChanging" onrowcommand="grdChar_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                            CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME" ControlStyle-Width="98%" ReadOnly="true" />
                <asp:BoundField DataField="SAMP_FRAC_REQ" HeaderText="Samp Fraction Required" SortExpression="SAMP_FRAC_REQ" ReadOnly="true" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsChar" runat="server"  EnableUpdate="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_CHARACTERISTIC" Where="(it.[CHAR_NAME] like '%' + @value + '%' OR @value IS NULL)" >  
            <WhereParameters>
                <asp:ControlParameter ControlID="txtSearch" DbType="String" DefaultValue="" Name="value" PropertyName="Text" />
            </WhereParameters>
        </asp:EntityDataSource>

        <asp:GridView ID="grdAnalMethod" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="true"  Visible="false"            
            AutoGenerateColumns="False" DataKeyNames="ANALYTIC_METHOD_IDX" DataSourceID="dsAnalMethod" onpageindexchanging="grdAnalMethod_PageIndexChanging" onrowcommand="grdAnalMethod_RowCommand" OnRowDataBound="grdAnalMethod_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("ANALYTIC_METHOD_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                            CommandArgument='<%# Eval("ANALYTIC_METHOD_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" ToolTip="Cancel" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ANALYTIC_METHOD_ID" HeaderText="Analytical Method ID" SortExpression="ANALYTIC_METHOD_ID" ControlStyle-Width="98%"   />
                <asp:BoundField DataField="ANALYTIC_METHOD_CTX" HeaderText="Context" SortExpression="ANALYTIC_METHOD_CTX" ReadOnly="true" />
                <asp:BoundField DataField="ANALYTIC_METHOD_NAME" HeaderText="Name" SortExpression="ANALYTIC_METHOD_NAME"  ControlStyle-Width="98%"  />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" ReadOnly="true" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsAnalMethod" runat="server"  EnableUpdate="true" ConnectionString="name=OpenEnvironmentEntities" 
            Where="(it.[ANALYTIC_METHOD_ID] like '%' + @value + '%' OR @value IS NULL) or (it.[ANALYTIC_METHOD_CTX] like '%' + @value + '%' OR @value IS NULL) or  (it.[ANALYTIC_METHOD_NAME] like '%' + @value + '%' OR @value IS NULL)"
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_ANAL_METHOD"  OrderBy="case when it.[ANALYTIC_METHOD_CTX] = @ctx then '1' else '2' end, it.[ANALYTIC_METHOD_CTX], it.[ANALYTIC_METHOD_ID]" > 
            <WhereParameters>
                <asp:ControlParameter ControlID="txtSearch" DbType="String" DefaultValue="" Name="value" PropertyName="Text" />
            </WhereParameters> 
            <OrderByParameters>
                <asp:SessionParameter DbType="String" SessionField="OrgID" Name="ctx" />
            </OrderByParameters>
        </asp:EntityDataSource>

        <asp:GridView ID="grdSampPrep" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="true"  Visible="false"
            AutoGenerateColumns="False" DataKeyNames="SAMP_PREP_IDX" DataSourceID="dsSampPrep" onpageindexchanging="grdSampPrep_PageIndexChanging" onrowcommand="grdSampPrep_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("SAMP_PREP_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Delete"
                            CommandArgument='<%# Eval("SAMP_PREP_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SAMP_PREP_METHOD_ID" HeaderText="Samp Prep ID" SortExpression="SAMP_PREP_METHOD_ID" ControlStyle-Width="98%"   />
                <asp:BoundField DataField="SAMP_PREP_METHOD_CTX" HeaderText="Context" SortExpression="SAMP_PREP_METHOD_CTX" ControlStyle-Width="98%" />
                <asp:BoundField DataField="SAMP_PREP_METHOD_NAME" HeaderText="Name" SortExpression="SAMP_PREP_METHOD_NAME"  />
                <asp:BoundField DataField="SAMP_PREP_METHOD_DESC" HeaderText="Description" SortExpression="SAMP_PREP_METHOD_DESC"  />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" ReadOnly="true" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsSampPrep" runat="server"  EnableUpdate="true" EnableDelete="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_SAMP_PREP" AutoGenerateWhereClause="true"  OrderBy="it.[SAMP_PREP_METHOD_ID]" >  
            <WhereParameters>
                <asp:SessionParameter DbType="String" SessionField="OrgID" Name="SAMP_PREP_METHOD_CTX" />
            </WhereParameters>
        </asp:EntityDataSource>

        <asp:GridView ID="grdSampColl" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="true"  Visible="false"
            AutoGenerateColumns="False" DataKeyNames="SAMP_COLL_METHOD_IDX" DataSourceID="dsSampColl" onpageindexchanging="grdSampColl_PageIndexChanging" onrowcommand="grdSampColl_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("SAMP_COLL_METHOD_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Delete"  OnClientClick="return GetConfirmation();"
                            CommandArgument='<%# Eval("SAMP_COLL_METHOD_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SAMP_COLL_METHOD_ID" HeaderText="Sample Collection ID" SortExpression="SAMP_COLL_METHOD_ID" ControlStyle-Width="98%"  />
                <asp:BoundField DataField="SAMP_COLL_METHOD_CTX" HeaderText="Context" SortExpression="SAMP_COLL_METHOD_CTX" ReadOnly="true" />
                <asp:BoundField DataField="SAMP_COLL_METHOD_NAME" HeaderText="Name" SortExpression="SAMP_COLL_METHOD_NAME"  ControlStyle-Width="98%"  />
                <asp:BoundField DataField="SAMP_COLL_METHOD_DESC" HeaderText="Description" SortExpression="SAMP_COLL_METHOD_DESC"  ControlStyle-Width="98%"  />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" ReadOnly="true" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsSampColl" runat="server"  EnableUpdate="true" EnableDelete="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_SAMP_COL_METHOD" AutoGenerateWhereClause="true" OrderBy="it.[SAMP_COLL_METHOD_ID]" >  
            <WhereParameters>
                <asp:SessionParameter DbType="String" SessionField="OrgID" Name="SAMP_COLL_METHOD_CTX" />
            </WhereParameters>
        </asp:EntityDataSource>

        <asp:GridView ID="grdLab" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="true"  Visible="false"
            AutoGenerateColumns="False" DataKeyNames="LAB_IDX" DataSourceID="dsLab" onpageindexchanging="grdLab_PageIndexChanging" onrowcommand="grdLab_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("LAB_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Delete"
                            CommandArgument='<%# Eval("LAB_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LAB_NAME" HeaderText="Lab Name" SortExpression="LAB_NAME" ControlStyle-Width="98%"  />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsLab" runat="server"  EnableUpdate="true"  EnableDelete="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_LAB"  AutoGenerateWhereClause="true" OrderBy="it.[LAB_NAME]"  >  
            <WhereParameters>
                <asp:SessionParameter DbType="String" SessionField="OrgID" Name="ORG_ID" />
            </WhereParameters>
        </asp:EntityDataSource>



        <asp:GridView ID="grdCounty" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="50" AllowPaging="True"  Visible="false"
            AutoGenerateColumns="False" DataKeyNames="STATE_CODE, COUNTY_CODE" DataSourceID="dsCounty"  >
            <Columns>
                <asp:BoundField DataField="STATE_CODE" HeaderText="State Code" SortExpression="STATE_CODE"  />
                <asp:BoundField DataField="COUNTY_CODE" HeaderText="County Code" SortExpression="COUNTY_CODE"  />
                <asp:BoundField DataField="COUNTY_NAME" HeaderText="County Name" SortExpression="COUNTY_NAME" />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" ReadOnly="true" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsCounty" runat="server"  EnableUpdate="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_COUNTY" >  
        </asp:EntityDataSource>

        <div class="clear"></div>
        <div class="btnRibbon">
            <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add New" Visible="false"   />
        </div>

    </div>

    <!-- ******************** MODAL PANEL -->
    <asp:Panel ID="pnlModal" Width="500px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnNewSave">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl" runat="server" CssClass="modalTitle" Style="cursor: move">
                Enter New Reference Record
            </asp:Panel>
            <div class="row" >
                <div runat="server" id="lblID" class="fldLbl">ID:</div>
                <asp:TextBox ID="txtID" runat="server" CssClass="fldTxt" MaxLength="20"></asp:TextBox>
            </div>
            <div class="row" >
                <div runat="server" id="lblName" class="fldLbl">Name:</div>
                <asp:TextBox ID="txtName" runat="server" CssClass="fldTxt" MaxLength="120" Width="300px"></asp:TextBox>
            </div>
            <div class="row" style="margin-bottom:10px">
                <div runat="server" id="lblDesc" class="fldLbl">Description:</div>
                <asp:TextBox ID="txtDesc" runat="server" CssClass="fldTxt" MaxLength="1000"  Width="300px" ></asp:TextBox>
            </div>
            <div class="row" ></div>
            <div class="btnRibbon">
                <asp:Button ID="btnNewSave" runat="server" Text="Save" CssClass="btn" OnClick="btnNewSave_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
