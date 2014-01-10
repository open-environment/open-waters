<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="RefData.aspx.cs" Inherits="OpenEnvironment.RefData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <asp:ListItem Text="Analytical Method" Value="AnalyticalMethod"></asp:ListItem>
            <asp:ListItem Text="Characteristic" Value="Characteristic"></asp:ListItem>
            <asp:ListItem Text="MeasureUnit" Value="MeasureUnit"></asp:ListItem>
            <asp:ListItem Text="NetType" Value="NetType"></asp:ListItem>
            <asp:ListItem Text="ResultDetectionCondition" Value="ResultDetectionCondition"></asp:ListItem>
            <asp:ListItem Text="ResultLaboratoryComment" Value="ResultLaboratoryComment"></asp:ListItem>
            <asp:ListItem Text="ResultMeasureQualifier" Value="ResultMeasureQualifier"></asp:ListItem>
            <asp:ListItem Text="ResultSampleFraction" Value="ResultSampleFraction"></asp:ListItem>
            <asp:ListItem Text="ResultStatus" Value="ResultStatus"></asp:ListItem>
            <asp:ListItem Text="ResultTemperatureBasis" Value="ResultTemperatureBasis"></asp:ListItem>
            <asp:ListItem Text="ResultTimeBasis" Value="ResultTimeBasis"></asp:ListItem>
            <asp:ListItem Text="ResultValueType" Value="ResultValueType"></asp:ListItem>
            <asp:ListItem Text="ResultWeightBasis" Value="ResultWeightBasis"></asp:ListItem>
            <asp:ListItem Text="SampleCollectionEquipment" Value="SampleCollectionEquipment"></asp:ListItem>
            <asp:ListItem Text="SampleContainerColor" Value="SampleContainerColor"></asp:ListItem>
            <asp:ListItem Text="SampleContainerType" Value="SampleContainerType"></asp:ListItem>
            <asp:ListItem Text="SampleTissueAnatomy" Value="SampleTissueAnatomy"></asp:ListItem>
            <asp:ListItem Text="Taxon" Value="Taxon"></asp:ListItem>
            <asp:ListItem Text="TimeZone" Value="TimeZone"></asp:ListItem>
        </asp:DropDownList>
        </div>
        <br />
        <asp:GridView ID="grdRef" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="100" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="REF_DATA_IDX" DataSourceID="dsRefGen" onpageindexchanging="grdRef_PageIndexChanging" onrowcommand="grdRef_RowCommand" >
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
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="VALUE" HeaderText="Value" SortExpression="VALUE" ControlStyle-Width="98%" />
                <asp:BoundField DataField="TEXT" HeaderText="Description" SortExpression="TEXT" ControlStyle-Width="98%" />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsRefGen" runat="server"  EnableUpdate="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_DATA" Where="it.[Table] = @tbl">  
            <WhereParameters>
                <asp:ControlParameter ControlID="ddlRef" DbType="String" DefaultValue="" Name="tbl" PropertyName="SelectedValue" />
            </WhereParameters>
        </asp:EntityDataSource>
        <asp:GridView ID="grdChar" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" PageSize="500" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="CHAR_NAME" DataSourceID="dsChar" onpageindexchanging="grdChar_PageIndexChanging" onrowcommand="grdChar_RowCommand" >
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                            CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                            CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="UpdateButton" ImageUrl="~/App_Images/ico_save.png" CommandName="Update" runat="server" /> 
                        <asp:ImageButton ID="CancelButton" ImageUrl="~/App_Images/ico_del.png" CommandName="Cancel" runat="server" /> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME" ControlStyle-Width="98%" />
                <asp:BoundField DataField="DEFAULT_DETECT_LIMIT" HeaderText="Detection Limit" SortExpression="DEFAULT_DETECT_LIMIT" ControlStyle-Width="98%" />
                <asp:BoundField DataField="DEFAULT_UNIT" HeaderText="Default Unit" SortExpression="DEFAULT_UNIT" />
                <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" ReadOnly="true" />
                <asp:BoundField DataField="UPDATE_DT" HeaderText="Last Updated" SortExpression="UPDATE_DT" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:EntityDataSource ID="dsChar" runat="server"  EnableUpdate="true" ConnectionString="name=OpenEnvironmentEntities" 
            DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_REF_CHARACTERISTIC" >  
        </asp:EntityDataSource>
        <div class="btnRibbon">
        <asp:Button ID="btnGetRefData" runat="server" CssClass="btn" OnClick="btnGetRefData_Click"
            Text="Sync Ref Data from EPA" />
        </div>
    </div>
</asp:Content>
