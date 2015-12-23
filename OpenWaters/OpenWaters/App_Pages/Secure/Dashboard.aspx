<%@ Page Title="Open Waters - Dashboard" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="OpenEnvironment.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ($) {
            $('#signup_1').hide();

            if ($('#ctl00_MainContent_spnWiz1').attr('class') != 'signup_header_check')
                $('#signup_1').show();
            else if ($('#ctl00_MainContent_spnWiz2').attr('class') != 'signup_header_check')
                $('#signup_2').show();
            else if ($('#ctl00_MainContent_spnWiz3').attr('class') != 'signup_header_check')
                $('#signup_3').show();
            else if ($('#ctl00_MainContent_spnWiz4').attr('class') != 'signup_header_check')
                $('#signup_4').show();
            else if ($('#ctl00_MainContent_spnWiz5').attr('class') != 'signup_header_check')
                $('#signup_5').show();
            else if ($('#ctl00_MainContent_spnWiz6').attr('class') != 'signup_header_check')
                $('#signup_6').show();



            $('#signup_steps').find('.ui-accordion-header').click(function () {
                $(this).next().slideToggle('fast');
                //hide the other panels
                $(".ui-accordion-content").not($(this).next()).slideUp('fast');
            });
        });
    </script>

    <div class="indent">

        <table style="width:100%; height:100%; margin: 0px; vertical-align:top;">
            <tr style="vertical-align:top; height:10px;">
                <td>
                    <h1>Open Waters Dashboard</h1>
                </td>
            </tr>
            <tr>
                <td style="width:33%; vertical-align:top;">
                    <div class="brdr-green" style="min-width:350px">
                        <div class="ie9roundedgradient"><div class="rndPnlTop-green">Data Collection Metrics</div></div>
                        <div class="indent">
                            <div class="row">
                                <div class="fldLbl" style="width:250px; ">Organizations Using Open Waters: </div>
                                <asp:Label ID="lblOrg" runat="server" CssClass="fldTxt" style="font-weight:bold" ></asp:Label>
                            </div>
                            <asp:Panel ID="pnlOrgSpecific" runat="server" Visible="false">
                                <div class="row">
                                    <asp:Label ID="lblOrgName" runat="server" CssClass="bold"></asp:Label> <div class="bold" > Data Counts</div>
                                </div>
                                <div class="indent">
                                    <div class="row">
                                        <div class="fldLbl" style="width:250px; ">Number of Projects: </div>
                                        <asp:Label ID="lblProject2" runat="server" CssClass="fldTxt" style="font-weight:bold" ></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="fldLbl" style="width:250px; ">Number of Samples: </div>
                                        <asp:Label ID="lblSamp" runat="server" CssClass="fldTxt" style="font-weight:bold" ></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="fldLbl" style="width:250px; ">Samples Pending Transfer to EPA: </div>
                                        <asp:Label ID="lblSampPend2" runat="server" CssClass="fldTxt" style="font-weight:bold" ></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="fldLbl" style="width:250px; ">Number of Results: </div>
                                        <asp:Label ID="lblResult" runat="server" CssClass="fldTxt" style="font-weight:bold" ></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>

                            <br />
                            <br />
                        </div>
                    </div>

                    <asp:Panel ID="pnlAdminTasks" runat="server" CssClass="brdr-green" style="min-width:350px" Visible="false">
                        <div class="ie9roundedgradient"><div class="rndPnlTop-green">Admin Tasks</div></div>
                        <div class="indent">
                            <div class="row">
                                <asp:Label ID="lblAdminMsg" runat="server" CssClass="failureNotification"></asp:Label>
                            </div>
                            <asp:Panel ID="pnlPendingUsers" runat="server" CssClass="indent">
                                <h3>Pending Users Requiring Your Approval</h3>
                                <asp:GridView ID="grdPendingUsers" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False"  
                                    AlternatingRowStyle-CssClass="alt" onrowcommand="grdPendingUsers_RowCommand" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ApproveButton" runat="server" CausesValidation="False" CommandName="Approve"
                                                    CommandArgument='<% #Eval("USER_IDX")+","+ Eval("ORG_ID") %>' ImageUrl="~/App_Images/ico_pass.png" ToolTip="Approve" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reject">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="RejectButton" runat="server" CausesValidation="False" CommandName="Reject"
                                                    CommandArgument='<% #Eval("USER_IDX")+","+ Eval("ORG_ID") %>' ImageUrl="~/App_Images/ico_fail.png" ToolTip="Reject" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="USER_ID" HeaderText="User ID" SortExpression="USER_ID" />
                                        <asp:BoundField DataField="ORG_ID" HeaderText="Organization" SortExpression="ORG_ID" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                        </div>
                    </asp:Panel>

                </td>
                <td style="width:67%; vertical-align:top;">
                    <div class="brdr-green success" style="min-width:350px; background-color: #8ec165;" >
                        <div class="ie9roundedgradient"><div class="rndPnlTop-green">Getting Started Guide</div></div>
                        <div class="indent" >


                            <!-- GETTING STARTED WIZARD -->
                            <div id="signup_steps" class="ui-accordion ui-helper-reset ">
                                <h3 class="ui-accordion-header ui-helper-reset ui-corner-all"><span class="ui-icon ui-icon-exp">1</span>
                                    <a target="_blank">
                                        <span class="signup_tabs_header ">Create or Join an Organization</span>
                                        <span id="spnWiz1" runat="server" class=""></span>
                                    </a>
                                </h3>
                                <div class="signup_container infoSlide ui-accordion-content ui-helper-reset ui-corner-bottom" id="signup_1" style="height: 20px; overflow: auto; padding-top: 13px; padding-bottom: 13px; display: block;">
                                    <span >
                                        <asp:Label ID="lblWiz1" runat="server" CssClass="left" Width="450px" ></asp:Label>
                                        <asp:Button ID="btnWiz1" runat="server" Text="Get Started" CssClass="btn right" OnClick="btnWiz1_Click"  />
                                    </span>
                                </div>

                                <h3 class="ui-accordion-header ui-helper-reset ui-corner-all"><span class="ui-icon ui-icon-exp">2</span>
                                    <a target="_blank">
                                        <span class="signup_tabs_header">Authorize Open Waters to Submit Data</span>
                                        <span id="spnWiz2" runat="server" class=""></span>
                                    </a>
                                </h3>
                                <div class="signup_container infoSlide ui-accordion-content ui-helper-reset ui-corner-bottom" id="signup_2" style="height: 20px; overflow: auto; padding-top: 13px; padding-bottom: 13px; display: none;">
                                    <asp:Label ID="lblWiz2" runat="server" CssClass="left" Width="450px" Text="In order to submit data to EPA using Open Waters, you must contact EPA and request that they authorize Open Waters to submit data."></asp:Label>
                                    <asp:Button ID="btnWiz2" runat="server" Text="Get Started" CssClass="btn right" OnClick="btnWiz2_Click" />
                                </div>

                                <h3 class="ui-accordion-header ui-helper-reset ui-corner-all"><span class="ui-icon ui-icon-exp">3</span>
                                    <a target="_blank">
                                        <span class="signup_tabs_header">Enter 1 or more monitoring locations</span>
                                        <span id="spnWiz3" runat="server" class=""></span>
                                    </a>
                                </h3>
                                <div class="signup_container infoSlide ui-accordion-content ui-helper-reset ui-corner-bottom" id="signup_3" style="height: 20px; display: none; overflow: auto; padding-top: 13px; padding-bottom: 13px;">
                                    <asp:Label ID="lblWiz3" runat="server" CssClass="left" Width="450px" Text="After you create an organization, you can then enter or import monitoring locations." ></asp:Label>
                                    <asp:Button ID="btnWiz3" runat="server" Text="Enter" CssClass="btn right" OnClick="btnWiz3_Click"  />                                    
                                    <asp:Button ID="btnWiz3b" runat="server" Text="Import" CssClass="btn right" OnClick="btnWiz4b_Click"  />
                                </div>

                                <h3 class="ui-accordion-header ui-helper-reset ui-corner-all"><span class="ui-icon ui-icon-exp">4</span>
                                    <a target="_blank">
                                        <span class="signup_tabs_header">Enter 1 or more projects</span>
                                        <span id="spnWiz4" runat="server" class=""></span>
                                    </a>
                                </h3>
                                <div class="signup_container infoSlide ui-accordion-content ui-helper-reset ui-corner-bottom" id="signup_4" style="height: 40px; overflow: auto; padding-top: 13px; padding-bottom: 13px; display: none;">
                                    <asp:Label ID="lblWiz4" runat="server" CssClass="left" Width="450px" Text="After you create an organization, you can then enter or import projects." ></asp:Label>
                                    <asp:Button ID="btnWiz4" runat="server" Text="Enter" CssClass="btn right" OnClick="btnWiz4_Click"  />
                                    <asp:Button ID="btnWiz4b" runat="server" Text="Import" CssClass="btn right" OnClick="btnWiz4b_Click"  />
                                </div>

                                <h3 class="ui-accordion-header ui-helper-reset ui-corner-all"><span class="ui-icon ui-icon-exp">5</span>
                                    <a target="_blank">
                                        <span class="signup_tabs_header">Pick default settings for your Organization</span>
                                        <span id="spnWiz5" runat="server" class=""></span>
                                    </a>
                                </h3>
                                <div class="signup_container infoSlide ui-accordion-content ui-helper-reset ui-corner-bottom" id="signup_5" style="height: 20px; overflow: auto; padding-top: 13px; padding-bottom: 13px; display: none;">
                                    <asp:Label ID="lblWiz5" runat="server" CssClass="left" Width="450px" Text="After you create an organization, you can then pick default settings for your organization (e.g. which Characteristics you will sample, default TimeZone, etc) that can aid in faster data entry." ></asp:Label>
                                    <asp:Button ID="btnWiz5" runat="server" Text="Get Started" CssClass="btn right" OnClick="btnWiz5_Click"  />
                                </div>

                                <h3 class="ui-accordion-header ui-helper-reset ui-corner-all"><span class="ui-icon ui-icon-exp">6</span>
                                    <a target="_blank">
                                        <span class="signup_tabs_header">Enter 1 or more activites</span>
                                        <span id="spnWiz6" runat="server" class=""></span>
                                    </a>
                                </h3>
                                <div class="signup_container infoSlide ui-accordion-content ui-helper-reset ui-corner-bottom" id="signup_6" style="height: 20px; overflow: auto; padding-top: 13px; padding-bottom: 13px; display: none;">
                                    <asp:Label ID="lblWiz6" runat="server" CssClass="left" Width="450px" Text="After you create a monitoring location and project, you can then enter or import samples."></asp:Label>
                                    <asp:Button ID="btnWiz6" runat="server" Text="Enter" CssClass="btn right" OnClick="btnWiz6_Click"  />
                                    <asp:Button ID="btnWiz6b" runat="server" Text="Import" CssClass="btn right" OnClick="btnWiz4b_Click"  />
                                </div>
                            </div>


                        </div>
                        <div id="clear" style="clear:both;"></div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
