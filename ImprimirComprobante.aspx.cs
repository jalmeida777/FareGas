using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImprimirComprobante : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            hfIdComprobante.Value = Request.QueryString["i_IdComprobante"];
            ReportViewer1.LocalReport.Refresh();
        }
    }
    protected void btnSalir_Click(object sender, ImageClickEventArgs e)
    {
        int i_IdMenu = int.Parse(Request.QueryString["IdMenu"]);
        Response.Redirect("~/CrearComprobante.aspx?i_IdComprobante=" + hfIdComprobante.Value + "&IdMenu=" + i_IdMenu);
    }
}