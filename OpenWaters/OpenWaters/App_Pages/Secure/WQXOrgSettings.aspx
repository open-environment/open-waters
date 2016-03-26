<%@ Page Title="" Language="C#" MasterPageFile="~/MasterWQX.master" AutoEventWireup="true" CodeBehind="WQXOrgSettings.aspx.cs" Inherits="OpenEnvironment.WQXOrgSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <link href="../../Scripts/chosen.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function GetConfirmation() {
            var reply = confirm("WARNING: This will remove the characteristic from this organization - are you sure you want to continue?");
            if (reply) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <asp:ObjectDataSource ID="dsChar" runat="server" SelectMethod="GetT_WQX_REF_CHARACTERISTIC" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="false" Name="onlyUsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsTimeZone" runat="server" SelectMethod="GetT_WQX_REF_DEFAULT_TIME_ZONE" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsTaxa" runat="server" SelectMethod="GetT_WQX_REF_DATA" TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref">
        <SelectParameters>
            <asp:Parameter DefaultValue="Taxon" Name="tABLE" Type="String" />
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
            <asp:Parameter DefaultValue="true" Name="UsedInd" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsAnalMethod" runat="server" SelectMethod="GetT_WQX_REF_ANAL_METHOD"  TypeName="OpenEnvironment.App_Logic.DataAccessLayer.db_Ref" >
        <selectparameters>
            <asp:Parameter DefaultValue="true" Name="ActInd" Type="Boolean" />
        </selectparameters>
    </asp:ObjectDataSource>



    <h1>
        Edit Organization Settings
    </h1>
    <p>
        <asp:Label ID="lblMsg" runat="server" CssClass="failureNotification"></asp:Label>
        <div class="fldPass">Before entering activities please setup some default settings for your organization. This includes the default timezone (used to determine the correct timezone for activities), 
            and a list of the characteristics and taxa that your organization samples for (used to populate the characteristic drop-down on the Activity Edit page).
        </div>
    </p>
    <div class="row">
        <span class="fldLbl">Organization ID:</span>
        <asp:TextBox ID="txtOrgID" runat="server" MaxLength="30" Width="250px" CssClass="fldTxt"></asp:TextBox>
    </div>
    <div class="row">
        <span class="fldLbl">Default Time Zone:</span>
        <asp:DropDownList ID="ddlTimeZone" runat="server" Width="258px" CssClass="fldTxt"></asp:DropDownList>
    </div>
    <div class="row">
        <asp:RadioButtonList ID="rbType" runat="server" AutoPostBack="true" CssClass="fldPass" OnSelectedIndexChanged="rbType_SelectedIndexChanged" RepeatDirection="Horizontal"
            style="  border-radius: 5px; padding: 10px; background-color: lightgray; font-size: 16px; margin-top: 20px;" >
            <asp:ListItem Text="Edit Characteristics List" Value="C"></asp:ListItem>
            <asp:ListItem Text="Edit Taxa List" Value="T"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="row">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script>
                jQuery(document).ready(function () {
                    jQuery(".chosen").data("placeholder", "Being typing or select from list...").chosen({ allow_single_deselect: true });
                });
            </script>
            <table width="100%">
                <tr style="vertical-align:top;">
                    <td style="width:50%">
                        <asp:Panel ID="pnlChars" runat="server" CssClass="row" Visible="false">
                            <h2>Characteristics Used</h2>
                            <asp:DropDownList ID="ddlChar" runat="server" CssClass="chosen"></asp:DropDownList>
                            <asp:Button ID="btnAddChar" runat="server" CssClass="btn" Text="Add Characteristic" onclick="btnAddChar_Click"/>

                            <asp:GridView ID="grdChar" runat="server" CssClass="grd" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="False"
                                AutoGenerateColumns="False" DataKeyNames="CHAR_NAME" onrowcommand="grdChar_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemTemplate>
                                        <asp:ImageButton ID="ImaSelectButton" runat="server" CausesValidation="False" CommandName="Select" 
                                            CommandArgument='<% #Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/selectbutton.png" ToolTip="Select" />
                                            <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                                                CommandArgument='<%# Eval("CHAR_NAME") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" OnClientClick="return GetConfirmation();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CHAR_NAME" HeaderText="Characteristic" SortExpression="CHAR_NAME" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_UNIT" HeaderText="Unit" SortExpression="DEFAULT_UNIT" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_DETECT_LIMIT" HeaderText="Detect Limit" SortExpression="DEFAULT_DETECT_LIMIT" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_LOWER_QUANT_LIMIT" HeaderText="Detect Limit" SortExpression="DEFAULT_LOWER_QUANT_LIMIT" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_UPPER_QUANT_LIMIT" HeaderText="Detect Limit" SortExpression="DEFAULT_UPPER_QUANT_LIMIT" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="T_WQX_REF_ANAL_METHOD.ANALYTIC_METHOD_ID" HeaderText="Analysis Method"  ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_SAMP_FRACTION" HeaderText="Sample Fraction" SortExpression="DEFAULT_SAMP_FRACTION" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_RESULT_STATUS" HeaderText="Status" SortExpression="DEFAULT_RESULT_STATUS" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="DEFAULT_RESULT_VALUE_TYPE" HeaderText="Value Type" SortExpression="DEFAULT_RESULT_VALUE_TYPE" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="CREATE_DT" HeaderText="Create Date" SortExpression="CREATE_DT" DataFormatString = "{0:d}"  />
                                    <asp:BoundField DataField="CREATE_USERID" HeaderText="Created By" SortExpression="CREATE_USERID" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlTaxa" runat="server" CssClass="row"  Visible="false">
                            <h2>Taxa Used
                            </h2>
                            <asp:DropDownList ID="ddlTaxa" runat="server" CssClass="chosen"></asp:DropDownList>
                            <asp:Button ID="btnAddTaxa" runat="server" CssClass="btn" Text="Add Taxa" onclick="btnAddTaxa_Click"/>

                            <asp:GridView ID="grdTaxa" runat="server" CssClass="grd" AlternatingRowStyle-CssClass="alt" AllowPaging="False" AutoGenerateColumns="False" 
                                DataKeyNames="BIO_SUBJECT_TAXONOMY" OnRowCommand="grdTaxa_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="DelButton" runat="server" CausesValidation="False" CommandName="Deletes"
                                                CommandArgument='<%# Eval("BIO_SUBJECT_TAXONOMY") %>' ImageUrl="~/App_Images/ico_del.png" ToolTip="Delete" OnClientClick="return GetConfirmation();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BIO_SUBJECT_TAXONOMY" HeaderText="Taxa Name" SortExpression="BIO_SUBJECT_TAXONOMY" ControlStyle-Width="98%" />
                                    <asp:BoundField DataField="CREATE_DT" HeaderText="Added Date" SortExpression="CREATE_DT" />
                                    <asp:BoundField DataField="CREATE_USERID" HeaderText="Added By" SortExpression="CREATE_USERID" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                    </td>
                    <td style="width:50%">
                        <br /><br />
                        <asp:Panel ID="pnlSelChar" runat="server" Visible="false" style="padding-top: 80px">
                            <h2>Characteristic Default Values</h2>
                            <div class="row" > 
                                <span class="fldLbl">Characteristic:</span>
                                <asp:TextBox ID="txtSelChar" runat="server" CssClass="fldTxt" Enabled="false" Width="220px"></asp:TextBox>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Default Unit:</span>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="fldTxt"  Width="220px"></asp:DropDownList>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Detection Limit:</span>
                                <asp:TextBox ID="txtDetectLimit" runat="server" CssClass="fldTxt"  Width="220px" MaxLength="12" ></asp:TextBox>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Lower Quant Limit:</span>
                                <asp:TextBox ID="txtQuantLower" runat="server" CssClass="fldTxt"  Width="220px" MaxLength="12" ></asp:TextBox>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Upper Quant Limit:</span>
                                <asp:TextBox ID="txtQuantUpper" runat="server" CssClass="fldTxt"  Width="220px" MaxLength="12" ></asp:TextBox>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Analysis Method:</span>
                                <asp:DropDownList ID="ddlAnalMethod" runat="server" CssClass="fldTxt"  Width="220px"></asp:DropDownList>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Sample Fraction:</span>
                                <asp:DropDownList ID="ddlFraction" runat="server" CssClass="fldTxt"  Width="220px"></asp:DropDownList>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Result Status:</span>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="fldTxt"  Width="220px"></asp:DropDownList>
                            </div>
                            <div class="row" > 
                                <span class="fldLbl">Result Value Type:</span>
                                <asp:DropDownList ID="ddlValueType" runat="server" CssClass="fldTxt" Width="220px" ></asp:DropDownList>
                            </div>
                            <div class="row" > 
                                <asp:Label ID="lblMsgDtl" runat="server" CssClass="failureNotification"></asp:Label>
                            </div>
                            <div class="btnRibbon">
                                <asp:Button ID="btnSaveDtl" runat="server" CssClass="btn" OnClick="btnSaveDtl_Click" Text="Save" />
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate></asp:UpdatePanel>
    </div>

    <div class="btnRibbon">
        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" onclick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="Exit" onclick="btnCancel_Click" />
    </div>

</asp:Content>
