<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeBehind="AdmDataSynch.aspx.cs" Inherits="OpenEnvironment.AdmDataSynch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Administration Data Synchronization Tasks
    </h1>
    <div class="divHelp">
        Site Administrators can use this page to pull necessary information from EPA.
    </div>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <div class="btnRibbon">
        <asp:Button ID="btnPullOrgs" runat="server" CssClass="btn"  Text="Pull Organizations List from EPA" OnClick="btnPullOrgs_Click"/>
        <div>Data Last Retrieved: <asp:Label ID="lblLastOrgDate" runat="server"></asp:Label></div>
    </div>   
    <div class="btnRibbon">
        <asp:DropDownList ID="ddlRefTable" runat="server">
            <asp:ListItem Selected="True" Value="ALL" Text="All Tables"></asp:ListItem>
            <asp:ListItem Value="---" Text="--------------------"></asp:ListItem>
            <asp:ListItem Value="ActivityMedia" Text="ActivityMedia"></asp:ListItem>
            <asp:ListItem Value="ActivityMediaSubdivision" Text="ActivityMediaSubdivision"></asp:ListItem>
            <asp:ListItem Value="ActivityType" Text="ActivityType"></asp:ListItem>
            <asp:ListItem Value="ActivityRelativeDepth" Text="ActivityRelativeDepth"></asp:ListItem>
            <asp:ListItem Value="AnalyticalMethod" Text="AnalyticalMethod"></asp:ListItem>
            <asp:ListItem Value="Assemblage" Text="Assemblage"></asp:ListItem>
            <asp:ListItem Value="BiologicalIntent" Text="BiologicalIntent"></asp:ListItem>
            <asp:ListItem Value="CellForm" Text="CellForm"></asp:ListItem>
            <asp:ListItem Value="CellShape" Text="CellShape"></asp:ListItem>
            <asp:ListItem Value="Characteristic" Text="Characteristic"></asp:ListItem>
            <asp:ListItem Value="County" Text="County"></asp:ListItem>
            <asp:ListItem Value="Country" Text="Country"></asp:ListItem>
            <asp:ListItem Value="DetectionQuantitationLimitType" Text="DetectionQuantitationLimitType"></asp:ListItem>
            <asp:ListItem Value="FrequencyClassDescriptor" Text="FrequencyClassDescriptor"></asp:ListItem>
            <asp:ListItem Value="Habit" Text="Habit"></asp:ListItem>
            <asp:ListItem Value="HorizontalCollectionMethod" Text="HorizontalCollectionMethod"></asp:ListItem>
            <asp:ListItem Value="HorizontalCoordinateReferenceSystemDatum" Text="HorizontalCoordinateReferenceSystemDatum"></asp:ListItem>
            <asp:ListItem Value="MeasureUnit" Text="MeasureUnit"></asp:ListItem>
            <asp:ListItem Value="MethodSpeciation" Text="MethodSpeciation"></asp:ListItem>
            <asp:ListItem Value="MetricType" Text="MetricType"></asp:ListItem>
            <asp:ListItem Value="MonitoringLocationType" Text="MonitoringLocationType"></asp:ListItem>
            <asp:ListItem Value="NetType" Text="NetType"></asp:ListItem>
            <asp:ListItem Value="ResultDetectionCondition" Text="ResultDetectionCondition"></asp:ListItem>
            <asp:ListItem Value="ResultLaboratoryComment" Text="ResultLaboratoryComment"></asp:ListItem>
            <asp:ListItem Value="ResultMeasureQualifier" Text="ResultMeasureQualifier"></asp:ListItem>
            <asp:ListItem Value="ResultSampleFraction" Text="ResultSampleFraction"></asp:ListItem>
            <asp:ListItem Value="ResultStatus" Text="ResultStatus"></asp:ListItem>
            <asp:ListItem Value="ResultTemperatureBasis" Text="ResultTemperatureBasis"></asp:ListItem>
            <asp:ListItem Value="ResultTimeBasis" Text="ResultTimeBasis"></asp:ListItem>
            <asp:ListItem Value="ResultValueType" Text="ResultValueType"></asp:ListItem>
            <asp:ListItem Value="ResultWeightBasis" Text="ResultWeightBasis"></asp:ListItem>
            <asp:ListItem Value="SampleCollectionEquipment" Text="SampleCollectionEquipment"></asp:ListItem>
            <asp:ListItem Value="SampleContainerColor" Text="SampleContainerColor"></asp:ListItem>
            <asp:ListItem Value="SampleContainerType" Text="SampleContainerType"></asp:ListItem>
            <asp:ListItem Value="SampleTissueAnatomy" Text="SampleTissueAnatomy"></asp:ListItem>
            <asp:ListItem Value="SamplingDesignType" Text="SamplingDesignType"></asp:ListItem>
            <asp:ListItem Value="State" Text="State"></asp:ListItem>
            <asp:ListItem Value="StatisticalBase" Text="StatisticalBase"></asp:ListItem>
            <asp:ListItem Value="Taxon" Text="Taxon"></asp:ListItem>
            <asp:ListItem Value="ThermalPreservativeUsed" Text="ThermalPreservativeUsed" ></asp:ListItem>
            <asp:ListItem Value="TimeZone" Text="TimeZone"></asp:ListItem>
            <asp:ListItem Value="ToxicityTestType" Text="ToxicityTestType"></asp:ListItem>
            <asp:ListItem Value="Tribe" Text="Tribes"></asp:ListItem>
            <asp:ListItem Value="VerticalCollectionMethod" Text="VerticalCollectionMethod"></asp:ListItem>
            <asp:ListItem Value="VerticalCoordinateReferenceSystemDatum" Text="VerticalCoordinateReferenceSystemDatum"></asp:ListItem>
            <asp:ListItem Value="Voltinism" Text="Voltinism"></asp:ListItem>
            <asp:ListItem Value="WellFormationType" Text="WellFormationType"></asp:ListItem>
            <asp:ListItem Value="WellType" Text="WellType"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnRefData" runat="server" CssClass="btn"  Text="Pull Reference Data from EPA" OnClick="btnRefData_Click" />
        <div>Data Last Retrieved: <asp:Label ID="lblLastRefDate" runat="server"></asp:Label></div>
    </div>   
</asp:Content>
