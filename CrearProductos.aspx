<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearProductos.aspx.cs" Inherits="CrearProductos" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />

<script language="javascript">
    $(function () {

        $('.textboxIngresoDecimal').keypress(function (e) {
            var character = String.fromCharCode(e.keyCode)
            var newValue = this.value + character;
            if (isNaN(newValue) || hasDecimalPlace(newValue, 2)) {
                e.preventDefault();
                return false;
            }
        });
        
        function hasDecimalPlace(value, x) {
            var pointIndex = value.indexOf('.');
            return pointIndex >= 0 && pointIndex < value.length - x;
        }

    });

    function ValidaDecimal(e) {
        var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
        if (tecla > 31 && (tecla < 48 || tecla > 57) && tecla != 46)
            return false;
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                        Servicio</h1>
                </td>
            </tr>
        </table>
    </div>
    <div class="toolbar">
        <table width="100%">
            <tr>
                <td width="65">
                    <asp:ImageButton ID="btnGuardar" runat="server" 
                                ImageUrl="~/images/Guardar.jpg" onclick="btnGuardar_Click" />
                    <cc1:ConfirmButtonExtender ID="btnGuardar_ConfirmButtonExtender" runat="server" 
                                ConfirmText="¿Seguro de guardar los datos?" Enabled="True" 
                                TargetControlID="btnGuardar">
                    </cc1:ConfirmButtonExtender>
                </td>
                <td width="65">
                    <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" 
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
                            <td width="20">
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Empresa:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="combo" 
                    Width="280px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Código:" Visible="False"></asp:Label>
                                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td width="20">
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Descripción:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="inputNormal" placeholder="Descripción"
                    Width="400px" style="text-transform:uppercase" MaxLength="200"></asp:TextBox>
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td width="20">
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Text="Precio:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="inputNormalMoneda" 
                                    MaxLength="6" Width="120px" onkeypress="return ValidaDecimal(event);"></asp:TextBox>
                                <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="#18AC85" 
                        Text="*"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Estado:"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkEstado" runat="server" Checked="True" Text="Habilitado" />
                            </td>
                            <td>
                &nbsp;</td>
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
                            <td>
                &nbsp;</td>
                            <td width="20">
                &nbsp;</td>
                        </tr>
                    </table>
        <tr>
            <td width="15%">
                &nbsp;</td>
            <td>
                &nbsp;</table>
</asp:Content>

