using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class ListarStockGlobal : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtUsuario"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (Page.IsPostBack == false)
        {
            Permisos();
        }
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ReporteStockGlobal.aspx");
    }

    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            string n_IdProducto = e.GetValue("n_IdProducto").ToString();
            int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);

            LinkButton lbProducto = new LinkButton();
            lbProducto = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[2]), "lbProducto");
            lbProducto.PostBackUrl = "CrearProducto.aspx?n_IdProducto=" + n_IdProducto + "&IdMenu=30";
            lbProducto.Enabled = chkEditar.Checked;

            LinkButton lbMinka = new LinkButton();
            lbMinka = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[3]), "lbMinka");
            Label lblIdAlmacenMinka = new Label();
            lblIdAlmacenMinka = (Label)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[3]), "lblIdAlmacenMinka");
            lbMinka.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + lblIdAlmacenMinka.Text + "&origen=global&IdMenu=" + i_IdMenu;

            LinkButton lbBellavista = new LinkButton();
            lbBellavista = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[4]), "lbBellavista");
            Label lblIdAlmacenBellavista = new Label();
            lblIdAlmacenBellavista = (Label)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[4]), "lblIdAlmacenBellavista");
            lbBellavista.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + lblIdAlmacenBellavista.Text + "&origen=global&IdMenu=" + i_IdMenu;

            LinkButton lbSantaAnita = new LinkButton();
            lbSantaAnita = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[5]), "lbSantaAnita");
            Label lblIdAlmacenSantaAnita = new Label();
            lblIdAlmacenSantaAnita = (Label)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[5]), "lblIdAlmacenSantaAnita");
            lbSantaAnita.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + lblIdAlmacenSantaAnita.Text + "&origen=global&IdMenu=" + i_IdMenu;

            LinkButton lbSantaClara = new LinkButton();
            lbSantaClara = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[6]), "lbSantaClara");
            Label lblIdAlmacenSantaClara = new Label();
            lblIdAlmacenSantaClara = (Label)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[6]), "lblIdAlmacenSantaClara");
            lbSantaClara.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + lblIdAlmacenSantaClara.Text + "&origen=global&IdMenu=" + i_IdMenu;

            LinkButton lbPlayCentral = new LinkButton();
            lbPlayCentral = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[7]), "lbPlayCentral");
            Label lblIdAlmacenPlayCentral = new Label();
            lblIdAlmacenPlayCentral = (Label)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[7]), "lblIdAlmacenPlayCentral");
            lbPlayCentral.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + lblIdAlmacenPlayCentral.Text + "&origen=global&IdMenu=" + i_IdMenu;

            LinkButton lbAlmacenCentral = new LinkButton();
            lbAlmacenCentral = (LinkButton)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[8]), "lbAlmacenCentral");
            Label lblIdAlmacenCentral = new Label();
            lblIdAlmacenCentral = (Label)ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)(ASPxGridView1.Columns[8]), "lblIdAlmacenCentral");
            lbAlmacenCentral.PostBackUrl = "ListarKardex.aspx?n_IdProducto=" + n_IdProducto + "&n_IdAlmacen=" + lblIdAlmacenCentral.Text + "&origen=global&IdMenu=" + i_IdMenu;
        }
    }

    void Permisos()
    {
        DataTable dtUsuario = new DataTable();
        dtUsuario = (DataTable)Session["dtUsuario"];
        int i_IdRol = int.Parse(dtUsuario.Rows[0]["i_IdRol"].ToString());
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);

        DataTable dtPermisos = new DataTable();
        SqlDataAdapter daPermisos = new SqlDataAdapter("Play_Permisos_Select " + i_IdRol + "," + i_IdMenu, conexion);
        daPermisos.Fill(dtPermisos);

        chkEditar.Checked = bool.Parse(dtPermisos.Rows[0]["b_Estado"].ToString());

    }
    protected void callbackPanel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        int n_IdProducto = Convert.ToInt32(e.Parameter);
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("select v_RutaImagen,v_CodigoInterno,v_Descripcion from producto where n_IdProducto=" + n_IdProducto, conexion);
        da.Fill(dt);
        lblCodigo.Text = dt.Rows[0]["v_CodigoInterno"].ToString();
        lblProducto.Text = dt.Rows[0]["v_Descripcion"].ToString();
        ImagenGrande.ImageUrl = dt.Rows[0]["v_RutaImagen"].ToString();
    }
}