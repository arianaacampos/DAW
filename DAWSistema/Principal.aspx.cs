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

            if (SessionManager.GetInstance.Usuario == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // 🔥 Leemos con Singleton
                string usuario = SessionManager.GetInstance.Usuario;
                lblUsuario.Text = usuario;
                lblUsuarioSidebar.Text = usuario;

                cardFlota.Visible = false;
                cardBitacora.Visible = false;
                cardSeguridad.Visible = false;
                linkBitacora.Visible = false;

                if (usuario.ToLower() == "juan")
                {
                    // Usuario normal: Solo reservas (la tarjeta de clave ya es fija para todos)
                }
                else if (usuario.ToLower() == "admin")
                {
                    cardFlota.Visible = true;
                    cardBitacora.Visible = true;
                    linkBitacora.Visible = true;
                }
                else if (usuario.ToLower() == "master")
                {
                    cardBitacora.Visible = true;
                    linkBitacora.Visible = true;
                    cardSeguridad.Visible = true;
                    cardReservas.Visible = false;
                    linkReservas.Visible = false;
                }
            }
        }
        protected void btnMisReservas_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisReservas.aspx");
        }

        protected void btnBitacora_Click(object sender, EventArgs e)
        {
            Response.Redirect("Bitacora.aspx");
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario != null)
            {
                string usuario = SessionManager.GetInstance.Usuario;
                BitacoraGestor.RegistrarAccion(usuario, "Logout exitoso");
            }

            // 🔥 Cerramos sesión con Singleton
            SessionManager.GetInstance.Logout();
            Response.Redirect("Login.aspx");
        }
        protected void btnVehiculos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vehiculos.aspx");
        }

        protected void btnCambiarClave_Click(object sender, EventArgs e)
        {
            Response.Redirect("CambiarClave.aspx");
        }

    }
}