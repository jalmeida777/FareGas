<%@ Page Title="Imprimir Factura Percival" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="ImprimirFacturaPercival.aspx.cs" Inherits="ImprimirFacturaPercival" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div class="divBusqueda">
        <table width="100%">
            <tr>
                <td>
                    <h1 class="label">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/statistics.png" />
                        &nbsp;Imprimir Factura PERCIVAL<asp:HiddenField ID="hfIdComprobante" 
                            runat="server" />
                    </h1>
                </td>
            </tr>
        </table>
    </div>


 <div class="toolbar">
            <table width="100%"><tr>
                <td width="65">
                    
                            <asp:ImageButton ID="btnSalir" runat="server" ImageUrl="~/images/Salir.jpg" 
                                onclick="btnSalir_Click" />
                    
                </td>
                <td>
                   
                    <asp:ImageButton ID="btnImprimir" runat="server" 
                        ImageUrl="~/images/Imprimir.jpg" onclick="btnImprimir_Click" />
                </td>
                </tr></table>
            </div>
                          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" SelectCommand="FG_Comprobante_Seleccionar" 
    SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="hfIdComprobante" Name="i_IdComprobante" 
                PropertyName="Value" Type="Int32" />
        </SelectParameters>
        </asp:SqlDataSource>
                         <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:PlayConnectionString %>" 
        SelectCommand="Play_DetComprobante_Listar_2" 
        SelectCommandType="StoredProcedure">
                             <SelectParameters>
                                 <asp:ControlParameter ControlID="hfIdComprobante" Name="i_IdComprobante" 
                                     PropertyName="Value" Type="Int32" />
                             </SelectParameters>
    </asp:SqlDataSource>
                         <table width="100%" 
        
        style="border-bottom: 1px solid #ddd; background-image: url('../images/form_sheetbg.png'); background-repeat: repeat; ">
        <tr>
            <td width="7%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="7%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="7%">
                &nbsp;</td>
            <td>
                <div class="divDocumento">
                <table width="100%" cellspacing="5" >
        <tr>
            <td height="10" width="20">
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
    <rsweb:reportviewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                    WaitMessageFont-Names="Verdana" 
    WaitMessageFont-Size="14pt" Width="100%">
                    <LocalReport ReportPath="FacturaPERCIVAL.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="dsComprobante" />
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="dsDetalle" />
                        </DataSources>
                    </LocalReport>
                </rsweb:reportviewer>
            </td>
            <td width="20">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td width="20">
                &nbsp;</td>
        </tr>
        </table>
        <tr>
            <td width="7%">
                &nbsp;</td>
            <td>
                &nbsp;</table>

</asp:Content>

