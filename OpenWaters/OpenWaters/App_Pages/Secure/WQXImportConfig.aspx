<%@ Page Title="Open Waters - Import Logic" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportConfig.aspx.cs" Inherits="OpenEnvironment.WQXImportConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
   <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the import template - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function GetConfirmationCol() {
            var reply = confirm("WARNING: This will delete the data field - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".charRelated").hide();
            $("#HC1").hide();
            $("#HC2").hide();

            $("#ctl00_ctl00_MainContent_BodyContent_ddlFieldMap").change(function (event) {
                if ($("#ctl00_ctl00_MainContent_BodyContent_ddlFieldMap").val() == "CHAR") {
                    $(".charRelated").show();
                }
                else
                {
                    $(".charRelated").hide();
                }
            });

            $("#ctl00_ctl00_MainContent_BodyContent_ddlFieldMapHC").change(function (event) {
                if ($("#ctl00_ctl00_MainContent_BodyContent_ddlFieldMapHC").val() == "ACTIVITY_ID") {
                    $("#HC2").show();
                    $("#HC1").hide();
                }
                else {
                    $("#HC1").show();
                    $("#HC2").hide();
                }
            });

        });

    </script>
    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="MeasureUnit" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="onlyUsedInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>

    <h1>
        Import Logic Templates
    </h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <table style="width: 100%">
        <tr>
            <td style="width: 30%; vertical-align:top">
                <h2>
                    Import Template
                </h2>
                <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" 
                    AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" SelectedRowStyle-Font-Bold="true" 
                    onrowcommand="grdImport_RowCommand"  EmptyDataText="None" >
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="ImaSelectButton" runat="server" CausesValidation="False" CommandName="Select" 
                                    CommandArgument='<% #Eval("TEMPLATE_ID") %>' ImageUrl="~/App_Images/selectbutton.png" ToolTip="Select" />
                                <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmation();" 
                                    CommandArgument='<% #Eval("TEMPLATE_ID") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TEMPLATE_ID" HeaderText="ID" SortExpression="TEMPLATE_ID" />
                        <asp:BoundField DataField="TEMPLATE_NAME" HeaderText="Template Name" SortExpression="TEMPLATE_NAME" />
                        <asp:BoundField DataField="TYPE_CD" HeaderText="Data Type" SortExpression="TYPE_CD" />
                        <asp:BoundField DataField="CREATE_DT" HeaderText="Create Date" SortExpression="CREATE_DT" DataFormatString="{0:MM/dd/yyyy}"  />
                        <asp:BoundField DataField="CREATE_USERID" HeaderText="Created By" SortExpression="CREATE_USERID" />
                    </Columns>
                </asp:GridView>
                <div class="btnRibbon">
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Back" OnClick="btnCancel_Click"   />
                    <asp:Button ID="btnNew" runat="server" CssClass="btn" Text="Define New Import Logic"  />
                </div>

            </td>
            <td style="vertical-align:top">
                <asp:Panel ID="pnlDtl" runat="server" Visible="false">
                    <h2>
                        Mapped Columns
                    </h2>
                    Any columns that are not mapped will be ignored during import.
                    <asp:GridView ID="grdTemplateDtl" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" EmptyDataText="None" OnRowCommand="grdTemplateDtl_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmationCol();"
                                        CommandArgument='<% #Eval("TEMPLATE_DTL_ID") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="COL_NUM" HeaderText="Column" SortExpression="COL_NUM" />
                            <asp:BoundField DataField="FIELD_MAP" HeaderText="Field Map" SortExpression="FIELD_MAP" />
                            <asp:BoundField DataField="CHAR_NAME" HeaderText="Char Name" SortExpression="CHAR_NAME" />
                            <asp:BoundField DataField="CHAR_DEFAULT_UNIT" HeaderText="Default Unit" SortExpression="CHAR_DEFAULT_UNIT"  />
                            <asp:BoundField DataField="CHAR_DEFAULT_SAMP_FRACTION" HeaderText="Default Sample Fraction" SortExpression="CHAR_DEFAULT_SAMP_FRACTION"  />
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnAddDynamic" runat="server" CssClass="btn" Text="Add New" />
                    <h2>
                        Hard-Coded Values
                    </h2>
                    <asp:GridView ID="grdHardCode" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" EmptyDataText="None" OnRowCommand="grdHardCode_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmationCol();"
                                        CommandArgument='<% #Eval("TEMPLATE_DTL_ID") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="FIELD_MAP" HeaderText="Field Map" SortExpression="FIELD_MAP" />
                            <asp:BoundField DataField="CHAR_NAME" HeaderText="Char Name" SortExpression="CHAR_NAME" />
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnAddHardCode" runat="server" CssClass="btn" Text="Add New" />
                </asp:Panel>
            </td>
        </tr>
    </table>


    <!-- modal popup  add new template -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_NewTemplate" runat="server" TargetControlID="btnNew"
        PopupControlID="pnlModal" CancelControlID="btnClose" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModal" Width="500px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnConfigSave">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add New Import Logic Template
            </asp:Panel>
            <div class="row" >
                <span class="fldLbl">Template Type:</span>
                <asp:DropDownList ID="chkTemplateType" runat="server" >
                    <asp:ListItem Value="S_CT" Text="Sample (1 row per sample)" Selected="True"></asp:ListItem>
<%--                    <asp:ListItem Value="S" Text="Sample (1 row per result)"></asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <div class="row" >
                <span class="fldLbl">Template Name:</span>
                <asp:TextBox ID="txtTemplateNew" runat="server"></asp:TextBox>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnConfigSave" runat="server" Text="Save" CssClass="btn" OnClick="btnTemplateAdd_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hdnTemplateID" runat="server" />

    <!-- modal for add new column -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_NewCol" runat="server" TargetControlID="btnAddDynamic"
        PopupControlID="pnlModalNewColumn" CancelControlID="btnCloseHC" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl3">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModalNewColumn" Width="600px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnAddColumn">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl3" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add Mapped Column
            </asp:Panel>
            <div class="row" >
                <span class="fldLbl">Column Number:</span>
                <asp:TextBox ID="txtColumn" runat="server" MaxLength="2"></asp:TextBox>
            </div>
            <div class="row" >
                <span class="fldLbl">Field:</span>
                <asp:DropDownList ID="ddlFieldMap" runat="server" >
                    <asp:ListItem Value="ACT_COMMENTS" Text="ACT_COMMENTS"></asp:ListItem>
                    <asp:ListItem Value="ACT_MEDIA" Text="ACT_MEDIA"></asp:ListItem>
                    <asp:ListItem Value="ACT_START_DATE" Text="ACT_START_DATE"></asp:ListItem>
                    <asp:ListItem Value="ACT_START_TIME" Text="ACT_START_TIME"></asp:ListItem>
                    <asp:ListItem Value="ACT_SUBMEDIA" Text="ACT_SUBMEDIA"></asp:ListItem>
                    <asp:ListItem Value="ACT_TYPE" Text="ACT_TYPE"></asp:ListItem>
                    <asp:ListItem Value="ACTIVITY_ID" Text="ACTIVITY_ID"></asp:ListItem>
                    <asp:ListItem Value="CHAR" Text="Characteristic"></asp:ListItem>
                    <asp:ListItem Value="MONLOC_ID" Text="MONLOC_ID"></asp:ListItem>
                    <asp:ListItem Value="RESULT_VALUE_TYPE" Text="RESULT_VALUE_TYPE"></asp:ListItem>
                    <asp:ListItem Value="RESULT_STATUS" Text="RESULT_STATUS"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="row charRelated" id="divChar1" >
                <span class="fldLbl">Characteristic Name:</span>
                <asp:DropDownList ID="ddlColChar" runat="server" CssClass="fldTxt"  style="width:300px"></asp:DropDownList>
            </div>
            <div class="row charRelated" id="divChar2" >
                <span class="fldLbl">Characteristic Unit Code:</span>
                <asp:DropDownList ID="ddlColCharUnit" runat="server" CssClass="fldTxt"  style="width:300px"></asp:DropDownList>
            </div>
            <div class="row charRelated" id="divChar3" >
                <span class="fldLbl">Sample Fraction:</span>
                <asp:DropDownList ID="ddlSampFraction" runat="server" CssClass="fldTxt"  style="width:300px"></asp:DropDownList>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnAddColumn" runat="server" Text="Save" CssClass="btn" OnClick="btnAddColumn_Click" />
                <asp:Button ID="btnCloseCol" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>


    <!-- modal for add new hard code column -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_HardCode" runat="server" TargetControlID="btnAddHardCode"
        PopupControlID="pnlModalHardCode" CancelControlID="btnCloseHC" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl2">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModalHardCode" Width="500px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnHardCodeAdd">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl2" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add New Hardcoded Column
            </asp:Panel>
            <div class="row" >
                <span class="fldLbl">Field:</span>
                <asp:DropDownList ID="ddlFieldMapHC" runat="server" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="ACT_COMMENTS" Text="ACT_COMMENTS"></asp:ListItem>
                    <asp:ListItem Value="ACT_MEDIA" Text="ACT_MEDIA"></asp:ListItem>
                    <asp:ListItem Value="ACT_SUBMEDIA" Text="ACT_SUBMEDIA"></asp:ListItem>
                    <asp:ListItem Value="ACT_TYPE" Text="ACT_TYPE"></asp:ListItem>
                    <asp:ListItem Value="ACTIVITY_ID" Text="ACTIVITY_ID"></asp:ListItem>
                    <asp:ListItem Value="RESULT_VALUE_TYPE" Text="RESULT_VALUE_TYPE"></asp:ListItem>
                    <asp:ListItem Value="RESULT_STATUS" Text="RESULT_STATUS"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="row" id="HC1">
                <span class="fldLbl">Hardcoded Value:</span>
                <asp:TextBox ID="txtHardCodeValue" runat="server"></asp:TextBox>
            </div>
            <div class="row" id="HC2">
                <span class="fldLbl">ID Auto Format:</span>
                <asp:DropDownList ID="ddlHardActID" runat="server">
                    <asp:ListItem Value="#M_D_T" Text="MonLoc_Date_Time"></asp:ListItem>
                    <asp:ListItem Value="#M_D_TS" Text="MonLoc_Date_Time_Seconds"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnHardCodeAdd" runat="server" Text="Save" CssClass="btn" OnClick="btnHardCodeAdd_Click" />
                <asp:Button ID="btnCloseHC" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>

