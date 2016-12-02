<%@ Page Title="Open Waters-Import Translations" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportDefaults.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.WQXImportTranslate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
    <h1>
        Import Translations
    </h1>
    <div class="divHelp">
        Define translations if you want Open Waters to automatically translate your reported data to another value when importing data.
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <div id="gridwrap" style="width:100%; overflow:auto">
        <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" OnRowCommand="grdImport_RowCommand" 
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
        <asp:Button ID="btnExit" runat="server" CssClass="btn" Text="Exit" OnClick="btnExit_Click"  />
        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add Translation"  />
    </div>


    <!-- modal for add new hard code column -->
    <ajaxToolkit:ModalPopupExtender ID="MPE_Translate" runat="server" TargetControlID="btnAdd" PopupControlID="pnlModal" 
        CancelControlID="btnCloseHC" BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl2">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModal" Width="700px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnAddTranslate">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl2" runat="server" CssClass="modalTitle" Style="cursor: move">
                Add New Translation
            </asp:Panel>
            <div class="row" >
                <span class="fldLbl">Field:</span>
                <asp:DropDownList ID="ddlField" runat="server" >
<%--                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="Station ID" Text="Station ID"></asp:ListItem>
                    <asp:ListItem Value="Activity Type Code" Text="Activity Type Code"></asp:ListItem>
                    <asp:ListItem Value="Activity Media" Text="Activity Media"></asp:ListItem>
                    <asp:ListItem Value="Activity Submedia" Text="Activity Submedia"></asp:ListItem>--%>
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
                <asp:Button ID="btnCloseHC" runat="server" Text="Cancel" CssClass="btn" />
            </div>
        </div>
    </asp:Panel>

</asp:Content>
