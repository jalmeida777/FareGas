using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearCorrelativo : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            ListarTipoDocumento();
            ListarEmpresa();


            if (Request.QueryString["i_IdCorrelativo"] != null)
            {
                int i_IdCorrelativo = int.Parse(Request.QueryString["i_IdCorrelativo"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Faregas_Correlativo_Seleccionar " + i_IdCorrelativo.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = i_IdCorrelativo.ToString();
                ddlDocumento.SelectedValue = dt.Rows[0]["n_IdTipoDocumento"].ToString();
                ddlEmpresa.SelectedValue = dt.Rows[0]["i_IdEmpresa"].ToString();
                txtSerie.Text = dt.Rows[0]["c_Serie"].ToString();
                txtCorrInicial.Text = dt.Rows[0]["v_CorrelativoInicial"].ToString();
                txtCorrFinal.Text = dt.Rows[0]["v_CorrelativoFinal"].ToString();
                txtCorrActual.Text = dt.Rows[0]["v_CorrelativoActual"].ToString();
                txtAutorizacion.Text = dt.Rows[0]["v_NroAutorizacion"].ToString(); 

               
            }
            else
            {
            }

            txtSerie.Focus();
        }

    }

    void ListarEmpresa()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Faregas_Empresa_Combo", conexion);
        da.Fill(dt);
        ddlEmpresa.DataSource = dt;
        ddlEmpresa.DataTextField = "v_RazonSocial";
        ddlEmpresa.DataValueField = "i_IdEmpresa";
        ddlEmpresa.DataBind();
        ddlEmpresa.SelectedIndex = 0;
    }

    void ListarTipoDocumento()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Faregas_TipoDocumento_Combo", conexion);
        da.Fill(dt);

        ddlDocumento.DataSource = dt;
        ddlDocumento.DataTextField = "v_Descripcion";
        ddlDocumento.DataValueField = "n_IdTipoDocumento";
        ddlDocumento.DataBind();
        ddlDocumento.SelectedIndex = 0;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarCorrelativo.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtSerie.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la Serie' });</script>", false);
            txtSerie.Focus();
            return;
        }

        try
        {
            if (lblCodigo.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Faregas_Correlativo_Actualizar";
                cmd.Parameters.AddWithValue("@i_IdCorrelativo", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@n_IdTipoDocumento", ddlDocumento.SelectedValue);
                cmd.Parameters.AddWithValue("@i_IdEmpresa", ddlEmpresa.SelectedValue);
                cmd.Parameters.AddWithValue("@c_Serie", txtSerie.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_CorrelativoInicial", txtCorrInicial.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_CorrelativoFinal", txtCorrFinal.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_CorrelativoActual", txtCorrActual.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_NroAutorizacion", txtAutorizacion.Text.Trim().ToUpper());
                                
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Correlativo actualizado.' });</script>", false);
            }
            //else
            //{
            //    string i_IdProducto = "";
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conexion;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandText = "Faregas_Producto_Registrar";
            //    cmd.Parameters.AddWithValue("@i_IdEmpresa", ddlEmpresa.SelectedValue);
            //    //cmd.Parameters.AddWithValue("@i_IdCategoriaProducto", ddlCategoria.SelectedValue);
            //    cmd.Parameters.AddWithValue("@v_Descripcion", txtDescripcion.Text.Trim().ToUpper());
            //    cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
            //    conexion.Open();
            //    i_IdProducto = cmd.ExecuteScalar().ToString();
            //    conexion.Close();
            //    lblCodigo.Text = i_IdProducto;
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Producto registrado.' });</script>", false);
            //}

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El código interno ya se encuentra en uso!' });</script>", false);
        }
    }
}