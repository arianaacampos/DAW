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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] != null)
            {
                Response.Redirect("Principal.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string claveIngresada = txtClave.Text.Trim();

            UsuarioGestor gestor = new UsuarioGestor();
            string resultado = gestor.IntentarLogin(usuarioIngresado, claveIngresada);
            if (resultado == "OK")
            {
                SessionManager.GetInstance.Usuario = usuarioIngresado;
                BitacoraGestor.RegistrarAccion(usuarioIngresado, "Login exitoso");

                SeguridadGestor segGestor = new SeguridadGestor();
                string estadoSistema = segGestor.VerificarSistemaCompleto();

                if (estadoSistema != "OK")
                {
                    Session["ErrorIntegridad"] = estadoSistema;
                    Response.Redirect("ControlIntegridad.aspx");
                }
                else
                {
                    if (Session["FechaInicio"] != null) Response.Redirect("Vehiculos.aspx");
                    else Response.Redirect("Principal.aspx");
                }
            }
        }
    }
    }
        