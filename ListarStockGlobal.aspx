<%@ Page Title="Stock Global" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarStockGlobal.aspx.cs" Inherits="ListarStockGlobal" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <style type="text/css">
        .InfoTable td
        {
            padding: 0 4px;
            vertical-align: top;
        }
    </style>
    <script type="text/javascript">
        var keyValue;
        function OnMoreInfoClick(element, key) {
            callbackPanel.SetContentHtml("");
            popup.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




<div class="divBusqueda">
    <table width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <h1 class="label">
                    Stock Global</h1>
            </td>
        </tr>
        </table>
    </div>

        <div class="toolbar">
            <table width="100%"><tr>
                <td width="65">
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/Imprimir.jpg" onclick="btnImprimir_Click" />
                </td>
                <td width="65">
                  
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                  
                </td>
                <td>
                   
                    <asp:CheckBox ID="chkEditar" runat="server" Visible="False" />
                   
                </td>
                </tr></table>
            </div>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" 
    AutoGenerateColumns="False" 
    CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
    DataSourceID="SqlDataSource1" Width="100%" KeyFieldName="n_IdProducto" 
        EnableRowsCache="False" EnableViewState="False" 
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Código" FieldName="v_CodigoInterno" 
                VisibleIndex="0" Width="50px">
                <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                    AutoFilterCondition="Equals" FilterMode="DisplayText" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Imágen" 
                VisibleIndex="1" Width="65px">
                <Settings AllowAutoFilter="False" AllowSort="False" />
                <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                            <asp:Image ID="Image1" runat="server" Height="60px" 
                                ImageUrl='<%# Bind("v_RutaImagen") %>' style="margin-right: 0px" Width="60px" />
                                </a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Producto" FieldName="Producto" 
                VisibleIndex="2">
                <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                    AutoFilterCondition="Contains" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbProducto" runat="server" Text='<%# Bind("Producto") %>'></asp:LinkButton>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Minka" FieldName="MINKA" VisibleIndex="3" 
                Width="100px">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbMinka" runat="server" Text='<%# Bind("MINKA") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdAlmacenMinka" runat="server" Text="1" Visible="False"></asp:Label>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Bellavista" FieldName="BELLAVISTA" 
                VisibleIndex="4" Width="100px">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbBellavista" runat="server" 
                        Text='<%# Bind("BELLAVISTA") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdAlmacenBellavista" runat="server" Text="2" Visible="False"></asp:Label>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Santa Anita" FieldName="SANTAANITA" 
                VisibleIndex="5" Width="100px">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbSantaAnita" runat="server" 
                        Text='<%# Bind("SANTAANITA") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdAlmacenSantaAnita" runat="server" Text="3" Visible="False"></asp:Label>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Santa Clara" FieldName="SANTACLARA" 
                VisibleIndex="6" Width="100px">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbSantaClara" runat="server" 
                        Text='<%# Bind("SANTACLARA") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdAlmacenSantaClara" runat="server" Text="4" Visible="False"></asp:Label>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="El Agustino" FieldName="ELAGUSTINO" 
                VisibleIndex="7" Width="100px">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbPlayCentral" runat="server" 
                        Text='<%# Bind("ELAGUSTINO") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdAlmacenPlayCentral" runat="server" Text="5" Visible="False"></asp:Label>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Almacén Central" FieldName="ALMACENCENTRAL" 
                VisibleIndex="8" Width="100px">
                <Settings AllowAutoFilter="False" />
                <DataItemTemplate>
                    <asp:LinkButton ID="lbAlmacenCentral" runat="server" 
                        Text='<%# Bind("ALMACENCENTRAL") %>'></asp:LinkButton>
                    <asp:Label ID="lblIdAlmacenCentral" runat="server" Text="6" Visible="False"></asp:Label>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Total" FieldName="TOTAL" VisibleIndex="10" 
                Width="100px">
                <Settings AllowAutoFilter="False" />
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Precio" FieldName="f_Precio" 
                VisibleIndex="9" Width="100px">
                <PropertiesTextEdit DisplayFormatString="C">
                </PropertiesTextEdit>
                <Settings AllowAutoFilter="True" AllowAutoFilterTextInputTimer="False" 
                    AllowDragDrop="True" AllowGroup="False" AllowHeaderFilter="False" 
                    AllowSort="True" AutoFilterCondition="Equals" />
                <HeaderStyle HorizontalAlign="Center" />
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>

        </Columns>
        <SettingsPager PageSize="5" ShowDefaultImages="False">
            <AllButton Text="All">
            </AllButton>
            <NextPageButton Text="Next &gt;">
            </NextPageButton>
            <PrevPageButton Text="&lt; Prev">
            </PrevPageButton>
        </SettingsPager>
        <Settings ShowFilterRow="True" ShowHeaderFilterBlankItems="False" />
        <SettingsCookies StoreFiltering="False" />
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
    SelectCommand="Play_StockGlobal_Listar" SelectCommandType="StoredProcedure">
</asp:SqlDataSource>

        

    

    <dx:aspxpopupcontrol ID="popup" runat="server" AllowDragging="True" 
        ClientInstanceName="popup" 
        CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
        HeaderText="Foto" Height="200px" MaxHeight="400px" MaxWidth="400px" 
        MinHeight="200px" MinWidth="200px" PopupHorizontalAlign="OutsideRight" 
        SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css">
        <CloseButtonStyle>
            <Paddings Padding="0px" />
        </CloseButtonStyle>
        <ContentStyle>
            <BorderBottom BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" />
        </ContentStyle>
        <HeaderStyle>
        <Paddings PaddingBottom="4px" PaddingLeft="10px" PaddingRight="4px" 
            PaddingTop="4px" />
        </HeaderStyle>
        <ContentCollection>
            <dx:popupcontrolcontentcontrol runat="server">
                <dx:aspxcallbackpanel ID="callbackPanel" runat="server" 
                    ClientInstanceName="callbackPanel" Height="200px" 
                    OnCallback="callbackPanel_Callback" RenderMode="Table" Width="200px">
                    <panelcollection>
                        <dx:panelcontent runat="server" SupportsDisabledAttribute="True">
                            <table class="style1">
                                <tr>
                                    <td>
                                        <asp:Image ID="ImagenGrande" runat="server" Height="200px" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" ForeColor="#FF6600"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblProducto" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </dx:panelcontent>
                    </panelcollection>
                </dx:aspxcallbackpanel>
            </dx:popupcontrolcontentcontrol>
        </ContentCollection>
        <LoadingPanelImage Url="~/App_Themes/PlasticBlue/Web/dvLoading.gif">
        </LoadingPanelImage>
        <ClientSideEvents Shown="popup_Shown" />
    </dx:aspxpopupcontrol>

            

    

</asp:Content>

