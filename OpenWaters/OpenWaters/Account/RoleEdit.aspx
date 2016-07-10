<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="OpenEnvironment.RoleEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the role - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }  
    </script>
    <h2>
        Edit Role
    </h2>
    <div class="divHelp">
        This page allows application administrators to edit a role and add users to a particular application role.
    </div>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    </p>
    <asp:Panel ID="pnl1" runat="server" DefaultButton="btnSave">
        <div class="row">
            <span class="fldLbl">Role Name</span>
            <asp:TextBox ID="txtRoleName" runat="server" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Role Description</span>
            <asp:TextBox ID="txtRoleDesc" Width="400px" runat="server" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <table>
                <tr>
                    <td>
                        Available Users<br />
                        <asp:ListBox ID="lbAllUsers" runat="server" Height="150px" Width="150px" CssClass="fldTxt">
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" CssClass="btn" Width="180px" runat="server" Text="Add User to Role &gt;&gt;" OnClick="btnAdd_Click" /><br />
                        <asp:Button ID="btnRemove" CssClass="btn" Width="180px" runat="server" Text="&lt;&lt; Remove User From Role" OnClick="btnRemove_Click" />
                    </td>
                    <td>
                        Users in Role<br />
                        <asp:ListBox ID="lbUserInRole" runat="server" Height="150px" Width="150px" CssClass="fldTxt">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Go Back" OnClick="btnBack_Click" />
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn" OnClientClick="return GetConfirmation();" Text="Delete" OnClick="btnDelete_Click" />
        </div>
    </asp:Panel>
</asp:Content>
