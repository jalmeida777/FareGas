using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ListarCarroceria : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Label lblTitulo = (Label)Master.FindControl("lblTitulo");
            if (lblTitulo != null)
            {
                lblTitulo.Text = "Administración de Carrocerías";
            }
            ListarCategoria();
            Listar();
        }
        txtBuscar.Focus();
    }

    void ListarCategoria()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Faregas_Categoria_Combo", conexion);
        da.Fill(dt);

        ddlCategoria.DataSource = dt;
        ddlCategoria.DataTextField = "v_Categoria";
        ddlCategoria.DataValueField = "i_IdCategoria";
        ddlCategoria.DataBind();
        ddlCategoria.SelectedIndex = 0;
    }

    void Listar()
    {
        string Estado = "";
        if (chkEstado.Checked) { Estado = "1"; } else { Estado = "0"; }

        string i_IdCategoria = "";
        i_IdCategoria = ddlCategoria.SelectedValue.ToString();


        DataTable dt = new DataTable();
        
        SqlDataAdapter da = new SqlDataAdapter("Play_Carroceria_Listar " + i_IdCategoria + ",'" + txtBuscar.Text.Trim() + "'," + Estado, conexion);
        da.Fill(dt);
        gv.DataSource = dt;
        gv.DataBind();
    }

   

    protected void ddlClase_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Principal.aspx");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearCarroceria.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        Listar();
    }

    protected void gvModelo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i_IdCarroceria = int.Parse(gv.DataKeys[e.Row.RowIndex].Value.ToString());
            ImageButton btnEditar = e.Row.FindControl("btnEditar") as ImageButton;

            if (btnEditar != null)
            {
                btnEditar.PostBackUrl = "CrearCarroceria.aspx?i_IdCarroceria=" + i_IdCarroceria;
            }
        }
    }
    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listar();
    }
}