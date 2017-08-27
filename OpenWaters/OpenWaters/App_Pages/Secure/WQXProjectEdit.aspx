<%@ Page Title="Open Waters - Project Details" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXProjectEdit.aspx.cs" Inherits="OpenEnvironment.WQXProjectEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="SamplingDesignType" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <h2>
        Edit Project
    </h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <asp:Label ID="lblProjectIDX" runat="server" Style="display:none"/>
    </p>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
        <div class="row">
            <span class="fldLbl">Project ID:</span>
            <asp:TextBox ID="txtProjID" runat="server" MaxLength="35" Width="250px" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Project Name:</span>
            <asp:TextBox ID="txtProjName" Width="250px" MaxLength="120" runat="server" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Project Description:</span>
            <asp:TextBox ID="txtProjDesc" runat="server" MaxLength="4000" TextMode="MultiLine" Rows="3" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Sampling Design Type</span>
            <asp:DropDownList ID="ddlSampDesignTypeCode" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">QAPP Approved?: </span>
            <asp:CheckBox ID="chkQAPPInd" runat="server" CssClass="fldTxt" />
            <span class="fldLbl">Approval Agency: </span><asp:TextBox ID="txtQAPPAgency" runat="server" Width="250px" MaxLength="200" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Active?</span>
            <asp:CheckBox ID="chkActInd" runat="server" CssClass="fldTxt" />
        </div>
        <div class="row">
            <span class="fldLbl">Send to EPA</span>
            <asp:CheckBox ID="chkWQXInd" runat="server" CssClass="fldTxt" />
        </div>

        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save &amp; Exit" 
                onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" 
                onclick="btnCancel_Click" />
        </div>
    </asp:Panel>

</asp:Content>
