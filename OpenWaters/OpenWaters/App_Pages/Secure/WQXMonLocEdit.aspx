<%@ Page Title="Open Waters - Mon Loc Details" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXMonLocEdit.aspx.cs" Inherits="OpenEnvironment.WQXMonLocEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCEohIbvKCfnAfGgT0omUpaifCQdBLLAz0"></script>
    <script type="text/javascript">
        var map;
        var marker;

        $(document).ready(function ($) {

            countyDDL();

            //**************MAP OPTIONS********************
            var options =
            {
                zoom: 9,
                mapTypeId: google.maps.MapTypeId.TERRAIN                
            };

            //**************INITIALIZE MAP ********************
            map = new google.maps.Map(document.getElementById("map"), options);

            //event: user clicks on map to place marker
            google.maps.event.addListener(map, 'click', function (event) {
                placeMarker(event.latLng);
            });


            //event: close modal and return lat/long to main form 
            $('.modalClose').click(function () {
                $('#ctl00_ctl00_MainContent_BodyContent_txtLatitude').val($('#ctl00_ctl00_MainContent_BodyContent_txtLatModal').val());
                $('#ctl00_ctl00_MainContent_BodyContent_txtLongitude').val($('#ctl00_ctl00_MainContent_BodyContent_txtLongModal').val());
            });


            //event: clicking button to display modal
            $('#ctl00_ctl00_MainContent_BodyContent_btnMapShow').click('shown.bs.modal', function () {
                google.maps.event.trigger(map, "resize");

                //place previously saved marker on the map
                var iLat = $('#ctl00_ctl00_MainContent_BodyContent_txtLatitude').val();
                var iLong = $('#ctl00_ctl00_MainContent_BodyContent_txtLongitude').val();

                if (iLat.length > 0 && iLong.length > 0) {
                    var latLng = new google.maps.LatLng(iLat, iLong);
                    placeMarker(latLng);
                    map.setCenter(latLng);
                }
                else {
                    //***************ATTEMPT TO CENTER MAP ON USERS LOCATION **********************
                    if (navigator.geolocation) {
                        navigator.geolocation.getCurrentPosition(function (position) {
                            map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
                        });
                    }
                }
            });


            //event: state dropdown change
            //$("#ctl00_ctl00_MainContent_BodyContent_ddlState").on("change", function () {
            //    countyDDL();
            //});

        });

        function countyDDL()
        {
            if ($("#ctl00_ctl00_MainContent_BodyContent_ddlState").val().length == 0) {
                $("#ctl00_ctl00_MainContent_BodyContent_ddlCounty").attr("disabled", true);
            } else {
                $("#ctl00_ctl00_MainContent_BodyContent_ddlCounty").attr("disabled", false);
            }
        }

        function placeMarker(location) {
            //clear any previous markers
            if (marker) { marker.setMap(null) }

            //add marker to map
            marker = new google.maps.Marker({ position: location, map: map });

            //populate modal textboxes
            $('#ctl00_ctl00_MainContent_BodyContent_txtLatModal').val(location.lat().toFixed(6));
            $('#ctl00_ctl00_MainContent_BodyContent_txtLongModal').val(location.lng().toFixed(6));
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="MPE1" runat="server" TargetControlID="btnMapShow" PopupControlID="pnlModal" CancelControlID="btnClose"   
        BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlModTtl">
    </ajaxToolkit:ModalPopupExtender>

    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="MonitoringLocationType" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsCounty" runat="server" SelectMethod="GetT_WQX_REF_COUNTY" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="OK" Name="StateCode" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <h2>
        Edit Monitoring Location
    </h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <asp:Label ID="lblMonLocIDX" runat="server" Style="display:none"/>
    </p>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
        <div class="row">
            <span class="fldLbl">Mon Loc ID:</span>
            <asp:TextBox ID="txtMonLocID" runat="server" Width="250px" CssClass="fldTxt" MaxLength="35" ></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMonLocID" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>

        </div>
        <div class="row">
            <span class="fldLbl">Mon Loc Name:</span>
            <asp:TextBox ID="txtMonLocName" Width="250px" runat="server" CssClass="fldTxt" MaxLength="255"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMonLocName" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Mon Loc Type:</span>
            <asp:DropDownList ID="ddlMonLocType" runat="server" CssClass="fldTxt" ></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonLocType" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Mon Loc Desc:</span>
            <asp:TextBox ID="txtMonLocDesc" runat="server" Width="250px"  CssClass="fldMultiTxt" TextMode="MultiLine" Rows="4" ></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">8-Digit HUC:</span>
            <asp:TextBox ID="txtHUC8" MaxLength="8" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">12-Digit HUC:</span>
            <asp:TextBox ID="txtHUC12" MaxLength="12" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">On Tribal Land?:</span>
            <asp:CheckBox ID="chkLandInd" runat="server" CssClass="fldTxt" />
            <asp:TextBox ID="txtLandName" runat="server" Width="230px" MaxLength="200" CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Latitude:</span>
            <asp:TextBox ID="txtLatitude" runat="server" Width="90px" MaxLength="12"  CssClass="fldTxt"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLatitude" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
            <span class="fldLbl" style="width:80px">&nbsp;&nbsp;&nbsp;Longitude:</span>
            <asp:TextBox ID="txtLongitude" runat="server" Width="90px" MaxLength="14"  CssClass="fldTxt"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLongitude" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
            <asp:ImageButton ID="btnMapShow" runat="server" ImageUrl="~/App_Images/ico_map.png" CausesValidation="false"  />
        </div>
        <div class="row">
            <span class="fldLbl">Source Map Scale:</span>
            <asp:TextBox ID="txtSourceMapScale" MaxLength="12" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Horiz. Collection Method</span>
            <asp:DropDownList ID="ddlHorizMethod" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlHorizMethod" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Horiz. Reference Datum</span>
            <asp:DropDownList ID="ddlHorizDatum" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlHorizDatum" ErrorMessage="Required"   
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <span class="fldLbl">Vertical Measure:</span>
            <asp:TextBox ID="txtVertMeasure" runat="server" Width="90px" MaxLength="12"  CssClass="fldTxt"></asp:TextBox>
            <span class="fldLbl" style="width:60px">&nbsp;&nbsp;&nbsp;Unit:</span>
            <asp:DropDownList ID="ddlVertUnit" runat="server" CssClass="fldTxt"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Vert. Collection Method</span>
            <asp:DropDownList ID="ddlVertMethod" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Vert. Reference Datum</span>
            <asp:DropDownList ID="ddlVertDatum" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">State</span>
            <asp:DropDownList ID="ddlState" runat="server" CssClass="fldTxt" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">County</span>
            <asp:DropDownList ID="ddlCounty" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Country</span>
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Well Type</span>
            <asp:DropDownList ID="ddlWellType" runat="server" CssClass="fldTxt" Width="250px"></asp:DropDownList>
        </div>
        <div class="row">
            <span class="fldLbl">Aquifer</span>
            <asp:TextBox ID="txtAquifer" MaxLength="120" runat="server" Width="250px"  CssClass="fldTxt"></asp:TextBox>
        </div>
        <div class="row">
            <span class="fldLbl">Active?</span>
            <asp:CheckBox ID="chkActInd" runat="server" CssClass="fldTxt" />
        </div>
        <div class="row">
            <span class="fldLbl">Send to EPA</span>
            <asp:CheckBox ID="chkWQXInd" runat="server" CssClass="fldTxt" />
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save &amp; Exit" onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Cancel" onclick="btnCancel_Click" CausesValidation="false" />
        </div>
    </asp:Panel>


    <!-- ******************** MODAL PANEL -->
    <asp:Panel ID="pnlModal" Width="500px" runat="server" CssClass="modalWindow" Style="display: none;" DefaultButton="btnNewSave">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl" runat="server" CssClass="modalTitle" Style="cursor: move">
                Select From Map
            </asp:Panel>
            <div class="row" >
                <div id ="map" class="mapSmall" style="height:350px" />
            </div>
            <div class="row" >
                <div runat="server" id="lblName" class="fldLbl">Latitude:</div>
                <asp:TextBox ID="txtLatModal" runat="server" CssClass="fldTxt" MaxLength="20" Width="300px"></asp:TextBox>
            </div>
            <div class="row" style="margin-bottom:10px">
                <div runat="server" id="lblDesc" class="fldLbl">Longitude:</div>
                <asp:TextBox ID="txtLongModal" runat="server" CssClass="fldTxt" MaxLength="20"  Width="300px" ></asp:TextBox>
            </div>
            <div class="row" ></div>
            <div class="btnRibbon">
                <asp:Button ID="btnNewSave" runat="server" Text="Set" CssClass="btn modalClose"  CausesValidation="false" />
                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" />
            </div>
        </div>
    </asp:Panel>


</asp:Content>
