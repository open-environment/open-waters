<%@ Page Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXImportNew.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.WQXImportNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function DisplayLoadingDiv()
        {
            $('#openModal').show();
        }
    </script>
    <asp:ObjectDataSource ID="dsProject" runat="server" SelectMethod="GetWQX_PROJECT"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsTemplate" runat="server" SelectMethod="GetWQX_IMPORT_TEMPLATE"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <h1>
        Bulk Import Data
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
    <asp:Panel ID="pnlLab" runat="server" >
        <div class="fltTitle">Step 1: Select Data to Import</div>
        <div class="fltMain">
            <div class="row"> 
                <span class="fldLbl">Data to Import:</span>
                <asp:DropDownList CssClass="fldTxt" ID="ddlImportType" runat="server" OnSelectedIndexChanged="ddlImportType_SelectedIndexChanged" AutoPostBack="True" >
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Text="Monitoring Locations" Value="MLOC"></asp:ListItem>
                    <asp:ListItem Text="Activities" Value="SAMP"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:HyperLink ID="hlTemplate" runat="server" Target="_blank" Visible="false">Download a Blank Import Template</asp:HyperLink>
            </div>                
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlSampOptions" runat="server" Visible="false" > 
        <div class="fltTitle">Step 2: Define Data Import Rules</div>
        <div class="fltMain">
            <div class="row">
                <asp:Button ID="btnDefaults" runat="server" CssClass="btn" Text="Global Import Rules" OnClick="btnDefaults_Click"  />
            </div>
            <div class="row">
                <span class="fldLbl">Select Import Logic:</span>
                <asp:DropDownList CssClass="fldTxt" ID="ddlTemplate" runat="server" DataSourceID="dsTemplate" DataTextField="TEMPLATE_NAME"  DataValueField="TEMPLATE_ID">
                </asp:DropDownList>
                <asp:Button ID="btnNewTemplate" runat="server" CssClass="btn" Text="Define New / Edit Import Logic" OnClick="btnNewTemplate_Click" />
            </div>
            <div class="row">
                <span class="fldLbl">Import to Project:</span>
                <asp:DropDownList CssClass="fldTxt" ID="ddlProject" runat="server"  DataSourceID="dsProject" DataTextField="PROJECT_ID"  DataValueField="PROJECT_IDX">
                </asp:DropDownList>
            </div>
        </div>
    </asp:Panel>
    <div class="clear"></div>
    <asp:Panel ID="pnlPasteData" runat="server" Visible="false" > 
        <div class="fltTitle">Step 3: Paste Data</div>
        <div class="fltMain">
            <div class="row">
                Copy and paste your data from a spreadsheet into the text area below:<br />
                <asp:TextBox ID="txtPaste" TextMode="MultiLine" Width="98%" Height="98%" Rows="10" runat="server"></asp:TextBox>
            </div>
            <div class="row">
                <asp:Button ID="btnParse" runat="server" CssClass="btn" Text="Continue" onclick="btnParse_Click" OnClientClick="DisplayLoadingDiv()" />
            </div>
        </div>
    </asp:Panel>
    <br />

</asp:Content>
