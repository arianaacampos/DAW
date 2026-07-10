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
    public partial class Vehiculos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarBarraSuperior();
            }
        }
        private void ConfigurarBarraSuperior()
        {
            if (SessionManager.GetInstance.Usuario == null)
            {
                btnAcceso.Text = "👤 Iniciar Sesión";
                btnSalir.Visible = false;
                linkPanel.Visible = false;
            }
            else
            {
                string usuario = SessionManager.GetInstance.Usuario;
                btnAcceso.Text = $"🏠 Mi Panel ({usuario})";
                btnSalir.Visible = true;
                linkPanel.Visible = true;
            }
        }

        protected void btnAcceso_Click(object sender, EventArgs e)
        {

            if (SessionManager.GetInstance.Usuario != null)
            {
                Response.Redirect("Principal.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            SessionManager.GetInstance.Logout();
            Response.Redirect("Inicio.aspx");
        }

        protected void btnResPeugeot_Click(object sender, EventArgs e) { ProcesarReserva("Peugeot 208 Allure"); }
        protected void btnResCorolla_Click(object sender, EventArgs e) { ProcesarReserva("Toyota Corolla Cross"); }
        protected void btnResFrontier_Click(object sender, EventArgs e) { ProcesarReserva("Nissan Frontier S"); }
        protected void btnResHiace_Click(object sender, EventArgs e) { ProcesarReserva("Toyota Hiace AT"); }
        protected void btnResVirtus_Click(object sender, EventArgs e) { ProcesarReserva("Volkswagen Virtus MSI"); }

        private void ProcesarReserva(string modeloVehiculo)
        {
            if (Session["FechaInicio"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('⚠️ Primero tenes que elegir las fechas de tu viaje.'); window.location='Inicio.aspx';", true);
                return;
            }

            if (SessionManager.GetInstance.Usuario == null)
            {
                string scriptLogin = "alert('⚠️ Iniciá sesión'); window.location.href='Login.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "AlertaLogin", scriptLogin, true);
                return;
            }

            string usuarioLogueado = SessionManager.GetInstance.Usuario;
            DateTime fechaSeleccionada = Convert.ToDateTime(Session["FechaInicio"].ToString());

            ReservaGestor gestor = new ReservaGestor();
            gestor.RegistrarReserva(usuarioLogueado, modeloVehiculo, fechaSeleccionada);

            string scriptExito = $"alert('Reservaste tu {modeloVehiculo}.'); window.location.href='MisReservas.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "PopupReserva", scriptExito, true);
        }
    }
}
