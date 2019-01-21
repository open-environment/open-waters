<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQX_Mgmt.aspx.cs" Inherits="OpenEnvironment.WQX_Mgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h1>WQX Submission Management</h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <br />
    <asp:Panel ID="pnlFilter" CssClass="fltBox" runat="server" DefaultButton="btnSubmit"  style="width:40%; display:inline-block;   vertical-align: top;">
        <div class="fltTitle">Data Filters</div>
        <div class="fltMain">
            <div class="row">
                <asp:RadioButtonList ID="rbFilter" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="View Submitted Data" Value="SUB" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="View Pending Data" Value="PEND"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="row">
                <span class="left">Start Date:</span>
                <asp:TextBox ID="txtStartDate" runat="server" Width="80px"  CssClass="fldTxt"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"  AutoCompleteValue="false"
                    Enabled="True" TargetControlID="txtStartDate" MaskType="Date" Mask="99/99/9999">
                </ajaxToolkit:MaskedEditExtender>
                <span class="left">End Date:</span>
                <asp:TextBox ID="txtEndDate" runat="server" Width="80px" CssClass="fldTxt"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="true"  AutoCompleteValue="false"
                    Enabled="True" TargetControlID="txtEndDate" MaskType="Date" Mask="99/99/9999">
                </ajaxToolkit:MaskedEditExtender>
                <asp:CheckBox ID="chkAllOrgs" runat="server" Visible="false" Text="Show all Orgs" CssClass="fldTxt" style="padding-right:10px;" />
                <asp:Button ID="btnFilter" runat="server" CssClass="btn left" Text="Apply" OnClick="btnFilter_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlResubmit" CssClass="fltBox" runat="server" DefaultButton="btnSubmit" style="width:50%; display:inline-block;   vertical-align: top;">
        <div class="fltTitle">Submission Options</div>
        <div class="fltMain">
            <div class="row">
                <asp:RadioButtonList ID="rbSubmit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Submit As Individual Records" Value=""></asp:ListItem>
                    <asp:ListItem Text="Submit As One File" Value=""></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" onclick="btnSubmit_Click" Text="Submit All Pending Records to EPA" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReset" runat="server" CssClass="btn" Text="Reset Service" onclick="btnReset_Click" onmouseout="BalloonPopupControlBehavior.hidePopup();" />
            <ajaxToolkit:BalloonPopupExtender ID="btnReset_BalloonPopupExtender" BalloonPopupControlID="pnlBalloon" Position="TopRight" runat="server" TargetControlID="btnReset" UseShadow="false" DisplayOnMouseOver="True" ></ajaxToolkit:BalloonPopupExtender>
            Current Status:&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Text="Unknown"></asp:Label>
            &nbsp;<asp:Panel ID="pnlBalloon" runat="server" >Reset submission task if it appears to be unresponsive</asp:Panel>
        </div>
    </asp:Panel>
    <asp:GridView ID="grdWQXLog" runat="server" AutoGenerateColumns="False" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" onrowcommand="grdWQXLog_RowCommand" Width="90%">
        <Columns>
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("LOG_ID") %>' Text="View File"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
            <asp:BoundField DataField="LOG_ID" HeaderText="ID" SortExpression="LOG_ID" />
            <asp:BoundField DataField="TABLE_CD" HeaderText="Type" SortExpression="TABLE_CD" />
            <asp:BoundField DataField="RECORD" HeaderText="Record" SortExpression="RECORD" />
            <asp:BoundField DataField="SUBMIT_DT" HeaderText="Submission Date" SortExpression="SUBMIT_DT" />
            <asp:BoundField DataField="SUBMIT_TYPE" HeaderText="Submission Type" SortExpression="SUBMIT_TYPE" />
            <asp:BoundField DataField="CDX_SUBMIT_TRANSID" HeaderText="CDX Transaction ID" SortExpression="CDX_SUBMIT_TRANSID" />
            <asp:BoundField DataField="CDX_SUBMIT_STATUS" HeaderText="CDX Status" SortExpression="CDX_SUBMIT_STATUS" />
        </Columns>
    </asp:GridView>
    <asp:GridView ID="grdWQXPending" runat="server" AutoGenerateColumns="False" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="90%" Visible="false">
        <Columns>
            <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
            <asp:BoundField DataField="TABLE_CD" HeaderText="Type" SortExpression="TABLE_CD" />
            <asp:BoundField DataField="REC_ID" HeaderText="ID" SortExpression="REC_ID" />
            <asp:BoundField DataField="UPDATE_USERID" HeaderText="Last Updated" SortExpression="UPDATE_USERID" />
            <asp:BoundField DataField="UPDATE_DT" HeaderText="Updated Date" SortExpression="UPDATE_DT" />
        </Columns>
    </asp:GridView>
    <div class="btnRibbon">
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" ImageUrl="~/App_Images/ico_xls.png" onclick="btnExcel_Click" />
    </div>

</asp:Content>
