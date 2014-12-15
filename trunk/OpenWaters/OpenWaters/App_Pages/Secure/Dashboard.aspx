<%@ Page Title="Open Waters - Dashboard" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="OpenEnvironment.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="indent">
        <table style="width:100%; height:100%; margin: 0px; vertical-align:top;">
            <tr style="vertical-align:top; height:10px;">
                <td>
                    <h1>Open Waters Dashboard</h1>
                </td>
            </tr>
            <tr>
                <td style="width:33%; vertical-align:top;">
                    <br />
                    <div class="brdr-green" style="min-width:350px">
                        <div class="ie9roundedgradient"><div class="rndPnlTop-green">Data Collection Metrics</div></div>
                        <div class="indent">
                            <div class="row">
                                <div class="fldLbl" style="width:250px; ">Number of Organizations: </div>
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
                </td>
                <td style="width:33%; vertical-align:top;">
                </td>
                <td style="width:33%; vertical-align:top;">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
