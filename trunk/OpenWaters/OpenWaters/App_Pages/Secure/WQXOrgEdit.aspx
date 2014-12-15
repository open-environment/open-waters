<%@ Page Title="Open Waters - Organization Details" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgEdit.aspx.cs" Inherits="OpenEnvironment.WQXOrgEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will remove the characteristic from this organization - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <link href="../../Scripts/chosen.css" rel="stylesheet" type="text/css" />
    <script>
        jQuery(document).ready(function () {
            jQuery(".chosen").data("placeholder", "Being typing or select from list...").chosen({ allow_single_deselect: true });
        });
    </script>

    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="Tribe" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="onlyUsedInd" Type="Boolean" />
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
        <div class="row">
            <span class="fldLbl">CDX Submitter:</span>
            <asp:TextBox ID="txtCDX" runat="server" Width="250px" MaxLength="100" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">CDX Submitter Password:</span>
            <asp:TextBox ID="txtCDXPwd" runat="server" Width="250px" TextMode="Password" MaxLength="100" CssClass="fldTxt"></asp:TextBox>
        </div>        
        <asp:Panel ID="pnlRoles" runat="server" CssClass="row">
            <table>
                <tr>
                    <td>
                        Available Users<br />
                        <asp:ListBox ID="lbAllUsers" runat="server" Height="150px" Width="150px" CssClass="fldTxt">
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" CssClass="btn" Width="180px" runat="server" Text="Add User to Org &gt;&gt;" OnClick="btnAdd_Click" /><br />
                        <asp:Button ID="btnRemove" CssClass="btn" Width="180px" runat="server" Text="&lt;&lt; Remove User From Org" OnClick="btnRemove_Click" />
                    </td>
                    <td>
                        Users in Organization<br />
                        <asp:ListBox ID="lbUserInRole" runat="server" Height="150px" Width="150px" CssClass="fldTxt">
                        </asp:ListBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save &amp; Exit" onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" onclick="btnCancel_Click" />
        </div>
        <asp:Panel ID="pnlChars" runat="server" CssClass="row">
            <h2>Characteristics Used</h2>
            <asp:DropDownList ID="ddlChar" runat="server" CssClass="chosen"></asp:DropDownList>
            <asp:Button ID="btnAddChar" runat="server" CssClass="btn" Text="Add Characteristic" onclick="btnAddChar_Click"/>

            <asp:GridView ID="grdChar" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="False"
                AutoGenerateColumns="False" DataKeyNames="CHAR_NAME" onrowcommand="grdChar_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                                CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" OnClientClick="return GetConfirmation();" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME" ControlStyle-Width="98%" />
                    <asp:BoundField DataField="CREATE_DT" HeaderText="Create Date" SortExpression="CREATE_DT" />
                    <asp:BoundField DataField="CREATE_USERID" HeaderText="Created By" SortExpression="CREATE_USERID" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>

</asp:Content>
