<%@ Page Title="Open Waters - Map" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Maps.aspx.cs" Inherits="OpenEnvironment.Map2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server" >
    <script type="text/javascript" src="../../Scripts/jquery-1.10.2.min.js" ></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCEohIbvKCfnAfGgT0omUpaifCQdBLLAz0"></script>
    <script type="text/javascript">
        var map;
        var gmarkers = [];
        var bounds = new google.maps.LatLngBounds();
        var splits = [];

        window.onload = function InitializeMap() {

            //**************OPTIONS********************
            var options =
            {
                zoom: 5,
                center: new google.maps.LatLng(35.515, -95.962),
                mapTypeId: google.maps.MapTypeId.TERRAIN,
                mapTypeControl: true,
                fullscreenControl: true,
                mapTypeControlOptions:
                {
                    style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
                    position: google.maps.ControlPosition.TOP_RIGHT
                }
            };


            //**************INITIALIZE MAP and INFO WINDOW********************
            map = new google.maps.Map(document.getElementById("map"), options);
            infowindow = new google.maps.InfoWindow({ content: '...' });


            //**************GRAB MARKER DATA FROM DATABASE AND PLOT***************
            PageMethods.GetSites(OnGetSitesComplete);
        }

        function OnGetSitesComplete(result, userContext, methodName) {

            for (var i = 0; i < result.length; i++) {
                splits = result[i].split("|");
                createMarker(splits[0], splits[1], splits[2], splits[3]);
            }

            map.fitBounds(bounds);
        }


        function createMarker(lat, lng, infoTitle, infoBody) {

            var myLatLng = new google.maps.LatLng(lat, lng);
            var marker = new google.maps.Marker({
                position: myLatLng,                
                map: map
            });

            //update bounds
            bounds.extend(myLatLng);

            //**********ADD INFO WINDOW TO MARKER*****************************
            if (marker != null) {
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.setContent('<div class=fltbox><div class="mapInfoTitle">' + infoTitle + '</div><div class=mapInfoMain>' + infoBody + '</div></div>');
                    infowindow.open(map, marker);
                });

                gmarkers.push(marker);
            }
        }
    </script>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" AsyncPostBackTimeout="99999999" EnablePageMethods="true" />

    <table style="width:100%; height:100%; padding:0; margin:0;">
        <tr>
            <td>
                <div id="over_map">Water Quality Map</div>
                <div id ="map" class="mapSmall" />
            </td>
        </tr>    
        <tr>
            <td>
                <h2>Most Recent Results</h2>
                <asp:GridView ID="grdMonLoc" runat="server" AutoGenerateColumns="False" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                        DataSourceID="dsMonLoc" onrowcommand="grdMonLoc_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="View">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="DataButton" runat="server" CausesValidation="False" CommandName="Data" CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl="~/App_Images/ico_info.png" ToolTip="View All Results" />
                                <asp:ImageButton ID="ChartButton" runat="server" CausesValidation="False" CommandName="Chart" CommandArgument='<% #Eval("MONLOC_IDX") %>' ImageUrl="~/App_Images/ico_chart.png" ToolTip="Chart" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MONLOC_NAME" HeaderText="Location" SortExpression="MONLOC_NAME" />
                        <asp:BoundField DataField="ACT_START_DT" HeaderText="Last Sampled" DataFormatString="{0:d}" SortExpression="ACT_START_DT" />
                        <asp:BoundField DataField="Alkalinity__total" HeaderText="Alkalinity (mg/l)" SortExpression="Alkalinity__total" />
                        <asp:BoundField DataField="Ammonia" HeaderText="Ammonia (mg/l)" SortExpression="Ammonia" />
                        <asp:BoundField DataField="Escherichia_coli" HeaderText="E coli" SortExpression="Escherichia_coli" />
                        <asp:BoundField DataField="Nitrate" HeaderText="Nitrate (mg/l)" SortExpression="Nitrate" />
                        <asp:BoundField DataField="Nitrite" HeaderText="Nitrite (mg/l)" SortExpression="Nitrite" />
                        <asp:BoundField DataField="pH" HeaderText="pH" SortExpression="pH" />
                        <asp:BoundField DataField="Phosphorus" HeaderText="Phosphorus (mg/l)" SortExpression="Phosphorus" />
                        <asp:BoundField DataField="Salinity" HeaderText="Salinity (ppt)" SortExpression="Salinity" />
                        <asp:BoundField DataField="Specific_Conductance" HeaderText="Conductance (mS/cm)" SortExpression="Specific_Conductance" />
                        <asp:BoundField DataField="Temperature__air" HeaderText="Air Temp (F)" SortExpression="Temperature__air" />
                        <asp:BoundField DataField="Temperature__water" HeaderText="Water Temp (C)" SortExpression="Temperature__water" />
                        <asp:BoundField DataField="Total_Dissolved_Solids" HeaderText="TDS (g/l)" SortExpression="Total_Dissolved_Solids" />
                        <asp:BoundField DataField="Turbidity" HeaderText="Turbidity" SortExpression="Turbidity" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="dsMonLoc" runat="server" SelectMethod="GetV_WQX_ACTIVITY_LATEST" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
