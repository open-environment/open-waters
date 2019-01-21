<%@ Page Title="Open Waters - Monitoring Locations" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXMonLoc.aspx.cs" Inherits="OpenEnvironment.WQXMonLoc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the monitoring location - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" runat="server" Interval="20000" ontick="Timer1_Tick"></asp:Timer>
    <ajaxToolkit:ModalPopupExtender ID="MPE1" runat="server" TargetControlID="btnConfig"
        PopupControlID="pnlModal" CancelControlID="btnClose" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl">
    </ajaxToolkit:ModalPopupExtender>
    <div style="float:left; width:100%"> 
        <h1 style="display:inline;">Monitoring Locations</h1>
        <div style="float:right"> 
            <asp:CheckBox ID="chkDeletedInd" runat="server" Checked="false" Text="Display Deleted Locations" AutoPostBack="True" OnCheckedChanged="chkDeletedInd_CheckedChanged" />
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification" ></asp:Label>
            <br />
            <asp:GridView ID="grdMonLoc" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt" OnRowCommand="grdMonLoc_RowCommand" style="float:left">
                <Columns>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits"
                                CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl="~/App_Images/ico_edit.png"
                                ToolTip="Edit" />
                            <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmation();" 
                                CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl="~/App_Images/ico_del.png"
                                ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MONLOC_ID" HeaderText="ID" SortExpression="MONLOC_ID" />
                    <asp:BoundField DataField="MONLOC_NAME" HeaderText="Name" SortExpression="MONLOC_NAME" />
                    <asp:BoundField DataField="MONLOC_TYPE" HeaderText="Type" SortExpression="MONLOC_TYPE" />
                    <asp:BoundField DataField="MONLOC_DESC" HeaderText="Description" SortExpression="MONLOC_DESC" />
                    <asp:BoundField DataField="HUC_EIGHT" HeaderText="8-Digit HUC" SortExpression="HUC_EIGHT" />
                    <asp:BoundField DataField="HUC_TWELVE" HeaderText="12-Digit HUC" SortExpression="HUC_TWELVE" />
                    <asp:BoundField DataField="TRIBAL_LAND_NAME" HeaderText="Tribal Land" SortExpression="TRIBAL_LAND_NAME" />
                    <asp:BoundField DataField="LATITUDE_MSR" HeaderText="Latitude" SortExpression="LATITUDE_MSR" />
                    <asp:BoundField DataField="LONGITUDE_MSR" HeaderText="Longitude" SortExpression="LONGITUDE_MSR" />
                    <asp:BoundField DataField="SOURCE_MAP_SCALE" HeaderText="Source Map Scale" SortExpression="SOURCE_MAP_SCALE" />
                    <asp:BoundField DataField="HORIZ_COLL_METHOD" HeaderText="Horiz. Collection Method" SortExpression="HORIZ_COLL_METHOD" />
                    <asp:BoundField DataField="HORIZ_REF_DATUM" HeaderText="Horiz. Datum" SortExpression="HORIZ_REF_DATUM" />
                    <asp:BoundField DataField="VERT_MEASURE" HeaderText="Vertical Measure" SortExpression="VERT_MEASURE" />
                    <asp:BoundField DataField="VERT_MEASURE_UNIT" HeaderText="Unit" SortExpression="VERT_MEASURE_UNIT" />
                    <asp:BoundField DataField="VERT_COLL_METHOD" HeaderText="Vertical Collection Method" SortExpression="VERT_COLL_METHOD" />
                    <asp:BoundField DataField="VERT_REF_DATUM" HeaderText="Vertical Datum" SortExpression="VERT_REF_DATUM" />
                    <asp:BoundField DataField="COUNTY_CODE" HeaderText="County" SortExpression="COUNTY_CODE" />
                    <asp:BoundField DataField="STATE_CODE" HeaderText="State" SortExpression="STATE_CODE" />
                    <asp:BoundField DataField="COUNTRY_CODE" HeaderText="Country" SortExpression="COUNTRY_CODE" />
                    <asp:BoundField DataField="WELL_TYPE" HeaderText="Well Type" SortExpression="WELL_TYPE" />
                    <asp:BoundField DataField="AQUIFER_NAME" HeaderText="Aquifer" SortExpression="AQUIFER_NAME" />
                    <asp:BoundField DataField="FORMATION_TYPE" HeaderText="Formation" SortExpression="FORMATION_TYPE" />
                    <asp:BoundField DataField="WELLHOLE_DEPTH_MSR" HeaderText="Wellhole Depth" SortExpression="WELLHOLE_DEPTH_MSR" />
                    <asp:BoundField DataField="WELLHOLE_DEPTH_MSR_UNIT" HeaderText="Depth Unit" SortExpression="WELLHOLE_DEPTH_MSR_UNIT" />
                    <asp:TemplateField HeaderText="Send to EPA"> 
                        <ItemStyle HorizontalAlign="Center" Width="50px" />                        
                        <ItemTemplate> 
                            <asp:CheckBox ID="chkWQX" Enabled="false" runat="server" Checked='<% #Eval("WQX_IND") %>' />
                            <asp:ImageButton ID="WQXButton" runat="server" CausesValidation="False" CommandName="WQX"
                                CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl='<%# GetImage((string)Eval("WQX_SUBMIT_STATUS"),(Boolean)Eval("WQX_IND")) %>'
                                ToolTip="Click to view WQX History" />
                        </ItemTemplate> 
                    </asp:TemplateField> 

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="btnRibbon">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add New" onclick="btnAdd_Click" Enabled="False" />
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" style="float:right;" ImageUrl="~/App_Images/ico_xls.png" onclick="btnExcel_Click" />
        <asp:ImageButton ID="btnConfig" runat="server" Height="24px"  style="float:right; padding-right:5px; padding-left:5px" ImageUrl="~/App_Images/ico_config.png" />
    </div>
    <br />


    <!-- ******************** MODAL PANEL -->
    <asp:Panel ID="pnlModal" Width="500px" runat="server" CssClass="modalWindow" Style="display: none;">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl" runat="server" CssClass="modalTitle" Style="cursor: move">
                Select Fields to Include in View
            </asp:Panel>
            <div class="row" style="height:200px;">
                <div class="fldLbl">
                    <asp:CheckBoxList ID="chkFields" Width="500px" runat="server" RepeatColumns="2">
                        <asp:ListItem Text="HUC_EIGHT"></asp:ListItem>
                        <asp:ListItem Text="HUC_TWELVE"></asp:ListItem>
                        <asp:ListItem Text="TRIBAL_LAND"></asp:ListItem>
                        <asp:ListItem Text="SOURCE_MAP_SCALE"></asp:ListItem>
                        <asp:ListItem Text="HORIZ_COLL_METHOD"></asp:ListItem>
                        <asp:ListItem Text="HORIZ_REF_DATUM"></asp:ListItem>
                        <asp:ListItem Text="VERT_MEASURE"></asp:ListItem>
                        <asp:ListItem Text="COUNTRY_CODE"></asp:ListItem>
                        <asp:ListItem Text="STATE_CODE"></asp:ListItem>
                        <asp:ListItem Text="COUNTY_CODE"></asp:ListItem>
                        <asp:ListItem Text="WELL_TYPE"></asp:ListItem>
                        <asp:ListItem Text="AQUIFER_NAME"></asp:ListItem>
                        <asp:ListItem Text="FORMATION_TYPE"></asp:ListItem>
                        <asp:ListItem Text="WELLHOLE_DEPTH"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnConfigSave" runat="server" Text="Save" CssClass="btn" OnClick="btnConfigSave_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
