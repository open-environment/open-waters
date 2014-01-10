<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="Maps.aspx.cs" Inherits="OpenEnvironment.Map2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server" >
    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        var map;
        var gmarkers = [];
        var splits = [];

        window.onload = function InitializeMap() {

            //**************OPTIONS********************
            var options =
            {
                zoom: 9,
                center: new google.maps.LatLng(35.515, -95.962),
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: true,
                mapTypeControlOptions:
                {
                    style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
                    poistion: google.maps.ControlPosition.TOP_RIGHT,
                    mapTypeIds: [google.maps.MapTypeId.ROADMAP,
                      google.maps.MapTypeId.TERRAIN,
                      google.maps.MapTypeId.HYBRID,
                      google.maps.MapTypeId.SATELLITE]
                },
                navigationControl: true,
                navigationControlOptions:
                {
                    style: google.maps.NavigationControlStyle.ZOOM_PAN
                },
                scaleControl: true,
                disableDoubleClickZoom: false,
                draggable: true,
                streetViewControl: true,
                draggableCursor: 'move'
            };



            //**************MAP********************
            map = new google.maps.Map(document.getElementById("map"), options);


            //*********INITIALIZE INFO WINDOW******
            infowindow = new google.maps.InfoWindow({
                content: '...'
            });


            //**************GRAB MARKER DATA FROM DATABASE AND PLOT***************
            PageMethods.GetSites(OnGetSitesComplete);

        }

        function OnGetSitesComplete(result, userContext, methodName) {

            for (var i = 0; i < result.length; i++) {
                splits = result[i].split("|");
                createMarker(splits[0], splits[1], splits[2], splits[3]);
            }
        }



        function createMarker(lat, lng, infoTitle, infoBody) {

            var myLatLng = new google.maps.LatLng(lat, lng);
            var marker;

            marker = new google.maps.Marker({
                position: myLatLng,                
                map: map,
                optimized: true,
                name: lat,
                //icon: greenimage,
                zIndex: 2
            });


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
    <script  type="text/javascript">
        jQuery(document).ready(function () {

            jQuery('#hideshow').live('click', function (event) {
                if (jQuery('#hideshow').val() == "Full Screen") {
                    jQuery('#hideshow').val('Exit Full Screen');
                    jQuery('#map').removeClass('mapSmall');
                    jQuery('#map').addClass('mapBig');
                    jQuery('#over_map_right').css({ top: '10px' });
                    jQuery('#over_map').css({ top: '10px' });
                    jQuery('#over_map').css({ left: '80px' });
                }
                else {
                    jQuery('#hideshow').val('Full Screen');
                    jQuery('#map').removeClass('mapBig');
                    jQuery('#map').addClass('mapSmall');
                    jQuery('#over_map_right ').css({ top: '130px' });
                    jQuery('#over_map').css({ top: '130px' });
                    jQuery('#over_map').css({ left: '280px' });
                }

                google.maps.event.trigger(map, 'resize');
            });
        });    </script>
    <style type="text/css"> 
        #over_map { position: absolute; background-color: #666666; padding:5px; top: 130px; left: 280px; font-weight:bold; color:White; z-index: 99; border-color:#333333; border-width:1px; border-style:solid; border-radius: 5px 5px / 5px 5px; font-size: 11pt; box-shadow: 3px 3px 3px #888888;  opacity: 0.6; filter: alpha(opacity=60); }
        #over_map_right { position: absolute; background-color: transparent; top: 130px; right: 120px; z-index: 99; }
    </style>
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" AsyncPostBackTimeout="99999999" EnablePageMethods="true" />

    <table style="width:100%; height:100%; padding:0; margin:0;">
        <tr>
            <td>
                <div id="over_map">Muscogee Creek Nation - Water Quality Map</div>
                <div id="over_map_right">
                    <input type='button' id='hideshow' value='Full Screen' class="btn" />
                </div>
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
