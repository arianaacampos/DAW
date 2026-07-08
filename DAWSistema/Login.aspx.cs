using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAWSistema
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si el usuario ya está logueado y quiere volver al login de vivo, lo mandamos al menú
            if (Session["UsuarioLogueado"] != null)
            {
                Response.Redirect("Principal.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string claveIngresada = txtClave.Text.Trim();

            // Ejecutamos tu lógica de negocios (UsuarioGestor)
            UsuarioGestor gestor = new UsuarioGestor();
            string resultado = gestor.IntentarLogin(usuarioIngresado, claveIngresada);

            if (resultado == "OK")
            {
                // Guardamos la sesión activa
                Session["UsuarioLogueado"] = usuarioIngresado;

                // Bitácora automática silenciosa
                BitacoraGestor.RegistrarAccion(usuarioIngresado, "Login exitoso");

                // Redirigimos al menú principal
                Response.Redirect("Principal.aspx");
            }
            else
            {
                // Mostramos el error devuelto por la BLL (ej. Contraseña incorrecta, Cuenta bloqueada, etc.)
                lblMensaje.Text = resultado;

                // Registramos el incidente en la bitácora
                string usuarioFallo = string.IsNullOrEmpty(usuarioIngresado) ? "Desconocido" : usuarioIngresado;
                BitacoraGestor.RegistrarAccion(usuarioFallo, "Fallo de login: " + resultado);
            }
        }
    }
    }
        