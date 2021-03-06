﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ListarCliente.aspx.cs" Inherits="ListarCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 138px;
        }
        .style2
        {
            width: 133px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divBusqueda">
            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <h1 class="label">
                            Administración de Clientes</h1>
                    </td>
                </tr>
                <tr>
                    <td width="55" class="label">
                        <asp:Label ID="Label2" runat="server" Text="Buscar:"></asp:Label>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="225">
                                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="inputs" 
                                        placeholder="Buscar" Width="200px" AutoPostBack="True" 
                                        ontextchanged="txtBuscar_TextChanged"></asp:TextBox>
                                </td>
                                <td width="130">
                                    <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="True" Checked="True" 
                                        oncheckedchanged="chkEstado_CheckedChanged" Text="Ver Habilitados" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEsTaller" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="chkEsTaller_CheckedChanged" Text="Es Taller" />
                                </td>
                                <td align="right" class="style2">
                                    &nbsp;</td>
                                <td align="right" width="70">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="toolbar">
            <table width="100%">
                <tr>
                    <td width="65">
                        <asp:ImageButton ID="btnConsultar" runat="server" 
                    ImageUrl="~/images/Buscar.jpg" onclick="btnConsultar_Click" />
                    </td>
                    <td width="65">
                        <asp:ImageButton ID="btnNuevo" runat="server" ImageUrl="~/images/Nuevo.jpg" 
                        onclick="btnNuevo_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                        onclick="btnSalir_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="images/loading.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Panel ID="Panel1" runat="server" Height="600px" ScrollBars="Vertical" 
                Width="100%">
            <asp:GridView ID="gvCliente" runat="server" AutoGenerateColumns="False" 
                    CssClass="grid" DataKeyNames="i_IdPersona" 
                    onrowdatabound="gvProveedor_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField DataField="n_IdPersona" HeaderText="Id" Visible="False">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="v_Nombres" HeaderText="Nombre">
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="v_Celular" HeaderText="Celular">
                    <ItemStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="v_Telefono" HeaderText="Telefono">
                    <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="v_Email" HeaderText="Email" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="~/images/edit.png" 
                                    ToolTip="Editar" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

