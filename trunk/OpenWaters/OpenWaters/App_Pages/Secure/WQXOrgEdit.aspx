<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgEdit.aspx.cs" Inherits="OpenEnvironment.WQXOrgEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="Tribe" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <h2>
        Edit Organization
    </h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <asp:Label ID="lblMonLocIDX" runat="server" Style="display:none"/>
    </p>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
        <div class="row">
            <span class="fldLbl">Organization ID:</span>
            <asp:TextBox ID="txtOrgID" runat="server" MaxLength="30" Width="250px" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Organization Name:</span>
            <asp:TextBox ID="txtOrgName" Width="250px" MaxLength="120" runat="server" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Description:</span>
            <asp:TextBox ID="txtOrgDesc" Width="250px" MaxLength="500" TextMode="MultiLine" Rows="2" runat="server" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Tribal Code:</span>
            <asp:DropDownList ID="ddlTribalCode" runat="server" Width="258px" CssClass="fldTxt"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Email:</span>
            <asp:TextBox ID="txtOrgEmail" MaxLength="120" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Phone:</span>
            <asp:TextBox ID="txtOrgPhone" MaxLength="15" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Phone Extension:</span>
            <asp:TextBox ID="txtOrgPhoneExt" runat="server" Width="250px" MaxLength="6" CssClass="fldTxt"></asp:TextBox>
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save &amp; Exit" onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" onclick="btnCancel_Click" />
        </div>
    </asp:Panel>

</asp:Content>
