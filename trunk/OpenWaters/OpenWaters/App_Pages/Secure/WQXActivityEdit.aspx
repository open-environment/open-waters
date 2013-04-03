<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true"
    CodeBehind="WQXActivityEdit.aspx.cs" Inherits="OpenEnvironment.WQXActivityEdit" %>

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
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="onlyUsedInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsAnalMethod" runat="server" SelectMethod="GetT_WQX_REF_ANAL_METHOD"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>
    <h2>Activity</h2>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <asp:Label ID="lblActivityIDX" runat="server" Style="display: none" />
    </p>
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave" style="min-width:800px" >
        <div class="row">
            <span class="fldLbl">Activity ID:</span>
            <asp:TextBox ID="txtActivityID" runat="server" MaxLength="35" Width="200px" CssClass="fldTxt"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtActivityID" ErrorMessage="Required"  
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Monitoring Location:</span>
                <asp:DropDownList ID="ddlMonLoc" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonLoc" ErrorMessage="Required"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Project:</span>
                <asp:DropDownList ID="ddlProject" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProject" ErrorMessage="Required"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <span class="fldLbl">Activity Type:</span>
            <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlActivityType" ErrorMessage="Required"  
            CssClass="failureNotification"></asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Activity Media:</span>
                <asp:DropDownList ID="ddlActMedia" runat="server" CssClass="fldTxt" Width="205px"></asp:DropDownList>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlActMedia" ErrorMessage="Required"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
            <span class="fldLbl">Activity Submedia:</span>
            <asp:DropDownList ID="ddlActSubMedia" runat="server" CssClass="fldTxt" Width="255px"></asp:DropDownList>
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Activity Date/Time:</span>
                <asp:TextBox ID="txtStartDate" runat="server" Width="200px" CssClass="fldTxt" onblur="SyncTextBoxes(this, 'txtEndDate')"  ></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="true" AutoCompleteValue="false"
                    Enabled="True" TargetControlID="txtStartDate" MaskType="DateTime" AcceptAMPM="true" Mask="99/99/9999 99:99">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Required"  
                CssClass="failureNotification"></asp:RequiredFieldValidator>
            </div>
            <span class="fldLbl">Activity End Date:</span>
            <asp:TextBox ID="txtEndDate" runat="server" Width="240px" CssClass="fldTxt"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"  AutoCompleteValue="false"
                    Enabled="True" TargetControlID="txtEndDate" MaskType="DateTime" AcceptAMPM="true" Mask="99/99/9999 99:99">
                </ajaxToolkit:MaskedEditExtender>
        </div>
        <div class="row">
            <span class="fldLbl">Activity Comments:</span>
            <asp:TextBox ID="txtActComments" TextMode="MultiLine" Rows="2" runat="server" 
                Width="660px" CssClass="fldTxt"></asp:TextBox>            
        </div>
        <div class="row">
            <div style="width:380px; float:left"> 
                <span class="fldLbl">Active?</span>
                <asp:CheckBox ID="chkActInd" runat="server" CssClass="fldTxt" Checked="True" />
            </div>
            <span class="fldLbl">Send to EPA</span>
            <asp:CheckBox ID="chkWQXInd" runat="server" CssClass="fldTxt" Checked="True" />
        </div>
        <br />
        <div class="btnRibbon">
            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" CausesValidation="false" />
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>Results</h2>
            <asp:Label ID="lblMsgDtl" runat="server" CssClass="failureNotification"></asp:Label>
            <asp:GridView ID="grdResults" runat="server" GridLines="None" CssClass="grd" PagerStyle-CssClass="pgr"
                AutoGenerateColumns="False" AlternatingRowStyle-CssClass="alt" DataKeyNames="RESULT_IDX, CHAR_NAME, RESULT_MSR_UNIT"
                OnRowCommand="grdResults_RowCommand" OnRowCancelingEdit="grdResults_RowCancelingEdit"
                OnRowEditing="grdResults_RowEditing" OnRowUpdating="grdResults_RowUpdating" OnRowDeleting="grdResults_RowDeleting"
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
                            <asp:TextBox ID="txtResultVal" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("RESULT_MSR") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewResultVal" CssClass="grdCtrl" Width="98%" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblResult" runat="server" Text='<%# Bind("RESULT_MSR") %>'></asp:Label>
                        </ItemTemplate>
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
                    <asp:TemplateField HeaderText="Analytical Method" SortExpression="ANALYTIC_METHOD_IDX">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAnalMethod" CssClass="grdCtrl" Width="98%" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAnalMethod" runat="server" Text='<%# Eval("ANALYTIC_METHOD_IDX") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlNewAnalMethod" CssClass="grdCtrl" Width="98%" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detection Limit" SortExpression="DETECTION_LIMIT">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDetectLimit" CssClass="grdCtrl" Width="98%" runat="server" Text='<%# Bind("DETECTION_LIMIT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDetectLimit" runat="server" Text='<%# Eval("DETECTION_LIMIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNewDetectLimit" CssClass="grdCtrl" Width="98%" runat="server"></asp:TextBox>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlResultBtn" runat="server" CssClass="btnRibbon">
        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="Add Result" OnClick="btnAdd_Click" />
        <asp:ImageButton ID="btnExcel" runat="server" Height="24px" Style="float: right;" ImageUrl="~/App_Images/ico_xls.png" OnClick="btnExcel_Click" />
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
