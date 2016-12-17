<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgData.aspx.cs" Inherits="OpenEnvironment.WQXOrgData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <link href="../../Scripts/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/openenvi.tabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the translation - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="onlyUsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsTimeZone" runat="server" SelectMethod="GetT_WQX_REF_DEFAULT_TIME_ZONE" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsTaxa" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="Taxon" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsAnalMethod" runat="server" SelectMethod="GetT_WQX_REF_ANAL_METHOD"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>

    <h1>
        Organization Data Rules
    </h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>

    <asp:Panel ID="pnlTabs" runat="server" >
        <div id="tabs-container"  >
            <ul class="tabs-menu" >
                <li class="current"><a href="#tab-1">General Defaults</a></li>
                <li><a href="#tab-2">Characteristic Defaults</a></li>
                <li><a href="#tab-3">Taxa Defaults</a></li>
                <li><a href="#tab-4">Import Translations</a></li>
            </ul>
            <div class="tab" style="clear: both;width:100%; min-height:400px">
                <div id="tab-1" class="tab-content" style="width:95%">
                    <div class="divHelp">
                        Before entering activities, you must set the default timezone (used to set the correct timezone for activities).
                    </div>
                    <div class="row">
                        <span class="fldLbl">Default Time Zone:</span>
                        <asp:DropDownList ID="ddlTimeZone" runat="server" Width="258px" CssClass="fldTxt"></asp:DropDownList>
                    </div>
                    <br /><br /><br />
                    <div class="btnRibbon">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" />
                    </div>


                </div>
                <div id="tab-2" class="tab-content" style="width:90%">
                    <div class="divHelp">
                        This tab serves two purposes: <br/>
                        (1) Characteristics you list here will be included in the characteristic drop down when manually entering samples (as opposed to importing them) <br/>
                        (2) Define default values (such as Default Detection Limit, Unit of Measure, etc) that will be populated by Open Waters when importing sampling results data. 
                        When importing samples, if a field is not included in your import, Open Waters will look here to use a default value if it is available.
                    </div>
                </div>
                <div id="tab-3" class="tab-content" style="width:90%">
                    <div class="divHelp">
                        Taxa you list here will be included in the taxonomy drop down when manually entering biological samples
                    </div>
                </div>
                <div id="tab-4" class="tab-content" style="width:90%">
                    <div class="divHelp">
                        Define translations if you want Open Waters to automatically translate your reported data to another value when importing data.
                    </div>
                    <div id="gridwrap" style="width:100%; overflow:auto">
                        <asp:GridView ID="grdTranslate" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" OnRowCommand="grdTranslate_RowCommand" 
                            Width="100%" Style="word-wrap: break-word; "  >
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmation();" 
                                            CommandArgument='<% #Eval("TRANSLATE_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="COL_NAME" HeaderText="Field" SortExpression="COL_NAME" />
                                <asp:BoundField DataField="DATA_FROM" HeaderText="Data Reported As" SortExpression="DATA_FROM"  />
                                <asp:BoundField DataField="DATA_TO" HeaderText="Will Get Translated To" SortExpression="DATA_TO"  />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="btnRibbon">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add Translation"  />
                    </div>
                </div>

            </div>
        </div>

    </asp:Panel>




    <!-- modal for adding an import translation -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_Translate" runat="server" TargetControlID="btnAdd" PopupControlID="pnlModal" 
        CancelControlID="btnCloseModal1" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl2">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModal" Width="700px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnAddTranslate">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl2" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add New Translation
            </asp:Panel>
            <div class="row" >
                <span class="fldLbl">Field:</span>
                <asp:DropDownList ID="ddlField" runat="server" >
                </asp:DropDownList>
            </div>
            <div class="row" >
                <span class="fldLbl">Value From:</span>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
            </div>
            <div class="row" >
                <span class="fldLbl">Translates To:</span>
                <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnAddTranslate" runat="server" Text="Save" CssClass="btn" OnClick="btnAddTranslate_Click" />
                <asp:Button ID="btnCloseModal1" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
