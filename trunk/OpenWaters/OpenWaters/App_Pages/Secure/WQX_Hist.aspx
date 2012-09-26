<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQX_Hist.aspx.cs" Inherits="OpenEnvironment.WQX_Hist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <h1>WQX Submission History</h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd"
            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
            DataSourceID="dsWQXHistory" onrowcommand="GridView1_RowCommand" Width="90%">
        <Columns>
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("LOG_ID") %>'
                        Text="View File"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="LOG_ID" HeaderText="ID" 
                SortExpression="LOG_ID" />
            <asp:BoundField DataField="SUBMIT_DT" HeaderText="Submission Date" 
                SortExpression="SUBMIT_DT" />
            <asp:BoundField DataField="SUBMIT_TYPE" HeaderText="Submission Type" 
                SortExpression="SUBMIT_TYPE" />
            <asp:BoundField DataField="CDX_SUBMIT_TRANSID" HeaderText="CDX Transaction ID" 
                SortExpression="CDX_SUBMIT_TRANSID" />
            <asp:BoundField DataField="CDX_SUBMIT_STATUS" HeaderText="CDX Status" 
                SortExpression="CDX_SUBMIT_STATUS" />
        </Columns>
    </asp:GridView>
    <div class="btnRibbon">
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn" onclick="btnBack_Click" />
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" style="float:right; " ImageUrl="~/App_Images/ico_xls.png" onclick="btnExcel_Click" />
    </div>

    <asp:ObjectDataSource ID="dsWQXHistory" runat="server" 
        SelectMethod="GetWQX_TRANSACTION_LOG" 
        TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="MLOC" Name="TableCD" Type="String" />
            <asp:SessionParameter DefaultValue="" Name="TableIdx" SessionField="MonLocIDX" 
                Type="Int32" /> 
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
