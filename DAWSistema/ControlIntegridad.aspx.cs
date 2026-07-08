using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAWSistema
{
    public partial class ControlIntegridad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario == null) Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                if (SessionManager.GetInstance.Usuario.ToLower() == "master")
                {
                    panelMaster.Visible = true;
                    if (Session["ErrorIntegridad"] != null) lblDetalleError.Text = Session["ErrorIntegridad"].ToString();
                }
                else
                {
                    panelBloqueado.Visible = true;
                }
            }
        }
        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            SeguridadGestor gestor = new SeguridadGestor();
            gestor.RecalcularDigitos();
            // Lo mandamos al panel principal limpio
            Response.Redirect("Principal.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            SessionManager.GetInstance.Logout();
            Response.Redirect("Login.aspx");
        }

        // BOTÓN 2: RESTORE
        protected void btnRestore_Click(object sender, EventArgs e)
        {
            // Lo mandamos a la pantalla de Backup/Restore que hicimos antes
            Response.Redirect("Seguridad.aspx");
        }



    }
}
