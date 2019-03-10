<%@ Page Title="Open Waters - Import" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true"  CodeBehind="WQXImportFromEPA.aspx.cs" Inherits="OpenEnvironment.WQXImportFromEPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function DisplayLoadingDiv()
        {
            $('#openModal').show();
        }
    </script>
    <h1>
        Retrieve Data from EPA
    </h1>

    <div id="openModal" class="modalDialog" style="display:none">
        <div>
       	    <h2>Data Review in Progress</h2>
            <p>Please be patient while your sampling data is parsed and validated. Do not refresh this page. Data validation results will be shown shortly. </p>
            <br />
            <img alt="loading" src="../../App_Images/loading.gif" style="padding-left:85px;" />
            <br />
        </div>
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:TextBox ID="txtOrgTest" runat="server" Visible="False"></asp:TextBox>
    <asp:Panel ID="pnlFilter" CssClass="fltBox" runat="server">
        <div class="fltTitle">Step 1: Select Data Source </div>
        <div class="fltMain">
                <div class="row"> 
                    <asp:RadioButtonList ID="rbImportType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbImportType_SelectedIndexChanged">
                        <asp:ListItem Value="1" Enabled="false">Import data from spreadsheet</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Import data directly from EPA-WQX</asp:ListItem>
                    </asp:RadioButtonList>
                </div>                
        </div>

        <asp:Panel ID="pnlWQX" runat="server" >
            <div class="fltTitle">Step 2: Select Type of Data to Import from EPA-WQX:</div>
            <div class="fltMain">
                <div class="row">
                    <asp:DropDownList ID="ddlWQXImportType" runat="server" CssClass="fldTxt">
                        <asp:ListItem Value="MLOC" Text="Monitoring Locations"></asp:ListItem>
                        <asp:ListItem Value="PROJ" Text="Projects"></asp:ListItem>
                        <asp:ListItem Value="ACT" Text="Activities"></asp:ListItem>                 
                    </asp:DropDownList>
                </div>
                <div class="row">
                    <asp:Button ID="btnWQXContinue" runat="server" CssClass="btn" Text="Begin Import from EPA-WQX" OnClick="btnWQXContinue_Click" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlLab" runat="server" Visible="false" >
            <div class="fltTitle">Step 2: Select Data to Import </div>
            <div class="fltMain">
                <div class="row"> 
                    <span class="fldLbl">Import Data Structure:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlImportType" runat="server" >
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Monitoring Locations" Value="MLOC"></asp:ListItem>
                        <asp:ListItem Text="Sample Results - 1 row per result" Value="SAMP"></asp:ListItem>
                        <asp:ListItem Text="Sample Results - 1 row per sample (1 column per result)" Value="SAMP_CT"></asp:ListItem>
                        <asp:ListItem Text="Bio Metrics" Value="BIO_METRIC"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:HyperLink ID="hlTemplate" runat="server" Visible="False" Target="_blank">Download a Blank Import Template</asp:HyperLink>
                </div>                
                <asp:Panel ID="pnlImportLogic" runat="server" CssClass="row" Visible="false" > 
                    <span class="fldLbl">Select Import Logic:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlTemplate" runat="server" DataSourceID="dsTemplate" DataTextField="TEMPLATE_NAME"  DataValueField="TEMPLATE_ID">
                    </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsTemplate" runat="server" SelectMethod="GetWQX_IMPORT_TEMPLATE"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    <asp:Button ID="btnNewTemplate" runat="server" CssClass="btn" Text="Define New / Edit Import Logic"  />
                </asp:Panel>
                <asp:Panel ID="pnlProject" runat="server" CssClass="row" Visible="false" > 
                    <span class="fldLbl">Import to Project:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlProject" runat="server"  DataSourceID="dsProject" DataTextField="PROJECT_ID"  DataValueField="PROJECT_IDX">
                    </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsProject" runat="server" SelectMethod="GetWQX_PROJECT"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
                                <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
                                <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                </asp:Panel>
                <asp:Button ID="btnDefaults" runat="server" CssClass="btn" Text="Defaults" OnClick="btnDefaults_Click" Visible="false" />
                <asp:Button ID="btnTranslate" runat="server" CssClass="btn" Text="Translations" OnClick="btnTranslate_Click" Visible="false" />
                <div class="row">
                    Copy and paste your data from a spreadsheet into the text area below:<br />
                    <asp:TextBox ID="txtPaste" TextMode="MultiLine" Width="98%" Height="98%" Rows="10" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <asp:Button ID="btnParse" runat="server" CssClass="btn" Text="Continue"   Visible="false" OnClientClick="DisplayLoadingDiv()" />
        </asp:Panel>
    </asp:Panel>


    <h1>
        Import History
    </h1>
    <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" onrowcommand="grdImport_RowCommand"   >
        <Columns>
            <asp:TemplateField HeaderText="Delete">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <ItemTemplate>
                    <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                        CommandArgument='<% #Eval("IMPORT_ID") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IMPORT_ID" HeaderText="ID" SortExpression="IMPORT_ID" />
            <asp:BoundField DataField="FILE_NAME" HeaderText="Data Type" SortExpression="FILE_NAME" />
            <asp:BoundField DataField="CREATE_DT" HeaderText="Import Date" SortExpression="CREATE_DT" />
            <asp:BoundField DataField="IMPORT_STATUS" HeaderText="Import Status" SortExpression="IMPORT_STATUS" />
            <asp:BoundField DataField="IMPORT_PROGRESS_MSG" HeaderText="Status Description" SortExpression="IMPORT_PROGRESS_MSG" />
        </Columns>
    </asp:GridView>
</asp:Content>
