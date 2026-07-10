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
    public partial class CambiarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario == null) Response.Redirect("Login.aspx");
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNuevaClave.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('⚠️ Ingresa una contraseña.');", true);
                return;
            }

            string usuario = SessionManager.GetInstance.Usuario;
            string nuevaClave = txtNuevaClave.Text.Trim();

            UsuarioGestor gestor = new UsuarioGestor();
            gestor.CambiarClave(usuario, nuevaClave);

            ClientScript.RegisterStartupScript(this.GetType(), "Exito", "alert('✅ ¡Contraseña cambiada con exito!'); window.location.href='Principal.aspx';", true);
        }
    }
}