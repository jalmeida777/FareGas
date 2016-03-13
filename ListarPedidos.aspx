<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarPedidos.aspx.cs" Inherits="ListarPedidos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx1" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

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
                            Ordenes de Servicio</h1>
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
                        ID="CalendarExtender1" 
                        runat="server" 
                        TargetControlID="txtFechaInicial" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td width="85" class="label">
                        <asp:Label ID="Label2" runat="server" Text="Fecha Final:" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="110">
                        <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="inputsFecha" 
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFechaFinal_CalendarExtender" runat="server" 
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
                    <td class="label" width="85">
                        <asp:Label ID="Label5" runat="server" Text="Nro. Orden:"></asp:Label>
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
            <table width="100%"><tr><td width="65">
                <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" ToolTip="Buscar" />
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" ToolTip="Nuevo" />
                </td>
                <td>
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                    <asp:Label ID="lblFechaInicial" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblFechaFinal" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblEmpresa" runat="server" Visible="False"></asp:Label>
                </td>
                </tr></table>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <img src="images/loading.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical" 
                Width="100%">


                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
                    DataSourceID="SqlDataSource1" EnableCallbackCompression="False" 
                    EnableCallBacks="False" EnableRowsCache="False" EnableTheming="False" 
                    EnableViewState="False" KeyFieldName="n_IdPedido" 
                    onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" Width="100%">
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="{0:C}" FieldName="f_Total" 
                            ShowInColumn="Total" ShowInGroupFooterColumn="Total" SummaryType="Sum" />
                    </TotalSummary>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="N° Orden" VisibleIndex="0" Width="80px" 
                            FieldName="v_NumeroPedido" ShowInCustomizationForm="True">
                            <DataItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" 
                                    ForeColor="Blue" Text='<%# Bind("v_NumeroPedido") %>'></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha" FieldName="d_FechaEmision" 
                            VisibleIndex="1" Width="180px" ShowInCustomizationForm="True">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Propietario" FieldName="Propietario" 
                            VisibleIndex="3" Width="200px" ShowInCustomizationForm="True">
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
                        <dx:GridViewDataTextColumn Caption="Total" FieldName="f_Total" VisibleIndex="13" 
                            Width="80px" ShowInCustomizationForm="True">
                            <PropertiesTextEdit DisplayFormatString="{0:C}"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sub Total" FieldName="f_SubTotal" 
                            ShowInCustomizationForm="True" VisibleIndex="11" Width="80px">
                            <propertiestextedit displayformatstring="{0:C}"></propertiestextedit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="IGV" FieldName="f_Impuesto" 
                            ShowInCustomizationForm="True" VisibleIndex="12" Width="80px">
                            <propertiestextedit displayformatstring="{0:C}"></propertiestextedit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Estado" FieldName="Estado" VisibleIndex="14" 
                            Width="50px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="N° Comprobante" 
                            FieldName="v_NumeroComprobante" VisibleIndex="7" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="N° Certificado" 
                            FieldName="v_NumeroCertificado" VisibleIndex="8" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="N° Hoja" FieldName="v_NumeroHoja" 
                            VisibleIndex="9" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Observaciones" FieldName="t_Obs" 
                            VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tipo Comprobante" 
                            FieldName="v_TipoDocumento" VisibleIndex="6" Width="100px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AutoFilterRowInputDelay="0" />
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
                                    <Settings ShowFooter="True" showhorizontalscrollbar="True" />
                                    <SettingsCookies StoreFiltering="False" />
                                    <SettingsDetail ShowDetailRow="True" />
                                    <Settings ShowFooter="True" ShowHorizontalScrollBar="True" />
                                    <SettingsCookies StoreFiltering="False" />
                                    <settingsdetail showdetailbuttons="False" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
                    SelectCommand="Play_Pedido_Listar" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblFechaInicial" Name="FechaInicio" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblFechaFinal" Name="FechaFin" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblEmpresa" Name="v_RazonSocial" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtPropietario" Name="v_Propietario" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtOrdenServicio" Name="v_NumeroPedido" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtPlaca" Name="v_NroPlaca" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                    </SelectParameters>
                </asp:SqlDataSource>


                                <br />


            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnConsultar" />
            <asp:AsyncPostBackTrigger ControlID="ASPxGridView1" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

