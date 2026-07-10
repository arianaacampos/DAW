using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAWSistema
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && SessionManager.GetInstance.Usuario != null)
            {
                btnIrLogin.Text = "🏠 Mi Panel";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Session["FechaInicio"] = txtFechaInicio.Text;
            Session["FechaFin"] = txtFechaFin.Text;
            Session["Ciudad"] = txtCiudad.Text;
            Response.Redirect("Vehiculos.aspx");
        }

        protected void btnIrLogin_Click(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario != null)
                Response.Redirect("Principal.aspx");
            else
                Response.Redirect("Login.aspx");
        }
    }
}