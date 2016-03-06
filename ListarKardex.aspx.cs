using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class ListarKardex : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Listar();
        }
    }

    void Listar() 
    {
        string n_IdProducto = Request.QueryString["n_IdProducto"];
        string n_IdAlmacen = Request.QueryString["n_IdAlmacen"];

        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Kardex_Listar " + n_IdProducto + "," + n_IdAlmacen, conexion);
        da.Fill(dt);
        gvKardex.DataSource = dt;
        gvKardex.DataBind();

        DataTable dtProducto = new DataTable();
        SqlDataAdapter daProducto = new SqlDataAdapter("select v_Descripcion from Producto where n_IdProducto = " + n_IdProducto, conexion);
        daProducto.Fill(dtProducto);
        lblProducto.Text = dtProducto.Rows[0]["v_Descripcion"].ToString();

        DataTable dtAlmacen = new DataTable();
        SqlDataAdapter daAlmacen = new SqlDataAdapter("select v_Descripcion from Almacen where n_IdAlmacen = " + n_IdAlmacen, conexion);
        daAlmacen.Fill(dtAlmacen);
        lblAlmacen.Text = dtAlmacen.Rows[0]["v_Descripcion"].ToString();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        string origen = Request.QueryString["origen"];
        string IdMenu = Request.QueryString["IdMenu"];
        if (origen == "sucursal")
        {
            Response.Redirect("ListarStockActual.aspx?IdMenu=" + IdMenu);
        }
        else if (origen == "global")
        {
            Response.Redirect("ListarStockGlobal.aspx?IdMenu=" + IdMenu);
        }
    }

    protected void gvKardex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lblNumeroDocumento = e.Row.FindControl("LinkButton1") as LinkButton;
            Label lblIdDocumento = e.Row.FindControl("lblIdDocumento") as Label;
            string TipoDocumento = e.Row.Cells[5].Text;
            string motivo = e.Row.Cells[1].Text;
            string IdMenu = Request.QueryString["IdMenu"];

            if (lblNumeroDocumento != null)
            {
                if (TipoDocumento == "Nota de Ingreso")
                {
                    if (motivo == "INVENTARIO INICIAL")
                    {
                        lblNumeroDocumento.PostBackUrl = "CrearInventarioInicial.aspx?n_IdNotaIngreso=" + lblIdDocumento.Text + "&IdMenu=" + IdMenu;
                    }
                    else { lblNumeroDocumento.PostBackUrl = "CrearNotaIngreso.aspx?n_IdNotaIngreso=" + lblIdDocumento.Text + "&IdMenu=" + IdMenu; }
                }
                else if (TipoDocumento == "Nota de Salida")
                {
                    lblNumeroDocumento.PostBackUrl = "CrearNotaSalida.aspx?n_IdNotaSalida=" + lblIdDocumento.Text + "&IdMenu=" + IdMenu;
                }
                else if (TipoDocumento == "Boleta")
                {
                    lblNumeroDocumento.PostBackUrl = "CrearPedido.aspx?n_IdPedido=" + lblIdDocumento.Text + "&td=3" + "&IdMenu=" + IdMenu;
                }
                else if (TipoDocumento == "Factura")
                {
                    lblNumeroDocumento.PostBackUrl = "CrearPedido.aspx?n_IdPedido=" + lblIdDocumento.Text + "&td=2" + "&IdMenu=" + IdMenu;
                }
                else if (TipoDocumento == "Pedido")
                {
                    lblNumeroDocumento.PostBackUrl = "CrearPedido.aspx?n_IdPedido=" + lblIdDocumento.Text + "&td=2" + "&IdMenu=" + IdMenu;
                }
                else if (TipoDocumento == "Orden de Compra")
                {
                    lblNumeroDocumento.PostBackUrl = "CrearOrdenCompra.aspx?i_IdOrdenCompra=" + lblIdDocumento.Text + "&IdMenu=" + IdMenu;
                }
                else if (TipoDocumento == "Orden de Traslado")
                {
                    lblNumeroDocumento.PostBackUrl = "CrearOrdenTraslado.aspx?i_IdOrdenTraslado=" + lblIdDocumento.Text + "&IdMenu=" + IdMenu;
                }
            }
        }
    }
}