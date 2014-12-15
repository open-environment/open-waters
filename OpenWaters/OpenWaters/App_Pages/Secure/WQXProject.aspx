<%@ Page Title="Open Waters - Projects" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXProject.aspx.cs" Inherits="OpenEnvironment.WQXProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the project - are you sure you want to continue?");
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
    <asp:ObjectDataSource ID="dsProject" runat="server" SelectMethod="GetWQX_PROJECT" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
        <selectparameters>
            <asp:Parameter DefaultValue="false" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <h1>
        Projects
    </h1>
    <asp:UpdatePanel runat="server" ID="TimedPanel" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
            <asp:GridView ID="grdProject" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" 
                onrowcommand="grdProject_RowCommand" 
                onrowcancelingedit="grdProject_RowCancelingEdit" 
                onrowediting="grdProject_RowEditing" onrowupdating="grdProject_RowUpdating" >
                <Columns>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits"
                                CommandArgument='<% #Eval("PROJECT_IDX") %>' ImageUrl="~/App_Images/ico_edit.png"
                                ToolTip="Edit" />
                            <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmation();" 
                                CommandArgument='<% #Eval("PROJECT_IDX") %>' ImageUrl="~/App_Images/ico_del.png"
                                ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PROJECT_ID" HeaderText="ID" SortExpression="PROJECT_ID" />
                    <asp:BoundField DataField="PROJECT_NAME" HeaderText="Name" SortExpression="PROJECT_NAME" />
                    <asp:BoundField DataField="PROJECT_DESC" HeaderText="Description" SortExpression="PROJECT_DESC" />
                    <asp:BoundField DataField="SAMP_DESIGN_TYPE_CD" HeaderText="Sampling Design Type" SortExpression="SAMP_DESIGN_TYPE_CD" />
                    <asp:BoundField DataField="QAPP_APPROVAL_IND" HeaderText="QAPP Approval" SortExpression="QAPP_APPROVAL_IND" />
                    <asp:BoundField DataField="QAPP_APPROVAL_AGENCY" HeaderText="QAPP Approval Agency" SortExpression="QAPP_APPROVAL_AGENCY" />
                    <asp:BoundField DataField="ACT_IND" HeaderText="Active" SortExpression="ACT_IND" />
                    <asp:TemplateField HeaderText="Send to EPA"> 
                        <ItemStyle HorizontalAlign="Center" />                        
                        <ItemTemplate> 
                            <asp:CheckBox ID="chkWQX" Enabled="false" runat="server" Checked='<% #Eval("WQX_IND") %>' />
                            <asp:ImageButton ID="WQXButton" runat="server" CausesValidation="False" CommandName="WQX"
                                CommandArgument='<% #Eval("PROJECT_IDX") %>' ImageURL='<%# GetImage((string)Eval("WQX_SUBMIT_STATUS"),(Boolean)Eval("WQX_IND")) %>'
                                ToolTip="WQX History" />
                        </ItemTemplate> 
                    </asp:TemplateField> 

                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="btnRibbon">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add New" onclick="btnAdd_Click" />
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" style="float:right; " ImageUrl="~/App_Images/ico_xls.png" onclick="btnExcel_Click" />
        <asp:ImageButton ID="btnConfig" runat="server" Height="24px"  style="float:right; padding-right:5px; padding-left:5px" ImageUrl="~/App_Images/ico_config.png" />
    </div>
    <br />

    <!-- ******************** MODAL PANEL -->
    <asp:Panel ID="pnlModal" Width="500px" runat="server" CssClass="modalWindow" Style="display: none;">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl" runat="server" CssClass="modalTitle" Style="cursor: move">
                Select Fields to Include in View
            </asp:Panel>
            <div class="row" style="height:100px;">
                <div class="fldLbl">
                    <asp:CheckBoxList ID="chkFields" Width="500px" runat="server" RepeatColumns="2">
                        <asp:ListItem Text="SAMP_DESIGN_TYPE_CD"></asp:ListItem>
                        <asp:ListItem Text="QAPP_APPROVAL"></asp:ListItem>
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
