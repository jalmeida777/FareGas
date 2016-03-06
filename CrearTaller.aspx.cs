using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearTaller : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["i_IdPersona"] != null)
            {
                int i_IdPersona = int.Parse(Request.QueryString["i_IdPersona"].ToString());

                DataTable dtPersona = new DataTable();
                SqlDataAdapter daPersona = new SqlDataAdapter("select v_Nombres from persona where i_IdPersona = " + i_IdPersona.ToString(), conexion);
                daPersona.Fill(dtPersona);
                lblNombreTaller.Text = dtPersona.Rows[0]["v_Nombres"].ToString();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_TallerPrecio_Listar " + i_IdPersona.ToString(), conexion);
                da.Fill(dt);
                gv.DataSource = dt;
                gv.DataBind();

                CheckBox chk = new CheckBox();
                TextBox txt = new TextBox();

                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    chk = (CheckBox)gv.Rows[i].FindControl("chkEstado");
                    if (chk.Checked == true)
                    {
                        txt = (TextBox)gv.Rows[i].FindControl("txtPrecioConvenio");
                        gv.Rows[i].BackColor = System.Drawing.Color.AliceBlue;
                        txt.Enabled = true;
                    }
                }

            }

           
        }

    }


    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListarTaller.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        int i_IdPersona = int.Parse(Request.QueryString["i_IdPersona"].ToString());

        //Registrar Lista de Precios al Contado
        TextBox PrecioConvenioContado = new TextBox();
        CheckBox chk = new CheckBox();

        for (int i = 0; i < gv.Rows.Count; i++)
        {
            chk = (CheckBox)gv.Rows[i].FindControl("chkEstado");

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conexion;
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "Play_TallerPrecio_Actualizar";
            cmd2.Parameters.AddWithValue("@i_IdTallerPrecio", gv.DataKeys[i].Value.ToString());
            PrecioConvenioContado = (TextBox)gv.Rows[i].FindControl("txtPrecioConvenio");
            cmd2.Parameters.AddWithValue("@f_PrecioTaller", PrecioConvenioContado.Text);
            cmd2.Parameters.AddWithValue("@b_Estado", chk.Checked);
            conexion.Open();
            cmd2.ExecuteNonQuery();
            conexion.Close();
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Precios actualizados' });</script>", false);

    }
    protected void chkEstado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = new CheckBox();
        TextBox txt = new TextBox();
        double precioLista = 0;
        for (int i = 0; i < gv.Rows.Count; i++)
        {

            chk = (CheckBox)gv.Rows[i].FindControl("chkEstado");
            if (chk.Checked == true)
            {
                txt = (TextBox)gv.Rows[i].FindControl("txtPrecioConvenio");
                txt.Enabled = true;
                gv.Rows[i].BackColor = System.Drawing.Color.AliceBlue;
                txt.Focus();
            }
            else
            {
                precioLista = double.Parse(gv.Rows[i].Cells[1].Text);
                txt = (TextBox)gv.Rows[i].FindControl("txtPrecioConvenio");
                txt.Text = precioLista.ToString("N2");
                gv.Rows[i].BackColor = System.Drawing.Color.White;
                txt.Enabled = false;
            }
        }
    }
}