<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="WQX_Mgmt.aspx.cs" Inherits="OpenEnvironment.WQX_Mgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h1>WQX Submission Management</h1>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <br />
    <asp:Panel ID="pnlFilter" CssClass="fltBox" runat="server" DefaultButton="btnSubmit">
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" PagerStyle-CssClass="pgr" 
            AlternatingRowStyle-CssClass="alt" DataSourceID="dsWQXHistory" onrowcommand="GridView1_RowCommand" Width="90%">
        <Columns>
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("LOG_ID") %>' Text="View File"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LOG_ID" HeaderText="ID" SortExpression="LOG_ID" />
            <asp:BoundField DataField="TABLE_CD" HeaderText="Type" SortExpression="TABLE_CD" />
            <asp:BoundField DataField="RECORD" HeaderText="Record" SortExpression="RECORD" />
            <asp:BoundField DataField="SUBMIT_DT" HeaderText="Submission Date" SortExpression="SUBMIT_DT" />
            <asp:BoundField DataField="SUBMIT_TYPE" HeaderText="Submission Type" SortExpression="SUBMIT_TYPE" />
            <asp:BoundField DataField="CDX_SUBMIT_TRANSID" HeaderText="CDX Transaction ID" SortExpression="CDX_SUBMIT_TRANSID" />
            <asp:BoundField DataField="CDX_SUBMIT_STATUS" HeaderText="CDX Status" SortExpression="CDX_SUBMIT_STATUS" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="dsWQXHistory" runat="server" 
        SelectMethod="GetV_WQX_TRANSACTION_LOG"
        TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="TableCD" Type="String" />
            <asp:Parameter Name="startDt" Type="DateTime" />
            <asp:Parameter DefaultValue="" Name="endDt" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
