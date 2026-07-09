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
                string rol = SessionManager.GetInstance.Rol;

                // 🔥 Validamos contra el ROL, no contra el nombre
                if (rol == "WebMaster")
                {
                    panelMaster.Visible = true;
                    panelBloqueado.Visible = false;

                    if (Session["ErrorIntegridad"] != null)
                        lblDetalleError.Text = Session["ErrorIntegridad"].ToString();
                }
                else
                {
                    panelMaster.Visible = false;
                    panelBloqueado.Visible = true;
                }
            }
        }
        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            SeguridadGestor gestor = new SeguridadGestor();
            gestor.RecalcularDigitos();

            // Listo, arreglado. Lo dejamos entrar al menú principal.
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
            try
            {
                if (!fuRestore.HasFile)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('⚠️ Por favor seleccioná un archivo .bak');", true);
                    return;
                }

                // Guardamos el backup temporalmente y lo restauramos
                string rutaTemporal = @"C:\Backups\RestoreEmergencia.bak";
                fuRestore.SaveAs(rutaTemporal);

                SeguridadGestor gestor = new SeguridadGestor();
                gestor.HacerRestore(rutaTemporal, SessionManager.GetInstance.Usuario);

                ClientScript.RegisterStartupScript(this.GetType(), "Ok", "alert('✅ ¡Sistema restaurado! Ahora podés ingresar normalmente.'); window.location.href='Principal.aspx';", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('❌ Error al restaurar: {ex.Message}');", true);
            }
        }



    }
}
