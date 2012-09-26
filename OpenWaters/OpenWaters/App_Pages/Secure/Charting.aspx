<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Charting.aspx.cs" Inherits="OpenEnvironment.Charting" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <asp:ObjectDataSource ID="dsChartTS" runat="server" SelectMethod="SP_WQXAnalysis" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
        <SelectParameters>
            <asp:Parameter DefaultValue="SERIES" Name="TypeText" Type="String" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
            <asp:ControlParameter ControlID="ddlMonLoc" ConvertEmptyStringToNull="true"  DefaultValue="0" Name="MonLocIDX" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlCharacteristic" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" DefaultValue="" Name="charName" Type="String" />
            <asp:Parameter DefaultValue="01/02/2000" Name="startDt" Type="DateTime" />
            <asp:Parameter DefaultValue="12/31/2012" Name="endDt" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsMonLoc" runat="server" SelectMethod="GetWQX_MONLOC" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX" >
        <selectparameters>
            <asp:Parameter DefaultValue="false" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="onlyUsedInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:Panel ID="pnlFilter" CssClass="fltBox" runat="server" DefaultButton="btnChart">
        <div class="fltTitle">Select Chart Options</div>
        <div class="fltMain">
            <div class="row">
                <div style="width:420px; float:left"> 
                    <span class="fldLbl">Chart Type:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlChartType" runat="server" 
                        AutoPostBack="True">
                        <asp:ListItem Text="Time Series" Value="SERIES"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div style="width:420px; float:left"> 
                    <span class="fldLbl" >Characteristic:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlCharacteristic" runat="server" ></asp:DropDownList>
                </div>
                <div style=" float:left"> 
                    <span class="fldLbl" >Monitoring Location:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlMonLoc" runat="server"  ></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <span class="fldLbl">Date Range:</span>
                <asp:TextBox ID="txtStartDt" runat="server" Width="80px" CssClass="fldTxt"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="txtStartDt_MaskedEditExtender" runat="server"
                    Enabled="True" TargetControlID="txtStartDt" MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:CalendarExtender ID="txtStartDt_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="txtStartDt">
                </ajaxToolkit:CalendarExtender>
                <span class="fldLbl" style="width:20px; margin:0 4px 0 4px;">to</span>
                <asp:TextBox ID="txtEndDt" runat="server" Width="80px" CssClass="fldTxt" ></asp:TextBox>
                &nbsp;&nbsp;
                <ajaxToolkit:MaskedEditExtender ID="txtEndDt_MaskedEditExtender" runat="server"
                    Enabled="True" TargetControlID="txtEndDt" MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:CalendarExtender ID="txtEndDt_CalExtender" runat="server" Enabled="True" TargetControlID="txtEndDt">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="row">
                <asp:Button ID="btnChart" runat="server" CssClass="btn" Text="Generate" onclick="btnChart_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlResults" runat="server" Visible="false">
    <h2>Chart</h2>
    <table><tr>
    <td style="width:70%; vertical-align:top">
    <asp:Chart ID="Chart1" runat="server" BackColor="LightGray" Width="600px" >
        <Series>
            <asp:Series Name="Series1" ChartType="Line" XValueMember="START_DT"  XValueType="DateTime" MarkerSize="3" MarkerStyle="Circle" YValueMembers="RESULT_MSR">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    </td>
    <td style="width:30%; vertical-align:top">
    <asp:Panel ID="Panel1" CssClass="fltBox" runat="server" DefaultButton="btnChart" style="min-width:220px"> 
        <div class="fltTitle">Chart Display Options</div>
        <div class="fltMain">
            <div class="row">
                    <span class="fldLbl" style="width:100px" >Line Type:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlLineType" runat="server" 
                        AutoPostBack="True" onselectedindexchanged="ddlLineType_SelectedIndexChanged" >
                        <asp:ListItem Text="Curved Line" Value="SLINE"></asp:ListItem>
                        <asp:ListItem Text="Line" Value="LINE"></asp:ListItem>
                        <asp:ListItem Text="Points" Value="POINT"></asp:ListItem>
                    </asp:DropDownList>
            </div>
        </div>
        <br /><br />
    </asp:Panel>
    </td>
    </tr></table>
    <h2>Data</h2>
    <asp:GridView ID="grdResults" runat="server" GridLines="None" CssClass="grd"  PagerStyle-CssClass="pgr" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" >
        <Columns>
            <asp:BoundField DataField="MONLOC_NAME" HeaderText="Monitoring Location" SortExpression="MONLOC_NAME"  />
            <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME"  />
            <asp:BoundField DataField="START_DT" HeaderText="Date" DataFormatString="{0:d}" SortExpression="START_DT"  />
            <asp:BoundField DataField="RESULT_MSR" HeaderText="Result" SortExpression="RESULT_MSR"  />
            <asp:BoundField DataField="DETECTION_LIMIT" HeaderText="Detection Limit" SortExpression="DETECTION_LIMIT"  />
            <asp:BoundField DataField="RESULT_MSR_UNIT" HeaderText="Unit" SortExpression="RESULT_MSR_UNIT"  />
        </Columns>
    </asp:GridView>
    <div class="btnRibbon" style="height:24px">
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" Style="float: right;" ImageUrl="~/App_Images/ico_xls.png" onclick="btnExcel_Click"  />
    </div>
    </asp:Panel>
</asp:Content>
