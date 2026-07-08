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
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                string usuario = Session["UsuarioLogueado"].ToString();
                lblUsuario.Text = usuario;

                // ====== CONTROL DE ROLES ======
                if (usuario == "Admin")
                {
                    // Es el jefe: Puede ver la bitácora
                    btnBitacora.Visible = true;
                }
                else
                {
                    // Es un cliente normal: Ocultamos el botón de la bitácora
                    btnBitacora.Visible = false;
                }
            }
        }
        protected void btnVehiculos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vehiculos.aspx");
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bitacora.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            // Registramos la salida en la Bitácora antes de matar la sesión
            if (Session["UsuarioLogueado"] != null)
            {
                string usuario = Session["UsuarioLogueado"].ToString();
                BitacoraGestor.RegistrarAccion(usuario, "Logout exitoso");
            }

            // Matamos el objeto Session para liberar memoria e invalidar el acceso
            Session.Abandon();

            // Lo redirigimos de vuelta al Login
            Response.Redirect("Login.aspx");
        }

    }
}