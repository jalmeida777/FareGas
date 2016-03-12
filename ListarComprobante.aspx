<%@ Page Title="Listar Comprobantes" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarComprobante.aspx.cs" Inherits="ListarComprobante" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="divBusqueda">
                <table width="100%">
                    <tr>
                        <td colspan="7">
                            <h1 class="label">
                                Comprobantes</h1>
                        </td>
                    </tr>
                    <tr>
                        <td width="90" class="label">
                            <asp:Label ID="Label1" runat="server" Text="Fecha Inicial:" Font-Bold="False"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha"
                            MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender 
                        ID="CalendarExtender2" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="140" class="label">
                            <asp:Label ID="Label2" runat="server" Text="Fecha Final:" Font-Bold="False"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                            MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender0" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="90">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Text="Empresa:"></asp:Label>
                        </td>
                        <td class="label" width="280">
                            <asp:DropDownList ID="ddlEmpresa" runat="server" 
                            CssClass="combo" 
                            Width="270px">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                        
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="label" width="90">
                            <asp:Label ID="Label4" runat="server" Text="Nro. Placa:"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtPlaca" runat="server" CssClass="inputNormal" MaxLength="50" 
                            Width="90px"></asp:TextBox>
                        </td>
                        <td class="label" width="140">
                            <asp:Label ID="Label5" runat="server" Text="Nro. Comprobante:"></asp:Label>
                        </td>
                        <td width="110">
                            <asp:TextBox ID="txtOrdenServicio" runat="server" CssClass="inputNormal" 
                            MaxLength="10" Width="90px"></asp:TextBox>
                        </td>
                        <td width="70">
                            <asp:Label ID="Label6" runat="server" Text="Propietario:"></asp:Label>
                        </td>
                        <td class="label" width="210">
                            <asp:TextBox ID="txtPropietario" runat="server" CssClass="inputNormal" 
                            Width="270px"></asp:TextBox>
                        </td>
                        <td class="label">
                        &nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="toolbar">
                <table width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" ToolTip="Buscar" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                            <asp:Label ID="lblFechaInicial" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblFechaFinal" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpresa" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical" 
                Width="100%">
                <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                    DataSourceID="SqlDataSource1" EnableCallbackCompression="False" 
                    EnableCallBacks="False" EnableRowsCache="False" EnableTheming="False" 
                    EnableViewState="False" KeyFieldName="i_IdComprobante" 
                    onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" Width="100%">
                    <SettingsBehavior AutoFilterRowInputDelay="0" />
                    <SettingsBehavior AutoFilterRowInputDelay="0" />
                    <Settings ShowFooter="True" />
                    <SettingsCookies StoreFiltering="False" />
                    <SettingsDetail ShowDetailRow="True" />
                    <Settings ShowFooter="True" />
                    <SettingsCookies StoreFiltering="False" />
                    <SettingsDetail ShowDetailRow="True" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="{0:C}" FieldName="f_Total" 
                            ShowInColumn="Total" ShowInGroupFooterColumn="Total" SummaryType="Sum" />
                    </TotalSummary>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="N° Comprobante" VisibleIndex="0" Width="80px" 
                            FieldName="v_NumeroComprobante" ShowInCustomizationForm="True">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# Bind("v_NumeroComprobante") %>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha" FieldName="d_FechaEmision" 
                            VisibleIndex="1" Width="120px" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cliente" FieldName="Cliente" 
                            VisibleIndex="3" Width="120px" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Forma de Pago" FieldName="v_FormaPago" 
                            VisibleIndex="4" Width="100px" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Placa" FieldName="v_NroPlaca" 
                            VisibleIndex="2" Width="120px" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Creado por" FieldName="Usuario" 
                            VisibleIndex="5" Width="150px" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total" FieldName="f_Total" VisibleIndex="8" 
                            Width="80px" ShowInCustomizationForm="True">
                            <PropertiesTextEdit DisplayFormatString="{0:C}">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sub Total" FieldName="f_SubTotal" 
                            ShowInCustomizationForm="True" VisibleIndex="6" Width="80px">
                            <propertiestextedit displayformatstring="{0:C}">
                            </propertiestextedit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="IGV" FieldName="f_Impuesto" 
                            ShowInCustomizationForm="True" VisibleIndex="7" Width="80px">
                            <propertiestextedit displayformatstring="{0:C}">
                            </propertiestextedit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Estado" FieldName="Estado" VisibleIndex="9" 
                            Width="50px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                <SettingsBehavior AutoFilterRowInputDelay="0" />
                <SettingsBehavior AutoFilterRowInputDelay="0" />
                <SettingsBehavior AutoFilterRowInputDelay="0" />
                    <SettingsBehavior AutoFilterRowInputDelay="0" />
                    <SettingsPager PageSize="5" ShowDefaultImages="False">
                        <AllButton Text="All">
                        </AllButton>
                        <NextPageButton Text="Next &gt;">
                        </NextPageButton>
                        <PrevPageButton Text="&lt; Prev">
                        </PrevPageButton>
                    </SettingsPager>
                <Settings ShowFooter="True" />
                <SettingsCookies StoreFiltering="False" />
                <SettingsDetail ShowDetailRow="True" />
                <Settings ShowFooter="True" />
                <SettingsCookies StoreFiltering="False" />
                <SettingsDetail ShowDetailRow="True" />
                <Settings ShowFooter="True" />
                <SettingsCookies StoreFiltering="False" />
                <SettingsDetail ShowDetailRow="True" />
                    <Settings ShowFooter="True" />
                    <SettingsCookies StoreFiltering="False" />
                    <SettingsDetail ShowDetailRow="True" />
                    <Images SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                        <LoadingPanelOnStatusBar Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
                        </LoadingPanelOnStatusBar>
                        <LoadingPanel Url="~/App_Themes/PlasticBlue/GridView/Loading.gif">
                        </LoadingPanel>
                    </Images>
                    <ImagesFilterControl>
                        <LoadingPanel Url="~/App_Themes/PlasticBlue/Editors/Loading.gif">
                        </LoadingPanel>
                    </ImagesFilterControl>
                    <Styles CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                        CssPostfix="PlasticBlue">
                        <Header ImageSpacing="10px" SortingImageSpacing="10px">
                        </Header>
                    </Styles>
                    <StylesEditors>
                        <CalendarHeader Spacing="11px">
                        </CalendarHeader>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" 
                                CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                                DataSourceID="SqlDataSource2" 
                                onbeforeperformdataselect="gvDetalle_BeforePerformDataSelect" Width="100%">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="i_Cantidad" 
                                        VisibleIndex="0" Width="80px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Precio Unitario S/." 
                                        FieldName="f_PrecioUnitario" VisibleIndex="3" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:C}">
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Precio Total S/." FieldName="f_PrecioTotal" 
                                        VisibleIndex="4" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="{0:C}">
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Producto" FieldName="Producto" 
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager ShowDefaultImages="False" Visible="False">
                                    <AllButton Text="All">
                                    </AllButton>
                                    <NextPageButton Text="Next &gt;">
                                    </NextPageButton>
                                    <PrevPageButton Text="&lt; Prev">
                                    </PrevPageButton>
                                </SettingsPager>
                                <Images SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
                                    <LoadingPanelOnStatusBar Url="~/App_Themes/PlasticBlue/GridView/gvLoadingOnStatusBar.gif">
                                    </LoadingPanelOnStatusBar>
                                    <LoadingPanel Url="~/App_Themes/PlasticBlue/GridView/Loading.gif">
                                    </LoadingPanel>
                                </Images>
                                <ImagesFilterControl>
                                    <LoadingPanel Url="~/App_Themes/PlasticBlue/Editors/Loading.gif">
                                    </LoadingPanel>
                                </ImagesFilterControl>
                                <Styles CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" 
                                    CssPostfix="PlasticBlue">
                                    <Header ImageSpacing="10px" SortingImageSpacing="10px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <CalendarHeader Spacing="11px">
                                    </CalendarHeader>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_Comprobante_Listar" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblFechaInicial" Name="FechaInicio" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblFechaFinal" Name="FechaFin" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblEmpresa" Name="v_RazonSocial" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtPropietario" Name="v_Propietario" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtOrdenServicio" Name="v_NumeroComprobante" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtPlaca" Name="v_NroPlaca" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_DetComprobante_Listar" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="i_IdComprobante" SessionField="i_IdComprobante" 
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnConsultar" />
            <asp:AsyncPostBackTrigger ControlID="ASPxGridView2" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

