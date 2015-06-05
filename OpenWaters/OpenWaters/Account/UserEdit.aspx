<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true"
    CodeBehind="UserEdit.aspx.cs" Inherits="OpenEnvironment.UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the user - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <h2>
        Edit User
    </h2>
    <div class="divHelp">
        This page allows application administrators to edit an application user.
    </div>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    </p>
    <asp:Panel ID="pnl1" runat="server" DefaultButton="btnSave">
        <div class="row">
            <span class="fldLbl">User Name</span>
            <asp:TextBox ID="txtUserIDX" style="display:none;" runat="server" ></asp:TextBox>
            <asp:TextBox ID="txtUserID" runat="server" MaxLength="25" CssClass="fldTxt" Enabled="False"></asp:TextBox>
            <asp:CheckBox ID="chkActive" CssClass="fldTxt" runat="server" Text="Active" />
        </div>
        <asp:Panel ID="pnlPwd" runat="server" CssClass="row">
            <span class="fldLbl">Password</span>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="fldTxt" Width="200px" TextMode="Password" MaxLength="40"></asp:TextBox>
        </asp:Panel>
        <div class="row">
            <span class="fldLbl">First Name</span>
            <asp:TextBox ID="txtFName" runat="server" CssClass="fldTxt" Width="200px" MaxLength="40"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl" >Last Name</span>
            <asp:TextBox ID="txtLName" runat="server" CssClass="fldTxt" Width="200px" MaxLength="40"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Email</span>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="fldTxt" Width="200px"  MaxLength="150"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Phone</span>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="fldTxt" Width="100px" MaxLength="12"></asp:TextBox>
            <span class="fldLbl" style="width:10px" ></span>
            <span class="fldLbl" style="width:75px" >Extention</span>
            <asp:TextBox ID="txtPhoneExt" runat="server" CssClass="fldTxt"  Width="50px" MaxLength="4"></asp:TextBox>
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnBack" runat="server" CssClass="btn" OnClick="btnBack_Click" Text="Go Back" />
            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn" OnClick="btnDelete_Click" OnClientClick="return GetConfirmation();" Text="Delete" />
        </div>
    </asp:Panel>
</asp:Content>
