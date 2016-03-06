using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CrearOrdenTraslado : System.Web.UI.Page
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            InicializarTabla();
            txtFechaInicial.Text = DateTime.Now.ToShortDateString();
            ListarAlmacen();
        }
    }

    void InicializarTabla() 
    {
        DataTable dtDetalle = new DataTable();
        dtDetalle.Columns.Add("n_IdProducto");
        dtDetalle.Columns.Add("Producto");
        dtDetalle.Columns.Add("v_CodigoInterno");
        dtDetalle.Columns.Add("Almacen Central");
        dtDetalle.Columns.Add("Bellavista");
        dtDetalle.Columns.Add("Minka");
        dtDetalle.Columns.Add("EL AGUSTINO");
        dtDetalle.Columns.Add("Santa Anita");
        dtDetalle.Columns.Add("Santa Clara");
        dtDetalle.Columns.Add("n_IdAlmacen");
        dtDetalle.Columns.Add("f_StockDisponible");
        dtDetalle.Columns.Add("f_StockContable");
        Session["Detalle"] = dtDetalle;
    }

    public void ListarAlmacen()
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter("Play_Almacen_Combo 1", conexion);
        da.Fill(dt);
        ddlAlmacen.DataSource = dt;
        ddlAlmacen.DataTextField = "v_Descripcion";
        ddlAlmacen.DataValueField = "n_IdAlmacen";
        ddlAlmacen.DataBind();
        ddlAlmacen.SelectedIndex = 0;
    }

    void Listar() 
    {
        string Producto = "";
        SqlDataAdapter da = new SqlDataAdapter("Play_Stock_Pivot_Almacen_Listar " + ddlAlmacen.SelectedValue + ",'" + Producto + "'", conexion);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gv.DataSource = dt;
        gv.DataBind();

        string Almacen = ddlAlmacen.SelectedItem.Text;
        for (int i = 0; i < gv.Columns.Count; i++)
        {
            if (gv.Columns[i].HeaderText.Trim().ToUpper() == Almacen.Trim().ToUpper())
            {
                gv.Columns[i].Visible = false;
            }
            else
            {
                gv.Columns[i].Visible = true;
            }
        }
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string n_IdUsuario = dtUsuario.Rows[0]["n_IdUsuario"].ToString();

            int stock = 0;
            bool HayStock = true;
            //Validar que hay stock suficiente para hacer la salida
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //SANTA CLARA
                TextBox txtCantidadSANTACLARA = new TextBox();
                txtCantidadSANTACLARA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                int CantidadSantaClara = int.Parse(txtCantidadSANTACLARA.Text);
                if (CantidadSantaClara > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < CantidadSantaClara) { HayStock = false; break; }
                }

                //SANTA ANITA
                TextBox txtCantidadSANTAANITA = new TextBox();
                txtCantidadSANTAANITA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                int CantidadSantaAnita = int.Parse(txtCantidadSANTAANITA.Text);
                if (CantidadSantaAnita > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < CantidadSantaAnita) { HayStock = false; break; }
                }

                //Bellavista
                TextBox txtCantidadBELLAVISTA = new TextBox();
                txtCantidadBELLAVISTA = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                int CantidadBellavista = int.Parse(txtCantidadBELLAVISTA.Text);
                if (CantidadBellavista > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < CantidadBellavista) { HayStock = false; break; }
                }

                //Minka
                TextBox txtCantidadMINKA = new TextBox();
                txtCantidadMINKA = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                int CantidadMinka = int.Parse(txtCantidadMINKA.Text);
                if (CantidadMinka > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < CantidadMinka) { HayStock = false; break; }
                }

                //Play Central
                TextBox txtCantidadPLAYCENTRAL = new TextBox();
                txtCantidadPLAYCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                int CantidadPlayCentral = int.Parse(txtCantidadPLAYCENTRAL.Text);
                if (CantidadPlayCentral > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < CantidadPlayCentral) { HayStock = false; break; }
                }

                TextBox txtCantidadALMACENCENTRAL = new TextBox();
                txtCantidadALMACENCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");
                int CantidadAlmacenCentral = int.Parse(txtCantidadALMACENCENTRAL.Text);
                if (CantidadAlmacenCentral > 0)
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("select isnull(f_StockContable,0) from stock where n_IdProducto = " + gv.DataKeys[i].Value.ToString() + " and n_IdAlmacen = " + ddlAlmacen.SelectedValue, conexion);
                    da.Fill(dt);
                    stock = int.Parse(dt.Rows[0][0].ToString());
                    if (stock < CantidadAlmacenCentral) { HayStock = false; break; }
                }
            }
            if (HayStock == false) 
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'No hay stock suficiente para relizar la operación' });</script>", false);
                return;
            }


            SqlTransaction tran;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
            cn.Open();
            tran = cn.BeginTransaction();

            try
            {
                //Registrar Cabecera de Orden de Traslado
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Play_OrdenTraslado_Insertar";
                cmd.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmd.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                cmd.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string i_IdOrdenTraslado = cmd.ExecuteScalar().ToString();
                cmd.Dispose();

                if (i_IdOrdenTraslado.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Orden de Traslado ha terminado' });</script>", false);
                    return;
                }

                SqlCommand cmd0 = new SqlCommand();
                cmd0.Connection = cn;
                cmd0.Transaction = tran;
                cmd0.CommandType = CommandType.Text;
                cmd0.CommandText = "select v_NumeroOrdenTraslado from OrdenTraslado where i_IdOrdenTraslado = " + i_IdOrdenTraslado;
                lblNumero.Text = cmd0.ExecuteScalar().ToString();
                cmd0.Dispose();

                //Registrar Detalle de Orden de Traslado
                #region Detalle Orden Traslado
                for (int i = 0; i < gv.Rows.Count; i++)
			    {
			        //MINKA
                    TextBox txtCantidadMINKA = new TextBox();
                    txtCantidadMINKA = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                    if (int.Parse(txtCantidadMINKA.Text) > 0)
                    {
                        SqlCommand cmdDetalleTrasladoMINKA = new SqlCommand();
                        cmdDetalleTrasladoMINKA.Connection = cn;
                        cmdDetalleTrasladoMINKA.Transaction = tran;
                        cmdDetalleTrasladoMINKA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTrasladoMINKA.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTrasladoMINKA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTrasladoMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTrasladoMINKA.Parameters.AddWithValue("@n_IdAlmacen", 1);
                        cmdDetalleTrasladoMINKA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadMINKA.Text));
                        cmdDetalleTrasladoMINKA.ExecuteNonQuery();
                        cmdDetalleTrasladoMINKA.Dispose();
                    }

                    //BELLAVISTA
                    TextBox txtCantidadBELLAVISTA = new TextBox();
                    txtCantidadBELLAVISTA = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                    if (int.Parse(txtCantidadBELLAVISTA.Text) > 0)
                    {
                        SqlCommand cmdDetalleTrasladoBELLAVISTA = new SqlCommand();
                        cmdDetalleTrasladoBELLAVISTA.Connection = cn;
                        cmdDetalleTrasladoBELLAVISTA.Transaction = tran;
                        cmdDetalleTrasladoBELLAVISTA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTrasladoBELLAVISTA.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTrasladoBELLAVISTA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTrasladoBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTrasladoBELLAVISTA.Parameters.AddWithValue("@n_IdAlmacen", 2);
                        cmdDetalleTrasladoBELLAVISTA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                        cmdDetalleTrasladoBELLAVISTA.ExecuteNonQuery();
                        cmdDetalleTrasladoBELLAVISTA.Dispose();
                    }

                    //SANTA ANITA
                    TextBox txtCantidadSANTAANITA = new TextBox();
                    txtCantidadSANTAANITA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                    if (int.Parse(txtCantidadSANTAANITA.Text) > 0)
                    {
                        SqlCommand cmdDetalleTrasladoSANTAANITA = new SqlCommand();
                        cmdDetalleTrasladoSANTAANITA.Connection = cn;
                        cmdDetalleTrasladoSANTAANITA.Transaction = tran;
                        cmdDetalleTrasladoSANTAANITA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTrasladoSANTAANITA.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTrasladoSANTAANITA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTrasladoSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTrasladoSANTAANITA.Parameters.AddWithValue("@n_IdAlmacen", 3);
                        cmdDetalleTrasladoSANTAANITA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                        cmdDetalleTrasladoSANTAANITA.ExecuteNonQuery();
                        cmdDetalleTrasladoSANTAANITA.Dispose();
                    }

                    //SANTA CLARA
                    TextBox txtCantidadSANTACLARA = new TextBox();
                    txtCantidadSANTACLARA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                    if (int.Parse(txtCantidadSANTACLARA.Text) > 0)
                    {
                        SqlCommand cmdDetalleTrasladoSANTACLARA = new SqlCommand();
                        cmdDetalleTrasladoSANTACLARA.Connection = cn;
                        cmdDetalleTrasladoSANTACLARA.Transaction = tran;
                        cmdDetalleTrasladoSANTACLARA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTrasladoSANTACLARA.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTrasladoSANTACLARA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTrasladoSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTrasladoSANTACLARA.Parameters.AddWithValue("@n_IdAlmacen", 4);
                        cmdDetalleTrasladoSANTACLARA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                        cmdDetalleTrasladoSANTACLARA.ExecuteNonQuery();
                        cmdDetalleTrasladoSANTACLARA.Dispose();
                    }

                    //PLAY CENTRAL
                    TextBox txtCantidadPLAYCENTRAL = new TextBox();
                    txtCantidadPLAYCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                    if (int.Parse(txtCantidadPLAYCENTRAL.Text) > 0)
                    {
                        SqlCommand cmdDetalleTrasladoPLAYCENTRAL = new SqlCommand();
                        cmdDetalleTrasladoPLAYCENTRAL.Connection = cn;
                        cmdDetalleTrasladoPLAYCENTRAL.Transaction = tran;
                        cmdDetalleTrasladoPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTrasladoPLAYCENTRAL.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTrasladoPLAYCENTRAL.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTrasladoPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTrasladoPLAYCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", 5);
                        cmdDetalleTrasladoPLAYCENTRAL.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                        cmdDetalleTrasladoPLAYCENTRAL.ExecuteNonQuery();
                        cmdDetalleTrasladoPLAYCENTRAL.Dispose();
                    }

                    //ALMACEN CENTRAL
                    TextBox txtCantidadALMACENCENTRAL = new TextBox();
                    txtCantidadALMACENCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");
                    if (int.Parse(txtCantidadALMACENCENTRAL.Text) > 0)
                    {
                        SqlCommand cmdDetalleTrasladoALMACENCENTRAL = new SqlCommand();
                        cmdDetalleTrasladoALMACENCENTRAL.Connection = cn;
                        cmdDetalleTrasladoALMACENCENTRAL.Transaction = tran;
                        cmdDetalleTrasladoALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdDetalleTrasladoALMACENCENTRAL.CommandText = "Play_OrdenTrasladoDetalle_Insertar";
                        cmdDetalleTrasladoALMACENCENTRAL.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                        cmdDetalleTrasladoALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleTrasladoALMACENCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", 6);
                        cmdDetalleTrasladoALMACENCENTRAL.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                        cmdDetalleTrasladoALMACENCENTRAL.ExecuteNonQuery();
                        cmdDetalleTrasladoALMACENCENTRAL.Dispose();
                    }

                }

                #endregion

                //Registrar Cabecera de Nota de Salida
                #region Cabecera Nota Salida
                SqlCommand cmdNotaSalida = new SqlCommand();
                cmdNotaSalida.Connection = cn;
                cmdNotaSalida.Transaction = tran;
                cmdNotaSalida.CommandType = CommandType.StoredProcedure;
                cmdNotaSalida.CommandText = "Play_NotaSalida_Registrar";
                cmdNotaSalida.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmdNotaSalida.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                cmdNotaSalida.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                cmdNotaSalida.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                cmdNotaSalida.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                cmdNotaSalida.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                string n_IdNotaSalida = cmdNotaSalida.ExecuteScalar().ToString();
                cmdNotaSalida.Dispose();

                if (n_IdNotaSalida.Trim() == "0")
                {
                    tran.Rollback();
                    cn.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Salida ha terminado' });</script>", false);
                    return;
                }

                string v_NumeroNotaSalida = "";
                SqlCommand cmdSalida = new SqlCommand();
                cmdSalida.Connection = cn;
                cmdSalida.Transaction = tran;
                cmdSalida.CommandType = CommandType.Text;
                cmdSalida.CommandText = "select v_NumeroNotaSalida from NotaSalida where n_IdNotaSalida = " + n_IdNotaSalida;
                v_NumeroNotaSalida = cmdSalida.ExecuteScalar().ToString();
                cmdSalida.Dispose();
                #endregion

                for (int i = 0; i < gv.Rows.Count; i++)
                {

                    //Registrar Detalle Nota de Salida SANTA CLARA
                    #region Detalle Nota Salida Santa Clara
                    TextBox txtCantidadSANTACLARA = new TextBox();
                    txtCantidadSANTACLARA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                    if (int.Parse(txtCantidadSANTACLARA.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalidaSANTACLARA = new SqlCommand();
                        cmdDetalleNotaSalidaSANTACLARA.Connection = cn;
                        cmdDetalleNotaSalidaSANTACLARA.Transaction = tran;
                        cmdDetalleNotaSalidaSANTACLARA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalidaSANTACLARA.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalidaSANTACLARA.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalidaSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalidaSANTACLARA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                        cmdDetalleNotaSalidaSANTACLARA.ExecuteNonQuery();
                        cmdDetalleNotaSalidaSANTACLARA.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockSANTACLARA = new SqlCommand();
                        cmdStockSANTACLARA.Connection = cn;
                        cmdStockSANTACLARA.Transaction = tran;
                        cmdStockSANTACLARA.CommandType = CommandType.StoredProcedure;
                        cmdStockSANTACLARA.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockSANTACLARA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdStockSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockSANTACLARA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                        cmdStockSANTACLARA.ExecuteNonQuery();
                        cmdStockSANTACLARA.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardexSANTACLARA = new SqlCommand();
                        cmdKardexSANTACLARA.Connection = cn;
                        cmdKardexSANTACLARA.Transaction = tran;
                        cmdKardexSANTACLARA.CommandType = CommandType.StoredProcedure;
                        cmdKardexSANTACLARA.CommandText = "Play_Kardex_Insertar";
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardexSANTACLARA.ExecuteNonQuery();
                        cmdKardexSANTACLARA.Dispose();
                    }
                    #endregion

                    //Registrar Detalle Nota de Salida SANTA ANITA
                    #region Detalle Nota Salida Santa Anita
                    TextBox txtCantidadSANTAANITA = new TextBox();
                    txtCantidadSANTAANITA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                    if (int.Parse(txtCantidadSANTAANITA.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalidaSANTAANITA = new SqlCommand();
                        cmdDetalleNotaSalidaSANTAANITA.Connection = cn;
                        cmdDetalleNotaSalidaSANTAANITA.Transaction = tran;
                        cmdDetalleNotaSalidaSANTAANITA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalidaSANTAANITA.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalidaSANTAANITA.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalidaSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalidaSANTAANITA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                        cmdDetalleNotaSalidaSANTAANITA.ExecuteNonQuery();
                        cmdDetalleNotaSalidaSANTAANITA.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockSANTAANITA = new SqlCommand();
                        cmdStockSANTAANITA.Connection = cn;
                        cmdStockSANTAANITA.Transaction = tran;
                        cmdStockSANTAANITA.CommandType = CommandType.StoredProcedure;
                        cmdStockSANTAANITA.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockSANTAANITA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdStockSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockSANTAANITA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                        cmdStockSANTAANITA.ExecuteNonQuery();
                        cmdStockSANTAANITA.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardexSANTAANITA = new SqlCommand();
                        cmdKardexSANTAANITA.Connection = cn;
                        cmdKardexSANTAANITA.Transaction = tran;
                        cmdKardexSANTAANITA.CommandType = CommandType.StoredProcedure;
                        cmdKardexSANTAANITA.CommandText = "Play_Kardex_Insertar";
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardexSANTAANITA.ExecuteNonQuery();
                        cmdKardexSANTAANITA.Dispose();
                    }
                    #endregion

                    //Registrar Detalle Nota de Salida BELLAVISTA
                    #region Detalle Nota Salida Bellavista
                    TextBox txtCantidadBELLAVISTA = new TextBox();
                    txtCantidadBELLAVISTA = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                    if (int.Parse(txtCantidadBELLAVISTA.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalidaBELLAVISTA = new SqlCommand();
                        cmdDetalleNotaSalidaBELLAVISTA.Connection = cn;
                        cmdDetalleNotaSalidaBELLAVISTA.Transaction = tran;
                        cmdDetalleNotaSalidaBELLAVISTA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalidaBELLAVISTA.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalidaBELLAVISTA.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalidaBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalidaBELLAVISTA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                        cmdDetalleNotaSalidaBELLAVISTA.ExecuteNonQuery();
                        cmdDetalleNotaSalidaBELLAVISTA.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockBELLAVISTA = new SqlCommand();
                        cmdStockBELLAVISTA.Connection = cn;
                        cmdStockBELLAVISTA.Transaction = tran;
                        cmdStockBELLAVISTA.CommandType = CommandType.StoredProcedure;
                        cmdStockBELLAVISTA.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockBELLAVISTA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdStockBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockBELLAVISTA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                        cmdStockBELLAVISTA.ExecuteNonQuery();
                        cmdStockBELLAVISTA.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardexBELLAVISTA = new SqlCommand();
                        cmdKardexBELLAVISTA.Connection = cn;
                        cmdKardexBELLAVISTA.Transaction = tran;
                        cmdKardexBELLAVISTA.CommandType = CommandType.StoredProcedure;
                        cmdKardexBELLAVISTA.CommandText = "Play_Kardex_Insertar";
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardexBELLAVISTA.ExecuteNonQuery();
                        cmdKardexBELLAVISTA.Dispose();
                    }
                    #endregion

                    //Registrar Detalle Nota de Salida MINKA
                    #region Detalle Nota Salida Minka
                    TextBox txtCantidadMINKA = new TextBox();
                    txtCantidadMINKA = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                    if (int.Parse(txtCantidadMINKA.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalidaMINKA = new SqlCommand();
                        cmdDetalleNotaSalidaMINKA.Connection = cn;
                        cmdDetalleNotaSalidaMINKA.Transaction = tran;
                        cmdDetalleNotaSalidaMINKA.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalidaMINKA.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalidaMINKA.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalidaMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalidaMINKA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadMINKA.Text));
                        cmdDetalleNotaSalidaMINKA.ExecuteNonQuery();
                        cmdDetalleNotaSalidaMINKA.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockMINKA = new SqlCommand();
                        cmdStockMINKA.Connection = cn;
                        cmdStockMINKA.Transaction = tran;
                        cmdStockMINKA.CommandType = CommandType.StoredProcedure;
                        cmdStockMINKA.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockMINKA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdStockMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockMINKA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadMINKA.Text));
                        cmdStockMINKA.ExecuteNonQuery();
                        cmdStockMINKA.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardexMINKA = new SqlCommand();
                        cmdKardexMINKA.Connection = cn;
                        cmdKardexMINKA.Transaction = tran;
                        cmdKardexMINKA.CommandType = CommandType.StoredProcedure;
                        cmdKardexMINKA.CommandText = "Play_Kardex_Insertar";
                        cmdKardexMINKA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardexMINKA.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardexMINKA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardexMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardexMINKA.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdKardexMINKA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadMINKA.Text));
                        cmdKardexMINKA.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardexMINKA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardexMINKA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardexMINKA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardexMINKA.ExecuteNonQuery();
                        cmdKardexMINKA.Dispose();
                    }
                    #endregion

                    //Registrar Detalle Nota de Salida PLAY CENTRAL
                    #region Detalle Nota Salida Play Central
                    TextBox txtCantidadPLAYCENTRAL = new TextBox();
                    txtCantidadPLAYCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                    if (int.Parse(txtCantidadPLAYCENTRAL.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalidaPLAYCENTRAL = new SqlCommand();
                        cmdDetalleNotaSalidaPLAYCENTRAL.Connection = cn;
                        cmdDetalleNotaSalidaPLAYCENTRAL.Transaction = tran;
                        cmdDetalleNotaSalidaPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalidaPLAYCENTRAL.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalidaPLAYCENTRAL.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalidaPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalidaPLAYCENTRAL.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                        cmdDetalleNotaSalidaPLAYCENTRAL.ExecuteNonQuery();
                        cmdDetalleNotaSalidaPLAYCENTRAL.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockPLAYCENTRAL = new SqlCommand();
                        cmdStockPLAYCENTRAL.Connection = cn;
                        cmdStockPLAYCENTRAL.Transaction = tran;
                        cmdStockPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdStockPLAYCENTRAL.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockPLAYCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdStockPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockPLAYCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                        cmdStockPLAYCENTRAL.ExecuteNonQuery();
                        cmdStockPLAYCENTRAL.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardexPLAYCENTRAL = new SqlCommand();
                        cmdKardexPLAYCENTRAL.Connection = cn;
                        cmdKardexPLAYCENTRAL.Transaction = tran;
                        cmdKardexPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdKardexPLAYCENTRAL.CommandText = "Play_Kardex_Insertar";
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardexPLAYCENTRAL.ExecuteNonQuery();
                        cmdKardexPLAYCENTRAL.Dispose();
                    }
                    #endregion

                    //Registrar Detalle Nota de Salida ALMACENCENTRAL
                    #region Detalle Nota Salida Almacen Central
                    TextBox txtCantidadALMACENCENTRAL = new TextBox();
                    txtCantidadALMACENCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");
                    if (int.Parse(txtCantidadALMACENCENTRAL.Text) > 0)
                    {
                        SqlCommand cmdDetalleNotaSalidaALMACENCENTRAL = new SqlCommand();
                        cmdDetalleNotaSalidaALMACENCENTRAL.Connection = cn;
                        cmdDetalleNotaSalidaALMACENCENTRAL.Transaction = tran;
                        cmdDetalleNotaSalidaALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdDetalleNotaSalidaALMACENCENTRAL.CommandText = "Play_NotaSalidaDetalle_Insert";
                        cmdDetalleNotaSalidaALMACENCENTRAL.Parameters.AddWithValue("@n_IdNotaSalida", n_IdNotaSalida);
                        cmdDetalleNotaSalidaALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdDetalleNotaSalidaALMACENCENTRAL.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                        cmdDetalleNotaSalidaALMACENCENTRAL.ExecuteNonQuery();
                        cmdDetalleNotaSalidaALMACENCENTRAL.Dispose();

                        //Actualizar Stock
                        SqlCommand cmdStockALMACENCENTRAL = new SqlCommand();
                        cmdStockALMACENCENTRAL.Connection = cn;
                        cmdStockALMACENCENTRAL.Transaction = tran;
                        cmdStockALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdStockALMACENCENTRAL.CommandText = "Play_Stock_Restar_Actualizar";
                        cmdStockALMACENCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdStockALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdStockALMACENCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                        cmdStockALMACENCENTRAL.ExecuteNonQuery();
                        cmdStockALMACENCENTRAL.Dispose();

                        //Registrar Kardex
                        SqlCommand cmdKardexALMACENCENTRAL = new SqlCommand();
                        cmdKardexALMACENCENTRAL.Connection = cn;
                        cmdKardexALMACENCENTRAL.Transaction = tran;
                        cmdKardexALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                        cmdKardexALMACENCENTRAL.CommandText = "Play_Kardex_Insertar";
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@c_TipoMovimiento", "S");
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdTipoDocumento", 9);
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                        cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                        cmdKardexALMACENCENTRAL.ExecuteNonQuery();
                        cmdKardexALMACENCENTRAL.Dispose();
                    }
                    #endregion
                }
                    

                //Registrar Documentos de Orden de Traslado
                SqlCommand cmdOrdenTrasladoDocumento = new SqlCommand();
                cmdOrdenTrasladoDocumento.Connection = cn;
                cmdOrdenTrasladoDocumento.Transaction = tran;
                cmdOrdenTrasladoDocumento.CommandType = CommandType.StoredProcedure;
                cmdOrdenTrasladoDocumento.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@n_IdTipoDocumento", 9);//Nota de Salida
                cmdOrdenTrasladoDocumento.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaSalida);
                cmdOrdenTrasladoDocumento.ExecuteNonQuery();
                cmdOrdenTrasladoDocumento.Dispose();


                //Actualizar Correlativo de Orden de Traslado
                SqlCommand cmd5 = new SqlCommand();
                cmd5.Connection = cn;
                cmd5.Transaction = tran;
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.CommandText = "Play_Correlativo_Aumentar";
                cmd5.Parameters.AddWithValue("@n_IdTipoDocumento", 11);//Orden de Traslado
                cmd5.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmd5.ExecuteNonQuery();
                cmd5.Dispose();

                //Actualizar Correlativo de Nota de Salida
                SqlCommand cmdNotaSalidaCorrelativo = new SqlCommand();
                cmdNotaSalidaCorrelativo.Connection = cn;
                cmdNotaSalidaCorrelativo.Transaction = tran;
                cmdNotaSalidaCorrelativo.CommandType = CommandType.StoredProcedure;
                cmdNotaSalidaCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                cmdNotaSalidaCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 9);//Nota de Salida
                cmdNotaSalidaCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", ddlAlmacen.SelectedValue);
                cmdNotaSalidaCorrelativo.ExecuteNonQuery();
                cmdNotaSalidaCorrelativo.Dispose();


                //Validar que tenga productos para cada almacén y así efectuar el movimiento de ingreso.

                bool TieneCantidadMinka = false;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso MINKA
                    TextBox txtCantidadMINKA = new TextBox();
                    txtCantidadMINKA = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                    if (int.Parse(txtCantidadMINKA.Text) > 0)
                    {
                        TieneCantidadMinka = true;
                    }
                }

                bool TieneCantidadSantaAnita = false;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso SANTA ANITA
                    TextBox txtCantidadSANTAANITA = new TextBox();
                    txtCantidadSANTAANITA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                    if (int.Parse(txtCantidadSANTAANITA.Text) > 0)
                    {
                        TieneCantidadSantaAnita = true;
                    }
                }

                bool TieneCantidadSantaClara = false;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso SANTA CLARA
                    TextBox txtCantidadSANTACLARA = new TextBox();
                    txtCantidadSANTACLARA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                    if (int.Parse(txtCantidadSANTACLARA.Text) > 0)
                    {
                        TieneCantidadSantaClara = true;
                    }
                }

                bool TieneCantidadBellavista = false;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso BELLAVISTA
                    TextBox txtCantidadBELLAVISTA = new TextBox();
                    txtCantidadBELLAVISTA = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                    if (int.Parse(txtCantidadBELLAVISTA.Text) > 0)
                    {
                        TieneCantidadBellavista = true;
                    }
                }

                bool TieneCantidadPlayCentral = false;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso PLAYCENTRAL
                    TextBox txtCantidadPLAYCENTRAL = new TextBox();
                    txtCantidadPLAYCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                    if (int.Parse(txtCantidadPLAYCENTRAL.Text) > 0)
                    {
                        TieneCantidadPlayCentral = true;
                    }
                }

                bool TieneCantidadAlmacenCentral = false;
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    //Registrar Detalle Nota de Ingreso ALMACENCENTRAL
                    TextBox txtCantidadALMACENCENTRAL = new TextBox();
                    txtCantidadALMACENCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");
                    if (int.Parse(txtCantidadALMACENCENTRAL.Text) > 0)
                    {
                        TieneCantidadAlmacenCentral = true;
                    }
                }


                #region NotaIngreso Minka
                for (int x = 2; x < gv.Columns.Count; x++)
                {
                    if (gv.Columns[x].Visible == true) 
                    {

                        if (x == 2 && TieneCantidadMinka == true)//Minka
                        {
                            //Registrar Cabecera de la Nota de Ingreso
                            SqlCommand cmdNotaIngreso = new SqlCommand();
                            cmdNotaIngreso.Connection = cn;
                            cmdNotaIngreso.Transaction = tran;
                            cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", 1);
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                            cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                            cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                            string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                            cmdNotaIngreso.Dispose();

                            if (n_IdNotaIngreso.Trim() == "0")
                            {
                                tran.Rollback();
                                cn.Close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                                return;
                            }

                            string v_NumeroNotaIngreso = "";
                            SqlCommand cmdIngreso = new SqlCommand();
                            cmdIngreso.Connection = cn;
                            cmdIngreso.Transaction = tran;
                            cmdIngreso.CommandType = CommandType.Text;
                            cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                            v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                            cmdIngreso.Dispose();

                            for (int i = 0; i < gv.Rows.Count; i++)
                            {
                                //Registrar Detalle Nota de Ingreso MINKA
                                TextBox txtCantidadMINKA = new TextBox();
                                txtCantidadMINKA = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                                if (int.Parse(txtCantidadMINKA.Text) > 0)
                                {
                                    SqlCommand cmdDetalleNotaIngresoMINKA = new SqlCommand();
                                    cmdDetalleNotaIngresoMINKA.Connection = cn;
                                    cmdDetalleNotaIngresoMINKA.Transaction = tran;
                                    cmdDetalleNotaIngresoMINKA.CommandType = CommandType.StoredProcedure;
                                    cmdDetalleNotaIngresoMINKA.CommandText = "Play_NotaIngresoDetalle_Insert";
                                    cmdDetalleNotaIngresoMINKA.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                                    cmdDetalleNotaIngresoMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdDetalleNotaIngresoMINKA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadMINKA.Text));
                                    cmdDetalleNotaIngresoMINKA.ExecuteNonQuery();
                                    cmdDetalleNotaIngresoMINKA.Dispose();

                                    Label lblIdAlmacenMINKA = new Label();
                                    lblIdAlmacenMINKA = (Label)gv.Rows[i].FindControl("lblIdAlmacenMINKA");

                                    //Actualizar Stock
                                    SqlCommand cmdStockMINKA = new SqlCommand();
                                    cmdStockMINKA.Connection = cn;
                                    cmdStockMINKA.Transaction = tran;
                                    cmdStockMINKA.CommandType = CommandType.StoredProcedure;
                                    cmdStockMINKA.CommandText = "Play_Stock_Actualizar";
                                    cmdStockMINKA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenMINKA.Text);
                                    cmdStockMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdStockMINKA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadMINKA.Text));
                                    cmdStockMINKA.ExecuteNonQuery();
                                    cmdStockMINKA.Dispose();

                                    //Registrar Kardex
                                    SqlCommand cmdKardexMINKA = new SqlCommand();
                                    cmdKardexMINKA.Connection = cn;
                                    cmdKardexMINKA.Transaction = tran;
                                    cmdKardexMINKA.CommandType = CommandType.StoredProcedure;
                                    cmdKardexMINKA.CommandText = "Play_Kardex_Insertar";
                                    cmdKardexMINKA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                                    cmdKardexMINKA.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                                    cmdKardexMINKA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                                    cmdKardexMINKA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdKardexMINKA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenMINKA.Text);
                                    cmdKardexMINKA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadMINKA.Text));
                                    cmdKardexMINKA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                                    cmdKardexMINKA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                                    cmdKardexMINKA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                                    cmdKardexMINKA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                                    cmdKardexMINKA.ExecuteNonQuery();
                                    cmdKardexMINKA.Dispose();
                                }
                            }

                            //Registrar Documentos de Orden de Traslado
                            SqlCommand cmdOrdenTrasladoDocumentoMINKA = new SqlCommand();
                            cmdOrdenTrasladoDocumentoMINKA.Connection = cn;
                            cmdOrdenTrasladoDocumentoMINKA.Transaction = tran;
                            cmdOrdenTrasladoDocumentoMINKA.CommandType = CommandType.StoredProcedure;
                            cmdOrdenTrasladoDocumentoMINKA.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@n_IdAlmacen", 1);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                            cmdOrdenTrasladoDocumentoMINKA.ExecuteNonQuery();
                            cmdOrdenTrasladoDocumentoMINKA.Dispose();

                            //Actualizar Correlativo de Nota de Ingreso
                            SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                            cmdNotaIngresoCorrelativo.Connection = cn;
                            cmdNotaIngresoCorrelativo.Transaction = tran;
                            cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", 1);
                            cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                            cmdNotaIngresoCorrelativo.Dispose();

                        }
                #endregion
                #region NotaIngreso SantaAnita
                        if (x == 3 && TieneCantidadSantaAnita == true)//Santa Anita
                        {
                            //Registrar Cabecera de la Nota de Ingreso
                            SqlCommand cmdNotaIngreso = new SqlCommand();
                            cmdNotaIngreso.Connection = cn;
                            cmdNotaIngreso.Transaction = tran;
                            cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", 3);
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                            cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                            cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                            string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                            cmdNotaIngreso.Dispose();

                            if (n_IdNotaIngreso.Trim() == "0")
                            {
                                tran.Rollback();
                                cn.Close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                                return;
                            }

                            string v_NumeroNotaIngreso = "";
                            SqlCommand cmdIngreso = new SqlCommand();
                            cmdIngreso.Connection = cn;
                            cmdIngreso.Transaction = tran;
                            cmdIngreso.CommandType = CommandType.Text;
                            cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                            v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                            cmdIngreso.Dispose();

                            for (int i = 0; i < gv.Rows.Count; i++)
                            {
                                //Registrar Detalle Nota de Ingreso SANTA ANITA
                                TextBox txtCantidadSANTAANITA = new TextBox();
                                txtCantidadSANTAANITA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                                if (int.Parse(txtCantidadSANTAANITA.Text) > 0)
                                {
                                    SqlCommand cmdDetalleNotaIngresoSANTAANITA = new SqlCommand();
                                    cmdDetalleNotaIngresoSANTAANITA.Connection = cn;
                                    cmdDetalleNotaIngresoSANTAANITA.Transaction = tran;
                                    cmdDetalleNotaIngresoSANTAANITA.CommandType = CommandType.StoredProcedure;
                                    cmdDetalleNotaIngresoSANTAANITA.CommandText = "Play_NotaIngresoDetalle_Insert";
                                    cmdDetalleNotaIngresoSANTAANITA.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                                    cmdDetalleNotaIngresoSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdDetalleNotaIngresoSANTAANITA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                                    cmdDetalleNotaIngresoSANTAANITA.ExecuteNonQuery();
                                    cmdDetalleNotaIngresoSANTAANITA.Dispose();

                                    Label lblIdAlmacenSantaAnita = new Label();
                                    lblIdAlmacenSantaAnita = (Label)gv.Rows[i].FindControl("lblIdAlmacenSantaAnita");

                                    //Actualizar Stock
                                    SqlCommand cmdStockSANTAANITA = new SqlCommand();
                                    cmdStockSANTAANITA.Connection = cn;
                                    cmdStockSANTAANITA.Transaction = tran;
                                    cmdStockSANTAANITA.CommandType = CommandType.StoredProcedure;
                                    cmdStockSANTAANITA.CommandText = "Play_Stock_Actualizar";
                                    cmdStockSANTAANITA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenSantaAnita.Text);
                                    cmdStockSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdStockSANTAANITA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                                    cmdStockSANTAANITA.ExecuteNonQuery();
                                    cmdStockSANTAANITA.Dispose();

                                    //Registrar Kardex
                                    SqlCommand cmdKardexSANTAANITA = new SqlCommand();
                                    cmdKardexSANTAANITA.Connection = cn;
                                    cmdKardexSANTAANITA.Transaction = tran;
                                    cmdKardexSANTAANITA.CommandType = CommandType.StoredProcedure;
                                    cmdKardexSANTAANITA.CommandText = "Play_Kardex_Insertar";
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenSantaAnita.Text);
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTAANITA.Text));
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                                    cmdKardexSANTAANITA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                                    cmdKardexSANTAANITA.ExecuteNonQuery();
                                    cmdKardexSANTAANITA.Dispose();
                                }
                            }

                            //Registrar Documentos de Orden de Traslado
                            SqlCommand cmdOrdenTrasladoDocumentoMINKA = new SqlCommand();
                            cmdOrdenTrasladoDocumentoMINKA.Connection = cn;
                            cmdOrdenTrasladoDocumentoMINKA.Transaction = tran;
                            cmdOrdenTrasladoDocumentoMINKA.CommandType = CommandType.StoredProcedure;
                            cmdOrdenTrasladoDocumentoMINKA.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@n_IdAlmacen", 3);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                            cmdOrdenTrasladoDocumentoMINKA.ExecuteNonQuery();
                            cmdOrdenTrasladoDocumentoMINKA.Dispose();

                            //Actualizar Correlativo de Nota de Ingreso
                            SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                            cmdNotaIngresoCorrelativo.Connection = cn;
                            cmdNotaIngresoCorrelativo.Transaction = tran;
                            cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", 3);
                            cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                            cmdNotaIngresoCorrelativo.Dispose();
                        }
#endregion
                #region NotaIngreso SantaClara
                        if (x == 4 && TieneCantidadSantaClara == true)//Santa Clara
                        {
                            //Registrar Cabecera de la Nota de Ingreso
                            SqlCommand cmdNotaIngreso = new SqlCommand();
                            cmdNotaIngreso.Connection = cn;
                            cmdNotaIngreso.Transaction = tran;
                            cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", 4);
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                            cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                            cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                            string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                            cmdNotaIngreso.Dispose();

                            if (n_IdNotaIngreso.Trim() == "0")
                            {
                                tran.Rollback();
                                cn.Close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                                return;
                            }

                            string v_NumeroNotaIngreso = "";
                            SqlCommand cmdIngreso = new SqlCommand();
                            cmdIngreso.Connection = cn;
                            cmdIngreso.Transaction = tran;
                            cmdIngreso.CommandType = CommandType.Text;
                            cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                            v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                            cmdIngreso.Dispose();

                            for (int i = 0; i < gv.Rows.Count; i++)
                            {
                                //Registrar Detalle Nota de Ingreso SANTA CLARA
                                TextBox txtCantidadSANTACLARA = new TextBox();
                                txtCantidadSANTACLARA = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                                if (int.Parse(txtCantidadSANTACLARA.Text) > 0)
                                {
                                    SqlCommand cmdDetalleNotaIngresoSANTACLARA = new SqlCommand();
                                    cmdDetalleNotaIngresoSANTACLARA.Connection = cn;
                                    cmdDetalleNotaIngresoSANTACLARA.Transaction = tran;
                                    cmdDetalleNotaIngresoSANTACLARA.CommandType = CommandType.StoredProcedure;
                                    cmdDetalleNotaIngresoSANTACLARA.CommandText = "Play_NotaIngresoDetalle_Insert";
                                    cmdDetalleNotaIngresoSANTACLARA.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                                    cmdDetalleNotaIngresoSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdDetalleNotaIngresoSANTACLARA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                                    cmdDetalleNotaIngresoSANTACLARA.ExecuteNonQuery();
                                    cmdDetalleNotaIngresoSANTACLARA.Dispose();

                                    Label lblIdAlmacenSantaClara = new Label();
                                    lblIdAlmacenSantaClara = (Label)gv.Rows[i].FindControl("lblIdAlmacenSantaClara");

                                    //Actualizar Stock
                                    SqlCommand cmdStockSANTACLARA = new SqlCommand();
                                    cmdStockSANTACLARA.Connection = cn;
                                    cmdStockSANTACLARA.Transaction = tran;
                                    cmdStockSANTACLARA.CommandType = CommandType.StoredProcedure;
                                    cmdStockSANTACLARA.CommandText = "Play_Stock_Actualizar";
                                    cmdStockSANTACLARA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenSantaClara.Text);
                                    cmdStockSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdStockSANTACLARA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                                    cmdStockSANTACLARA.ExecuteNonQuery();
                                    cmdStockSANTACLARA.Dispose();

                                    //Registrar Kardex
                                    SqlCommand cmdKardexSANTACLARA = new SqlCommand();
                                    cmdKardexSANTACLARA.Connection = cn;
                                    cmdKardexSANTACLARA.Transaction = tran;
                                    cmdKardexSANTACLARA.CommandType = CommandType.StoredProcedure;
                                    cmdKardexSANTACLARA.CommandText = "Play_Kardex_Insertar";
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenSantaClara.Text);
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadSANTACLARA.Text));
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                                    cmdKardexSANTACLARA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                                    cmdKardexSANTACLARA.ExecuteNonQuery();
                                    cmdKardexSANTACLARA.Dispose();
                                }
                            }

                            //Registrar Documentos de Orden de Traslado
                            SqlCommand cmdOrdenTrasladoDocumentoMINKA = new SqlCommand();
                            cmdOrdenTrasladoDocumentoMINKA.Connection = cn;
                            cmdOrdenTrasladoDocumentoMINKA.Transaction = tran;
                            cmdOrdenTrasladoDocumentoMINKA.CommandType = CommandType.StoredProcedure;
                            cmdOrdenTrasladoDocumentoMINKA.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@n_IdAlmacen", 4);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdOrdenTrasladoDocumentoMINKA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                            cmdOrdenTrasladoDocumentoMINKA.ExecuteNonQuery();
                            cmdOrdenTrasladoDocumentoMINKA.Dispose();

                            //Actualizar Correlativo de Nota de Ingreso
                            SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                            cmdNotaIngresoCorrelativo.Connection = cn;
                            cmdNotaIngresoCorrelativo.Transaction = tran;
                            cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", 4);
                            cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                            cmdNotaIngresoCorrelativo.Dispose();

                        }
                        #endregion
                #region NotaIngreso Bellavista
                        if (x == 5 && TieneCantidadBellavista == true)//Bellavista
                        {
                            //Registrar Cabecera de la Nota de Ingreso
                            SqlCommand cmdNotaIngreso = new SqlCommand();
                            cmdNotaIngreso.Connection = cn;
                            cmdNotaIngreso.Transaction = tran;
                            cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", 2);
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                            cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                            cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                            string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                            cmdNotaIngreso.Dispose();

                            if (n_IdNotaIngreso.Trim() == "0")
                            {
                                tran.Rollback();
                                cn.Close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                                return;
                            }

                            string v_NumeroNotaIngreso = "";
                            SqlCommand cmdIngreso = new SqlCommand();
                            cmdIngreso.Connection = cn;
                            cmdIngreso.Transaction = tran;
                            cmdIngreso.CommandType = CommandType.Text;
                            cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                            v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                            cmdIngreso.Dispose();

                            for (int i = 0; i < gv.Rows.Count; i++)
                            {
                                //Registrar Detalle Nota de Ingreso BELLAVISTA
                                TextBox txtCantidadBELLAVISTA = new TextBox();
                                txtCantidadBELLAVISTA = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                                if (int.Parse(txtCantidadBELLAVISTA.Text) > 0)
                                {
                                    SqlCommand cmdDetalleNotaIngresoBELLAVISTA = new SqlCommand();
                                    cmdDetalleNotaIngresoBELLAVISTA.Connection = cn;
                                    cmdDetalleNotaIngresoBELLAVISTA.Transaction = tran;
                                    cmdDetalleNotaIngresoBELLAVISTA.CommandType = CommandType.StoredProcedure;
                                    cmdDetalleNotaIngresoBELLAVISTA.CommandText = "Play_NotaIngresoDetalle_Insert";
                                    cmdDetalleNotaIngresoBELLAVISTA.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                                    cmdDetalleNotaIngresoBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdDetalleNotaIngresoBELLAVISTA.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                                    cmdDetalleNotaIngresoBELLAVISTA.ExecuteNonQuery();
                                    cmdDetalleNotaIngresoBELLAVISTA.Dispose();

                                    Label lblIdAlmacenBELLAVISTA = new Label();
                                    lblIdAlmacenBELLAVISTA = (Label)gv.Rows[i].FindControl("lblIdAlmacenBELLAVISTA");

                                    //Actualizar Stock
                                    SqlCommand cmdStockBELLAVISTA = new SqlCommand();
                                    cmdStockBELLAVISTA.Connection = cn;
                                    cmdStockBELLAVISTA.Transaction = tran;
                                    cmdStockBELLAVISTA.CommandType = CommandType.StoredProcedure;
                                    cmdStockBELLAVISTA.CommandText = "Play_Stock_Actualizar";
                                    cmdStockBELLAVISTA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenBELLAVISTA.Text);
                                    cmdStockBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdStockBELLAVISTA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                                    cmdStockBELLAVISTA.ExecuteNonQuery();
                                    cmdStockBELLAVISTA.Dispose();

                                    //Registrar Kardex
                                    SqlCommand cmdKardexBELLAVISTA = new SqlCommand();
                                    cmdKardexBELLAVISTA.Connection = cn;
                                    cmdKardexBELLAVISTA.Transaction = tran;
                                    cmdKardexBELLAVISTA.CommandType = CommandType.StoredProcedure;
                                    cmdKardexBELLAVISTA.CommandText = "Play_Kardex_Insertar";
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenBELLAVISTA.Text);
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadBELLAVISTA.Text));
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                                    cmdKardexBELLAVISTA.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                                    cmdKardexBELLAVISTA.ExecuteNonQuery();
                                    cmdKardexBELLAVISTA.Dispose();
                                }
                            }



                            //Registrar Documentos de Orden de Traslado
                            SqlCommand cmdOrdenTrasladoDocumentoBELLAVISTA = new SqlCommand();
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Connection = cn;
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Transaction = tran;
                            cmdOrdenTrasladoDocumentoBELLAVISTA.CommandType = CommandType.StoredProcedure;
                            cmdOrdenTrasladoDocumentoBELLAVISTA.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Parameters.AddWithValue("@n_IdAlmacen", 2);
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                            cmdOrdenTrasladoDocumentoBELLAVISTA.ExecuteNonQuery();
                            cmdOrdenTrasladoDocumentoBELLAVISTA.Dispose();

                            //Actualizar Correlativo de Nota de Ingreso
                            SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                            cmdNotaIngresoCorrelativo.Connection = cn;
                            cmdNotaIngresoCorrelativo.Transaction = tran;
                            cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", 2);
                            cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                            cmdNotaIngresoCorrelativo.Dispose();

                        }
#endregion
                #region NotaIngreso PlayCentral
                        if (x == 6 && TieneCantidadPlayCentral == true)//PlayCentral
                        {
                            //Registrar Cabecera de la Nota de Ingreso
                            SqlCommand cmdNotaIngreso = new SqlCommand();
                            cmdNotaIngreso.Connection = cn;
                            cmdNotaIngreso.Transaction = tran;
                            cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", 5);
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                            cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                            cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                            string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                            cmdNotaIngreso.Dispose();

                            if (n_IdNotaIngreso.Trim() == "0")
                            {
                                tran.Rollback();
                                cn.Close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                                return;
                            }

                            string v_NumeroNotaIngreso = "";
                            SqlCommand cmdIngreso = new SqlCommand();
                            cmdIngreso.Connection = cn;
                            cmdIngreso.Transaction = tran;
                            cmdIngreso.CommandType = CommandType.Text;
                            cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                            v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                            cmdIngreso.Dispose();

                            for (int i = 0; i < gv.Rows.Count; i++)
                            {
                                //Registrar Detalle Nota de Ingreso PLAYCENTRAL
                                TextBox txtCantidadPLAYCENTRAL = new TextBox();
                                txtCantidadPLAYCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                                if (int.Parse(txtCantidadPLAYCENTRAL.Text) > 0)
                                {
                                    SqlCommand cmdDetalleNotaIngresoPLAYCENTRAL = new SqlCommand();
                                    cmdDetalleNotaIngresoPLAYCENTRAL.Connection = cn;
                                    cmdDetalleNotaIngresoPLAYCENTRAL.Transaction = tran;
                                    cmdDetalleNotaIngresoPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                                    cmdDetalleNotaIngresoPLAYCENTRAL.CommandText = "Play_NotaIngresoDetalle_Insert";
                                    cmdDetalleNotaIngresoPLAYCENTRAL.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                                    cmdDetalleNotaIngresoPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdDetalleNotaIngresoPLAYCENTRAL.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                                    cmdDetalleNotaIngresoPLAYCENTRAL.ExecuteNonQuery();
                                    cmdDetalleNotaIngresoPLAYCENTRAL.Dispose();

                                    Label lblIdAlmacenPLAYCENTRAL = new Label();
                                    lblIdAlmacenPLAYCENTRAL = (Label)gv.Rows[i].FindControl("lblIdAlmacenPLAYCENTRAL");

                                    //Actualizar Stock
                                    SqlCommand cmdStockPLAYCENTRAL = new SqlCommand();
                                    cmdStockPLAYCENTRAL.Connection = cn;
                                    cmdStockPLAYCENTRAL.Transaction = tran;
                                    cmdStockPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                                    cmdStockPLAYCENTRAL.CommandText = "Play_Stock_Actualizar";
                                    cmdStockPLAYCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenPLAYCENTRAL.Text);
                                    cmdStockPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdStockPLAYCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                                    cmdStockPLAYCENTRAL.ExecuteNonQuery();
                                    cmdStockPLAYCENTRAL.Dispose();

                                    //Registrar Kardex
                                    SqlCommand cmdKardexPLAYCENTRAL = new SqlCommand();
                                    cmdKardexPLAYCENTRAL.Connection = cn;
                                    cmdKardexPLAYCENTRAL.Transaction = tran;
                                    cmdKardexPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                                    cmdKardexPLAYCENTRAL.CommandText = "Play_Kardex_Insertar";
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenPLAYCENTRAL.Text);
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadPLAYCENTRAL.Text));
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                                    cmdKardexPLAYCENTRAL.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                                    cmdKardexPLAYCENTRAL.ExecuteNonQuery();
                                    cmdKardexPLAYCENTRAL.Dispose();
                                }
                            }



                            //Registrar Documentos de Orden de Traslado
                            SqlCommand cmdOrdenTrasladoDocumentoPLAYCENTRAL = new SqlCommand();
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Connection = cn;
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Transaction = tran;
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.CommandType = CommandType.StoredProcedure;
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", 5);
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.ExecuteNonQuery();
                            cmdOrdenTrasladoDocumentoPLAYCENTRAL.Dispose();

                            //Actualizar Correlativo de Nota de Ingreso
                            SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                            cmdNotaIngresoCorrelativo.Connection = cn;
                            cmdNotaIngresoCorrelativo.Transaction = tran;
                            cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", 5);
                            cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                            cmdNotaIngresoCorrelativo.Dispose();

                        }
                        #endregion
                #region NotaIngreso AlmacenCentral
                        if (x == 7 && TieneCantidadAlmacenCentral == true)//AlmacénCentral
                        {
                            //Registrar Cabecera de la Nota de Ingreso
                            SqlCommand cmdNotaIngreso = new SqlCommand();
                            cmdNotaIngreso.Connection = cn;
                            cmdNotaIngreso.Transaction = tran;
                            cmdNotaIngreso.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngreso.CommandText = "Play_NotaIngreso_Insertar";
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdAlmacen", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdMotivoTraslado", 6);
                            cmdNotaIngreso.Parameters.AddWithValue("@d_FechaEmision", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                            cmdNotaIngreso.Parameters.AddWithValue("@v_Referencia", lblNumero.Text);
                            cmdNotaIngreso.Parameters.AddWithValue("@t_Observacion", txtObservacion.Text.Trim());
                            cmdNotaIngreso.Parameters.AddWithValue("@n_IdUsuarioCreacion", n_IdUsuario);

                            string n_IdNotaIngreso = cmdNotaIngreso.ExecuteScalar().ToString();
                            cmdNotaIngreso.Dispose();

                            if (n_IdNotaIngreso.Trim() == "0")
                            {
                                tran.Rollback();
                                cn.Close();
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'El correlativo de la Nota de Ingreso ha terminado' });</script>", false);
                                return;
                            }

                            string v_NumeroNotaIngreso = "";
                            SqlCommand cmdIngreso = new SqlCommand();
                            cmdIngreso.Connection = cn;
                            cmdIngreso.Transaction = tran;
                            cmdIngreso.CommandType = CommandType.Text;
                            cmdIngreso.CommandText = "select v_NumeroNotaIngreso from NotaIngreso where n_IdNotaIngreso = " + n_IdNotaIngreso;
                            v_NumeroNotaIngreso = cmdIngreso.ExecuteScalar().ToString();
                            cmdIngreso.Dispose();

                            for (int i = 0; i < gv.Rows.Count; i++)
                            {
                                //Registrar Detalle Nota de Ingreso ALMACENCENTRAL
                                TextBox txtCantidadALMACENCENTRAL = new TextBox();
                                txtCantidadALMACENCENTRAL = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");
                                if (int.Parse(txtCantidadALMACENCENTRAL.Text) > 0)
                                {
                                    SqlCommand cmdDetalleNotaIngresoALMACENCENTRAL = new SqlCommand();
                                    cmdDetalleNotaIngresoALMACENCENTRAL.Connection = cn;
                                    cmdDetalleNotaIngresoALMACENCENTRAL.Transaction = tran;
                                    cmdDetalleNotaIngresoALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                                    cmdDetalleNotaIngresoALMACENCENTRAL.CommandText = "Play_NotaIngresoDetalle_Insert";
                                    cmdDetalleNotaIngresoALMACENCENTRAL.Parameters.AddWithValue("@n_IdNotaIngreso", n_IdNotaIngreso);
                                    cmdDetalleNotaIngresoALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdDetalleNotaIngresoALMACENCENTRAL.Parameters.AddWithValue("@i_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                                    cmdDetalleNotaIngresoALMACENCENTRAL.ExecuteNonQuery();
                                    cmdDetalleNotaIngresoALMACENCENTRAL.Dispose();

                                    Label lblIdAlmacenCentral = new Label();
                                    lblIdAlmacenCentral = (Label)gv.Rows[i].FindControl("lblIdAlmacenCentral");

                                    //Actualizar Stock
                                    SqlCommand cmdStockALMACENCENTRAL = new SqlCommand();
                                    cmdStockALMACENCENTRAL.Connection = cn;
                                    cmdStockALMACENCENTRAL.Transaction = tran;
                                    cmdStockALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                                    cmdStockALMACENCENTRAL.CommandText = "Play_Stock_Actualizar";
                                    cmdStockALMACENCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenCentral.Text);
                                    cmdStockALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdStockALMACENCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                                    cmdStockALMACENCENTRAL.ExecuteNonQuery();
                                    cmdStockALMACENCENTRAL.Dispose();

                                    //Registrar Kardex
                                    SqlCommand cmdKardexALMACENCENTRAL = new SqlCommand();
                                    cmdKardexALMACENCENTRAL.Connection = cn;
                                    cmdKardexALMACENCENTRAL.Transaction = tran;
                                    cmdKardexALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                                    cmdKardexALMACENCENTRAL.CommandText = "Play_Kardex_Insertar";
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@d_FechaMovimiento", DateTime.Parse(txtFechaInicial.Text + " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00")));
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@c_TipoMovimiento", "I");
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@i_IdMotivoTraslado", 6);
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdProducto", gv.DataKeys[i].Value);
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", lblIdAlmacenCentral.Text);
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@f_Cantidad", int.Parse(txtCantidadALMACENCENTRAL.Text));
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdCliente", DBNull.Value);
                                    cmdKardexALMACENCENTRAL.Parameters.AddWithValue("@n_IdProveedor", DBNull.Value);
                                    cmdKardexALMACENCENTRAL.ExecuteNonQuery();
                                    cmdKardexALMACENCENTRAL.Dispose();
                                }
                            }



                            //Registrar Documentos de Orden de Traslado
                            SqlCommand cmdOrdenTrasladoDocumentoALMACENCENTRAL = new SqlCommand();
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Connection = cn;
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Transaction = tran;
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.CommandType = CommandType.StoredProcedure;
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.CommandText = "Play_OrdenTrasladoDocumento_Insertar";
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Parameters.AddWithValue("@i_IdOrdenTraslado", i_IdOrdenTraslado);
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Parameters.AddWithValue("@n_IdAlmacen", 6);
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Parameters.AddWithValue("@v_NumeroDocumento", v_NumeroNotaIngreso);
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.ExecuteNonQuery();
                            cmdOrdenTrasladoDocumentoALMACENCENTRAL.Dispose();

                            //Actualizar Correlativo de Nota de Ingreso
                            SqlCommand cmdNotaIngresoCorrelativo = new SqlCommand();
                            cmdNotaIngresoCorrelativo.Connection = cn;
                            cmdNotaIngresoCorrelativo.Transaction = tran;
                            cmdNotaIngresoCorrelativo.CommandType = CommandType.StoredProcedure;
                            cmdNotaIngresoCorrelativo.CommandText = "Play_Correlativo_Aumentar";
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdTipoDocumento", 8);
                            cmdNotaIngresoCorrelativo.Parameters.AddWithValue("@n_IdAlmacen", 6);
                            cmdNotaIngresoCorrelativo.ExecuteNonQuery();
                            cmdNotaIngresoCorrelativo.Dispose();

                        }
                        #endregion

                    }
                }


                tran.Commit();
                BloquearOrden();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.notice({ message: 'Orden de Traslado Registrada Satisfactoriamente' });</script>", false);
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
        else 
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$.growl.warning({ message: 'Su sesión ha caducado. Vuelva a ingresar al sistema.' });</script>", false);
        }
        
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrearOrdenTraslado.aspx");
    }

    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove("Detalle");
        Response.Redirect("ListarOrdenTraslado.aspx");
    }

    protected void txtCantidadMINKA_TextChanged(object sender, EventArgs e)
    {
        ValidarCantidades();
    }

    protected void txtCantidadSANTAANITA_TextChanged(object sender, EventArgs e)
    {
        ValidarCantidades();
    }

    protected void txtCantidadSANTACLARA_TextChanged(object sender, EventArgs e)
    {
        ValidarCantidades();
    }

    protected void txtCantidadBELLAVISTA_TextChanged(object sender, EventArgs e)
    {
        ValidarCantidades();
    }

    void ValidarCantidades() 
    {
        int Cantidad;
        int Almacen1 = 0;
        int Almacen2 = 0;
        int Almacen3 = 0;
        int Almacen4 = 0;
        int Almacen5 = 0;
        int Almacen6 = 0;

        int TotalAlmacen = 0;
        int Diferencia = 0;
        int ValorUltimaCasilla = 0;
        int Resta = 0;
        TextBox txtAlmacen1 = new TextBox();
        TextBox txtAlmacen2 = new TextBox();
        TextBox txtAlmacen3 = new TextBox();
        TextBox txtAlmacen4 = new TextBox();
        TextBox txtAlmacen5 = new TextBox();
        TextBox txtAlmacen6 = new TextBox();

        int UltimaFila = UltimaFilaVisible();

        for (int i = 0; i < gv.Rows.Count; i++)
        {
            Cantidad = int.Parse(gv.Rows[i].Cells[0].Text);

            txtAlmacen1 = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
            txtAlmacen2 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
            txtAlmacen3 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
            txtAlmacen4 = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
            txtAlmacen5 = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
            txtAlmacen6 = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");


            Almacen1 = int.Parse(txtAlmacen1.Text);
            Almacen2 = int.Parse(txtAlmacen2.Text);
            Almacen3 = int.Parse(txtAlmacen3.Text);
            Almacen4 = int.Parse(txtAlmacen4.Text);
            Almacen5 = int.Parse(txtAlmacen5.Text);
            Almacen6 = int.Parse(txtAlmacen6.Text);


            TotalAlmacen = Almacen1 + Almacen2 + Almacen3 + Almacen4 + Almacen5 + Almacen6;

            if (TotalAlmacen > Cantidad)
            {
                Diferencia = TotalAlmacen - Cantidad;
                TextBox txt = (TextBox)gv.Rows[i].Cells[UltimaFila].Controls[1];
                ValorUltimaCasilla = int.Parse(txt.Text);
                Resta = ValorUltimaCasilla - Diferencia;
                if (Resta < 0)
                {
                    txt.Text = "0";
                    txtAlmacen1 = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                    txtAlmacen2 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                    txtAlmacen3 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                    txtAlmacen4 = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                    txtAlmacen5 = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                    txtAlmacen6 = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");

                    Almacen1 = int.Parse(txtAlmacen1.Text);
                    Almacen2 = int.Parse(txtAlmacen2.Text);
                    Almacen3 = int.Parse(txtAlmacen3.Text);
                    Almacen4 = int.Parse(txtAlmacen4.Text);
                    Almacen5 = int.Parse(txtAlmacen5.Text);
                    Almacen6 = int.Parse(txtAlmacen6.Text);

                    TotalAlmacen = Almacen1 + Almacen2 + Almacen3 + Almacen4 + Almacen5 + Almacen6;

                    if (TotalAlmacen > Cantidad)
                    {
                        Diferencia = TotalAlmacen - Cantidad;
                        TextBox txt2 = (TextBox)gv.Rows[i].Cells[UltimaFila-1].Controls[1];
                        ValorUltimaCasilla = int.Parse(txt2.Text);
                        Resta = ValorUltimaCasilla - Diferencia;
                        if (Resta < 0)
                        {
                            txt2.Text = "0";
                            txtAlmacen1 = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                            txtAlmacen2 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                            txtAlmacen3 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                            txtAlmacen4 = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                            txtAlmacen5 = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                            txtAlmacen6 = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");

                            Almacen1 = int.Parse(txtAlmacen1.Text);
                            Almacen2 = int.Parse(txtAlmacen2.Text);
                            Almacen3 = int.Parse(txtAlmacen3.Text);
                            Almacen4 = int.Parse(txtAlmacen4.Text);
                            Almacen5 = int.Parse(txtAlmacen5.Text);
                            Almacen6 = int.Parse(txtAlmacen6.Text);

                            TotalAlmacen = Almacen1 + Almacen2 + Almacen3 + Almacen4 + Almacen5 + Almacen6;

                            if (TotalAlmacen > Cantidad)
                            {
                                Diferencia = TotalAlmacen - Cantidad;
                                TextBox txt3 = (TextBox)gv.Rows[i].Cells[UltimaFila - 2].Controls[1];
                                ValorUltimaCasilla = int.Parse(txt3.Text);
                                Resta = ValorUltimaCasilla - Diferencia;
                                if (Resta < 0) 
                                {
                                    txt3.Text = "0";
                                    txtAlmacen1 = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                                    txtAlmacen2 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                                    txtAlmacen3 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                                    txtAlmacen4 = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                                    txtAlmacen5 = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                                    txtAlmacen6 = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");

                                    Almacen1 = int.Parse(txtAlmacen1.Text);
                                    Almacen2 = int.Parse(txtAlmacen2.Text);
                                    Almacen3 = int.Parse(txtAlmacen3.Text);
                                    Almacen4 = int.Parse(txtAlmacen4.Text);
                                    Almacen5 = int.Parse(txtAlmacen5.Text);
                                    Almacen6 = int.Parse(txtAlmacen6.Text);

                                    TotalAlmacen = Almacen1 + Almacen2 + Almacen3 + Almacen4 + Almacen5 + Almacen6;

                                    if (TotalAlmacen > Cantidad)
                                    {
                                        Diferencia = TotalAlmacen - Cantidad;
                                        TextBox txt4 = (TextBox)gv.Rows[i].Cells[UltimaFila - 3].Controls[1];
                                        ValorUltimaCasilla = int.Parse(txt4.Text);
                                        Resta = ValorUltimaCasilla - Diferencia;
                                        if (Resta < 0)
                                        {
                                            txt4.Text = "0";
                                            txtAlmacen1 = (TextBox)gv.Rows[i].FindControl("txtCantidadMINKA");
                                            txtAlmacen2 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTAANITA");
                                            txtAlmacen3 = (TextBox)gv.Rows[i].FindControl("txtCantidadSANTACLARA");
                                            txtAlmacen4 = (TextBox)gv.Rows[i].FindControl("txtCantidadBELLAVISTA");
                                            txtAlmacen5 = (TextBox)gv.Rows[i].FindControl("txtCantidadPLAYCENTRAL");
                                            txtAlmacen6 = (TextBox)gv.Rows[i].FindControl("txtCantidadALMACENCENTRAL");

                                            Almacen1 = int.Parse(txtAlmacen1.Text);
                                            Almacen2 = int.Parse(txtAlmacen2.Text);
                                            Almacen3 = int.Parse(txtAlmacen3.Text);
                                            Almacen4 = int.Parse(txtAlmacen4.Text);
                                            Almacen5 = int.Parse(txtAlmacen5.Text);
                                            Almacen6 = int.Parse(txtAlmacen6.Text);

                                            TotalAlmacen = Almacen1 + Almacen2 + Almacen3 + Almacen4 + Almacen5 + Almacen6;

                                            if (TotalAlmacen > Cantidad)
                                            {
                                                Diferencia = TotalAlmacen - Cantidad;
                                                TextBox txt5 = (TextBox)gv.Rows[i].Cells[UltimaFila - 4].Controls[1];
                                                ValorUltimaCasilla = int.Parse(txt5.Text);
                                                Resta = ValorUltimaCasilla - Diferencia;
                                                if (Resta < 0)
                                                {
                                                    txt5.Text = "0";
                                                }
                                                else 
                                                {
                                                    txt5.Text = Resta.ToString();
                                                }
                                            }
                                        }
                                        else 
                                        {
                                            txt4.Text = Resta.ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    txt3.Text = Resta.ToString();
                                }
                            }
                        }
                        else 
                        {
                            txt2.Text = Resta.ToString();
                        }
                    }
                }
                else
                {
                    txt.Text = Resta.ToString();
                }
            }

        }


    }

    int UltimaFilaVisible() 
    {
        int UltimaFila = 0;
        for (int i = 0; i < gv.Columns.Count; i++)
        {
            if (gv.Columns[i].Visible == true) 
            {
                UltimaFila = i;
            }
        }
        return UltimaFila;
    }

    void BloquearOrden() 
    {
        ddlAlmacen.Enabled = false;
        txtFechaInicial.Enabled = false;
        gv.Enabled = false;
        txtObservacion.Enabled = false;
        btnGuardar.Enabled = false;
        lnkAgregarProducto.Enabled = false;
    }

    protected void txtCantidadAlmacenCentral_TextChanged(object sender, EventArgs e)
    {
        ValidarCantidades();
    }

    protected void txtCantidadPLAYCENTRAL_TextChanged(object sender, EventArgs e)
    {
        ValidarCantidades();
    }

    protected void ibEstablecerSucursal_Click(object sender, ImageClickEventArgs e)
    {
        ddlAlmacen.Enabled = false;
        lnkAgregarProducto.Enabled = true;
        ibEstablecerSucursal.Visible = false;
    }

    protected void lnkAgregarProducto_Click(object sender, EventArgs e)
    {
        panelProductos.Visible = true;
        tblGeneral.Visible = false;
    }

    protected void gvBuscar_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
    {
        if (e.CommandArgs.CommandName == "Seleccionar") 
        {
            int n_IdProducto = int.Parse(e.KeyValue.ToString());
            SqlDataAdapter daBusqueda = new SqlDataAdapter("Play_Stock_Pivot_Almacen_Listar_2 " + ddlAlmacen.SelectedValue + "," + n_IdProducto, conexion);
            DataTable dtBusqueda = new DataTable();
            daBusqueda.Fill(dtBusqueda);
            if (dtBusqueda.Rows.Count > 0) 
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle = (DataTable)Session["Detalle"];

                //Validar Producto Repetido
                for (int i = 0; i < dtDetalle.Rows.Count; i++)
                {
                    if (dtDetalle.Rows[i]["n_IdProducto"].ToString() == n_IdProducto.ToString()) 
                    {
                        return;
                    }
                }

                DataRow dr;
                dr = dtDetalle.NewRow();
                dr["n_IdProducto"] = dtBusqueda.Rows[0]["n_IdProducto"].ToString();
                dr["Producto"] = dtBusqueda.Rows[0]["Producto"].ToString();
                dr["v_CodigoInterno"] = dtBusqueda.Rows[0]["v_CodigoInterno"].ToString();
                dr["Almacen Central"] = dtBusqueda.Rows[0]["Almacen Central"].ToString();
                dr["Bellavista"] = dtBusqueda.Rows[0]["Bellavista"].ToString();
                dr["Minka"] = dtBusqueda.Rows[0]["Minka"].ToString();
                dr["EL AGUSTINO"] = dtBusqueda.Rows[0]["EL AGUSTINO"].ToString();
                dr["Santa Anita"] = dtBusqueda.Rows[0]["Santa Anita"].ToString();
                dr["Santa Clara"] = dtBusqueda.Rows[0]["Santa Clara"].ToString();
                dr["n_IdAlmacen"] = dtBusqueda.Rows[0]["n_IdAlmacen"].ToString();
                dr["f_StockDisponible"] = dtBusqueda.Rows[0]["f_StockDisponible"].ToString();
                dr["f_StockContable"] = dtBusqueda.Rows[0]["f_StockContable"].ToString();
                dtDetalle.Rows.Add(dr);
                Session["Detalle"] = dtDetalle;
                gv.DataSource = dtDetalle;
                gv.DataBind();

                string Almacen = ddlAlmacen.SelectedItem.Text;
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    if (gv.Columns[i].HeaderText.Trim().ToUpper() == Almacen.Trim().ToUpper())
                    {
                        gv.Columns[i].Visible = false;
                    }
                    else
                    {
                        gv.Columns[i].Visible = true;
                    }
                }

                panelProductos.Visible = false;
                tblGeneral.Visible = true;
            }
        }
    }

    protected void btnSalirBusqueda_Click(object sender, ImageClickEventArgs e)
    {
        panelProductos.Visible = false;
        tblGeneral.Visible = true;
    }
}