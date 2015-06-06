<%@ Page Title="Open Waters - Activity Details" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXActivityEdit.aspx.cs" Inherits="OpenEnvironment.WQXActivityEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will delete the result - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function SyncTextBoxes(txt, txt2) {
            var t = $(txt).val();
            var z = $("input[id$=" + txt2 + "]").val();
            if (z == "") {
                $("input[id$=" + txt2 + "]").val(t);
            }
        } 
    </script>
    <script type="text/javascript">
        //handles css for tab clicking
        $(document).ready(function () {
            $(".tabs-menu a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("current");
                $(this).parent().siblings().removeClass("current");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <asp:ObjectDataSource ID="dsRefData" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="Characteristic" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsMonLoc" runat="server" SelectMethod="GetWQX_MONLOC"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsProject" runat="server" SelectMethod="GetWQX_PROJECT"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_WQX" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="WQXPending" Type="Boolean" />
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC_ByOrg"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
            <asp:Parameter DefaultValue="false" Name="RBPInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsTaxa" runat="server" SelectMethod="GetT_WQX_REF_TAXA_ByOrg"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsAnalMethod" runat="server" SelectMethod="GetT_WQX_REF_ANAL_METHOD"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsSampColl" runat="server" SelectMethod="GetT_WQX_REF_SAMP_COL_METHOD_ByContext"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:SessionParameter DefaultValue="" Name="Context" SessionField="OrgID" Type="String" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsLabName" runat="server" SelectMethod="GetT_WQX_REF_LAB" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="OrgID" SessionField="OrgID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsPrepMethod" runat="server" SelectMethod="GetT_WQX_REF_SAMP_PREP_ByContext" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="Context" SessionField="OrgID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <h2>Activity</h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <asp:HiddenField ID="hdnActivityIDX" runat="server" />
    </p>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave" style="min-width:800px" >
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl" style="width:130px">Activity ID:</span>
                <asp:TextBox ID="txtActivityID" runat="server" MaxLength="35" Width="230px" CssClass="fldTxt"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtActivityID" ErrorMessage="Required" Display="Dynamic"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Activity Date/Time:</span>
                <asp:TextBox ID="txtStartDate" runat="server" Width="130px" CssClass="fldTxt" onblur="SyncTextBoxes(this, 'txtEndDate')"  ></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="true" AutoCompleteValue="false"
                    Enabled="True" TargetControlID="txtStartDate" MaskType="DateTime" AcceptAMPM="true" Mask="99/99/9999 99:99">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Required"   
                CssClass="failureNotification"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtTimeZone" runat="server" Width="60px" CssClass="fldTxt" Visible="false"></asp:TextBox>
            </div>
            <div style="width:380px; float:left"> 
                <div style="width:380px; float:left"> 
                    <asp:CheckBox ID="chkActInd" runat="server" CssClass="fldTxt" Checked="True" Text="Active?" />
                </div>
            </div>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl" style="width:130px"">Monitoring Location:</span>
                <asp:DropDownList ID="ddlMonLoc" runat="server" CssClass="fldTxt" Width="230px"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonLoc" ErrorMessage="Required"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Project:</span>
                <asp:DropDownList ID="ddlProject" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProject" ErrorMessage="Required"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
            <div style="width:380px; float:left"> 
                <asp:CheckBox ID="chkWQXInd" runat="server" CssClass="fldTxt" Checked="True" Text="Send to EPA" />
            </div>
        </div>

        <div id="tabs-container">
            <ul class="tabs-menu">
                <li class="current"><a href="#tab-1">General Info</a></li>
                <li><a href="#tab-2">Biological Info</a></li>
            </ul>
            <div class="tab" style="clear: both;">
                <div id="tab-1" class="tab-content">

                    <div class="row">
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Activity Type:</span>
                            <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="fldTxt" Width="205px" ></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlActivityType" ErrorMessage="Required"  
                                CssClass="failureNotification"></asp:RequiredFieldValidator>
                        </div>
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Activity End Date:</span>
                            <asp:TextBox ID="txtEndDate" runat="server" Width="130px" CssClass="fldTxt"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"  AutoCompleteValue="false"
                                Enabled="True" TargetControlID="txtEndDate" MaskType="DateTime" AcceptAMPM="true" Mask="99/99/9999 99:99">
                            </ajaxToolkit:MaskedEditExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Activity Media:</span>
                            <asp:DropDownList ID="ddlActMedia" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlActMedia" ErrorMessage="Required"  
                            CssClass="failureNotification"></asp:RequiredFieldValidator>
                        </div>
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Activity Depth:</span>
                            <asp:TextBox ID="txtDepth" runat="server" Width="130px" CssClass="fldTxt" MaxLength="12"></asp:TextBox>
                            <asp:DropDownList ID="ddlDepthUnit" runat="server" CssClass="fldTxt" Width="45px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="m" Text="m"></asp:ListItem>
                                <asp:ListItem Value="ft" Text="ft"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Activity Submedia:</span>
                            <asp:DropDownList ID="ddlActSubMedia" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                        </div> 
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Collection Equip:</span>
                            <asp:DropDownList ID="ddlEquip" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                        </div> 
                    </div>
                    <div class="row">
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Collection Method:</span>
                            <asp:DropDownList ID="ddlSampColl" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                        </div> 
                    </div>
                    <div class="row" style="padding-bottom:20px;">
                        <span class="fldLbl">Activity Comments:</span>
                        <asp:TextBox ID="txtActComments" TextMode="MultiLine" Rows="2" runat="server" Width="600px" CssClass="fldTxt"></asp:TextBox>            
                    </div>
                </div>
                <div id="tab-2" class="tab-content">
                    <div class="row">
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Assemblage Sampled:</span>
                            <asp:DropDownList ID="ddlAssemblage" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                        </div>
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Collection Duration:</span>
                            <asp:TextBox ID="txtBioDuration" runat="server" Width="130px" CssClass="fldTxt" MaxLength="12"></asp:TextBox>
                            <asp:DropDownList ID="ddlBioDurUnit" runat="server" CssClass="fldTxt" Width="45px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="seconds" Text="seconds"></asp:ListItem>
                                <asp:ListItem Value="minutes" Text="minutes"></asp:ListItem>
                                <asp:ListItem Value="hours" Text="hours"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Sampling Component:</span>
                            <asp:TextBox ID="txtSamplingComponent" runat="server" CssClass="fldTxt" Width="205px" MaxLength="15"></asp:TextBox>
                        </div>
                        <div style="width:380px; float:left"> 
                            <span class="fldLbl">Components Sequence #:</span>
                            <asp:TextBox ID="txtSampComponentSeq" runat="server" Width="130px" CssClass="fldTxt" MaxLength="2"></asp:TextBox>
                        </div>
                    </div>

                </div>
            </div>

        </div>


        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" Style="float:left;" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" CausesValidation="false" Style="float:left;" />
            <div style="margin-left:20px; float:left;" >Result Type:</div>
            <asp:DropDownList ID="ddlEntryType" runat="server" CssClass="fldTxt" OnSelectedIndexChanged="ddlEntryType_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="C" Text="Chemical (default)"></asp:ListItem>
                <asp:ListItem Value="H" Text="Habitat Assessment"></asp:ListItem>
                <asp:ListItem Value="T" Text="Taxonomy Counts"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblModifyDate" runat="server" CssClass="smallnote" style="margin-left:40px;"></asp:Label>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                <script type="text/javascript">
                    //handles display of more result information div
                    function displayMore()
                    {
                        document.getElementById("pnlModal").style.display = 'block';
                        document.getElementById("pnlModalBackground").style.display = 'block';
                    }

                    function hideMore() {
                        document.getElementById("pnlModal").style.display = 'none';
                        document.getElementById("pnlModalBackground").style.display = 'none';
                    }
                </script>


            <div><h2 style="display:inline;">Results</h2> (click row to edit)</div> 
            <asp:Label ID="lblMsgDtl" runat="server" CssClass="failureNotification"></asp:Label>
            <asp:GridView ID="grdResults" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" 
                DataKeyNames="RESULT_IDX, CHAR_NAME, RESULT_MSR_UNIT, ANALYTIC_METHOD_IDX, RESULT_SAMP_FRACTION, RESULT_VALUE_TYPE, RESULT_STATUS, BIO_INTENT_NAME, FREQ_CLASS_CODE"
                OnRowCommand="grdResults_RowCommand" OnRowEditing="grdResults_RowEditing" OnRowUpdating="grdResults_RowUpdating" OnRowDeleting="grdResults_RowDeleting" 
                ShowFooter="true" OnRowDataBound="grdResults_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RESULT_IDX" HeaderText="Name" SortExpression="RESULT_IDX" Visible="false" />
                    <asp:TemplateField HeaderText="Characteristic" SortExpression="CHAR_NAME">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlChar" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblChar" runat="server" Text='<%# Eval("CHAR_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewChar" AutoPostBack="true" OnSelectedIndexChanged="ddlNewChar_SelectedIndexChanged" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Taxonomy" SortExpression="BIO_SUBJECT_TAXONOMY">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTaxa" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTaxa" runat="server" Text='<%# Eval("BIO_SUBJECT_TAXONOMY") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewTaxa" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Result">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtResultVal" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("RESULT_MSR") %>' MaxLength="12"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblResultDetectCond" runat="server" Text='<%# Bind("RESULT_DETECT_CONDITION") %>'></asp:Label>
                            <asp:Label ID="lblResult" runat="server" Text='<%# Bind("RESULT_MSR") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewResultVal" CssClass="grdCtrl" Width="98%" runat="server" MaxLength="12"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit" SortExpression="RESULT_MSR_UNIT">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUnit" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("RESULT_MSR_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewUnit" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detection Limit" SortExpression="DETECTION_LIMIT">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDetectLimit" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("DETECTION_LIMIT") %>' MaxLength="12"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDetectLimit" runat="server" Text='<%# Eval("DETECTION_LIMIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewDetectLimit" CssClass="grdCtrl" Width="98%" runat="server" MaxLength="12"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Analytical Method" SortExpression="ANALYTIC_METHOD_IDX">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAnalMethod" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                             <asp:Literal ID="hlProgram" runat="server" Text='<%# Bind("T_WQX_REF_ANAL_METHOD.ANALYTIC_METHOD_CTX") %>' />
                             <asp:Literal ID="Literal1" runat="server" Text='<%# Bind("T_WQX_REF_ANAL_METHOD.ANALYTIC_METHOD_ID") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewAnalMethod" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Samp Fraction" SortExpression="RESULT_SAMP_FRACTION">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlSampFraction" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSampFraction2" runat="server" Text='<%# Eval("RESULT_SAMP_FRACTION") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewSampFraction" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value Type" SortExpression="RESULT_VALUE_TYPE">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlResultValueType" CssClass="grdCtrl" Width="98%" runat="server"  />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblResultValueType" runat="server" Text='<%# Eval("RESULT_VALUE_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewResultValueType" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" SortExpression="RESULT_STATUS">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlResultStatus" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSampFraction" runat="server" Text='<%# Eval("RESULT_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewResultStatus" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Lab Analysis Date" SortExpression="LAB_ANALYSIS_START_DT">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAnalysisDate" CssClass="grdCtrl" Width="98%" runat="server"  Text='<%# Bind("LAB_ANALYSIS_START_DT", "{0:d}") %>' ></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAnalysisDate" runat="server" Text='<%# Bind("LAB_ANALYSIS_START_DT", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewAnalysisDate" CssClass="grdCtrl" Width="98%" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PQL" SortExpression="PQL">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPQL" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("PQL") %>' MaxLength="12"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPQL" runat="server" Text='<%# Eval("PQL") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewPQL" CssClass="grdCtrl" Width="98%" runat="server" MaxLength="12"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lower Quant Limit" SortExpression="LOWER_QUANT_LIMIT">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLowerQuantLimit" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("LOWER_QUANT_LIMIT") %>' MaxLength="12"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLowerQuantLimit" runat="server" Text='<%# Eval("LOWER_QUANT_LIMIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewLowerQuantLimit" CssClass="grdCtrl" Width="98%" runat="server" MaxLength="12"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upper Quant Limit" SortExpression="UPPER_QUANT_LIMIT">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUpperQuantLimit" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("UPPER_QUANT_LIMIT") %>' MaxLength="12"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUpperQuantLimit" runat="server" Text='<%# Eval("UPPER_QUANT_LIMIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewUpperQuantLimit" CssClass="grdCtrl" Width="98%" runat="server" MaxLength="12"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>                                        
                    <asp:TemplateField HeaderText="Biological Intent" SortExpression="BIO_INTENT_NAME">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlBioIntent" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBioIntent" runat="server" Text='<%# Eval("BIO_INTENT_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewBioIntent" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Frequency Class" SortExpression="FREQ_CLASS_CODE">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlFreqClass" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFreqClass" runat="server" Text='<%# Eval("FREQ_CLASS_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewFreqClass" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Comment" SortExpression="RESULT_COMMENT">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtComment" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("RESULT_COMMENT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblComment" runat="server" Text='<%# Eval("RESULT_COMMENT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewComment" CssClass="grdCtrl" Width="98%" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="More">
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <img id="imgMore1" src="../../App_Images/ico_plus.png" alt="more" class="moreButton" onclick="displayMore();"  />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="true" CommandName="Update" ImageUrl="~/App_Images/ico_save2.png" />
                            <asp:LinkButton Visible="false" ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" OnClientClick="return GetConfirmation();" CommandName="Delete" ImageUrl="~/App_Images/ico_del.png" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="false" CommandName="AddNew" ImageUrl="~/App_Images/ico_save2.png" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

    <!-- MODAL PANEL FOR MORE INFORMATION -->
    <div id="pnlModal" class="modalWindow" Style=" width: 500px; position: absolute; z-index: 100001; left: 588px; top: 130.5px; display:none">
        <div style="padding: 6px; background-color: #FFF; border: 1px solid #98B9DB;">
            <asp:Panel ID="pnlModTtl" runat="server" CssClass="modalTitle" >
                Additional Result Information
            </asp:Panel>
            <div class="row">
                <div style="width:380px; float:left"> 
                    <span class="fldLbl">Laboratory:</span>
                    <asp:DropDownList ID="ddlLabName" runat="server" CssClass="fldTxt" Width="230px"></asp:DropDownList>
                </div>
                <div style="width:380px; float:left"> 
                    <span class="fldLbl">Prep Method</span>
                    <asp:DropDownList ID="ddlPrepMethod" runat="server" CssClass="fldTxt" Width="230px"></asp:DropDownList>
                </div>
                <div style="width:380px; float:left"> 
                    <span class="fldLbl">Prep Start Date</span>
                    <asp:TextBox ID="txtPrepStartDate" runat="server" CssClass="fldTxt" Width="230px" ></asp:TextBox>
                </div>
                <div style="width:380px; float:left"> 
                    <span class="fldLbl">Dilution Factor</span>
                    <asp:TextBox ID="txtDilution" runat="server" CssClass="fldTxt" Width="230px" ></asp:TextBox>
                </div>
            </div>
            <div class="btnRibbon">
                <asp:Button ID="btnCloseMore" runat="server" Text="Close" CssClass="btn" UseSubmitBehavior="false" CausesValidation="false" OnClientClick="hideMore(); return false;" />
            </div>
        </div>
    </div>
    <div id="pnlModalBackground" class="modalBackground" style="position: fixed; left: 0px; top: 0px; z-index: 10000; width: 100%; height: 625px; display:none"></div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:Panel ID="pnlResultBtn" runat="server" CssClass="btnRibbon">
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" ImageUrl="~/App_Images/ico_xls.png" OnClick="btnExcel_Click" ToolTip="Export to Excel" />
    </asp:Panel>
    <asp:UpdatePanel ID="pnlMetrics" runat="server" Visible="false">
        <ContentTemplate>
            <h2>Activity Metrics</h2>
            <asp:GridView ID="grdMetrics" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr" AutoGenerateEditButton="true"  
            AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" DataKeyNames="ACTIVITY_METRIC_IDX" DataSourceID="dsMetric" >
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:BoundField DataField="ACTIVITY_METRIC_IDX" HeaderText="ACTIVITY_METRIC_IDX" SortExpression="ACTIVITY_METRIC_IDX" Visible="true" ReadOnly="True" />
                    <asp:BoundField DataField="METRIC_TYPE_ID" HeaderText="METRIC_TYPE_ID" SortExpression="METRIC_TYPE_ID" />
                    <asp:BoundField DataField="METRIC_TYPE_ID_CONTEXT" HeaderText="METRIC_TYPE_ID_CONTEXT" SortExpression="METRIC_TYPE_ID_CONTEXT" />
                    <asp:BoundField DataField="METRIC_TYPE_NAME" HeaderText="METRIC_TYPE_NAME" SortExpression="METRIC_TYPE_NAME" />
                    <asp:BoundField DataField="METRIC_SCORE" HeaderText="Metric Score" SortExpression="METRIC_SCORE" />
                </Columns>
                <PagerStyle CssClass="pgr" />
            </asp:GridView>
            <asp:EntityDataSource ID="dsMetric" runat="server" ConnectionString="name=OpenEnvironmentEntities" EnableUpdate="True" 
                DefaultContainerName="OpenEnvironmentEntities" EnableFlattening="False" EntitySetName="T_WQX_ACTIVITY_METRIC" Where="it.ACTIVITY_IDX=@ActID">
                <WhereParameters>
                    <asp:SessionParameter Name="ActID" DbType="Int32" SessionField="ActivityIDX" />
                </WhereParameters>
            </asp:EntityDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
