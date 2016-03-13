using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearComprobante : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            ListarFormaPago();
            ListarMoneda();
            ListarEmpresa();

            if (Request.QueryString["n_IdPedido"] != null)
            {
                decimal n_IdPedido = decimal.Parse(Request.QueryString["n_IdPedido"]);
                hfPedido.Value = n_IdPedido.ToString();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Faregas_Pedido_Seleccionar " + n_IdPedido, conexion);
                da.Fill(dt);
                ddlEmpresa.SelectedValue = dt.Rows[0]["i_IdEmpresa"].ToString();
                hfCliente.Value = dt.Rows[0]["i_IdCliente"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dt.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                ddlFormaPago.SelectedValue = dt.Rows[0]["n_IdFormaPago"].ToString();
                ddlMoneda.SelectedValue = dt.Rows[0]["n_IdMoneda"].ToString();

                hfSubTotal.Value = dt.Rows[0]["f_SubTotal"].ToString();
                hfIgv.Value = dt.Rows[0]["f_Impuesto"].ToString();
                hfTotal.Value = dt.Rows[0]["f_Total"].ToString();

                lblSubTotal.Text = double.Parse(dt.Rows[0]["f_SubTotal"].ToString()).ToString("N2");
                lblIGV.Text = double.Parse(dt.Rows[0]["f_Impuesto"].ToString()).ToString("N2");
                lblTotal.Text = double.Parse(dt.Rows[0]["f_Total"].ToString()).ToString("N2");


                MontoEscrito m = new MontoEscrito();
                
                string decimales;
                decimales = lblTotal.Text.Substring(lblTotal.Text.Length - 2, 2);
                lblMontoEscrito.Text = m.totext(double.Parse(lblTotal.Text)) + " " + decimales + "/100 " + ddlMoneda.SelectedItem.Text.ToUpper();


                lblPlaca.Text = dt.Rows[0]["v_NroPlaca"].ToString();

                    btnGuardar.Visible = true;
                    btnAnular.Visible = false;
                    btnImprimir.Visible = false;
                
                lblNombreCliente.Text = dt.Rows[0]["Cliente"].ToString();
                lblDni.Text = dt.Rows[0]["v_DocumentoIdentidad"].ToString();
                lblDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
                rblTipoComprobante.SelectedValue = dt.Rows[0]["v_TipoDocumento"].ToString();

                if (rblTipoComprobante.SelectedValue.Trim() == "Boleta")
                {
                    Label1.Text = "Boleta N° ";
                }
                else 
                {
                    Label1.Text = "Factura N° ";
                }

                rblTipoComprobante.Enabled = false;

                DataTable dtDetalle = new DataTable();
                SqlDataAdapter daDetalle = new SqlDataAdapter("Play_DetPedido_Listar " + n_IdPedido, conexion);
                daDetalle.Fill(dtDetalle);
                gv.DataSource = dtDetalle;
                Session["Detalle"] = dtDetalle;
                gv.DataBind();
                gv.Enabled = false;

                
            }
            if (Request.QueryString["i_IdComprobante"] != null) 
            {
                int i_IdComprobante = int.Parse(Request.QueryString["i_IdComprobante"]);
                hfComprobante.Value = i_IdComprobante.ToString();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("FG_Comprobante_Seleccionar " + i_IdComprobante, conexion);
                da.Fill(dt);

                lblNumeroComprobante.Text = dt.Rows[0]["v_NumeroComprobante"].ToString();
                ddlEmpresa.SelectedValue = dt.Rows[0]["i_IdEmpresa"].ToString();
                hfCliente.Value = dt.Rows[0]["i_IdCliente"].ToString();
                txtFechaInicial.Text = DateTime.Parse(dt.Rows[0]["d_FechaEmision"].ToString()).ToShortDateString();
                ddlFormaPago.SelectedValue = dt.Rows[0]["n_IdFormaPago"].ToString();
                ddlMoneda.SelectedValue = dt.Rows[0]["n_IdMoneda"].ToString();
                lblSubTotal.Text = double.Parse(dt.Rows[0]["f_SubTotal"].ToString()).ToString("N2");
                lblIGV.Text = double.Parse(dt.Rows[0]["f_Impuesto"].ToString()).ToString("N2");
                lblTotal.Text = double.Parse(dt.Rows[0]["f_Total"].ToString()).ToString("N2");
                lblPlaca.Text = dt.Rows[0]["v_NroPlaca"].ToString();
                lblNombreCliente.Text = dt.Rows[0]["v_Nombres"].ToString();
                lblDni.Text = dt.Rows[0]["v_DocumentoIdentidad"].ToString();
                lblDireccion.Text = dt.Rows[0]["v_Direccion"].ToString();
                rblTipoComprobante.SelectedValue = dt.Rows[0]["v_TipoDocumento"].ToString();

                if (rblTipoComprobante.SelectedValue.Trim() == "Boleta")
                {
                    Label1.Text = "Boleta N° ";
                }
                else 
                {
                    Label1.Text = "Factura N° ";
                }

                rblTipoComprobante.Enabled = false;
                lblMontoEscrito.Text = dt.Rows[0]["t_MontoEscrito"].ToString();
                txtObservacion.Text = dt.Rows[0]["t_Obs"].ToString();
                txtObservacion.Enabled = false;

                bool Estado = bool.Parse(dt.Rows[0]["b_Estado"].ToString());
                if (Estado == true)
                {
                    lblEstado.Text = "Registrado";
                    lblEstado.ForeColor = System.Drawing.Color.Green;
                    btnGuardar.Visible = false;
                    btnAnular.Visible = true;
                    btnImprimir.Visible = true;
                }
                else
                {
                    lblEstado.Text = "Anulado";
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    btnGuardar.Visible = false;
                    btnAnular.Visible = false;
                    btnImprimir.Visible = false;
                    Label1.ForeColor = System.Drawing.Color.Red;
                    lblNumeroComprobante.ForeColor = System.Drawing.Color.Red;
                }

                lblUsuarioRegistro.Text = dt.Rows[0]["v_Nombre"].ToString();
                lblFechaRegistro.Text = dt.Rows[0]["d_FechaRegistra"].ToString();
                ibUsuarioRegistro.ImageUrl = dt.Rows[0]["v_RutaFoto"].ToString();

                DataTable dtDetalle = new DataTable();
                SqlDataAdapter daDetalle = new SqlDataAdapter("Play_DetComprobante_Listar_2 " + i_IdComprobante, conexion);
                daDetalle.Fill(dtDetalle);
                gv.DataSource = dtDetalle;
                Session["Detalle"] = dtDetalle;
                gv.DataBind();
                gv.Enabled = false;
            }
        }
    }

    public void ListarFormaPago()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_FormaPago_Combo", conexion);
        da.Fill(dt);

        ddlFormaPago.DataSource = dt;
        ddlFormaPago.DataTextField = "v_FormaPago";
        ddlFormaPago.DataValueField = "n_IdFormaPago";
        ddlFormaPago.DataBind();
        ddlFormaPago.SelectedIndex = 0;
    }

    public void ListarMoneda()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Moneda_Combo", conexion);
        da.Fill(dt);
        ddlMoneda.DataSource = dt;
        ddlMoneda.DataTextField = "v_DescripcionMoneda";
        ddlMoneda.DataValueField = "n_IdMoneda";
        ddlMoneda.DataBind();
        ddlMoneda.SelectedIndex = 0;
    }

    public void ListarEmpresa()
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

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];

            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();
            string Usuario = dtUsuario.Rows[0]["v_Usuario"].ToString();

            SqlTransaction tran;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
            cn.Open();
            tran = cn.BeginTransaction();

            try
            {
                //Registrar Cabecera
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Faregas_Comprobante_Registrar";
                cmd.Parameters.AddWithValue("@i_IdEmpresa", ddlEmpresa.SelectedValue);
                cmd.Parameters.AddWithValue("@v_TipoDocumento", rblTipoComprobante.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdPedido", hfPedido.Value);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@i_IdCliente", hfCliente.Value);
                cmd.Parameters.AddWithValue("@n_IdFormaPago", ddlFormaPago.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMoneda", ddlMoneda.SelectedValue);
                cmd.Parameters.AddWithValue("@f_SubTotal", hfSubTotal.Value);
                cmd.Parameters.AddWithValue("@f_Impuesto", hfIgv.Value);
                cmd.Parameters.AddWithValue("@f_Total", hfTotal.Value);
                cmd.Parameters.AddWithValue("@v_NroPlaca", lblPlaca.Text);
                cmd.Parameters.AddWithValue("@t_Obs", txtObservacion.Text);
                cmd.Parameters.AddWithValue("@t_MontoEscrito", lblMontoEscrito.Text);
                cmd.Parameters.AddWithValue("@n_IdUsuarioRegistra", n_IdUsuario);
                string i_IdComprobante = cmd.ExecuteScalar().ToString();
                hfComprobante.Value = i_IdComprobante;
                cmd.Dispose();

                //Obtener el número del comprobante
                string NumeroComprobante = "";
                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroComprobante from Comprobante where i_IdComprobante  = " + i_IdComprobante;
                NumeroComprobante = cmd0.ExecuteScalar().ToString();
                cmd0.Dispose();

                DataTable dt = new DataTable();
                dt = (DataTable)Session["Detalle"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection = cn;
                    cmdDetalle.Transaction = tran;
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.CommandText = "Play_DetComprobante_Registrar";
                    cmdDetalle.Parameters.AddWithValue("@i_IdComprobante", i_IdComprobante);
                    cmdDetalle.Parameters.AddWithValue("@n_IdProducto", dt.Rows[i]["n_IdProducto"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@i_Cantidad", dt.Rows[i]["i_Cantidad"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_PrecioUnitario", dt.Rows[i]["f_PrecioUnitario"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_PrecioTotal", dt.Rows[i]["f_PrecioTotal"].ToString());
                    cmdDetalle.ExecuteNonQuery();
                    cmdDetalle.Dispose();
                }

                //Actualizar Correlativo del Comprobante
                SqlCommand cmdCorrelativo = new SqlCommand();
                cmdCorrelativo.Connection = cn;
                cmdCorrelativo.Transaction = tran;
                cmdCorrelativo.CommandType = CommandType.StoredProcedure;
                cmdCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                if (rblTipoComprobante.SelectedItem.Text == "Boleta")
                {
                    cmdCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 3);//Boleta
                }
                else if (rblTipoComprobante.SelectedItem.Text == "Factura")
                {
                    cmdCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 2);//Factura
                }
                cmdCorrelativo.Parameters.AddWithValue("@i_IdEmpresa", ddlEmpresa.SelectedValue);
                cmdCorrelativo.ExecuteNonQuery();
                cmdCorrelativo.Dispose();

                tran.Commit();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Comprobante registrado correctamente.' });</script>", false);
                lblNumeroComprobante.Text = NumeroComprobante;
                btnGuardar.Enabled = false;
                btnAnular.Visible = true;
                btnImprimir.Visible = true;
                lblEstado.Text = "Registrado";
                lblEstado.ForeColor = System.Drawing.Color.Green;
                lblFechaRegistro.Text = DateTime.Now.ToShortDateString();
                lblUsuarioRegistro.Text = Usuario;
                txtObservacion.Enabled = false;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
            }
            finally
            {
                cn.Close();
            }
        }
    }

    protected void btnAnular_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            //Anular el comprobante
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Comprobante_Anular";
                cmd.Parameters.AddWithValue("@i_IdComprobante", hfComprobante.Value);
                cmd.Parameters.AddWithValue("@n_IdUsuarioAnula", n_IdUsuario);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Comprobante anulado correctamente.' });</script>", false);
                btnAnular.Enabled = false;
                btnImprimir.Enabled = false;
                btnGuardar.Enabled = false;
                Label1.ForeColor = System.Drawing.Color.Red;
                lblNumeroComprobante.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "Anulado";
                lblEstado.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
            }
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);
        Response.Redirect("~/ImprimirComprobante.aspx?i_IdComprobante=" + hfComprobante.Value + "&IdMenu=" + i_IdMenu);
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);
        Response.Redirect("ListarComprobante.aspx?IdMenu=" + i_IdMenu);
    }
}