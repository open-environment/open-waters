<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAuth.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="OpenEnvironment.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding: 0px 0px 0px 15px">
        <h1>Support Materials:</h1>
        <br />
        <div class="indent">
            <b>Administration and User's Guide:</b> 
            <div class="indent">
                <div class="smallnote">Document describes how to use Open Waters.</div> 
                <asp:HyperLink ID="hlUsersGuide" runat="server" NavigateUrl="~/App_Docs/UsersGuide.docx" Target="_blank">Link</asp:HyperLink>
            </div>
            <br /><br />
            
            <b>Monitoring Location Import Template (Excel):</b>
            <div class="indent">
                <div class="smallnote">This template is used to bulk import a list of monitoring locations.</div> 
                <asp:HyperLink ID="hlMonLoc" runat="server" NavigateUrl="~/App_Docs/MonLoc_ImportTemplate.xlsx" Target="_blank">Link</asp:HyperLink>
            </div>        
            <br /><br />
            
            <b>Sample Import Template (Excel):</b> 
            <div class="indent">
                <div class="smallnote">This template is used when your characteristics are arranged in rows. This master template can be used for standard field and laboratory sampling, habitat 
                    assessments, and biological monitoring.
                </div> 
                <asp:HyperLink ID="hlSamp" runat="server" NavigateUrl="~/App_Docs/Samp_ImportTemplate.xlsx" Target="_blank">Link</asp:HyperLink>
            </div>

            <br /><br /><b>Sample Crosstab Template (Excel):</b>
            <div class="indent">
                <div class="smallnote">This template is used when your characteristics are arranged in columns. This is typical of csv files that export directly off of multiprobes (YSI, Manta, etc).</div> 
                <asp:HyperLink ID="hlSampCT" runat="server" NavigateUrl="~/App_Docs/SampCT_ImportTemplate.xlsx" Target="_blank">Link</asp:HyperLink>
            </div>

            <br /><br />
        </div>

        <h1>Email Support:</h1>
        <div class="indent">
            <div class="smallnote">Send email for additional support.</div> 
            <asp:HyperLink ID="hlEmail" runat="server" NavigateUrl = "mailto:abc@abc.com" Text = "abc@abc.com"></asp:HyperLink>
        </div>

    </div>
</asp:Content>
