<%@ Page Title="Open Waters - Graphs" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Charting.aspx.cs" Inherits="OpenEnvironment.Charting" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
<%--<script src="http://cdnjs.cloudflare.com/ajax/libs/moment.js/2.13.0/moment.min.js"></script>--%>
    <script src="../../Scripts/Chart.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../Scripts/datatables.min.css">
    <script src="../../Scripts/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var myChart;

            //initialize datatable
            $('#tblData').DataTable({
                dom: 'Bfrtip',
                buttons: [
                      'copy', 'excel', 'pdf'
                ]
            });

            $("#btnCreateChart").on('click', function () {

                //reset any error messages
                $("#ctl00_ctl00_MainContent_BodyContent_lblMsg").text("");

                //destroy chart to avoid caching issues
                if (myChart != null)
                    myChart.destroy();

                //get chart parameters
                var _chartType = $("#ctl00_ctl00_MainContent_BodyContent_ddlChartType").val();
                var _charName = $("#ctl00_ctl00_MainContent_BodyContent_ddlCharacteristic").val();
                var _begDt = $("#ctl00_ctl00_MainContent_BodyContent_txtStartDt").val();
                var _endDt = $("#ctl00_ctl00_MainContent_BodyContent_txtEndDt").val();
                var _decimal = $("#ctl00_ctl00_MainContent_BodyContent_txtDecimals").val();
                var _monLoc = [];
                $("#ctl00_ctl00_MainContent_BodyContent_lbMonLocSel option").each(function () {
                    _monLoc.push($(this).val().toString());
                });

                //validation
                if (_charName == "" && _chartType == "MLOC")
                {
                    $("#ctl00_ctl00_MainContent_BodyContent_lblMsg").text("Characteristic is required.");
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "Charting.aspx/getChartData",
                    contentType: "application/json; charset=utf-8",
                    data: "{'chartType' : '" + _chartType + "', 'charName' : '" + _charName + "', 'begDt': '" + _begDt + "', 'endDt': '" + _endDt + "', 'monLoc': '" + _monLoc + "', 'decimals': '" + _decimal + "'}",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(reponse) {
                    $('#ctl00_ctl00_MainContent_BodyContent_pnlResults').show();
                    $('#fltMain').slideToggle('fast');

                    var aData = reponse.d;
                    var aLabels = aData[0];
                    var aDatasets1 = aData[1];
                    var aRawData = aData[2];

                    //(re)populate table
                    var table = $('#tblData').DataTable();
                    table.clear().draw();
                    $.each(aRawData, function (i, item) {
                        table.row.add( [item.MONLOC_ID, item.CHAR_NAME, item.ACT_START_DT, item.RESULT_MSR, item.DETECTION_LIMIT, item.RESULT_MSR_UNIT] ).draw();
                    });

                    var ctx = document.getElementById("myChart");

                    var config = {
                        type: 'line',
                        data: {
                            labels: ['1/1/2012', '2/2/2012', '4/4/2012'],
                            datasets: [{
                                label: 'test',
                                data: [1, 3, 4],
                            }]
                        },
                        options: {
                            scales: {
                                xAxes: [{
                                    type: 'time'
                                }]
                            }
                        }
                    };

                    myChart = new Chart(ctx, config);

//                    myChart = new Chart(ctx, {
//                        type: (_chartType === "MLOC" ? "bar" : "line"),
//                        type: 'line',
//                        data: {
//                            labels: (_chartType === 'MLOC' ? aLabels : null), // ['d', 'e', 'f'], //aLabels
//                            labels: ['1/1/2012', '2/2/2012', '4/4/2012'],
//                            datasets: [{
//                                label: 'dd',
//                                label: _charName,
//                                data: (_chartType === 'MLOC' ? aDatasets1 : [{ x: -10, y: 0 }, { x: 0, y: 10 }, { x: 10, y: 5 }]),//aDatasets1,
//                                data: [1, 3, 4]//,
//                                data: [{ x: '1', y: 0 }, { x: '2', y: 10 }, { x: '3', y: 5 }],//aDatasets1,
                                //backgroundColor: [
                                //    'rgba(255, 99, 132, 0.5)',
                                //    'rgba(54, 162, 235, 0.5)',
                                //    'rgba(255, 206, 86, 0.5)',
                                //    'rgba(75, 192, 192, 0.5)',
                                //    'rgba(153, 102, 255, 0.5)',
                                //    'rgba(255, 159, 64, 0.5)',
                                //    'rgba(255, 99, 132, 0.5)',
                                //    'rgba(54, 162, 235, 0.5)'
                                //],
                                //borderColor: [
                                //    'rgba(255,99,132,1)',
                                //    'rgba(54, 162, 235, 1)',
                                //    'rgba(255, 206, 86, 1)',
                                //    'rgba(75, 192, 192, 1)',
                                //    'rgba(153, 102, 255, 1)',
                                //    'rgba(255, 159, 64, 1)',
                                //    'rgba(255,99,132,1)',
                                //    'rgba(54, 162, 235, 1)'
                                //],
                                //borderWidth: 1
//                            }]
//                        },
//                        options: {
//                            responsive: true,
//                            maintainAspectRatio: false,
//                            tooltips: {
//                                enabled: false
//                            },
//                            scales: {
//                                xAxes: [{
//                                    type: 'time'
//                                    type: (_chartType === 'MLOC' ? 'category' : 'time')//,
//                                    position: 'bottom'
//                                }]//,
//                                yAxes: [{
//                                    ticks: {
//                                        beginAtZero: false
//                                    }
 //                               }]
//                            }//,
                            //animation: {
                            //    onComplete: function () {
                            //        var ctx = this.chart.ctx;
                            //        ctx.textAlign = "center";
                            //        ctx.textBaseline = "bottom";
                            //        var chart = this;
                            //        var datasets = this.config.data.datasets;

                            //        datasets.forEach(function (dataset, i) {
                            //                    ctx.fillStyle = "Black";
                            //                    chart.getDatasetMeta(i).data.forEach(function (p, j) {
                            //                        ctx.fillText(datasets[i].data[j], p._model.x, p._model.y + 20);
                            //                    });
                            //        })
                            //    }
                            //}
//                        }
//                    });
                    console.log('4');

                }
                function OnErrorCall_(repo) {
                    $("#ctl00_ctl00_MainContent_BodyContent_lblMsg").text("Unable to display chart.");
                }
            });

            $('#fltTitle').click(function () {
                $(this).next().slideToggle('fast');
            });

            $('#chkZero').change(function () {
                myChart.options.scales.yAxes[0].ticks.beginAtZero = $(this).is(":checked");
                myChart.update();
            });

        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <asp:ObjectDataSource ID="dsMonLoc" runat="server" SelectMethod="GetWQX_MONLOC" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_RESULT_SampledCharacteristics"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX" >
        <selectparameters>
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
    <asp:Panel ID="pnlFilter" cssClass="fltBox" runat="server" >
        <div class="fltTitle" id="fltTitle">Select Chart Options</div>
        <div class="fltMain" id="fltMain">
            <div class="row">
                <div style="width:420px; float:left"> 
                    <span class="fldLbl">Chart Type:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlChartType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChartType_SelectedIndexChanged">
                        <asp:ListItem Text="Time Series" Value="SERIES"></asp:ListItem>
                        <asp:ListItem Text="Monitoring Location Averages" Value="MLOC"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div style="width:520px; float:left"> 
                    <span class="fldLbl" >Characteristic:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlCharacteristic" runat="server" style="max-width:330px;" ></asp:DropDownList>
                </div>
                <div style="float:left"> 
                    <span class="fldLbl" >Data to Include:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlDataInclude" runat="server"  >
                        <asp:ListItem Value="A" Text="Include all my data" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="W" Text="Only include data shared with EPA"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div style="width:520px; float:left"> 
                    <span class="fldLbl" >Monitoring Location:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlMonLoc" runat="server"  ></asp:DropDownList>
                    <asp:Panel ID="pnlMonLoc" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>
                                    Available<br />
                                    <asp:ListBox ID="lbMonLoc" runat="server" Height="150px" Width="150px" CssClass="fldTxt"></asp:ListBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" CssClass="btn" Width="180px" runat="server" Text="Add to Chart &gt;&gt;" OnClick="btnAdd_Click" /><br />
                                    <asp:Button ID="btnRemove" CssClass="btn" Width="180px" runat="server" Text="&lt;&lt; Remove from Chart" OnClick="btnRemove_Click" />
                                </td>
                                <td>
                                    Selected<br />
                                    <asp:ListBox ID="lbMonLocSel" runat="server" Height="150px" Width="150px" CssClass="fldTxt">
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>                    
                </div>
                <div style="float:left"> 
                    <span class="fldLbl" >Number of Decimals:</span>
                    <asp:TextBox ID="txtDecimals" runat="server" CssClass="fldTxt"></asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div style="width:520px; float:left"> 
                    <span class="fldLbl">Date Range:</span>
                    <asp:TextBox ID="txtStartDt" runat="server" Width="80px" CssClass="fldTxt"></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="txtStartDt_MaskedEditExtender" runat="server" Enabled="True" TargetControlID="txtStartDt" MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="txtStartDt_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtStartDt">
                    </ajaxToolkit:CalendarExtender>
                    <span class="fldLbl" style="width:20px; margin:0 4px 0 4px;">to</span>
                    <asp:TextBox ID="txtEndDt" runat="server" Width="80px" CssClass="fldTxt" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="txtEndDt_MaskedEditExtender" runat="server" Enabled="True" TargetControlID="txtEndDt" MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="txtEndDt_CalExtender" runat="server" Enabled="True" TargetControlID="txtEndDt">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="row">
                <input id="btnCreateChart" type="button" value="Show" class="btn" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlResults" runat="server" style="display:none" >
        <h2>Chart</h2>
        <table style="width:100%">
            <tr>
                <td style="width:70%; vertical-align:top">
                    <div style="max-width:800px; max-height:800px;min-height:500px">
                        <canvas id="myChart" ></canvas>
                    </div>
                </td>
                <td style="width:30%; vertical-align:top">
                    <asp:Panel ID="Panel1" CssClass="fltBox" runat="server" style="min-width:220px"> 
                        <div class="fltTitle">Chart Display Options</div>
                        <div class="fltMain">
                            <div class="row">
                                 <input type="checkbox" id="chkZero" name="chkZero" value="zero"> Include zero in y-axis<br>
<%--                                <span class="fldLbl" style="width:100px" >Line Type:</span>
                                <asp:DropDownList CssClass="fldTxt" ID="ddlLineType" runat="server" AutoPostBack="True"  >
                                    <asp:ListItem Text="Curved Line" Value="SLINE"></asp:ListItem>
                                    <asp:ListItem Text="Line" Value="LINE"></asp:ListItem>
                                    <asp:ListItem Text="Points" Value="POINT"></asp:ListItem>
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <h2>Data</h2>
        <table id="tblData" class="grd compact" >
            <thead>
                <tr>
                    <th>Monitoring Location</th>
                    <th>Characteristic</th>
                    <th>Date</th>
                    <th>Result</th>
                    <th>Detection Limit</th>
                    <th>Unit</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    </asp:Panel>
</asp:Content>
