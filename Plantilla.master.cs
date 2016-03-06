using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web.ASPxNavBar;

public partial class Plantilla : System.Web.UI.MasterPage
{
    SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false) 
        {
            ListarModulos();
            //ListarTipoCambioDelDia();
            if (Session["MenuHTML"] != null)
            {
            //    Literal1.Text = Session["MenuHTML"].ToString();
            }
            if (Session["IdMenu"] != null) 
            {
                string valor = Session["IdMenu"].ToString();
                MenuItem mi = this.Menu1.FindItem(valor);
                mi.Selected = true;
            }
        }

        Menu1_MenuItemClick(null, null);

        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            if (dtUsuario.Rows[0]["v_RutaFoto"].ToString().Trim() != "")
            {
                imgUsuario.ImageUrl = dtUsuario.Rows[0]["v_RutaFoto"].ToString();
            }
            else
            {
                imgUsuario.ImageUrl = "~/images/face.jpg";
            }
        }
        
    }


    void ListarModulos() 
    {
        if (Session["dtUsuario"] != null)
        {
            DataTable dtUsuario = new DataTable();
            dtUsuario = (DataTable)Session["dtUsuario"];
            string i_IdRol = dtUsuario.Rows[0]["i_IdRol"].ToString();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Play_Menus_Modulos " + i_IdRol, conexion);
            da.Fill(dt);

            MenuItem mnuNewMenuItem;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string i_IdMenu = dt.Rows[i]["i_IdMenu"].ToString();
                string v_Nombre = dt.Rows[i]["v_Nombre"].ToString();
                mnuNewMenuItem = new MenuItem(v_Nombre, i_IdMenu);
                //mnuNewMenuItem.ChildItems.Add(mnuNewMenuItem);
                Menu1.Items.Add(mnuNewMenuItem);
            }
        }
        else 
        {
            Response.Redirect("Login.aspx");
        }
    }

    //void ListarTipoCambioDelDia() 
    //{
    //    int dia = DateTime.Now.Day;
    //    int mes = DateTime.Now.Month;
    //    int año = DateTime.Now.Year;
    //    bool existe = false;
    //    SqlDataAdapter da = new SqlDataAdapter("Play_TC_Existencia " + año + "," + mes + "," + dia, conexion);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt != null)
    //    {
    //        if (dt.Rows.Count > 0)
    //        {
    //            existe = true;
    //        }
    //        else
    //        {
    //            existe = false;
    //        }
    //    }
    //    else
    //    {
    //        existe = false;
    //    }

    //    if (existe == true)
    //    {
    //        lblTC.Text = dt.Rows[0]["f_TC"].ToString();
    //    }
    //    else 
    //    {
    //        lblTC.Text = "0.00";
    //    }
    //}

    //protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    //{
    //    Session["IdMenu"] = Menu1.SelectedItem.Value;
    //    if (Session["dtUsuario"] != null)
    //    {
    //        DataTable dtUsuario = new DataTable();
    //        dtUsuario = (DataTable)Session["dtUsuario"];
    //        string i_IdRol = dtUsuario.Rows[0]["i_IdRol"].ToString();
    //        string i_IdMenuPrincipal = e.Item.Value;

    //        DataTable dt = new DataTable();
    //        SqlDataAdapter da = new SqlDataAdapter("Play_Menus_Menu " + i_IdRol + "," + i_IdMenuPrincipal, conexion);
    //        da.Fill(dt);

    //        Literal1.Text = "";
    //        Literal1.Text = Literal1.Text + "<ul>";

    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            string i_IdMenu = dt.Rows[i]["i_IdMenu"].ToString();
    //            string v_Nombre = dt.Rows[i]["v_Nombre"].ToString();

    //            Literal1.Text = Literal1.Text + "<li>";
    //            Literal1.Text = Literal1.Text + "<h3>&nbsp;" + v_Nombre + "</h3>";

    //            DataTable dtHijos = new DataTable();
    //            SqlDataAdapter daHijos = new SqlDataAdapter("Play_Menus_Menu_Hijos " + i_IdRol + "," + i_IdMenu, conexion);
    //            daHijos.Fill(dtHijos);

    //            if (dtHijos.Rows.Count > 0) { Literal1.Text = Literal1.Text + "<ul>"; }

    //            for (int x = 0; x < dtHijos.Rows.Count; x++)
    //            {
    //                string i_IdMenuHijo = dtHijos.Rows[x]["i_IdMenu"].ToString();
    //                string v_NombreHijo = dtHijos.Rows[x]["v_Nombre"].ToString();
    //                string v_UrlHijo = dtHijos.Rows[x]["v_Url"].ToString();

    //                Literal1.Text = Literal1.Text + " <li><a href='" + v_UrlHijo + "?idMenu=" + i_IdMenuHijo + "'>" + v_NombreHijo + "</a></li>";
    //            }

    //            if (dtHijos.Rows.Count > 0) { Literal1.Text = Literal1.Text + "</ul>"; }

    //            Literal1.Text = Literal1.Text + "</li>";


    //        }

    //        Literal1.Text = Literal1.Text + "</ul>";
    //        Session["MenuHTML"] = Literal1.Text;
    //    }
    //    else
    //    {
    //        Response.Redirect("Login.aspx");
    //    }

    //}

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        if (Menu1.SelectedItem != null)
        {
            Session["IdMenu"] = Menu1.SelectedItem.Value;
            if (Session["dtUsuario"] != null)
            {
                DataTable dtUsuario = new DataTable();
                dtUsuario = (DataTable)Session["dtUsuario"];
                string i_IdRol = dtUsuario.Rows[0]["i_IdRol"].ToString();
                //string i_IdMenuPrincipal = e.Item.Value;
                string i_IdMenuPrincipal = Menu1.SelectedItem.Value;

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Play_Menus_Menu " + i_IdRol + "," + i_IdMenuPrincipal, conexion);
                da.Fill(dt);

                //Literal1.Text = "";
                //Literal1.Text = Literal1.Text + "<ul>";
                Menu.Groups.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string i_IdMenu = dt.Rows[i]["i_IdMenu"].ToString();
                    string v_Nombre = dt.Rows[i]["v_Nombre"].ToString();

                    //Literal1.Text = Literal1.Text + "<li>";
                    //Literal1.Text = Literal1.Text + "<h3>&nbsp;" + v_Nombre + "</h3>";
                    NavBarGroup nvg = new NavBarGroup();
                    nvg.Text = v_Nombre;
                    Menu.Groups.Add(nvg);

                    DataTable dtHijos = new DataTable();
                    SqlDataAdapter daHijos = new SqlDataAdapter("Play_Menus_Menu_Hijos " + i_IdRol + "," + i_IdMenu, conexion);
                    daHijos.Fill(dtHijos);

                    //if (dtHijos.Rows.Count > 0) { Literal1.Text = Literal1.Text + "<ul>"; }

                    for (int x = 0; x < dtHijos.Rows.Count; x++)
                    {
                        string i_IdMenuHijo = dtHijos.Rows[x]["i_IdMenu"].ToString();
                        string v_NombreHijo = dtHijos.Rows[x]["v_Nombre"].ToString();
                        string v_UrlHijo = dtHijos.Rows[x]["v_Url"].ToString();

                        //Literal1.Text = Literal1.Text + " <li><a href='" + v_UrlHijo + "?idMenu=" + i_IdMenuHijo + "'>" + v_NombreHijo + "</a></li>";
                        NavBarItem nvi = new NavBarItem();
                        nvi.Text = v_NombreHijo;
                        nvi.NavigateUrl = v_UrlHijo + "?idMenu=" + i_IdMenuHijo;

                        nvg.Items.Add(nvi);
                    }

                    //if (dtHijos.Rows.Count > 0) { Literal1.Text = Literal1.Text + "</ul>"; }

                    //Literal1.Text = Literal1.Text + "</li>";


                }

                //Literal1.Text = Literal1.Text + "</ul>";
                //Session["MenuHTML"] = Literal1.Text;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Session.Abandon();
    }
}
