<%@ Page Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Assess_Unit.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.Assess_Unit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the assessment unit - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <h1>
        Assessment Unit Details
    </h1>
    <asp:HiddenField ID="hdnReportIDX" runat="server" />
    <asp:HiddenField ID="hdnAssessUnitIDX" runat="server" />
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave" style="min-width:800px" >
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Assessment Unit ID:</span>
                <asp:TextBox ID="txtAssessID" runat="server" Width="180px" CssClass="fldTxt" MaxLength="50"></asp:TextBox>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Assessment Unit Name:</span>
                <asp:TextBox ID="txtAssessName" runat="server" Width="180px" CssClass="fldTxt" MaxLength="255"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <span class="fldLbl">Location Description:</span>
            <asp:TextBox ID="txtLocDesc" runat="server" Width="555px" CssClass="fldTxt" MaxLength="2000" TextMode="MultiLine" Rows="3"  style="min-height:60px"></asp:TextBox>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Agency Code:</span>
                <asp:DropDownList ID="ddlAgencyCode" runat="server" CssClass="fldTxt" Width="180px" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="T" Text="Tribal"></asp:ListItem>
                    <asp:ListItem Value="S" Text="State"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">State Code:</span>
                <asp:DropDownList ID="ddlStateCode" runat="server" CssClass="fldTxt" Width="180px" >
                    <asp:ListItem Value="OK" Text="OK"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Status Indicator:</span>
                <asp:DropDownList ID="ddlStatusInd" runat="server" CssClass="fldTxt" Width="180px" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Water Type:</span>
                <asp:DropDownList ID="ddlWaterType" runat="server" CssClass="fldTxt" Width="180px" ></asp:DropDownList>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Water Size:</span>
                <asp:TextBox ID="txtWaterSize" runat="server" Width="50px" CssClass="fldTxt" MaxLength="15"></asp:TextBox>
                <asp:DropDownList ID="ddlWaterSizeUnit" runat="server" CssClass="fldTxt" Width="130px" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="MILES" Text="MILES"></asp:ListItem>
                    <asp:ListItem Value="SQUARE MILES" Text="SQUARE MILES"></asp:ListItem>
                    <asp:ListItem Value="ACRES" Text="ACRES"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Use Class Code:</span>
                <asp:TextBox ID="txtUseClassCode" runat="server" Width="180px" CssClass="fldTxt" MaxLength="15"></asp:TextBox>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Use Class Name:</span>
                <asp:TextBox ID="txtUseClassName" runat="server" Width="180px" CssClass="fldTxt" MaxLength="50"></asp:TextBox>
            </div>
        </div>
        <div class="row">

        </div>
        <h1>
            Associated Monitoring Locations
        </h1>
        <div class="row">
            <asp:GridView ID="grdMonLoc" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt" 
                ShowHeaderWhenEmpty="true" EmptyDataText="No associated monitoring locations." OnRowCommand="grdMonLoc_RowCommand"> 
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Deletes" 
                                CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MONLOC_ID" HeaderText="Monitoring Location" SortExpression="MONLOC_ID" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add Monitoring Locations" Style="float:left; margin-bottom: 20px" />
        </div>


        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" Style="float:left;" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" CausesValidation="false" Style="float:left;" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" CausesValidation="false" Style="float:right;" OnClick="btnDelete_Click" OnClientClick="return GetConfirmation();" />
        </div>
    </asp:Panel>


    <!-- modal for add new monloc -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_New" runat="server" TargetControlID="btnAdd"
        PopupControlID="pnlModalNewColumn" CancelControlID="btnCloseCol" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl3">
    </ajaxToolkit:ModalPopupExtender>
    <asp:ObjectDataSource ID="dsMonLoc" runat="server" SelectMethod="GetWQX_MONLOC"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:Panel ID="pnlModalNewColumn" Width="600px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnAddSave">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl3" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add Association Monitoring Location
            </asp:Panel>
            <div class="row">
                <span class="fldLbl">Monitoring Location:</span>
                <asp:DropDownList ID="ddlMonLoc" runat="server" CssClass="fldTxt" style="width:300px"></asp:DropDownList>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnAddSave" runat="server" Text="Save" CssClass="btn" OnClick="btnAddSave_Click" />
                <asp:Button ID="btnCloseCol" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>




