<%@ Page Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Assess_Assess.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.Assess_Assess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the assessment unit - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <h1>
        Assessment Details
    </h1>
    <asp:HiddenField ID="hdnReportIDX" runat="server" />
    <asp:HiddenField ID="hdnAssessIDX" runat="server" />
    <asp:ObjectDataSource ID="dsAssessUnit" runat="server" SelectMethod="GetT_ATTAINS_ASSESS_UNITS_byReportID"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Attains" >
        <selectparameters>
            <asp:SessionParameter DefaultValue="" Name="ReportID" SessionField="AssessRptIDX" Type="int32" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave" style="min-width:800px" >
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Assessment Unit:</span>
                <asp:DropDownList ID="ddlAssessUnit" runat="server" CssClass="fldTxt" Width="180px" ></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <span class="fldLbl" >Reporting Cycle (YYYY):</span>
            <asp:TextBox ID="txtRptCycle" runat="server" Width="70px" CssClass="fldTxt" MaxLength="4" style="margin-right:10px" ></asp:TextBox>
            <span class="fldLbl">Last Assessed (YYYY):</span>
            <asp:TextBox ID="txtLastAssess" runat="server" Width="70px" CssClass="fldTxt" MaxLength="4" style="margin-right:10px" ></asp:TextBox>
            <span class="fldLbl">Last Monitored (YYYY):</span>
            <asp:TextBox ID="txtLastMon" runat="server" Width="70px" CssClass="fldTxt" MaxLength="4" style="margin-right:10px" ></asp:TextBox>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Agency Code:</span>
                <asp:DropDownList ID="ddlAgencyCode" runat="server" CssClass="fldTxt" Width="180px" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="T" Text="Tribal"></asp:ListItem>
                    <asp:ListItem Value="S" Text="State"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Report Status:</span>
                <asp:DropDownList ID="ddlReportStatus" runat="server" CssClass="fldTxt" Width="180px" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="Final" Text="Final"></asp:ListItem>
                    <asp:ListItem Value="Draft" Text="Draft"></asp:ListItem>
                    <asp:ListItem Value="Public Comment" Text="Public Comment"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Trophic Status:</span>
                <asp:DropDownList ID="ddlTrophicStatus" runat="server" CssClass="fldTxt" Width="180px" >
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
        </div>
        <h1>
            Uses
        </h1>
        <div class="row">
            <asp:GridView ID="grdUses" runat="server" GridLines="None" CssClass="grd" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="alt" 
                ShowHeaderWhenEmpty="true" EmptyDataText="No uses specified." > 
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Deletes" 
                                CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MONLOC_ID" HeaderText="Use Name" SortExpression="MONLOC_ID" />
                    <asp:BoundField DataField="MONLOC_ID" HeaderText="Use Attainment" SortExpression="MONLOC_ID" />
                    <asp:BoundField DataField="MONLOC_ID" HeaderText="Trend Code" SortExpression="MONLOC_ID" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add Uses" Style="float:left; margin-bottom: 20px" />
        </div>


        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" Style="float:left;" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" CausesValidation="false" Style="float:left;" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" CausesValidation="false" Style="float:right;" OnClick="btnDelete_Click" OnClientClick="return GetConfirmation();" />
        </div>
    </asp:Panel>

</asp:Content>