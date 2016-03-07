using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearPedido : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            txtCumpleañosCliente.Text = DateTime.Now.ToShortDateString();
            txtCumpleañosPropietario.Text = DateTime.Now.ToShortDateString();
            ListarFormaPago();
            ListarMoneda();
            ListarEmpresa();
            ListarCombustible();
            ListarClaseVehiculo();
            ListarCategoria();
            ddlCategoria_SelectedIndexChanged(null, null);
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

    public void ListarCombustible()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Faregas_Combustible_Combo", conexion);
        da.Fill(dt);
        ddlCombustibleInicial.DataSource = dt;
        ddlCombustibleInicial.DataTextField = "v_NombreCombustible";
        ddlCombustibleInicial.DataValueField = "i_IdCombustible";
        ddlCombustibleInicial.DataBind();
        ddlCombustibleInicial.SelectedIndex = 0;

        ddlCombustibleFinal.DataSource = dt;
        ddlCombustibleFinal.DataTextField = "v_NombreCombustible";
        ddlCombustibleFinal.DataValueField = "i_IdCombustible";
        ddlCombustibleFinal.DataBind();
        ddlCombustibleFinal.SelectedIndex = 0;
    }

    public void ListarClaseVehiculo()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Faregas_ClaseVehiculo_Combo", conexion);
        da.Fill(dt);

        ddlClase.DataSource = dt;
        ddlClase.DataTextField = "v_NombreClase";
        ddlClase.DataValueField = "i_IdClaseVehiculo";
        ddlClase.DataBind();
        ddlClase.SelectedIndex = 0;
    }

    public void ListarProductos() 
    {
        string i_IdEmpresa = ddlEmpresa.SelectedValue;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Producto_Combo " + i_IdEmpresa, conexion);
        da.Fill(dt);
        ddlProducto.DataSource = dt;
        ddlProducto.DataTextField = "v_Descripcion";
        ddlProducto.DataValueField = "i_IdProducto";
        ddlProducto.DataBind();
    }

    public void ListarCategoria()
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

    public void ListarCarrocerias() 
    {
        string i_IdCategoria = ddlCategoria.SelectedValue;
        SqlDataAdapter da = new SqlDataAdapter("Faregas_Carroceria_Combo " + i_IdCategoria, conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ddlCarroceria.DataSource = dt;
        ddlCarroceria.DataTextField = "v_NombreCarroceria";
        ddlCarroceria.DataValueField = "i_IdCarroceria";
        ddlCarroceria.DataBind();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (hdnValue.Value == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Primero debe registrar el cliente.' });</script>", false);
            txtCliente.Focus();
            return;
        }
        if (txtFechaInicial.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la fecha.' });</script>", false);
            txtFechaInicial.Focus();
            return;
        }
        if (btnVerificarEmpresa.Visible == true) 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Primero debe seleccionar una empresa.' });</script>", false);
            ddlEmpresa.Focus();
            return;
        }
        if (gv.Rows.Count == 0) 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay nada para vender.' });</script>", false);
            return;
        }
        if (txtPago.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No olvide ingresar el monto Pagó con:.' });</script>", false);
            txtPago.Focus();
            return;
        }
        if (txtPlaca.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de placa.' });</script>", false);
            txtPlaca.Focus();
            return;
        }
        if (txtAnioFabricacion.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el año de fabricación.' });</script>", false);
            txtAnioFabricacion.Focus();
            return;
        }
        if (hfMarca.Value == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la marca del vehículo.' });</script>", false);
            return;
        }
        if (hfModelo.Value == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el modelo del vehículo.' });</script>", false);
            return;
        }
        if (txtNumeroSerie.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de serie.' });</script>", false);
            txtNumeroSerie.Focus();
            return;
        }
        if (txtNumeroMotor.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de motor.' });</script>", false);
            txtNumeroMotor.Focus();
            return;
        }
        if (txtColor.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el color.' });</script>", false);
            txtColor.Focus();
            return;
        }
        if (txtPotencia.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la potencia.' });</script>", false);
            txtPotencia.Focus();
            return;
        }
        if (txtVersion.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la versión.' });</script>", false);
            txtVersion.Focus();
            return;
        }
        if (txtCilindrada.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la cilindrada.' });</script>", false);
            txtCilindrada.Focus();
            return;
        }
        if (txtAsientos.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de asientos.' });</script>", false);
            txtAsientos.Focus();
            return;
        }
        if (txtPasajeros.Text == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de pasajeros.' });</script>", false);
            txtPasajeros.Focus();
            return;
        }
        if (txtLongitud.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la longitud.' });</script>", false);
            txtLongitud.Focus();
            return;
        }
        if (txtAncho.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el ancho.' });</script>", false);
            txtAncho.Focus();
            return;
        }
        if (txtAltura.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la altura.' });</script>", false);
            txtAltura.Focus();
            return;
        }
        if (txtEjes.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de ejes.' });</script>", false);
            txtEjes.Focus();
            return;
        }
        if (txtRuedas.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de ruedas.' });</script>", false);
            txtRuedas.Focus();
            return;
        }
        if (txtCilindros.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de cilindros.' });</script>", false);
            txtCilindros.Focus();
            return;
        }
        if (txtPesoSecoInicial.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar peso seco inicial.' });</script>", false);
            txtPesoSecoInicial.Focus();
            return;
        }
        if (txtCargaUtilInicial.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la carga util inicial.' });</script>", false);
            txtCargaUtilInicial.Focus();
            return;
        }
        if (txtPesoBrutoInicial.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el peso bruto inicial.' });</script>", false);
            txtPesoBrutoInicial.Focus();
            return;
        }

        if (txtNumeroDocumentoPropietario.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de documento del propietario.' });</script>", false);
            txtNumeroDocumentoPropietario.Focus();
            return;
        }
        if (txtNombrePropietario.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre del propietario.' });</script>", false);
            txtNombrePropietario.Focus();
            return;
        }

        if (Session["dtUsuario"] != null && Session["Detalle"]!=null)
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
                if (hdnPropietario.Value == "")
                {
                    //Registrar datos del propietario
                    string i_IdPersona = "0";
                    SqlCommand cmdPropietario = new SqlCommand();
                    cmdPropietario.Connection = cn;
                    cmdPropietario.Transaction = tran;
                    cmdPropietario.CommandType = CommandType.StoredProcedure;
                    cmdPropietario.CommandText = "Play_Cliente_Registrar2_Completo";
                    cmdPropietario.Parameters.AddWithValue("@v_Nombres", txtNombrePropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_DocumentoIdentidad", txtNumeroDocumentoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Distrito", txtDistritoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Direccion", txtDireccionPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Telefono", txtTelefonoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Celular", txtCelularPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Email", txtEmailPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Contacto", txtContactoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@d_FechaNacimiento", DateTime.Parse(txtCumpleañosPropietario.Text));
                    cmdPropietario.Parameters.AddWithValue("@c_Genero", rblSexoPropietario.SelectedValue);
                    cmdPropietario.Parameters.AddWithValue("@t_Comentario", txtComentarioPropietario.Text.Trim().ToUpper());
                    conexion.Open();
                    i_IdPersona = cmdPropietario.ExecuteScalar().ToString();
                    conexion.Close();
                    cmdPropietario.Dispose();
                    hdnPropietario.Value = i_IdPersona;
                }
                else 
                {
                    //Actualizar datos del propietario
                    SqlCommand cmdPropietario = new SqlCommand();
                    cmdPropietario.Connection = cn;
                    cmdPropietario.Transaction = tran;
                    cmdPropietario.CommandType = CommandType.StoredProcedure;
                    cmdPropietario.CommandText = "Play_Cliente2_Actualizar";
                    cmdPropietario.Parameters.AddWithValue("@i_IdPersona", hdnPropietario.Value);
                    cmdPropietario.Parameters.AddWithValue("@v_Nombres", txtNombrePropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_DocumentoIdentidad", txtNumeroDocumentoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Distrito", txtDistritoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Direccion", txtDireccionPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Telefono", txtTelefonoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Celular", txtCelularPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Email", txtEmailPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@v_Contacto", txtContactoPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@d_FechaNacimiento", DateTime.Parse(txtCumpleañosPropietario.Text));
                    cmdPropietario.Parameters.AddWithValue("@c_Genero", rblSexoPropietario.SelectedValue);
                    cmdPropietario.Parameters.AddWithValue("@t_Comentario", txtComentarioPropietario.Text.Trim().ToUpper());
                    cmdPropietario.Parameters.AddWithValue("@b_Estado", 1);
                    cmdPropietario.Parameters.AddWithValue("@b_Tipo", 0);
                    cmdPropietario.ExecuteNonQuery();
                    cmdPropietario.Dispose();
                }

                //Registrar Cabecera
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Faregas_Pedido_Registrar";
                cmd.Parameters.AddWithValue("@i_IdEmpresa", ddlEmpresa.SelectedValue);
                cmd.Parameters.AddWithValue("@v_NumeroCertificado", txtNumeroCertificado.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_NumeroHoja", txtNumeroHoja.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@n_IdFormaPago", ddlFormaPago.SelectedValue);
                cmd.Parameters.AddWithValue("@n_IdMoneda", ddlMoneda.SelectedValue);
                cmd.Parameters.AddWithValue("@f_SubTotal", double.Parse(lblSubTotal.Text));
                cmd.Parameters.AddWithValue("@f_Impuesto", double.Parse(lblIGV.Text));
                cmd.Parameters.AddWithValue("@f_Total", double.Parse(lblTotal.Text));
                cmd.Parameters.AddWithValue("@f_Pago", double.Parse(txtPago.Text));
                cmd.Parameters.AddWithValue("@f_Vuelto", double.Parse(lblVuelto.Text));
                cmd.Parameters.AddWithValue("@v_NroPlaca", txtPlaca.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@i_IdCategoria", ddlCategoria.SelectedValue);
                cmd.Parameters.AddWithValue("@i_IdMarca", hfMarca.Value);
                cmd.Parameters.AddWithValue("@i_IdModelo", hfModelo.Value);
                cmd.Parameters.AddWithValue("@i_IdClaseVehiculo", ddlClase.SelectedValue);
                cmd.Parameters.AddWithValue("@i_IdCarroceria", ddlCarroceria.SelectedValue);
                cmd.Parameters.AddWithValue("@v_Potencia", txtPotencia.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Version", txtVersion.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@i_Anio", int.Parse(txtAnioFabricacion.Text));
                cmd.Parameters.AddWithValue("@v_Serie", txtNumeroSerie.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Motor", txtNumeroMotor.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Color", txtColor.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@i_Cilindros", int.Parse(txtCilindros.Text));
                cmd.Parameters.AddWithValue("@f_Cilindrada", float.Parse(txtCilindrada.Text));
                cmd.Parameters.AddWithValue("@i_Ejes", int.Parse(txtEjes.Text));
                cmd.Parameters.AddWithValue("@i_Ruedas", int.Parse(txtRuedas.Text));
                cmd.Parameters.AddWithValue("@i_Asientos", int.Parse(txtAsientos.Text));
                cmd.Parameters.AddWithValue("@i_Pasajeros", int.Parse(txtPasajeros.Text));
                cmd.Parameters.AddWithValue("@f_Largo", float.Parse(txtLongitud.Text));
                cmd.Parameters.AddWithValue("@f_Ancho", float.Parse(txtAncho.Text));
                cmd.Parameters.AddWithValue("@f_Alto", float.Parse(txtAltura.Text));
                cmd.Parameters.AddWithValue("@i_IdCombustible", ddlCombustibleInicial.SelectedValue);

                cmd.Parameters.AddWithValue("@i_PesoNetoInicial", int.Parse(txtPesoSecoInicial.Text));

                if (txtPesoSecoFinal.Text == "")
                {
                    cmd.Parameters.AddWithValue("@i_PesoNetoFinal", 0);
                    txtPesoSecoFinal.Text = "0";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@i_PesoNetoFinal", int.Parse(txtPesoSecoFinal.Text));
                }

                cmd.Parameters.AddWithValue("@i_PesoBrutoInicial", int.Parse(txtPesoBrutoInicial.Text));

                if (txtPesoBrutoFinal.Text == "")
                {
                    cmd.Parameters.AddWithValue("@i_PesoBrutoFinal", 0);
                    txtPesoBrutoFinal.Text = "0";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@i_PesoBrutoFinal", int.Parse(txtPesoBrutoFinal.Text));
                }

                cmd.Parameters.AddWithValue("@i_CargaUtilInicial", int.Parse(txtCargaUtilInicial.Text));

                if (txtCargaUtilFinal.Text == "")
                {
                    cmd.Parameters.AddWithValue("@i_CargaUtilFinal", 0);
                    txtCargaUtilFinal.Text = "0";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@i_CargaUtilFinal", int.Parse(txtCargaUtilFinal.Text));
                }
                cmd.Parameters.AddWithValue("@v_Propietario", txtNombrePropietario.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@n_IdUsuarioRegistra", n_IdUsuario);
                cmd.Parameters.AddWithValue("@t_Obs", txtObservacion.Text);
                cmd.Parameters.AddWithValue("@v_RangoHojasInicial", txtNumeroHojaInicial.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_RangoHojasFinal", txtNumeroHojaFinal.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@i_IdPropietario", hdnPropietario.Value);
                cmd.Parameters.AddWithValue("@i_IdCliente", hdnValue.Value);
                string n_IdPedido = cmd.ExecuteScalar().ToString();
                cmd.Dispose();

                //Obtener el número del pedido
                string NumeroPedido = "";
                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroPedido from Pedido where n_IdPedido = " + n_IdPedido;
                NumeroPedido = cmd0.ExecuteScalar().ToString();
                cmd0.Dispose();

                //Registrar Detalle
                DataTable dt = new DataTable();
                dt = (DataTable)Session["Detalle"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Connection = cn;
                    cmdDetalle.Transaction = tran;
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.CommandText = "Play_DetPedido_Registrar";
                    cmdDetalle.Parameters.AddWithValue("@n_IdPedido", n_IdPedido);
                    cmdDetalle.Parameters.AddWithValue("@n_IdProducto", dt.Rows[i]["n_IdProducto"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@i_Cantidad", dt.Rows[i]["i_Cantidad"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_PrecioUnitario", dt.Rows[i]["f_PrecioUnitario"].ToString());
                    cmdDetalle.Parameters.AddWithValue("@f_PrecioTotal", dt.Rows[i]["f_PrecioTotal"].ToString());
                    cmdDetalle.ExecuteNonQuery();
                    cmdDetalle.Dispose();
                }

                //Actualizar Correlativo del Pedido
                SqlCommand cmdCorrelativo = new SqlCommand();
                cmdCorrelativo.Connection = cn;
                cmdCorrelativo.Transaction = tran;
                cmdCorrelativo.CommandType = CommandType.StoredProcedure;
                cmdCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                cmdCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 1);//Pedido
                cmdCorrelativo.Parameters.AddWithValue("@i_IdEmpresa", ddlEmpresa.SelectedValue);
                cmdCorrelativo.ExecuteNonQuery();
                cmdCorrelativo.Dispose();

                tran.Commit();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de servicio registrada correctamente.' });</script>", false);
                TabContainer1.Enabled = false;
                lblNumeroPedido.Text = NumeroPedido;
                btnGuardar.Enabled = false;
                btnAnular.Visible = true;
                btnImprimir.Visible = true;
                lblEstado.Text = "Registrado";
                lblEstado.ForeColor = System.Drawing.Color.Green;
                lblFechaRegistro.Text = DateTime.Now.ToShortDateString();
                lblUsuarioRegistro.Text = Usuario;
            }
            catch (Exception)
            {
                tran.Rollback();
            }
            finally 
            {
                cn.Close();
            }
            
        }
    }

    void InicializarGrilla()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("n_IdProducto", typeof(int));
        dt.Columns.Add("i_Cantidad", typeof(int));
        dt.Columns.Add("v_Descripcion", typeof(string));
        dt.Columns.Add("f_PrecioUnitario", typeof(double));
        dt.Columns.Add("f_PrecioTotal", typeof(double));
        Session["Detalle"] = dt;
        ListarProductos();
        ddlProducto_SelectedIndexChanged1(null, null);
    }

    protected void btnVerificarEmpresa_Click(object sender, ImageClickEventArgs e)
    {
        ddlEmpresa.Enabled = false;
        btnVerificarEmpresa.Visible = false;
        InicializarGrilla();
        panelServicio.Visible = true;
    }

    protected void ddlProducto_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string i_IdProducto = "";
        i_IdProducto = ddlProducto.SelectedValue;

        double PrecioUnitario = 0;
        DataTable dt1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Faregas_Precio_Grilla " + i_IdProducto, conexion);
        da.Fill(dt1);

        PrecioUnitario = double.Parse(dt1.Rows[0]["f_precio"].ToString());

        lblPrecio.Text = PrecioUnitario.ToString("N2");

    }

    protected void btnAgregarProducto_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCantidad.Text == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar la cantidad.' });</script>", false);
            txtCantidad.Focus();
            return;
        }
        if (int.Parse(txtCantidad.Text) == 0) 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar una cantidad mayor a cero.' });</script>", false);
            txtCantidad.Focus();
            return;
        }


        DataTable dt = new DataTable();
        dt = (DataTable)Session["Detalle"];

        string n_IdProducto = ddlProducto.SelectedValue;
        string n_IdProductoTabla = "";
        bool encontrado = false;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            n_IdProductoTabla = dt.Rows[i]["n_IdProducto"].ToString();
            if (n_IdProducto.Trim() == n_IdProductoTabla.Trim())
            {
                encontrado = true;
                break;
            }
        }

        if (encontrado == false)
        {
            DataRow dr;
            dr = dt.NewRow();
            dr["n_IdProducto"] = int.Parse(ddlProducto.SelectedValue);
            dr["i_Cantidad"] = int.Parse(txtCantidad.Text);
            dr["v_Descripcion"] = ddlProducto.SelectedItem.Text;
            dr["f_PrecioUnitario"] = double.Parse(lblPrecio.Text);
            double total = int.Parse(txtCantidad.Text) * double.Parse(lblPrecio.Text);
            dr["f_PrecioTotal"] = total;
            dt.Rows.Add(dr);
            gv.DataSource = dt;
            gv.DataBind();
            CalcularTotales();
        }
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Producto Repetido' });</script>", false);
        }
    }

    void CalcularTotales() 
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["Detalle"];

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                double subtotal = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    subtotal = subtotal + double.Parse(dt.Rows[i]["f_PrecioUnitario"].ToString());
                }
                lblSubTotal.Text = subtotal.ToString("N2");

                double igv = 0;
                if (rblTipoComprobante.SelectedItem.Text == "Boleta")
                {
                    igv = 0;
                    lblIGV.Text = "0.00";
                }
                else if (rblTipoComprobante.SelectedItem.Text == "Factura")
                {
                    igv = subtotal * double.Parse(lblIgvPorc.Text) / 100;
                    lblIGV.Text = igv.ToString("N2");
                }
                double total = 0;
                total = subtotal + igv;
                lblTotal.Text = total.ToString("N2");
                txtPago.Text = total.ToString("N2");
                lblVuelto.Text = "0.00";
            }
            else
            {
                lblSubTotal.Text = "0.00";
                lblIGV.Text = "0.00";
                lblTotal.Text = "0.00";
                txtPago.Text = "0.00";
                lblVuelto.Text = "0.00";
            }
        }
        else 
        {
            lblSubTotal.Text = "0.00";
            lblIGV.Text = "0.00";
            lblTotal.Text = "0.00";
            txtPago.Text = "0.00";
            lblVuelto.Text = "0.00";
        }
    }

    protected void rblTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoComprobante.SelectedItem.Text == "Boleta")
        {
            lblIgvPorc.Text = "00";
        }
        else if (rblTipoComprobante.SelectedItem.Text == "Factura")
        {
            SqlDataAdapter da = new SqlDataAdapter("CQ_Parametros_Select", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lblIgvPorc.Text = dt.Rows[0]["f_Igv"].ToString();
        }
        CalcularTotales();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["Detalle"] != null)
        {
            try
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle = (DataTable)Session["Detalle"];
                dtDetalle.Rows.RemoveAt(e.RowIndex);
                Session["Detalle"] = dtDetalle;
                gv.DataSource = dtDetalle;
                gv.DataBind();
                CalcularTotales();
            }
            catch (Exception)
            {

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
    }

    protected void btnSalirCliente_Click(object sender, ImageClickEventArgs e)
    {
        tblCliente.Visible = false;
        tblGeneral.Visible = true;
        toolbar.Visible = true;
    }

    protected void btnCliente_Click(object sender, ImageClickEventArgs e)
    {
        if (hdnValue.Value.Trim() == "")
        {

        }
        else
        {
            //Consultar datos del cliente y mostrarlos
            string i_IdPersona = hdnValue.Value;
            SqlDataAdapter da = new SqlDataAdapter("Play_Cliente2_Seleccionar " + i_IdPersona, conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    rblSexoCliente.SelectedValue = dt.Rows[0]["c_Genero"].ToString();
                    txtNombreCliente.Text = dt.Rows[0]["v_Nombres"].ToString();
                    txtNumeroDocumentoCliente.Text = dt.Rows[0]["v_DocumentoIdentidad"].ToString();
                    txtDistritoCliente.Text = dt.Rows[0]["v_Distrito"].ToString();
                    txtDireccionCliente.Text = dt.Rows[0]["v_Direccion"].ToString();
                    txtTelefonoCliente.Text = dt.Rows[0]["v_Telefono"].ToString();
                    txtCelularCliente.Text = dt.Rows[0]["v_Celular"].ToString();
                    txtEmailCliente.Text = dt.Rows[0]["v_Email"].ToString();
                    txtContactoCliente.Text = dt.Rows[0]["v_Contacto"].ToString();
                    if (dt.Rows[0]["d_FechaNacimiento"].ToString() != "")
                    {
                        txtCumpleañosCliente.Text = DateTime.Parse(dt.Rows[0]["d_FechaNacimiento"].ToString()).ToShortDateString();
                    }

                    txtComentarioCliente.Text = dt.Rows[0]["t_Comentario"].ToString();
                }
            }

            txtNombreCliente.Focus();
        }
        tblCliente.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);
        Response.Redirect("ListarPedidos.aspx?IdMenu=" + i_IdMenu);
    }

    protected void btnGuardarCliente_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNombreCliente.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el nombre del cliente.' });</script>", false);
            txtNombreCliente.Focus();
            return;
        }
        if (txtNumeroDocumentoCliente.Text.Trim() == "") 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de documento del cliente.' });</script>", false);
            txtNumeroDocumentoCliente.Focus();
            return;
        }
        try
        {
            if (hdnValue.Value.Trim() == "")
            {
                //Registrar cliente nuevo
                string i_IdPersona = "0";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Cliente_Registrar2_Completo";
                cmd.Parameters.AddWithValue("@v_Nombres", txtNombreCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_DocumentoIdentidad", txtNumeroDocumentoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Distrito", txtDistritoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccionCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefonoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Celular", txtCelularCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmailCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContactoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@d_FechaNacimiento", DateTime.Parse(txtCumpleañosCliente.Text));
                cmd.Parameters.AddWithValue("@c_Genero", rblSexoCliente.SelectedValue);
                cmd.Parameters.AddWithValue("@t_Comentario", txtComentarioCliente.Text.Trim().ToUpper());
                conexion.Open();
                i_IdPersona = cmd.ExecuteScalar().ToString();
                conexion.Close();
                cmd.Dispose();

                hdnValue.Value = i_IdPersona;
                tblCliente2.Visible = true;
                tblCliente1.Visible = false;
                lblNombreCliente.Text = txtNombreCliente.Text.Trim().ToUpper();

               
                //Mostrarlo como Propietario también
                rblSexoPropietario.SelectedValue = rblSexoCliente.SelectedValue;
                txtNombrePropietario.Text = txtNombreCliente.Text.Trim().ToUpper();
                txtNumeroDocumentoPropietario.Text = txtNumeroDocumentoCliente.Text.Trim().ToUpper();
                txtDistritoPropietario.Text = txtDistritoCliente.Text.Trim().ToUpper();
                txtDireccionPropietario.Text = txtDireccionCliente.Text.Trim().ToUpper();
                txtTelefonoPropietario.Text = txtTelefonoCliente.Text.Trim().ToUpper();
                txtCelularPropietario.Text = txtCelularCliente.Text.Trim().ToUpper();
                txtEmailPropietario.Text = txtEmailCliente.Text.Trim().ToUpper();
                txtContactoPropietario.Text = txtContactoCliente.Text.Trim().ToUpper();
                txtCumpleañosPropietario.Text = txtCumpleañosCliente.Text;
                txtComentarioPropietario.Text = txtComentarioCliente.Text.Trim().ToUpper();


                tblCliente.Visible = false;
                tblGeneral.Visible = true;
                toolbar.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Cliente registrado satisfactoriamente' });</script>", false);
            }
            else
            {
                string i_IdPersona = hdnValue.Value;

                //Actualizar datos del cliente
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Cliente2_Actualizar";
                cmd.Parameters.AddWithValue("@i_IdPersona", i_IdPersona);
                cmd.Parameters.AddWithValue("@v_Nombres", txtNombreCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_DocumentoIdentidad", txtNumeroDocumentoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Distrito", txtDistritoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Direccion", txtDireccionCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Telefono", txtTelefonoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Celular", txtCelularCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Email", txtEmailCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@v_Contacto", txtContactoCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@d_FechaNacimiento", DateTime.Parse(txtCumpleañosCliente.Text));
                cmd.Parameters.AddWithValue("@c_Genero", rblSexoCliente.SelectedValue);
                cmd.Parameters.AddWithValue("@t_Comentario", txtComentarioCliente.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@b_Estado", 1);
                cmd.Parameters.AddWithValue("@b_Tipo", 0);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
                cmd.Dispose();
                tblCliente.Visible = false;
                tblGeneral.Visible = true;
                toolbar.Visible = true;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Cliente actualizado satisfactoriamente' });</script>", false);
            }

            txtCliente.Text = txtNombreCliente.Text.Trim().ToUpper();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnVerificarCliente_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCliente.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de documento del cliente.' });</script>", false);
            txtCliente.Focus();
            return;
        }

        SqlDataAdapter da = new SqlDataAdapter("select i_IdPersona,v_Nombres from Persona where v_DocumentoIdentidad = '" + txtCliente.Text.Trim() + "'", conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hdnValue.Value = dt.Rows[0]["i_IdPersona"].ToString();
            lblNombreCliente.Text = dt.Rows[0]["v_Nombres"].ToString();
            tblCliente1.Visible = false;
            tblCliente2.Visible = true;

            //Mostrar como propietario también
            //Consultar datos del cliente y mostrarlos
            string i_IdPersona = hdnValue.Value;
            SqlDataAdapter daPersona = new SqlDataAdapter("Play_Cliente2_Seleccionar " + i_IdPersona, conexion);
            DataTable dtPersona = new DataTable();
            daPersona.Fill(dtPersona);
            if (dtPersona != null)
            {
                if (dtPersona.Rows.Count > 0)
                {
                    //Propietario
                    rblSexoPropietario.SelectedValue = dtPersona.Rows[0]["c_Genero"].ToString();
                    txtNombrePropietario.Text = dtPersona.Rows[0]["v_Nombres"].ToString();
                    txtNumeroDocumentoPropietario.Text = dtPersona.Rows[0]["v_DocumentoIdentidad"].ToString();
                    txtDistritoPropietario.Text = dtPersona.Rows[0]["v_Distrito"].ToString();
                    txtDireccionPropietario.Text = dtPersona.Rows[0]["v_Direccion"].ToString();
                    txtTelefonoPropietario.Text = dtPersona.Rows[0]["v_Telefono"].ToString();
                    txtCelularPropietario.Text = dtPersona.Rows[0]["v_Celular"].ToString();
                    txtEmailPropietario.Text = dtPersona.Rows[0]["v_Email"].ToString();
                    txtContactoPropietario.Text = dtPersona.Rows[0]["v_Contacto"].ToString();
                    if (dtPersona.Rows[0]["d_FechaNacimiento"].ToString() != "")
                    {
                        txtCumpleañosPropietario.Text = DateTime.Parse(dtPersona.Rows[0]["d_FechaNacimiento"].ToString()).ToShortDateString();
                    }

                    txtComentarioPropietario.Text = dtPersona.Rows[0]["t_Comentario"].ToString();
                }
            }

        }
        else 
        {
            tblCliente.Visible = true;
            tblGeneral.Visible = false;
            toolbar.Visible = false;
            txtNumeroDocumentoCliente.Text = txtCliente.Text.Trim();
            btnCliente.Visible = true;
        }
    }

    protected void btnVerificarPropietario_Click(object sender, ImageClickEventArgs e)
    {
        if (txtNumeroDocumentoPropietario.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe ingresar el número de documento del propietario.' });</script>", false);
            txtCliente.Focus();
            return;
        }

        SqlDataAdapter da = new SqlDataAdapter("select i_IdPersona,v_Nombres from Persona where v_DocumentoIdentidad = '" + txtNumeroDocumentoPropietario.Text.Trim() + "'", conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hdnPropietario.Value = dt.Rows[0]["i_IdPersona"].ToString();
            txtNumeroDocumentoPropietario.Enabled = false;

            //Mostrar como propietario también
            //Consultar datos del cliente y mostrarlos
            string i_IdPersona = hdnPropietario.Value;
            SqlDataAdapter daPersona = new SqlDataAdapter("Play_Cliente2_Seleccionar " + i_IdPersona, conexion);
            DataTable dtPersona = new DataTable();
            daPersona.Fill(dtPersona);
            if (dtPersona != null)
            {
                if (dtPersona.Rows.Count > 0)
                {
                    //Propietario
                    rblSexoPropietario.SelectedValue = dtPersona.Rows[0]["c_Genero"].ToString();
                    txtNombrePropietario.Text = dtPersona.Rows[0]["v_Nombres"].ToString();
                    txtNumeroDocumentoPropietario.Text = dtPersona.Rows[0]["v_DocumentoIdentidad"].ToString();
                    txtDistritoPropietario.Text = dtPersona.Rows[0]["v_Distrito"].ToString();
                    txtDireccionPropietario.Text = dtPersona.Rows[0]["v_Direccion"].ToString();
                    txtTelefonoPropietario.Text = dtPersona.Rows[0]["v_Telefono"].ToString();
                    txtCelularPropietario.Text = dtPersona.Rows[0]["v_Celular"].ToString();
                    txtEmailPropietario.Text = dtPersona.Rows[0]["v_Email"].ToString();
                    txtContactoPropietario.Text = dtPersona.Rows[0]["v_Contacto"].ToString();
                    if (dtPersona.Rows[0]["d_FechaNacimiento"].ToString() != "")
                    {
                        txtCumpleañosPropietario.Text = DateTime.Parse(dtPersona.Rows[0]["d_FechaNacimiento"].ToString()).ToShortDateString();
                    }

                    txtComentarioPropietario.Text = dtPersona.Rows[0]["t_Comentario"].ToString();
                    btnVerificarPropietario.Visible = false;
                }
            }

        }
        else
        {
            txtNombrePropietario.Text = "";
            rblSexoPropietario.SelectedValue = "M";
            txtDistritoPropietario.Text = "";
            txtDireccionPropietario.Text = "";
            txtTelefonoPropietario.Text = "";
            txtCelularPropietario.Text = "";
            txtEmailPropietario.Text = "";
            txtContactoPropietario.Text = "";
            txtCumpleañosPropietario.Text = DateTime.Now.ToShortDateString();
            txtComentarioPropietario.Text = "";
            hdnPropietario.Value = "";
            btnVerificarPropietario.Visible = false;

        }
    }

    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListarCarrocerias();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> BuscarMarcas(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select i_IdMarca,v_NombreMarca from marca where b_Estado = 1 and v_NombreMarca like @SearchText + '%'order by v_NombreMarca";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> categorias = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        categorias.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["v_NombreMarca"].ToString(), Convert.ToString(sdr["i_IdMarca"].ToString())));
                    }
                }
                conn.Close();
                //if (productos.Count == 0)
                //{
                //categorias.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear '" + prefixText + "'", "*"));
                //categorias.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear y Editar ... '" + prefixText + "'", "%"));
                //}


                return categorias;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> BuscarModelos(string prefixText, int count, string contextKey)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select i_IdModelo,v_NombreModelo from modelo where b_Estado = 1 and i_IdMarca=" + contextKey + " and v_NombreModelo like @SearchText + '%' order by v_NombreModelo";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> categorias = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        categorias.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr["v_NombreModelo"].ToString(), Convert.ToString(sdr["i_IdModelo"].ToString())));
                    }
                }
                conn.Close();
                //if (productos.Count == 0)
                //{
                //categorias.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear '" + prefixText + "'", "*"));
                //categorias.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("Crear y Editar ... '" + prefixText + "'", "%"));
                //}


                return categorias;
            }
        }
    }

    protected void hfMarca_ValueChanged(object sender, EventArgs e)
    {
        string selectedWidgetID = ((HiddenField)sender).Value;
        txtMarca.Enabled = false;
        btnEditarMarca.Visible = true;
        txtMarca.BackColor = System.Drawing.Color.FromName("#CEE7FF");
        txtModelo_AutoCompleteExtender.ContextKey = selectedWidgetID;
        txtModelo.Enabled = true;
        btnEditarModelo.Visible = false;
        txtModelo.Text = "";
        hfModelo.Value = "0";
        btnNuevoModelo.Visible = true;
        btnNuevaMarca.Visible = false;
        txtModelo.Focus();
    }

    protected void btnEditarMarca_Click(object sender, ImageClickEventArgs e)
    {
        txtMarca.Enabled = true;
        btnEditarMarca.Visible = false;
    }

    protected void hfModelo_ValueChanged(object sender, EventArgs e)
    {
        txtModelo.Enabled = false;
        btnEditarModelo.Visible = true;
        btnNuevoModelo.Visible = false;
        txtModelo.BackColor = System.Drawing.Color.FromName("#CEE7FF");
        if (hfMarca.Value != "") { txtMarca.BackColor = System.Drawing.Color.FromName("#CEE7FF"); }
        txtAnioFabricacion.Focus();
    }

    protected void btnEditarModelo_Click(object sender, ImageClickEventArgs e)
    {
        txtModelo.Enabled = true;
        btnEditarModelo.Visible = false;
    }

    protected void btnNuevaMarca_Click(object sender, ImageClickEventArgs e)
    {
        tblMarca.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
        txtDescripcionMarca.Focus();
    }

    protected void btnRegistrarMarca_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDescripcionMarca.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Ingrese la descripción de la marca.' });</script>", false);
            txtDescripcionMarca.Focus();
            return;
        }

        try
        {

                //Verificar existencia
                DataTable dtMarca = new DataTable();
                SqlDataAdapter daMarca = new SqlDataAdapter("COB_Marca_Existe '" + txtDescripcionMarca.Text.Trim() + "'", conexion);
                daMarca.Fill(dtMarca);
                int cantidad = int.Parse(dtMarca.Rows[0]["Cantidad"].ToString());

                if (cantidad > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Esta Marca ya se encuentra registrada.' });</script>", false);
                    txtDescripcionMarca.Focus();
                    return;
                }
                else
                {
                    //Registrar Marca
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_Marca_Registrar";
                    cmd.Parameters.AddWithValue("@v_NombreMarca", txtDescripcionMarca.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@b_Estado", 1);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Marca registrada correctamente.' });</script>", false);
                    txtDescripcionMarca.Text = "";
                    //Cerrar
                    tblMarca.Visible = false;
                    tblGeneral.Visible = true;
                    toolbar.Visible = true;
                }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnCancelarMarca_Click(object sender, ImageClickEventArgs e)
    {
        tblMarca.Visible = false;
        tblGeneral.Visible = true;
        toolbar.Visible = true;
    }

    protected void btnNuevoModelo_Click(object sender, ImageClickEventArgs e)
    {
        if (hfMarca.Value == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Debe seleccionar una marca.' });</script>", false);
            txtMarca.Focus();
            return;
        }

        tblModelo.Visible = true;
        tblGeneral.Visible = false;
        toolbar.Visible = false;
        txtDescripcionModelo.Focus();
    }

    protected void btnRegistrarModelo_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDescripcionModelo.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Ingrese la descripción del modelo.' });</script>", false);
            txtDescripcionModelo.Focus();
            return;
        }

        try
        {
                int IdMarca = int.Parse(hfMarca.Value);

                //Verificar existencia
                DataTable dtModelo = new DataTable();
                SqlDataAdapter daModelo = new SqlDataAdapter("COB_Modelo_Existe " + IdMarca + ",'" + txtDescripcionModelo.Text.Trim() + "'", conexion);
                daModelo.Fill(dtModelo);
                int cantidad = int.Parse(dtModelo.Rows[0]["Cantidad"].ToString());

                if (cantidad > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Esta modelo ya se encuentra registrado.' });</script>", false);
                    txtDescripcionModelo.Focus();
                    return;
                }
                else
                {
                    //registrar modelo
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Play_Modelo_Registrar";
                    cmd.Parameters.AddWithValue("@i_IdMarca", IdMarca);
                    cmd.Parameters.AddWithValue("@v_NombreModelo", txtDescripcionModelo.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@b_Estado", 1);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                    txtDescripcionModelo.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Modelo registrado correctamente.' });</script>", false);
                    //Cerrar
                    tblModelo.Visible = false;
                    tblGeneral.Visible = true;
                    toolbar.Visible = true;
                }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
        }
    }

    protected void btnCancelarModelo_Click(object sender, ImageClickEventArgs e)
    {
        tblModelo.Visible = false;
        tblGeneral.Visible = true;
        toolbar.Visible = true;
    }

    protected void txtPago_TextChanged(object sender, EventArgs e)
    {
        double total = double.Parse(lblTotal.Text);
        double pago = double.Parse(txtPago.Text);
        double vuelto = 0;
        if (pago < total)
        {
            txtPago.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: 'El pago debe ser mayor o igual al total.' });</script>", false);
            return;
        }
        vuelto = pago - total;
        lblVuelto.Text = vuelto.ToString("N2");
    }

    protected void btnAnular_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            //Validar que no esté amarrado a un comprobante
            //

            //Anular el pedido
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_Pedido_Anular";
                cmd.Parameters.AddWithValue("@n_IdPedido", hfPedido.Value);
                cmd.Parameters.AddWithValue("@n_IdUsuarioAnula", n_IdUsuario);
                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de servicio anulada correctamente.' });</script>", false);
                TabContainer1.Enabled = false;
                btnAnular.Enabled = false;
                btnImprimir.Enabled = false;
                btnGuardar.Enabled = false;
                Label1.ForeColor = System.Drawing.Color.Red;
                lblNumeroPedido.ForeColor = System.Drawing.Color.Red;
                lblEstado.Text = "Anulado";
                lblEstado.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.error({ message: '" + ex.Message + "' });</script>", false);
            }
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);
        Response.Redirect("CrearPedido.aspx?IdMenu=" + i_IdMenu);
    }
}