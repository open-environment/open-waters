<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true"  CodeBehind="WQXImport.aspx.cs" Inherits="OpenEnvironment.WQXImport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ObjectDataSource ID="dsImportLog" runat="server" 
        SelectMethod="GetWQX_IMPORT_LOG" 
        TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
    </asp:ObjectDataSource>
    <h1>
        Bulk Import Data
    </h1>
    <asp:Panel ID="pnlFilter" CssClass="fltBox" runat="server">
        <div class="fltTitle">Select File Type</div>
        <div class="fltMain">
            <div class="row">
                <div style="width:420px; float:left"> 
                    <span class="fldLbl">File Type:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlMonLoc" runat="server">
                        <asp:ListItem Text="Bio Metrics" Selected="True" Value="BIO_METRIC"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div style="width:420px; float:left"> 
                    <span class="fldLbl">Import to Project:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlProject" runat="server" 
                         DataSourceID="dsProject" DataTextField="PROJECT_ID" 
                         DataValueField="PROJECT_IDX"></asp:DropDownList>
                     <asp:ObjectDataSource ID="dsProject" runat="server" 
                         SelectMethod="GetWQX_PROJECT" 
                         TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
                         <SelectParameters>
                             <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
                             <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
                             <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
                         </SelectParameters>
                     </asp:ObjectDataSource>
                </div>
            </div>
        <br />
        <asp:TextBox ID="txtPaste" TextMode="MultiLine" Width="98%" Height="98%" Rows="10" runat="server"></asp:TextBox>

        </div><br />
    </asp:Panel>

    <asp:Button ID="btnParse" runat="server" CssClass="btn" Text="Import"  onclick="btnParse_Click" />

    <h1>
        Import History
    </h1>
    <asp:GridView ID="grdImport" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" 
        AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" 
        onrowcommand="grdImport_RowCommand" DataSourceID="dsImportLog" >
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
