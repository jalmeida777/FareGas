<%@ Page Title="Pedido" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearPedido.aspx.cs" Inherits="CrearPedido" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="css/tabs.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery.growl.js" type="text/javascript"></script>
    <link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function OnClienteSeleccionado(source, eventArgs) {

        var hdnValueID = "<%= hdnValue.ClientID %>";
        var hdnPropietarioID = "<%= hdnPropietario.ClientID %>";

        document.getElementById(hdnValueID).value = eventArgs.get_value();
        document.getElementById(hdnPropietarioID).value = eventArgs.get_value();
        __doPostBack(hdnValueID, "");
        __doPostBack(hdnPropietarioID, "");
    }

    function OnMarcaSeleccionado(source, eventArgs) {
        var hdnValueID = "<%= hfMarca.ClientID %>";
        document.getElementById(hdnValueID).value = eventArgs.get_value();
        __doPostBack(hdnValueID, "");
    }

    function OnModeloSeleccionado(source, eventArgs) {
        var hdnValueID = "<%= hfModelo.ClientID %>";
        document.getElementById(hdnValueID).value = eventArgs.get_value();
        __doPostBack(hdnValueID, "");
    }

</script>

<script type="text/javascript">

    function ValidaNumeros(e) {
        var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
        if (tecla > 31 && (tecla < 48 || tecla > 57))
            return false;
    }

    function ValidaDecimal(e) {
        var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
        if (tecla > 31 && (tecla < 48 || tecla > 57) && tecla != 46)
            return false;
    }

    function PesoBruto() {

        var idpsi = "<%= txtPesoSecoInicial.ClientID %>";
        var psi = document.getElementById(idpsi).value;
        if (psi == "") { psi = 0; }

        var idcui = "<%= txtCargaUtilInicial.ClientID %>";
        var cui = document.getElementById(idcui).value;
        if (cui == "") { cui = 0; }

        var resultadoi = parseInt(psi) + parseInt(cui);

        var idpbi = "<%= txtPesoBrutoInicial.ClientID %>";
        document.getElementById(idpbi).value = resultadoi;


        var idpsf = "<%= txtPesoSecoFinal.ClientID %>";
        var psf = document.getElementById(idpsf).value;
        if (psf == "") { psf = 0; }

        var idcuf = "<%= txtCargaUtilFinal.ClientID %>";
        var cuf = document.getElementById(idcuf).value;
        if (cuf == "") { cuf = 0; }

        var resultadof = parseInt(psf) + parseInt(cuf);

        var idpbf = "<%= txtPesoBrutoFinal.ClientID %>";
        document.getElementById(idpbf).value = resultadof;

    }


    $(function () {
        $('#name').bind('paste', function () {
            var self = this;
            setTimeout(function () {
                if (!/^[a-zA-Z\s]+$/.test($(self).val()));
            }, 0);
        });

        $('#txtLongitud').bind('paste', function () {
            var self = this;
            setTimeout(function () {
                if (!/^\d\s*(\.\d{1,2})+$/.test($(self).val()));
            }, 0);
        });

        $('.textboxIngresoDecimal').keypress(function (e) {
            var character = String.fromCharCode(e.keyCode)
            var newValue = this.value + character;
            if (isNaN(newValue) || hasDecimalPlace(newValue, 3)) {
                e.preventDefault();
                return false;
            }
        });


        function hasDecimalPlace(value, x) {
            var pointIndex = value.indexOf('.');
            return pointIndex >= 0 && pointIndex < value.length - x;
        }

    });


 </script>

<style>
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left:10px;
            z-index:99;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
          width: 150px !important;    
        }
        #divwidth div
       {
        width: 150px !important;   
       }
      .style1
      {
          width: 100%;
      }
 body *
{
	text-shadow: none;
}
 </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                <div class="toolbar" id="toolbar" runat ="server">
                    <asp:HiddenField ID="hdnValue" runat="server" />
                    <asp:HiddenField ID="hdnPropietario" runat="server" />
                    <asp:HiddenField ID="hfPedido" runat="server" />
            <table width="100%"><tr><td width="65">
                
                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/images/Guardar.jpg" 
                    
                    OnClientClick="if (confirm('Seguro de guardar?')) { btnGuardar.disabled = false; return true; } else { return false; }" 
                    onclick="btnGuardar_Click" />
                
                </td>
                <td width="65">
                  
                    <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" />
                  
                </td>
                <td width="65">
                   
                    <asp:ImageButton ID="btnAnular" runat="server" ImageUrl="~/images/Anular.jpg" 
                        Visible="False" 
                        
                        OnClientClick="if (confirm('Seguro de anular?')) { btnAnular.disabled = false; return true; } else { return false; }" 
                        onclick="btnAnular_Click" />
                   
                </td>
                <td width="65">
                   
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/Imprimir.jpg" Visible="False" />

                        <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" 
                    ConfirmText="¿Seguro de imprimir?" Enabled="True" 
                    TargetControlID="btnImprimir">
                </cc1:ConfirmButtonExtender>
                   
                </td>
                <td width="65">
                   
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                   
                </td>
                <td>
                   
                    &nbsp;</td>
                <td>
                   
                    &nbsp;</td>
                <td>
                   
                    &nbsp;</td>
                </tr></table>
            </div>

 <table width="100%" runat="server" id="tblGeneral"
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;">
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                <div class="divDocumento">
                
                    
    <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblIdPedido" runat="server" Visible="False"></asp:Label>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Orden de Servicio N° " ForeColor="#4C4C4C"></asp:Label>
                <asp:Label ID="lblNumeroPedido" runat="server" Font-Bold="True" Font-Size="20pt" 
                    ForeColor="#4C4C4C"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4" width="100%">
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                    Width="100%" CssClass="MyTabStyle" Visible="true">
                    
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1" Visible="true">
            <HeaderTemplate>
                Orden de Servicio
                    </HeaderTemplate>
                    
            <ContentTemplate>
                <table cellpadding="5" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;" 
                            width="170">
                            <asp:Label ID="Label6" runat="server" Text="Nro. DNI / RUC Cliente:"></asp:Label>
                            <asp:Label ID="Label188" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td style="padding-left: 5px">

                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tblCliente1" runat="server">
                                <tr runat="server">
                                    <td width="120" runat="server">
                                        <asp:TextBox ID="txtCliente" runat="server" 
                                            CssClass="inputNormal" Width="100px" MaxLength="11" onkeypress="return ValidaNumeros(event);"></asp:TextBox>
                                    </td>
                                    <td runat="server">
                                        <asp:ImageButton ID="btnVerificarCliente" runat="server" 
                                            ImageUrl="~/images/auth_ok.png" ToolTip="Verificar Cliente" 
                                            onclick="btnVerificarCliente_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" id="tblCliente2" runat="server" 
                                visible="False">
                                <tr runat="server">
                                    <td style="padding-right: 5px" runat="server">
                                        <asp:Label ID="lblNombreCliente" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td runat="server">
                                        <asp:ImageButton ID="btnCliente" runat="server" 
                                            ImageUrl="~/images/proveedor.gif" OnClick="btnCliente_Click" 
                                            ToolTip="Ver Cliente" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; ">
                            <asp:Label ID="Label2" runat="server" ForeColor="#4C4C4C" Text="Fecha:"></asp:Label>
                            <asp:Label ID="Label189" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td style="padding-left: 5px">
                            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                                Format="dd/MM/yyyy" TargetControlID="txtFechaInicial">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;">
                            <asp:Label ID="Label23" runat="server" Text="Forma de Pago:"></asp:Label>
                        </td>
                        <td style="padding-left: 5px">
                            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="combo" 
                                Width="220px">
                            </asp:DropDownList>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; ">
                            <asp:Label ID="Label24" runat="server" Text="Moneda:"></asp:Label>
                        </td>
                        <td style="padding-left: 5px">
                            <asp:DropDownList ID="ddlMoneda" runat="server" CssClass="combo" 
                                Enabled="False" Width="100px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;">
                            <asp:Label ID="Label52" runat="server" Text="Empresa:"></asp:Label>
                        </td>
                        <td style="padding-left: 5px">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="275">
                                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="combo" Width="270px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnVerificarEmpresa" runat="server" 
                                            ImageUrl="~/images/auth_ok.png" onclick="btnVerificarEmpresa_Click" 
                                            ToolTip="Elegir Empresa" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; ">
                            <asp:Label ID="Label44" runat="server" Text="Estado:"></asp:Label>
                        </td>
                        <td style="padding-left: 5px">
                            <asp:Label ID="lblEstado" runat="server" Font-Bold="True" ForeColor="#CC3300" 
                                Text="Pendiente"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="panelServicio" runat="server" BorderColor="#0A9E5D" 
                                BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                <table cellpadding="5" width="100%">
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 4px;" 
                                            width="160">
                                            <asp:Label ID="Label149" runat="server" Text="Servicio:"></asp:Label>
                                            <asp:Label ID="Label190" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                ForeColor="#18AC85" Text="*"></asp:Label>
                                        </td>
                                        <td style="padding-left: 5px" width="70">
                                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="inputNormal" 
                                                Width="50px" onkeypress="return ValidaNumeros(event);">1</asp:TextBox>
                                        </td>
                                        <td width="285">
                                            <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" 
                                                CssClass="combo" onselectedindexchanged="ddlProducto_SelectedIndexChanged1" 
                                                Width="280px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="80">
                                            <asp:Label ID="Label150" runat="server" Text="Precio S/.:"></asp:Label>
                                        </td>
                                        <td width="70">
                                            <asp:Label ID="lblPrecio" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnAgregarProducto" runat="server" 
                                                ImageUrl="~/images/Agregar.jpg" onclick="btnAgregarProducto_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                                CssClass="grid" DataKeyNames="n_IdProducto" onrowdeleting="gv_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="i_Cantidad" HeaderText="Cantidad">
                                    <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="v_Descripcion" HeaderText="Producto" />
                                    <asp:BoundField DataField="f_PrecioUnitario" DataFormatString="{0:n2}" 
                                        HeaderText="Precio">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="f_PrecioTotal" DataFormatString="{0:n2}" 
                                        HeaderText="Total">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibQuitar" runat="server" CommandName="delete" 
                                                ImageUrl="~/images/delete.gif" ToolTip="Quitar" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" cellpadding="5" cellspacing="3" width="100%">
                                <tr>
                                    <td align="left" 
                                        style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                                        width="150">
                                        <asp:Label ID="Label151" runat="server" Text="Tipo de Comprobante:"></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 5px">
                                        <asp:RadioButtonList ID="rblTipoComprobante" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="rblTipoComprobante_SelectedIndexChanged" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">Boleta</asp:ListItem>
                                            <asp:ListItem>Factura</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right" width="100">
                                        <asp:Label ID="Label20" runat="server" Text="SubTotal:"></asp:Label>
                                    </td>
                                    <td align="right" width="20">
                                        <asp:Label ID="lblSigno1" runat="server" Text="S/."></asp:Label>
                                    </td>
                                    <td align="right" width="100">
                                        <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" 
                                        
                                        style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                                        width="150">
                                        <asp:Label ID="Label124" runat="server" Text="Nro. Certificado:"></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 5px">
                                        <asp:TextBox ID="txtNumeroCertificado" runat="server" CssClass="inputNormal" 
                                            MaxLength="50" Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="right" width="100">
                                        <asp:Label ID="Label142" runat="server" Text="IGV:("></asp:Label>
                                        <asp:Label ID="lblIgvPorc" runat="server" Text="00"></asp:Label>
                                        <asp:Label ID="Label152" runat="server" Text="%)"></asp:Label>
                                    </td>
                                    <td align="right" width="20">
                                        <asp:Label ID="lblSigno6" runat="server" Text="S/."></asp:Label>
                                    </td>
                                    <td align="right" width="100">
                                        <asp:Label ID="lblIGV" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" 
                                        
                                        style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                                        <asp:Label ID="Label125" runat="server" Text="Nro. Hoja:"></asp:Label>
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:TextBox ID="txtNumeroHoja" runat="server" CssClass="inputNormal" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="12pt" 
                                            Text="Total:"></asp:Label>
                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                                        width="20">
                                        <asp:Label ID="lblSigno3" runat="server" Font-Bold="True" Font-Size="12pt" 
                                            Text="S/."></asp:Label>
                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12pt"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                                        <asp:Label ID="Label143" runat="server" Text="Nro. Hoja Inicial:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumeroHojaInicial" runat="server" CssClass="inputNormal" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                                        <asp:Label ID="Label25" runat="server" Text="Pagó con:"></asp:Label>
                                        <asp:Label ID="Label191" runat="server" Font-Bold="True" Font-Size="10pt" 
                                            ForeColor="#18AC85" Text="*"></asp:Label>
                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                                        width="20">
                                        <asp:Label ID="lblSigno4" runat="server" Text="S/."></asp:Label>
                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999">
                                        <asp:TextBox ID="txtPago" runat="server" CssClass="inputNormalMoneda" 
                                            onkeypress="return ValidaNumeros(event);" Width="60px" AutoPostBack="True" 
                                            ontextchanged="txtPago_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                                        <asp:Label ID="Label144" runat="server" Text="Nro. Hoja Final:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumeroHojaFinal" runat="server" CssClass="inputNormal" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label26" runat="server" Text="Vuelto:"></asp:Label>
                                    </td>
                                    <td align="right" width="20">
                                        <asp:Label ID="lblSigno5" runat="server" Text="S/."></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblVuelto" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtObservacion" runat="server" Height="80px" 
                                placeholder="Comentarios" TextMode="MultiLine" Width="380px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="300">
                                <tr>
                                    <td rowspan="2" width="60">
                                        <asp:ImageButton ID="ibUsuarioRegistro" runat="server" Height="50px" 
                                            ImageUrl="~/images/face.jpg" Width="50px" />
                                    </td>
                                    <td>
                                        Creado por:
                                        <asp:Label ID="lblUsuarioRegistro" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFechaRegistro" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
            </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Visible="true">
            <HeaderTemplate>
                Datos del Vehículo
                    </HeaderTemplate>
                    
            <ContentTemplate>

                
                <table cellpadding="5" cellspacing="3" width="100%">
                    <tr>
                        <td height="5" width="100">
                            &nbsp;</td>
                        <td height="5" width="205">
                            &nbsp;</td>
                        
                        
                        <td height="5" width="200">
                            &nbsp;</td>
                        <td height="5">
                            &nbsp;</td>
                        
                        
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            <asp:Label ID="Label127" runat="server" Text="Placa:"></asp:Label>
                            <asp:Label ID="Label165" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtPlaca" runat="server" CssClass="inputNormal" MaxLength="50"></asp:TextBox>
                        </td>
                        
                        
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label7" runat="server" Text="Año de Fabricación:"></asp:Label>
                            <asp:Label ID="Label155" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtAnioFabricacion" runat="server" CssClass="inputNormal" 
                                MaxLength="4" onkeypress="return ValidaNumeros(event);" Width="50px"></asp:TextBox>
                        </td>
                        
                        
                    </tr>
                    <tr>
                                    
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            <asp:Label ID="Label4" runat="server" Text="Categoría:"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="combo" 
                                Width="200px" AutoPostBack="True" 
                                onselectedindexchanged="ddlCategoria_SelectedIndexChanged">
                            </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label13" runat="server" Text="Carrocería:"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                            <asp:DropDownList ID="ddlCarroceria" runat="server" CssClass="combo" 
                                Width="200px">
                            </asp:DropDownList>
                             </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            <asp:Label ID="Label5" runat="server" Text="Marca:"></asp:Label>
                            <asp:Label ID="Label166" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="210">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                        <asp:TextBox ID="txtMarca" runat="server" CssClass="inputNormal" Width="200px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtMarca_AutoCompleteExtender" runat="server" 
                                            CompletionInterval="100" CompletionListCssClass="AutoExtender" 
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                            CompletionListItemCssClass="AutoExtenderList" DelimiterCharacters="" 
                                            Enabled="True" MinimumPrefixLength="2" 
                                            OnClientItemSelected="OnMarcaSeleccionado" ServiceMethod="BuscarMarcas" 
                                            ServicePath="" ShowOnlyCurrentWordInCompletionListItem="True" 
                                            TargetControlID="txtMarca">
                                        </cc1:AutoCompleteExtender>
                                        <asp:HiddenField ID="hfMarca" runat="server" 
                                OnValueChanged="hfMarca_ValueChanged" />
                                              </ContentTemplate>
                                </asp:UpdatePanel>
                                    </td>
                                    <td width="15">
                                        <asp:ImageButton ID="btnEditarMarca" runat="server" 
                                            ImageUrl="~/images/edit.png" OnClick="btnEditarMarca_Click" 
                                            ToolTip="Editar Marca" Visible="False" CausesValidation="False" 
                                            ClientIDMode="Static" />
                                    </td>
                                    <td align="left" width="20">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                        <asp:ImageButton ID="btnNuevaMarca" runat="server" ImageUrl="~/images/add.png" 
                                            OnClick="btnNuevaMarca_Click" ToolTip="Nueva Marca" 
                                            CausesValidation="False" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnNuevaMarca" />
                                            </Triggers>
                                            </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            
                          
                        </td>
                        
                        
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label12" runat="server" Text="Clase:"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:DropDownList ID="ddlClase" runat="server" CssClass="combo" Width="200px">
                            </asp:DropDownList>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            <asp:Label ID="Label101" runat="server" Text="Modelo:"></asp:Label>
                            <asp:Label ID="Label167" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            

                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="210">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                        <asp:TextBox ID="txtModelo" runat="server" CssClass="inputNormal" Width="200px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtModelo_AutoCompleteExtender" runat="server" 
                                            CompletionInterval="100" CompletionListCssClass="AutoExtender" 
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                            CompletionListItemCssClass="AutoExtenderList" DelimiterCharacters="" 
                                            Enabled="True" OnClientItemSelected="OnModeloSeleccionado" 
                                            ServiceMethod="BuscarModelos" ServicePath="" 
                                            ShowOnlyCurrentWordInCompletionListItem="True" TargetControlID="txtModelo" 
                                            UseContextKey="True">
                                        </cc1:AutoCompleteExtender>
                                        <asp:HiddenField ID="hfModelo" runat="server" 
                                OnValueChanged="hfModelo_ValueChanged" />
                                        </ContentTemplate>
                            </asp:UpdatePanel>
                                    </td>
                                    <td width="15">
                                        <asp:ImageButton ID="btnEditarModelo" runat="server" 
                                            ImageUrl="~/images/edit.png" OnClick="btnEditarModelo_Click" 
                                            ToolTip="Editar Modelo" Visible="False" ClientIDMode="Static" />
                                    </td>
                                    <td align="left" width="20">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                        <asp:ImageButton ID="btnNuevoModelo" runat="server" ImageUrl="~/images/add.png" 
                                            OnClick="btnNuevoModelo_Click" ToolTip="Nuevo Modelo" Visible="False" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnNuevoModelo" />
                                            </Triggers>
</asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            

                                
                        </td>
                        
                        
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label8" runat="server" Text="Combustible Inicial:"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:DropDownList ID="ddlCombustibleInicial" runat="server" CssClass="combo" 
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            
                            <asp:Label ID="Label9" runat="server" Text="N° Serie:"></asp:Label>
                            <asp:Label ID="Label168" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            
                            <asp:TextBox ID="txtNumeroSerie" runat="server" CssClass="inputNormal" 
                                MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                        
                        
                        
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label10" runat="server" Text="N° Motor:"></asp:Label>
                            <asp:Label ID="Label171" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtNumeroMotor" runat="server" CssClass="inputNormal" 
                                MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                        
                        
                        
                    </tr>
                    <tr><td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                        <asp:Label ID="Label11" runat="server" Text="Color:"></asp:Label>
                        <asp:Label ID="Label169" runat="server" Font-Bold="True" Font-Size="10pt" 
                            ForeColor="#18AC85" Text="*"></asp:Label>
                        </td><td 
                            class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtColor" runat="server" CssClass="inputNormal" MaxLength="50" 
                                Width="200px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label128" runat="server" Text="Potencia:"></asp:Label>
                            <asp:Label ID="Label172" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtPotencia" runat="server" CssClass="inputNormal" 
                                MaxLength="20" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td 
                            
                            style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            <asp:Label ID="Label129" runat="server" Text="Versión:"></asp:Label>
                            <asp:Label ID="Label170" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtVersion" runat="server" CssClass="inputNormal" 
                                MaxLength="20" Width="200px"></asp:TextBox>
                        </td>
                        <td class="tdDatos" 
                            style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px;">
                            <asp:Label ID="Label130" runat="server" Text="Cilindrada:"></asp:Label>
                            <asp:Label ID="Label173" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtCilindrada" runat="server" CssClass="inputNormal" 
                                MaxLength="20" Width="200px" onkeypress="return ValidaNumeros(event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr size="1" />
                        </td>
                    </tr>
                </table>



                <table cellpadding="5" cellspacing="3" width="100%">
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label108" runat="server" Text="N° Asientos:"></asp:Label>
                            <asp:Label ID="Label174" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px" width="120">
                            <asp:TextBox ID="txtAsientos" runat="server" CssClass="inputNormal" 
                                MaxLength="2" onkeypress="return ValidaNumeros(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label110" runat="server" Text="N° Pasajeros:"></asp:Label>
                            <asp:Label ID="Label175" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px" width="120">
                            <asp:TextBox ID="txtPasajeros" runat="server" CssClass="inputNormal" 
                                MaxLength="2" onkeypress="return ValidaNumeros(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label148" runat="server" Text="Combustible Final:"></asp:Label>
                            <asp:Label ID="Label176" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:DropDownList ID="ddlCombustibleFinal" runat="server" CssClass="combo" 
                                Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label113" runat="server" Text="Longitud(m):"></asp:Label>
                            <asp:Label ID="Label177" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtLongitud" runat="server" CssClass="textboxIngresoDecimal" 
                                MaxLength="5" onkeypress="return ValidaDecimal(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label114" runat="server" Text="Ancho(m):"></asp:Label>
                            <asp:Label ID="Label178" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtAncho" runat="server" CssClass="textboxIngresoDecimal" 
                                MaxLength="5" onkeypress="return ValidaDecimal(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label115" runat="server" Text="Altura(m):"></asp:Label>
                            <asp:Label ID="Label179" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtAltura" runat="server" CssClass="textboxIngresoDecimal" 
                                MaxLength="5" onkeypress="return ValidaDecimal(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label117" runat="server" Text="N° Ejes:"></asp:Label>
                            <asp:Label ID="Label180" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtEjes" runat="server" CssClass="inputNormal" MaxLength="2" onkeypress="return ValidaNumeros(event);" 
                                Width="80px">2</asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label118" runat="server" Text="N° Ruedas:"></asp:Label>
                            <asp:Label ID="Label181" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtRuedas" runat="server" CssClass="inputNormal" MaxLength="2" onkeypress="return ValidaNumeros(event);" 
                                Width="80px">4</asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label40" runat="server" Text="N° Cilindros:"></asp:Label>
                            <asp:Label ID="Label182" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtCilindros" runat="server" CssClass="inputNormal" 
                                MaxLength="2" onkeypress="return ValidaNumeros(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label119" runat="server" Text="Peso Seco Inicial (kg):"></asp:Label>
                            <asp:Label ID="Label183" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtPesoSecoInicial" runat="server" CssClass="inputNormal" 
                                MaxLength="5" onkeypress="return ValidaNumeros(event);" 
                                onkeyup="PesoBruto();" Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label42" runat="server" Text="Carga Util Inicial (kg):"></asp:Label>
                            <asp:Label ID="Label184" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtCargaUtilInicial" runat="server" CssClass="inputNormal" 
                                MaxLength="5" onkeypress="return ValidaNumeros(event);" 
                                onkeyup="PesoBruto();" Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label43" runat="server" Text="Peso Bruto Inicial (kg):"></asp:Label>
                            <asp:Label ID="Label185" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtPesoBrutoInicial" runat="server" CssClass="inputNormal" 
                                MaxLength="5" onkeypress="return ValidaNumeros(event);" 
                                Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label145" runat="server" Text="Peso Seco Final (kg):"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtPesoSecoFinal" runat="server" CssClass="inputNormal" 
                                MaxLength="5" onkeypress="return ValidaNumeros(event);" onkeyup="PesoBruto();" 
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label146" runat="server" Text="Carga Util Final (kg):"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtCargaUtilFinal" runat="server" CssClass="inputNormal" 
                                MaxLength="5" onkeypress="return ValidaNumeros(event);" onkeyup="PesoBruto();" 
                                Width="80px"></asp:TextBox>
                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 150px; padding-left: 10px;">
                            
                            <asp:Label ID="Label147" runat="server" Text="Peso Bruto Final (kg):"></asp:Label>
                        </td>
                        <td class="tdDatos" style="padding-left: 5px">
                            <asp:TextBox ID="txtPesoBrutoFinal" runat="server" CssClass="inputNormal" 
                                MaxLength="5" onkeypress="return ValidaNumeros(event);" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td class="tdDatos" style="padding-left: 5px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="tdDatos" style="padding-left: 5px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="tdDatos" style="padding-left: 5px">
                            &nbsp;</td>
                    </tr>
                </table>
                    </ContentTemplate>
                    
        </cc1:TabPanel>
                    
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" Visible="true">
            <HeaderTemplate>
                Datos del Propietario
                    </HeaderTemplate>
                    
            <ContentTemplate>
                <table cellspacing="5" width="100%">
                    <tr>
                        <td class="labelverde">
                            &nbsp;</td>
                        <td class="tdDatos">
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label132" runat="server" Text="Número de Documento:"></asp:Label>
                            <asp:Label ID="Label186" runat="server" Font-Bold="True" Font-Size="10pt" 
                                ForeColor="#18AC85" Text="*"></asp:Label>
                        </td>
                        <td class="tdDatos">
                            <table class="style1">
                                <tr>
                                    <td width="110">
                                        <asp:TextBox ID="txtNumeroDocumentoPropietario" runat="server" 
                                            CssClass="inputNormal" MaxLength="11" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnVerificarPropietario" runat="server" 
                                            ImageUrl="~/images/auth_ok.png" ToolTip="Verificar Propietario" 
                                            OnClick="btnVerificarPropietario_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            
                        <asp:Label ID="Label95" runat="server" Text="Nombre:"></asp:Label>
                            
                        <asp:Label ID="Label187" runat="server" Font-Bold="True" Font-Size="10pt" 
                            ForeColor="#18AC85" Text="*"></asp:Label>
                            
                        </td>
                        <td  class="tdDatos">
                            <asp:TextBox ID="txtNombrePropietario" runat="server" CssClass="inputNormal" 
                                MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                        <asp:Label ID="Label131" runat="server" Text="Genero:"></asp:Label>
                    </td>
                        <td  class="tdDatos">
                            <asp:RadioButtonList ID="rblSexoPropietario" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="M">Masculino</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" 
                            
                            style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label133" runat="server" Text="Distrito:"></asp:Label>
                        </td>
                        <td runat="server" class="tdDatos">
                            <asp:TextBox ID="txtDistritoPropietario" runat="server" CssClass="inputNormal" 
                                MaxLength="100" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;" 
                            runat="server">
                            <asp:Label ID="Label134" runat="server" Text="Dirección:"></asp:Label>
                        </td>
                        <td class="tdDatos" runat="server">
                            <asp:TextBox ID="txtDireccionPropietario" runat="server" 
                                CssClass="inputNormal" MaxLength="1000" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td runat="server" 
                            
                            style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label135" runat="server" Text="Teléfono:"></asp:Label>
                        </td>
                        <td runat="server" class="tdDatos">
                            <asp:TextBox ID="txtTelefonoPropietario" runat="server" CssClass="inputNormal" 
                                MaxLength="20"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td runat="server" 
                            
                                style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label136" runat="server" Text="Celular:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCelularPropietario" runat="server" CssClass="inputNormal" 
                                MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label137" runat="server" Text="Email:"></asp:Label>
                        </td>
                        <td class="tdDatos">
                            <asp:TextBox ID="txtEmailPropietario" runat="server" CssClass="inputNormal" 
                                MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label139" runat="server" Text="Contacto:"></asp:Label>
                        </td>
                        <td class="tdDatos">
                            <asp:TextBox ID="txtContactoPropietario" runat="server" CssClass="inputNormal" 
                                MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label140" runat="server" Text="Fecha Cumpleaños:"></asp:Label>
                        </td>
                        <td class="tdDatos">
                            <asp:TextBox ID="txtCumpleañosPropietario" runat="server" CssClass="inputsFecha" 
                                MaxLength="10"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCumpleañosPropietario_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd/MM/yyyy" 
                                TargetControlID="txtCumpleañosPropietario">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; width: 180px; padding-left: 10px;">
                            <asp:Label ID="Label141" runat="server" Text="Comentario:"></asp:Label>
                        </td>
                        <td class="tdDatos">
                            <asp:TextBox ID="txtComentarioPropietario" runat="server" TextMode="MultiLine" 
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td class="tdDatos">
                            &nbsp;</td>
                    </tr>
                </table>
                    </ContentTemplate>
                    
        </cc1:TabPanel>
                    
                </cc1:TabContainer>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</div>

</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
    </table>
    
     <table width="100%" runat="server" id="tblCliente"
        
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;" 
        visible="False">
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
            <div class="divDocumento">
                <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Cliente" ForeColor="#4C4C4C"></asp:Label>
                </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933" 
                width="170">
                <asp:Label ID="Label31" runat="server" Text="Número de Documento:"></asp:Label>
                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label192"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtNumeroDocumentoCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="11"></asp:TextBox>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label30" runat="server" Text="Nombre:"></asp:Label>
                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label193"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label47" runat="server" Text="Genero:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" 
                    Width="210px" ID="rblSexoCliente">
<asp:ListItem Value="M" Selected="True">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Femenino</asp:ListItem>
</asp:RadioButtonList>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label49" runat="server" Text="Distrito:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtDistritoCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="100" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label32" runat="server" Text="Dirección:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtDireccionCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="1000" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label33" runat="server" Text="Teléfono:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="20"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label48" runat="server" Text="Celular:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtCelularCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label34" runat="server" Text="Email:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtEmailCliente" runat="server" CssClass="inputNormal" MaxLength="50" 
                    Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label36" runat="server" Text="Contacto:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtContactoCliente" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label46" runat="server" Text="Fecha Cumpleaños:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtCumpleañosCliente" runat="server" CssClass="inputsFecha" 
                    MaxLength="10"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtCumpleañosCliente_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" 
                    TargetControlID="txtCumpleañosCliente">
                        </cc1:CalendarExtender>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label37" runat="server" Text="Comentario:"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtComentarioCliente" runat="server" TextMode="MultiLine" 
                    Width="200px"></asp:TextBox>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="padding-left: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnGuardarCliente" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnGuardarCliente_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalirCliente" runat="server" 
                                ImageUrl="~/images/Regresar.jpg" onclick="btnSalirCliente_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        </div>

            </td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
      </table>
     
     <table width="100%" runat="server" id="tblMarca"
        
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;" 
        visible="False">
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
            <div class="divDocumento">
                <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="Label157" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Marca" ForeColor="#4C4C4C"></asp:Label>
                </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label160" runat="server" Text="Nombre:"></asp:Label>
                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label194"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtDescripcionMarca" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="padding-left: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnRegistrarMarca" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnRegistrarMarca_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCancelarMarca" runat="server" 
                                ImageUrl="~/images/Regresar.jpg" onclick="btnCancelarMarca_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        </div>

            </td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
      </table>
     
     <table width="100%" runat="server" id="tblModelo"
        
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;" 
        visible="False">
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
            <div class="divDocumento">
                <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="Label162" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Modelo" ForeColor="#4C4C4C"></asp:Label>
                </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933">
                <asp:Label ID="Label163" runat="server" Text="Nombre:"></asp:Label>
                <asp:Label runat="server" Text="*" Font-Bold="True" Font-Size="10pt" 
                    ForeColor="#18AC85" ID="Label195"></asp:Label>
            </td>
            <td style="padding-left: 5px">
                <asp:TextBox ID="txtDescripcionModelo" runat="server" CssClass="inputNormal" 
                    MaxLength="50" Width="200px"></asp:TextBox>

            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="padding-left: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="65">
                            <asp:ImageButton ID="btnRegistrarModelo" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnRegistrarModelo_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCancelarModelo" runat="server" 
                                ImageUrl="~/images/Regresar.jpg" onclick="btnCancelarModelo_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        </div>

            </td>
            <td width="15%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="15%">
                &nbsp;</td>
        </tr>
      </table>
     
</asp:Content>

