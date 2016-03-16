<%@ Page Title="Crear Comprobante" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearComprobante.aspx.cs" Inherits="CrearComprobante" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery.growl.js" type="text/javascript"></script>
    <link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="toolbar" id="toolbar" runat ="server">
        <table width="100%">
            <tr>
                <td width="65">
                    <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/images/Guardar.jpg" 
                    
                    OnClientClick="if (confirm('Seguro de guardar?')) { btnGuardar.disabled = false; return true; } else { return false; }" 
                    onclick="btnGuardar_Click" />
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnAnular" runat="server" ImageUrl="~/images/Anular.jpg" 
                        Visible="False" 
                        
                        OnClientClick="if (confirm('Seguro de anular?')) { btnAnular.disabled = false; return true; } else { return false; }" 
                        onclick="btnAnular_Click" />
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/Imprimir.jpg" Visible="False" 
                        onclick="btnImprimir_Click" />
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
            </tr>
        </table>
    </div>

 <table width="100%" runat="server" id="tblGeneral"
        style="background-image: url('images/form_sheetbg.png'); background-repeat: repeat; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #ddd;">
        <tr>
            <td width="15%">
                <asp:HiddenField ID="hfPedido" runat="server" />
            </td>
            <td>
                <asp:HiddenField ID="hfCliente" runat="server" />
                <asp:HiddenField ID="hfComprobante" runat="server" />
            </td>
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
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" 
                    Text="Comprobante N° " ForeColor="#4C4C4C"></asp:Label>
                <asp:Label ID="lblNumeroComprobante" runat="server" Font-Bold="True" Font-Size="20pt" 
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
                <table cellpadding="5" cellspacing="3" width="100%" __designer:mapid="3f2">
                    <tr __designer:mapid="3f8">
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;" 
                            width="170" __designer:mapid="3f9">
                                        <asp:Label runat="server" Text="Tipo de Comprobante:" ID="Label151"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="3fc">

                                                    <asp:RadioButtonList runat="server" 
                                RepeatDirection="Horizontal" ID="rblTipoComprobante" Enabled="False"><asp:ListItem Selected="True">Boleta</asp:ListItem>
<asp:ListItem>Factura</asp:ListItem>
</asp:RadioButtonList>

                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; " 
                            __designer:mapid="409">
                            <asp:Label runat="server" Text="Fecha:" ForeColor="#4C4C4C" ID="Label2"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="40c">
                            <asp:TextBox runat="server" MaxLength="10" CssClass="inputsFecha" 
                                Enabled="False" ID="txtFechaInicial"></asp:TextBox>

                            <cc1:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                                TargetControlID="txtFechaInicial" ID="CalendarExtender1"></cc1:CalendarExtender>

                        </td>
                    </tr>
                    <tr __designer:mapid="3f8">
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;" 
                            width="170" __designer:mapid="3f9">
                            <asp:Label runat="server" Text="Cliente:" ID="Label6"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="3fc">

                                        <asp:Label runat="server" Font-Bold="False" ID="lblNombreCliente"></asp:Label>

                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; " 
                            __designer:mapid="409">
                            <asp:Label runat="server" Text="DNI / RUC:" ID="Label193"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="40c">
                            <asp:Label ID="lblDni" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr __designer:mapid="3f8">
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;" 
                            width="170" __designer:mapid="3f9">
                            <asp:Label runat="server" Text="Dirección:" ID="Label194"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="3fc">

                            <asp:Label ID="lblDireccion" runat="server"></asp:Label>

                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; " 
                            __designer:mapid="409">
                            <asp:Label runat="server" Text="Moneda:" ID="Label24"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="40c">
                            <asp:DropDownList runat="server" CssClass="combo" Enabled="False" Width="100px" 
                                ID="ddlMoneda"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr __designer:mapid="40f">
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;" 
                            __designer:mapid="410">
                            <asp:Label runat="server" Text="Forma de Pago:" ID="Label23"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="412">
                            <asp:DropDownList runat="server" CssClass="combo" Width="220px" 
                                ID="ddlFormaPago" Enabled="False"></asp:DropDownList>

                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; " 
                            __designer:mapid="414">
                            <asp:Label runat="server" Text="Placa:" ID="Label195"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="416">
                            <asp:Label ID="lblPlaca" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr __designer:mapid="418">
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; padding-left: 10px;" 
                            __designer:mapid="419">
                            <asp:Label runat="server" Text="Empresa:" ID="Label52"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="41b">
                                        <asp:DropDownList runat="server" CssClass="combo" Width="270px" 
                                ID="ddlEmpresa" Enabled="False"></asp:DropDownList>

                        </td>
                        <td style="border-right-style: solid; border-right-width: 1px; border-right-color: #339933; " 
                            __designer:mapid="422">
                            <asp:Label runat="server" Text="Estado:" ID="Label44"></asp:Label>

                        </td>
                        <td style="padding-left: 5px" __designer:mapid="424">
                            <asp:Label runat="server" Text="Pendiente" Font-Bold="True" ForeColor="#CC3300" 
                                ID="lblEstado"></asp:Label>

                        </td>
                    </tr>
                    <tr __designer:mapid="426">
                        <td colspan="4" __designer:mapid="427">
                            &nbsp;</td>
                    </tr>
                    <tr __designer:mapid="438">
                        <td colspan="4" __designer:mapid="439">
                            <asp:GridView runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="n_IdProducto" CssClass="grid" ID="gv" Enabled="False"><Columns>
<asp:BoundField DataField="i_Cantidad" HeaderText="Cantidad">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
                                    <asp:TemplateField HeaderText="Producto">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("v_Descripcion") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtProducto" runat="server" 
                                                Text='<%# Bind("v_Descripcion") %>' Width="100%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<asp:BoundField DataField="f_PrecioUnitario" DataFormatString="{0:n2}" HeaderText="Precio S/.">
<ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="f_PrecioTotal" DataFormatString="{0:n2}" HeaderText="Total S/.">
<ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView>

                        </td>
                    </tr>
                    <tr __designer:mapid="444">
                        <td colspan="4" __designer:mapid="445">
                            <table border="0" cellpadding="5" cellspacing="3" width="100%" 
                                __designer:mapid="446">
                                <tr __designer:mapid="447">
                                    <td align="left" 
                                        width="150" __designer:mapid="448">
                                        &nbsp;</td>
                                    <td align="left" style="padding-left: 5px" __designer:mapid="44a">
                                        <asp:HiddenField ID="hfSubTotal" runat="server" />
                                    </td>
                                    <td align="right" width="100" __designer:mapid="453">
                                        <asp:Label runat="server" Text="SubTotal:" ID="Label20"></asp:Label>

                                    </td>
                                    <td align="right" width="20" __designer:mapid="455">
                                        <asp:Label runat="server" Text="S/." ID="lblSigno1"></asp:Label>

                                    </td>
                                    <td align="right" width="100" __designer:mapid="457">
                                        <asp:Label runat="server" ID="lblSubTotal"></asp:Label>

                                    </td>
                                </tr>
                                <tr __designer:mapid="459">
                                    <td align="left" 
                                        width="150" __designer:mapid="45a">
                                        &nbsp;</td>
                                    <td align="left" style="padding-left: 5px" __designer:mapid="45c">
                                        <asp:HiddenField ID="hfIgv" runat="server" />
                                    </td>
                                    <td align="right" width="100" __designer:mapid="45e">
                                        <asp:Label runat="server" Text="IGV:(" ID="Label142"></asp:Label>

                                        <asp:Label runat="server" Text="00" ID="lblIgvPorc"></asp:Label>

                                        <asp:Label runat="server" Text="%)" ID="Label152"></asp:Label>

                                    </td>
                                    <td align="right" width="20" __designer:mapid="462">
                                        <asp:Label runat="server" Text="S/." ID="lblSigno6"></asp:Label>

                                    </td>
                                    <td align="right" width="100" __designer:mapid="464">
                                        <asp:Label runat="server" ID="lblIGV"></asp:Label>

                                    </td>
                                </tr>
                                <tr __designer:mapid="466">
                                    <td align="left" 
                                        __designer:mapid="467">
                                        &nbsp;</td>
                                    <td style="padding-left: 5px" __designer:mapid="469">
                                        <asp:HiddenField ID="hfTotal" runat="server" />
                                    </td>
                                    <td align="right" 
                                        
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                                        __designer:mapid="46b">
                                        <asp:Label runat="server" Text="Total:" Font-Bold="True" Font-Size="12pt" 
                                            ID="Label22"></asp:Label>

                                    </td>
                                    <td align="right" 
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                                        width="20" __designer:mapid="46d">
                                        <asp:Label runat="server" Text="S/." Font-Bold="True" Font-Size="12pt" 
                                            ID="lblSigno3"></asp:Label>

                                    </td>
                                    <td align="right" 
                                        
                                        style="border-top-style: solid; border-top-width: 1px; border-top-color: #999999" 
                                        __designer:mapid="46f">
                                        <asp:Label runat="server" Font-Bold="True" Font-Size="12pt" ID="lblTotal"></asp:Label>

                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                    <tr __designer:mapid="488">
                        <td __designer:mapid="489" colspan="4">
                            <table cellpadding="0" cellspacing="0" class="style1">
                                <tr>
                                    <td width="110">
                                        <asp:Label ID="Label192" runat="server" Text="Monto Escrito:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMontoEscrito" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr __designer:mapid="48d">
                        <td colspan="4" __designer:mapid="48e">
                            <asp:TextBox runat="server" TextMode="MultiLine" Height="80px" Width="380px" 
                                ID="txtObservacion" placeholder="Comentarios"></asp:TextBox>

                        </td>
                    </tr>
                    <tr __designer:mapid="490">
                        <td colspan="4" __designer:mapid="491">
                            <table width="500" __designer:mapid="492">
                                <tr __designer:mapid="493">
                                    <td rowspan="2" width="60" __designer:mapid="494">
                                        <asp:ImageButton runat="server" ImageUrl="~/images/face.jpg" Height="50px" 
                                            Width="50px" ID="ibUsuarioRegistro"></asp:ImageButton>

                                    </td>
                                    <td __designer:mapid="496">
                                        Creado por:
                                        <asp:Label runat="server" Font-Bold="True" ID="lblUsuarioRegistro"></asp:Label>

                                    </td>
                                </tr>
                                <tr __designer:mapid="498">
                                    <td __designer:mapid="499">
                                        <asp:Label runat="server" ID="lblFechaRegistro"></asp:Label>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr __designer:mapid="49b">
                        <td __designer:mapid="49c">
                            &nbsp;</td>
                        <td __designer:mapid="49d">
                            &nbsp;</td>
                        <td __designer:mapid="49e">
                            &nbsp;</td>
                        <td __designer:mapid="49f">
                            &nbsp;</td>
                    </tr>
                </table>
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
    
     </asp:Content>

