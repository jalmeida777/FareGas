using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearModelo : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            ListarMarca();

            if (Request.QueryString["i_IdModelo"] != null)
            {
                int i_IdModelo = int.Parse(Request.QueryString["i_IdModelo"].ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Modelo_Seleccionar " + i_IdModelo.ToString(), conexion);
                da.Fill(dt);
                lblCodigo.Text = i_IdModelo.ToString();
                txtDescripcion.Text = dt.Rows[0]["v_NombreModelo"].ToString();
                chkEstado.Checked = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                ddlMarca.SelectedValue = dt.Rows[0]["i_IdMarca"].ToString();
            }
            else
            {
            }

            txtDescripcion.Focus();
        }
    }

    void ListarMarca()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Marca_Combo", conexion);
        da.Fill(dt);
        ddlMarca.DataSource = dt;
        ddlMarca.DataTextField = "v_NombreMarca";
        ddlMarca.DataValueField = "i_IdMarca";
        ddlMarca.DataBind();
        ddlMarca.SelectedIndex = 0;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarModelo.aspx");
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
                cmd.CommandText = "Play_Modelo_Actualizar";
                cmd.Parameters.AddWithValue("@i_IdModelo", lblCodigo.Text);
                cmd.Parameters.AddWithValue("@i_IdMarca", ddlMarca.SelectedValue);
                cmd.Parameters.AddWithValue("@v_NombreModelo", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Modelo actualizado.' });</script>", false);
            }
            else
            {
                string i_IdModelo = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Modelo_Registrar";
                cmd.Parameters.AddWithValue("@i_IdMarca", ddlMarca.SelectedValue);
                cmd.Parameters.AddWithValue("@v_NombreModelo", txtDescripcion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", chkEstado.Checked);
                conexion.Open();
                i_IdModelo = cmd.ExecuteScalar().ToString();
                conexion.Close();
                lblCodigo.Text = i_IdModelo;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Modelo registrado.' });</script>", false);
            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El código interno ya se encuentra en uso!' });</script>", false);
        }
    }
}