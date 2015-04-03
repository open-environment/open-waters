<%@ Page Title="Open Waters - Organization Details" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgEdit.aspx.cs" Inherits="OpenEnvironment.WQXOrgEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script>
        jQuery(document).ready(function () {
            //radio button on doc ready
            var value2 = $('#ctl00_ctl00_MainContent_BodyContent_rbCDX input:checked').val();
            if (value2 == 1) {
                $("#divCDXme").show();
                $("#divCDXglobal").hide();
            }
            else {
                $("#divCDXme").hide();
                $("#divCDXglobal").show();
            }

            //click radio button
            $('#ctl00_ctl00_MainContent_BodyContent_rbCDX input').click(function () {
                $("#ctl00_ctl00_MainContent_BodyContent_pnlCDXResults").hide();
                var value = $('#ctl00_ctl00_MainContent_BodyContent_rbCDX input:checked').val();
                if (value == 1)
                {
                    $("#divCDXme").show();
                    $("#divCDXglobal").hide();
                }
                else
                {
                    $("#divCDXme").hide();
                    $("#divCDXglobal").show();
                }
            });
        });
    </script>

    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="Tribe" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <h1>
        Edit Organization
    </h1>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
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
        <asp:Panel ID="pnlCDX" runat="server" CssClass="" style="margin-top:20px; width: 600px; background:#dedede repeat-x top; border:1px solid #808080;" >
            <h3 style="margin-top:0px">Credentials for Submitting to EPA</h3>
            <div class="row">
                <asp:Label ID="lblCDXSubmitInd" runat="server"></asp:Label>
            </div>
            <asp:RadioButtonList ID="rbCDX" runat="server">
                <asp:ListItem Text="Submit to EPA using my own NAAS credentials" Value="1"></asp:ListItem>
                <asp:ListItem Text="Submit to EPA using Open Waters global NAAS credentials" Value="2"></asp:ListItem>
            </asp:RadioButtonList>
            <div id="divCDXme" style="display:none; padding: 0px 0px 0px 17px">
                <div class="row" >
                    <span class="fldLbl">CDX Submitter:</span>
                    <asp:TextBox ID="txtCDX" runat="server" Width="220px" MaxLength="100" CssClass="fldTxt"></asp:TextBox>
                </div>
                <div class="row">
                    <span class="fldLbl">CDX Submitter Password:</span>
                    <asp:TextBox ID="txtCDXPwd" runat="server" Width="220px" TextMode="Password" MaxLength="100" CssClass="fldTxt"></asp:TextBox>
                </div>        
                <div class="row">
                    <asp:Button ID="btnTestNAASLocal" runat="server" Text="Test My Credentials" CssClass="btn" OnClick="btnTestNAASLocal_Click" />
                </div>
            </div>
            <div id="divCDXglobal"  style="display:none">
                <asp:Button ID="btnTestNAASGlobal" runat="server" Text="Check if Open Waters is Authorized to Submit for Your Organization"  CssClass="btn" OnClick="btnTestNAASGlobal_Click" />
            </div>
            <asp:Panel ID="pnlCDXResults" runat="server" style="padding-left: 24px;" Visible="false" >
                <h3 style="margin-bottom:5px">Test Results</h3>
                <b>Authentication:</b>
                <br />
                <span id="spnAuth" runat="server" class="" style="left:0px; top:0px; display: inline-block; position: relative; width: 20px; height: 20px; background-size: 100% auto;"></span>
                <asp:Label ID="lblAuthResult" runat="server" style="vertical-align: top;"></asp:Label>
                <br />
                <br />
                <b>Ability to Submit:</b>
                <br />
                <span id="spnSubmit" runat="server" class="" style="left:0px; top:0px; display: inline-block; position: relative; width: 20px; height: 20px; background-size: 100% auto;"></span>
                <asp:Label ID="lblSubmitResult" runat="server"  style="vertical-align: top;"></asp:Label>
            </asp:Panel>

        </asp:Panel>
        <asp:Panel ID="pnlRoles" runat="server" CssClass="row">
            <table>
                <tr>
                    <td>
                        Available Users<br />
                        <asp:ListBox ID="lbAllUsers" runat="server" Height="150px" Width="150px" CssClass="fldTxt">
                        </asp:ListBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkAdmin" CssClass="fldLbl" runat="server" Text="Add as Admin" />
                        <br /><br />
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
            <asp:Button ID="btnSettings" runat="server" CssClass="btn" Text="Edit Default Data" OnClick="btnSettings_Click" />
        </div>
    </asp:Panel>

</asp:Content>
