<%@ Page Title="Open Waters - Mon Loc Details" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXMonLocEdit.aspx.cs" Inherits="OpenEnvironment.WQXMonLocEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="MonitoringLocationType" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsCounty" runat="server" SelectMethod="GetT_WQX_REF_COUNTY" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
        </SelectParameters>
    </asp:ObjectDataSource>
    <h2>
        Edit Monitoring Location
    </h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <asp:Label ID="lblMonLocIDX" runat="server" Style="display:none"/>
    </p>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
        <div class="row">
            <span class="fldLbl">Mon Loc ID:</span>
            <asp:TextBox ID="txtMonLocID" runat="server" Width="250px" CssClass="fldTxt" MaxLength="35" ></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMonLocID" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>

        </div>
        <div class="row">
            <span class="fldLbl">Mon Loc Name:</span>
            <asp:TextBox ID="txtMonLocName" Width="250px" runat="server" CssClass="fldTxt" MaxLength="255"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMonLocName" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Mon Loc Type:</span>
            <asp:DropDownList ID="ddlMonLocType" runat="server" CssClass="fldTxt" ></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonLocType" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Mon Loc Desc:</span>
            <asp:TextBox ID="txtMonLocDesc" runat="server" Width="250px"  CssClass="fldTxt" ></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">8-Digit HUC:</span>
            <asp:TextBox ID="txtHUC8" MaxLength="8" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">12-Digit HUC:</span>
            <asp:TextBox ID="txtHUC12" MaxLength="12" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">On Tribal Land?:</span>
            <asp:CheckBox ID="chkLandInd" runat="server" CssClass="fldTxt" />
            <asp:TextBox ID="txtLandName" runat="server" Width="250px" MaxLength="200" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Latitude:</span>
            <asp:TextBox ID="txtLatitude" runat="server" Width="90px" MaxLength="12"  CssClass="fldTxt"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLatitude" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
            <span class="fldLbl" style="width:80px">&nbsp;&nbsp;&nbsp;Longitude:</span>
            <asp:TextBox ID="txtLongitude" runat="server" Width="90px" MaxLength="14"  CssClass="fldTxt"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLongitude" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
            <asp:ImageButton ID="btnMap" runat="server" ImageUrl="~/App_Images/ico_map.png" PostBackUrl="http://maps.google.com/?q=-37.866963,144.980615" />
        </div>
        <div class="row">
            <span class="fldLbl">Source Map Scale:</span>
            <asp:TextBox ID="txtSourceMapScale" MaxLength="12" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Horiz. Collection Method</span>
            <asp:DropDownList ID="ddlHorizMethod" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlHorizMethod" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Horiz. Reference Datum</span>
            <asp:DropDownList ID="ddlHorizDatum" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlHorizDatum" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Vertical Measure:</span>
            <asp:TextBox ID="txtVertMeasure" runat="server" Width="90px" MaxLength="12"  CssClass="fldTxt"></asp:TextBox>
            <span class="fldLbl" style="width:80px">&nbsp;&nbsp;&nbsp;Unit:</span>
            <asp:DropDownList ID="ddlVertUnit" runat="server" CssClass="fldTxt"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Vert. Collection Method</span>
            <asp:DropDownList ID="ddlVertMethod" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Vert. Reference Datum</span>
            <asp:DropDownList ID="ddlVertDatum" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">County</span>
            <asp:DropDownList ID="ddlCounty" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">State</span>
            <asp:DropDownList ID="ddlState" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Country</span>
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Well Type</span>
            <asp:DropDownList ID="ddlWellType" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
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
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save &amp; Exit" onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" onclick="btnCancel_Click" />
        </div>
    </asp:Panel>
</asp:Content>
