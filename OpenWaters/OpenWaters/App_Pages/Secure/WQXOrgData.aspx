<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgData.aspx.cs" Inherits="OpenEnvironment.WQXOrgData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <link href="../../Scripts/chosen.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/openenvi.tabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetConfirmation(item) {
            var reply = confirm("WARNING: This will delete the " + item +  " - are you sure you want to continue?");
            return (reply ? true : false);
        }
    </script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery(".chosen").data("placeholder", "Being typing or select from list...").chosen({ allow_single_deselect: true });
        });
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
    <asp:HiddenField ID="hdnOrgID" runat="server" />
    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>

    <asp:Panel ID="pnlTabs" runat="server" >
        <div id="tabs-container"  >
            <ul class="tabs-menu" >
                <li id="tabby1" class="current"><a href="#tab-1">General Defaults</a></li>
                <li id="tabby2"><a href="#tab-2">Characteristic Defaults</a></li>
                <li id="tabby3"><a href="#tab-3">Taxa Defaults</a></li>
                <li id="tabby4"><a href="#tab-4">Import Translations</a></li>
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
                        (1) Characteristics listed here will be included in the characteristic dropdown when manually entering samples (as opposed to importing them) <br/>
                        (2) Define default values (such as Default Detection Limit, Unit of Measure, etc) that will be automatically populated when importing sampling data. 
                        When importing samples, if data is not included in your import file, Open Waters will apply the default value here if it is available.
                    </div>
                    <asp:Button ID="btnAddChar" runat="server" CssClass="btn" Text="Add Characteristic" OnClick="btnAddChar_Click" />
                    <asp:GridView ID="grdChar" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="False"
                        AutoGenerateColumns="False" DataKeyNames="CHAR_NAME" onrowcommand="grdChar_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImaSelectButton" runat="server" CausesValidation="False" CommandName="Select" CommandArgument='<% #Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                                    <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" OnClientClick="return GetConfirmation('characteristic data');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME"  />
                            <asp:BoundField DataField="DEFAULT_UNIT" HeaderText="Unit" SortExpression="DEFAULT_UNIT" />
                            <asp:BoundField DataField="DEFAULT_DETECT_LIMIT" HeaderText="Detect Limit" SortExpression="DEFAULT_DETECT_LIMIT" />
                            <asp:BoundField DataField="DEFAULT_LOWER_QUANT_LIMIT" HeaderText="Lower Quant Limit" SortExpression="DEFAULT_LOWER_QUANT_LIMIT" />
                            <asp:BoundField DataField="DEFAULT_UPPER_QUANT_LIMIT" HeaderText="Upper Quant Limit" SortExpression="DEFAULT_UPPER_QUANT_LIMIT" />
                            <asp:BoundField DataField="T_WQX_REF_ANAL_METHOD.ANALYTIC_METHOD_ID" HeaderText="Analysis Method"   />
                            <asp:BoundField DataField="DEFAULT_SAMP_FRACTION" HeaderText="Sample Fraction" SortExpression="DEFAULT_SAMP_FRACTION" />
                            <asp:BoundField DataField="DEFAULT_RESULT_STATUS" HeaderText="Status" SortExpression="DEFAULT_RESULT_STATUS" />
                            <asp:BoundField DataField="DEFAULT_RESULT_VALUE_TYPE" HeaderText="Value Type" SortExpression="DEFAULT_RESULT_VALUE_TYPE" />
                            <asp:BoundField DataField="CREATE_DT" HeaderText="Create Date" SortExpression="CREATE_DT" DataFormatString = "{0:d}"  />
                            <asp:BoundField DataField="CREATE_USERID" HeaderText="Created By" SortExpression="CREATE_USERID" />
                        </Columns>
                    </asp:GridView>

                </div>
                <div id="tab-3" class="tab-content" style="width:90%">
                    <div class="divHelp">
                        Taxa you list here will be included in the taxonomy drop down when manually entering biological samples
                    </div>
                    <asp:DropDownList ID="ddlTaxa" runat="server" CssClass="chosen" Visible="false"></asp:DropDownList>
                    <asp:Button ID="btnAddTaxa" runat="server" CssClass="btn" Text="Add Taxa" OnClick="btnAddTaxa_Click" />
                    <asp:GridView ID="grdTaxa" runat="server" CssClass="grd" AlternatingRowStyle-CssClass="alt" AllowPaging="False" AutoGenerateColumns="False" 
                        DataKeyNames="BIO_SUBJECT_TAXONOMY" OnRowCommand="grdTaxa_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                                        CommandArgument='<%# Eval("BIO_SUBJECT_TAXONOMY") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" OnClientClick="return GetConfirmation('taxa');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BIO_SUBJECT_TAXONOMY" HeaderText="Taxa Name" SortExpression="BIO_SUBJECT_TAXONOMY" ControlStyle-Width="98%" />
                            <asp:BoundField DataField="CREATE_DT" HeaderText="Added Date" SortExpression="CREATE_DT" />
                            <asp:BoundField DataField="CREATE_USERID" HeaderText="Added By" SortExpression="CREATE_USERID" />
                        </Columns>
                    </asp:GridView>

                </div>
                <div id="tab-4" class="tab-content" style="width:90%">
                    <div class="divHelp">
                        Define translations if you want Open Waters to automatically translate your reported data to another value when importing data.
                    </div>
                    <div class="btnRibbon">
                        <asp:Button ID="btnAddTranslate" runat="server" CssClass="btn" Text="Add Translation" OnClick="btnAddTranslate_Click"  />
                    </div>
                    <div id="gridwrap" style="width:100%; overflow:auto">
                        <asp:GridView ID="grdTranslate" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" 
                            OnRowCommand="grdTranslate_RowCommand" Width="100%" Style="word-wrap: break-word; "  >
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes" OnClientClick="return GetConfirmation('translation');" 
                                            CommandArgument='<% #Eval("TRANSLATE_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="COL_NAME" HeaderText="Field" SortExpression="COL_NAME" />
                                <asp:BoundField DataField="DATA_FROM" HeaderText="Data Reported As" SortExpression="DATA_FROM"  />
                                <asp:BoundField DataField="DATA_TO" HeaderText="Will Get Translated To" SortExpression="DATA_TO"  />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div> 

            </div>
        </div>

    </asp:Panel>



    <asp:Button runat="server" ID="btnHidden" style="display: none" />
    <!-- modal for adding an import translation -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_Translate" runat="server" TargetControlID="btnHidden" PopupControlID="pnlModal" 
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
                <asp:Button ID="btnAddTranslate2" runat="server" Text="Save" CssClass="btn" OnClick="btnAddTranslate2_Click" />
                <asp:Button ID="btnCloseModal1" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>


    <!-- modal for adding default characteristic -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_NewChar" runat="server" TargetControlID="btnHidden" PopupControlID="pnlModalNewChar" 
        CancelControlID="btnCloseModal2" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl3">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModalNewChar" Width="600px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnAddChar2">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl3" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add/Edit Characteristic Defaults
            </asp:Panel>
            <div class="row" >
                <span class="fldLbl">Characteristic Name:</span>
                <asp:DropDownList ID="ddlChar" runat="server" CssClass="chosen" style="max-width:320px"></asp:DropDownList>
            </div>
            <div class="row" > 
                <span class="fldLbl">Default Unit:</span>
                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="fldTxt"  Width="320px"></asp:DropDownList>
            </div>
            <div class="row" > 
                <span class="fldLbl">Detection Limit:</span>
                <asp:TextBox ID="txtDetectLimit" runat="server" CssClass="fldTxt"  Width="320px" MaxLength="12" ></asp:TextBox>
            </div>
            <div class="row" > 
                <span class="fldLbl">Lower Quant Limit:</span>
                <asp:TextBox ID="txtQuantLower" runat="server" CssClass="fldTxt"  Width="320px" MaxLength="12" ></asp:TextBox>
            </div>
            <div class="row" > 
                <span class="fldLbl">Upper Quant Limit:</span>
                <asp:TextBox ID="txtQuantUpper" runat="server" CssClass="fldTxt"  Width="320px" MaxLength="12" ></asp:TextBox>
            </div>
            <div class="row" > 
                <span class="fldLbl">Analysis Method:</span>
                <asp:DropDownList ID="ddlAnalMethod" runat="server" CssClass="fldTxt"  Width="320px"></asp:DropDownList>
            </div>
            <div class="row" > 
                <span class="fldLbl">Sample Fraction:</span>
                <asp:DropDownList ID="ddlFraction" runat="server" CssClass="fldTxt"  Width="320px"></asp:DropDownList>
            </div>
            <div class="row" > 
                <span class="fldLbl">Result Status:</span>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="fldTxt"  Width="320px"></asp:DropDownList>
            </div>
            <div class="row" > 
                <span class="fldLbl">Result Value Type:</span>
                <asp:DropDownList ID="ddlValueType" runat="server" CssClass="fldTxt" Width="320px" ></asp:DropDownList>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnAddChar2" runat="server" Text="Save" CssClass="btn" OnClick="btnAddChar2_Click" />
                <asp:Button ID="btnCloseModal2" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>



</asp:Content>
