<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="CrearTaller.aspx.cs" Inherits="CrearTaller" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            width: 386px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                        Taller -
                        <asp:Label ID="lblNombreTaller" runat="server"></asp:Label>
                    </h1>
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
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                &nbsp;</td>
                            <td align="right">
                                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td width="20">
                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td colspan="5">

                            <asp:GridView ID="gv" runat="server" 
                            AutoGenerateColumns="False" CssClass="grid" 
                            DataKeyNames="i_IdTallerPrecio" Width="700px">
                            <Columns>
                                <asp:BoundField DataField="v_Descripcion" HeaderText="Servicio" />
                                <asp:BoundField DataField="f_Precio" DataFormatString="{0:N2}" 
                                    HeaderText="Precio de Lista">
                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Precio Taller">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" 
                                            Text='<%# Bind("f_PrecioConvenio") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPrecioConvenio" runat="server" CssClass="inputNormalMoneda" 
                                            Enabled="False" Text='<%# Bind("f_PrecioTaller", "{0:N2}") %>' 
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="True" 
                                            Checked='<%# Convert.ToBoolean(Eval("b_Estado")) %>' 
                                            oncheckedchanged="chkEstado_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                                </td>
                            <td width="20">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="10" width="20">
                &nbsp;</td>
                            <td>
                &nbsp;</td>
                            <td class="style1">
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

