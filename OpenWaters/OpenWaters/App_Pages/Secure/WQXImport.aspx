<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true"  CodeBehind="WQXImport.aspx.cs" Inherits="OpenEnvironment.WQXImport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <h1>
        Bulk Import Data
    </h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="pnlFilter" CssClass="fltBox" runat="server">
        <div class="fltTitle">Step 1: Select Data to Import </div>
        <div class="fltMain">
                <div class="row"> 
                    <span class="fldLbl">Import Data Structure:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlImportType" runat="server" OnSelectedIndexChanged="ddlImportType_SelectedIndexChanged" AutoPostBack="True">
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
                    <asp:Button ID="btnNewTemplate" runat="server" CssClass="btn" Text="Define New / Edit Import Logic" OnClick="btnNewTemplate_Click" />
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
                <div class="row">
                    Copy and paste your data from a spreadsheet into the text area below:<br />
                    <asp:TextBox ID="txtPaste" TextMode="MultiLine" Width="98%" Height="98%" Rows="10" runat="server"></asp:TextBox>
                </div>
        </div>
        <br />
    </asp:Panel>

    <asp:Button ID="btnParse" runat="server" CssClass="btn" Text="Continue"  onclick="btnParse_Click" Visible="false" />

    <h1>
        Import History
    </h1>
    <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" 
        AlternatingRowStyle-CssClass="alt" onrowcommand="grdImport_RowCommand"  >
        <Columns>
            <asp:TemplateField HeaderText="Delete">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <ItemTemplate>
                    <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                        CommandArgument='<% #Eval("IMPORT_ID") %>' ImageUrl="~/App_Images/ico_del.png"
                        ToolTip="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IMPORT_ID" HeaderText="ID" SortExpression="IMPORT_ID" />
            <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" SortExpression="FILE_NAME" />
            <asp:BoundField DataField="TYPE_CD" HeaderText="Data Type" SortExpression="TYPE_CD" />
            <asp:BoundField DataField="CREATE_DT" HeaderText="Import Date" SortExpression="CREATE_DT" />
            <asp:BoundField DataField="IMPORT_STATUS" HeaderText="Import Status" SortExpression="IMPORT_STATUS" />
        </Columns>
    </asp:GridView>
</asp:Content>
