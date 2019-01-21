<%@ Page Title="Open Waters - Graphs" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Charting.aspx.cs" Inherits="OpenEnvironment.Charting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/moment.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Chart.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../Scripts/datatables/datatables.min.css">
    <script src="../../Scripts/datatables/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ctx = document.getElementById("myChart");
            var myChart;
            var chartData = {
                datasets: [{
                    lineTension: 0,
                    backgroundColor: "rgba(255, 99, 132, 0.2)",
                    borderColor: "rgba(255, 99, 132, 1)",
                    borderWidth: 1,
                    showLine: ($('#ddlChartStyle').val() != 'point') //false
                }]
            };

            var config = {
                data: chartData,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    tooltips: {
                        enabled: false
                    },
                    scales: {
                        xAxes: [{ }],
                        yAxes: [{
                            ticks: { }
                        }]
                    }
                }
            };

            //initialize datatable
            $('#tblData').DataTable({
                pageLength: 50,
                dom: 'Bfrtip',
                buttons: ['copy', 'excel', 'pdf']
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
                var _charName2 = $("#ctl00_ctl00_MainContent_BodyContent_ddlCharacteristic2").val();
                var _begDt = $("#ctl00_ctl00_MainContent_BodyContent_txtStartDt").val();
                var _endDt = $("#ctl00_ctl00_MainContent_BodyContent_txtEndDt").val();
                var _decimal = $("#ctl00_ctl00_MainContent_BodyContent_txtDecimals").val();
                var _wqxInd = $("#ctl00_ctl00_MainContent_BodyContent_ddlDataInclude").val();
                var _monLoc = [];
                if (_chartType == "MLOC") {
                    $("#ctl00_ctl00_MainContent_BodyContent_lbMonLocSel option").each(function () {
                        _monLoc.push($(this).val().toString());
                    });
                }
                else 
                    _monLoc.push($("#ctl00_ctl00_MainContent_BodyContent_ddlMonLoc").val());

                //validation
                if (_charName == "")
                {
                    $("#ctl00_ctl00_MainContent_BodyContent_lblMsg").text("Characteristic is required.");
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "Charting.aspx/getChartData",
                    contentType: "application/json; charset=utf-8",
                    data: "{'chartType' : '" + _chartType + "', 'charName' : '" + _charName + "', 'charName2' : '" + _charName2 + "', 'begDt': '" + _begDt + "', 'endDt': '" + _endDt + "', 'monLoc': '" + _monLoc + "', 'decimals': '" + _decimal + "', 'wqxInd':'" + _wqxInd + "'}",
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

                    //populate datatable
                    var table = $('#tblData').DataTable();
                    table.clear().draw();
                    $.each(aRawData, function (i, item) {
                        table.row.add([item.MONLOC_ID, item.CHAR_NAME, moment(item.ACT_START_DT).format('YYYY-MM-DD'), (_decimal.length > 0 ? String(item.RESULT_MSR.toFixed(_decimal)) : String(item.RESULT_MSR)), item.DETECTION_LIMIT, item.RESULT_MSR_UNIT]).draw();
                    });

                    //set chart data section
                    config.type = (_chartType === "MLOC" ? "bar" : "line");
                    config.options.scales.xAxes[0].type = (_chartType === 'MLOC' ? 'category' : 'time');
                    config.options.scales.yAxes[0].ticks.beginAtZero = $('#chkZero').is(":checked");
                    var unitTxt = '';
                    if (aData[2][0])
                        unitTxt = aData[2][0].RESULT_MSR_UNIT;

                    chartData.datasets[0].label = _charName + (_chartType === "MLOC" ? '' : ' (' + unitTxt + ')');
                    chartData.datasets[0].showLine = ($('#ddlChartStyle').val() != 'point');

                    if (_chartType == "MLOC") {
                        $("#ddlChartStyle").val('bar');
                        chartData.labels = aLabels;
                        chartData.datasets[0].data = aDatasets1;
                    }
                    else {
                        chartData.datasets[0].data = [];
                        $.each(aRawData, function (i, item) {
                            chartData.datasets[0].data.push({ x: item.ACT_START_DT, y: (_decimal.length > 0 ? item.RESULT_MSR.toFixed(_decimal) : item.RESULT_MSR)  });
                        });

                        //only used when adding 2nd characteristic
                        if (_charName2.length > 0) {
                            var myNewDataset = {
                                label: aData[3][0].CHAR_NAME + (_chartType === "MLOC" ? '' : ' (' + aData[3][0].RESULT_MSR_UNIT + ')'),  // "My Second dataset",
                                lineTension: 0,
                                borderWidth: 1,
                                fillColor: "rgba(187,205,151,0.2)",
                                strokeColor: "rgba(187,205,151,1)",
                                highlightFill: "rgba(187,205,151,0.2)",
                                highlightStroke: "rgba(187,205,151,1)"
                            }

                            if (!chartData.datasets[1]) {
                                chartData.datasets.push(myNewDataset);
                            }

                            chartData.datasets[1].data = [];
                            $.each(aData[3], function (i, item) {
                                chartData.datasets[1].data.push({ x: item.ACT_START_DT, y: (_decimal.length > 0 ? item.RESULT_MSR.toFixed(_decimal) : item.RESULT_MSR) });
                            });
                        }
                        else {
                            if (chartData.datasets[1]) {
                                chartData.datasets.splice(1,1);
                            }
                        }
                    }

                    config.data = chartData;

                    //draw chart
                    myChart = new Chart(ctx, config);
                }
                function OnErrorCall_(repo) {
                    $("#ctl00_ctl00_MainContent_BodyContent_lblMsg").text("Unable to display chart.");
                }
            });


            //click filter panel header ********************************
            $('#fltTitle').click(function () {
                $(this).next().slideToggle('fast');
            });


            //click zero axis checkbox ********************************
            $('#chkZero').change(function () {
                myChart.options.scales.yAxes[0].ticks.beginAtZero = $(this).is(":checked");
                myChart.update();
            });


            //change char name dropdown ********************************
            $('#ctl00_ctl00_MainContent_BodyContent_ddlCharacteristic').on('change', function () {
                if ($(this).val().length > 0)
                    $('#pnlChar2').show();
                else
                    $('#pnlChar2').hide();
            });


            //change chart style dropdown ********************************
            $('#ddlChartStyle').on('change', function () {
                //destroy chart to avoid caching issues
                if (myChart != null)
                    myChart.destroy();

                chartData.datasets[0].showLine = ($('#ddlChartStyle').val() != 'point');
                config.type = (this.value == 'point' ? 'line' : this.value);
                myChart = new Chart(ctx, config);
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
            <div id="pnlChar2" class="row" style="display:none;">
                <div style="width:520px; float:left"> 
                    <span class="fldLbl" >Second Characteristic:</span>
                    <asp:DropDownList CssClass="fldTxt" ID="ddlCharacteristic2" runat="server" style="max-width:330px;" ></asp:DropDownList>
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
                    <asp:TextBox ID="txtDecimals" runat="server" CssClass="fldTxt" MaxLength="1" style="width:30px"></asp:TextBox>
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
                    <span class="fldLbl" style="width:20px; margin:0 0 0 6px;">to</span>
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
<%--                                <input type="checkbox" id="chkLabels" name="chkLabels" value="false" checked="checked"> Show Labels<br>--%>
                                <br />
                                <select id="ddlChartStyle" class="fldTxt">
                                    <option value="line" selected="selected">Line Chart</option>
                                    <option value="point">Scatter Chart</option>
                                    <option value="bar">Bar Chart</option>
                                </select>
                            </div>
                            <br />
                        </div>
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
