<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgNew.aspx.cs" Inherits="OpenEnvironment.WQXOrgNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript" src="../../Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/list.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function ($) {
            $("#ctl00_ctl00_MainContent_BodyContent_grdOrg tbody").addClass('list');

            var options = {
                valueNames: ['orgID', 'orgName']
            };

            var userList = new List('users', options);
        });

        $(function () {
            $('#ctl00_ctl00_MainContent_BodyContent_chkConfirm').click(function () {
                if ($(this).is(':checked')) {
                    $('#ctl00_ctl00_MainContent_BodyContent_btnConfirm').removeAttr('disabled');
                } else {
                    $('#ctl00_ctl00_MainContent_BodyContent_btnConfirm').attr('disabled', 'disabled');
                }
            });
        });
    </script>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="pnl1" runat="server">
        <h1>
            Join an Organization
        </h1>
        <div class="divHelp" style="font-size:14px; font-style:normal">
            From the list below, select the Organization that you are affiliated with. 
            <br /><br />
            <span style="color:red; font-weight: bold">Do not request to join an Organization to which you do not belong. Doing so may result in termination of your Open Waters account.</span>
        </div>
        <div id="users">
            <input class="search" placeholder="Search" style="height:20px; width: 200px; font-size: 14px;" />
            <asp:GridView ID="grdOrg" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" 
                AlternatingRowStyle-CssClass="alt" onrowcommand="grdOrg_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edits"
                                CommandArgument='<% #Eval("ORG_ID") %>' ImageUrl="~/App_Images/selectbutton.png" ToolTip="Request to Join" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ORG_ID" HeaderText="Organization ID" SortExpression="ORG_ID" ItemStyle-CssClass="orgID" />
                    <asp:BoundField DataField="ORG_FORMAL_NAME" HeaderText="Organization Name" SortExpression="ORG_FORMAL_NAME" ItemStyle-CssClass="orgName" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="btnRibbon">
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" OnClick="btnCancel_Click" />
            <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="I Cannot Find My Organization in the List Above" OnClick="btnAdd_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNewOrgConfirm" runat="server" Visible="false">
        <h1>
            Confirm your Selection
        </h1>
        <br />
        <div class="row">
            <div class="fldTxt">You have selected organization:</div> 
            <asp:Textbox ID="txtOrgIDConfirm" runat="server" CssClass="fldTxt" ReadOnly="false"></asp:Textbox>
        </div>
        <br /><br />
        <asp:Label ID="lblConfirmText" runat="server" CssClass="fldErrLg" Style="max-width:500px;"></asp:Label>
        <br /><br />
        <div class="row">
            <asp:CheckBox ID="chkConfirm" runat="server" Text="I attest that I am authorized to maintain and submit water quality data for the Organization listed above. Any misrepresentation of this may result in account termination." />
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn" OnClick="btnConfirm_Click" Enabled="false"/>
        </div>
    </asp:Panel>

</asp:Content>
