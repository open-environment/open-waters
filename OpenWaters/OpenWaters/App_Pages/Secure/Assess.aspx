<%@ Page Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Assess.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.Assess" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <h1 style="display:inline;">Assessment Reports</h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification" ></asp:Label>
    <br />
    <asp:GridView ID="grdAssessmentReports" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" 
        AlternatingRowStyle-CssClass="alt" style="float:left" OnRowCommand="grdAssessmentReports_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Edit">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                <ItemTemplate>
                    <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits" 
                        CommandArgument='<% #Eval("ATTAINS_REPORT_IDX") %>' ImageUrl="~/App_Images/ico_edit.png" ToolTip="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="REPORT_NAME" HeaderText="Report Name" SortExpression="REPORT_NAME" />
            <asp:BoundField DataField="DATA_FROM" HeaderText="Reporting Period Start Date" SortExpression="DATA_FROM" DataFormatString="{0:d}" />
            <asp:BoundField DataField="DATA_TO" HeaderText="Reporting Period Start Date" SortExpression="DATA_TO" DataFormatString="{0:d}" />
            <asp:BoundField DataField="CREATE_DT" HeaderText="Last Updated" SortExpression="CREATE_DT" />
            <asp:TemplateField HeaderText="Submission Status"> 
                <ItemStyle HorizontalAlign="Center" Width="50px" />                        
                <ItemTemplate> 
                    <asp:ImageButton ID="SubmitButton" runat="server" CausesValidation="False" CommandName="ATTAINS"
                        CommandArgument='<% #Eval("ATTAINS_REPORT_IDX") %>' ImageUrl='<%# GetImage((string)Eval("ATTAINS_SUBMIT_STATUS")) %>'
                        ToolTip="Click to view ATTAINS Submission History" />
                </ItemTemplate> 
            </asp:TemplateField> 

        </Columns>
    </asp:GridView>
    <div class="btnRibbon">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add New" onclick="btnAdd_Click" />
    </div>
    <br />

</asp:Content>