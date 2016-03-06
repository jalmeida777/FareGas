using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearCarroceria : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ListarCategoria();
            if (Request.QueryString["i_IdCarroceria"] != null)
            {
                int i_IdCarroceria = int.Parse(Request.QueryString["i_IdCarroceria"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Carroceria_Seleccionar " + i_IdCarroceria.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = i_IdCarroceria.ToString();
                txtDescripcion.Text = dt.Rows[0]["v_NombreCarroceria"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                ddlCategoria.SelectedValue = dt.Rows[0]["i_IdCategoria"].ToString();
            }
            else
            {
            }

            txtDescripcion.Focus();
        }
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

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarCarroceria.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDescripcion.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la descripción' });</script>", false);
            txtDescripcion.Focus();
            return;
        }

        try
        {
            if (lblCodigo.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Carroceria_Actualizar";
                cmd.Parameters.AddWithValue("@i_IdCarroceria", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@i_IdCategoria", ddlCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@v_NombreCarroceria", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Carrocería actualizada.' });</script>", false);
            }
            else
            {
                string i_IdCarroceria = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Carroceria_Registrar";
                cmd.Parameters.AddWithValue("@i_IdCategoria", ddlCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@v_NombreCarroceria", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                i_IdCarroceria = cmd.ExecuteScalar().ToString();
                conexion.Close();
                lblCodigo.Text = i_IdCarroceria;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Carrocería registrada.' });</script>", false);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El código interno ya se encuentra en uso!' });</script>", false);
        }
    }
}