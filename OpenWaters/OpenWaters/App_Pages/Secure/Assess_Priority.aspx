<%@ Page Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Assess_Priority.aspx.cs" Inherits="OpenEnvironment.App_Pages.Secure.Assess_Priority" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the priority - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <h1>
        Priorities
    </h1>
    <asp:HiddenField ID="hdnReportIDX" runat="server" />
    <asp:HiddenField ID="hdnPriorityIDX" runat="server" />

    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave" style="min-width:800px" >
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
        </div>
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" Style="float:left;" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" CausesValidation="false" Style="float:left;" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="Delete" CausesValidation="false" Style="float:right;" OnClick="btnDelete_Click" OnClientClick="return GetConfirmation();" />
        </div>
    </asp:Panel>

</asp:Content>
