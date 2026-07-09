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
                // Usuario sin loguear: Mostramos el botón de Iniciar Sesión normal
                btnAcceso.Text = "👤 Iniciar Sesión";
                btnSalir.Visible = false;
                linkPanel.Visible = false;
            }
            else
            {
                // Ya inició sesión: Le cambiamos el texto y mostramos Cerrar Sesión
                string usuario = SessionManager.GetInstance.Usuario;
                btnAcceso.Text = $"🏠 Mi Panel ({usuario})";
                btnSalir.Visible = true;
                linkPanel.Visible = true;
            }
        }

        protected void btnAcceso_Click(object sender, EventArgs e)
        {
            // Este botón tiene doble función:
            // Si está logueado, lo lleva a su panel. Si no, al login.
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
            // Cierra la sesión con el Singleton
            SessionManager.GetInstance.Logout();
            Response.Redirect("Inicio.aspx");
        }

        // ==========================================
        // RESERVAS
        // ==========================================
        protected void btnResPeugeot_Click(object sender, EventArgs e) { ProcesarReserva("Peugeot 208 Allure"); }
        protected void btnResCorolla_Click(object sender, EventArgs e) { ProcesarReserva("Toyota Corolla Cross"); }
        protected void btnResFrontier_Click(object sender, EventArgs e) { ProcesarReserva("Nissan Frontier S"); }
        protected void btnResHiace_Click(object sender, EventArgs e) { ProcesarReserva("Toyota Hiace AT"); }
        protected void btnResVirtus_Click(object sender, EventArgs e) { ProcesarReserva("Volkswagen Virtus MSI"); }

        private void ProcesarReserva(string modeloVehiculo)
        {
            // 1. Validamos que haya elegido fechas en el inicio
            if (Session["FechaInicio"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('⚠️ Primero tenés que elegir las fechas de tu viaje.'); window.location='Inicio.aspx';", true);
                return;
            }

            // 2. Si no inició sesión, cartel y redirección al login
            if (SessionManager.GetInstance.Usuario == null)
            {
                string scriptLogin = "alert('⚠️ ¡Casi listo! Iniciá sesión o registrate para confirmar la reserva.'); window.location.href='Login.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "AlertaLogin", scriptLogin, true);
                return;
            }

            // 3. Reserva directa guardada en la base de datos
            string usuarioLogueado = SessionManager.GetInstance.Usuario;
            DateTime fechaSeleccionada = Convert.ToDateTime(Session["FechaInicio"].ToString());

            ReservaGestor gestor = new ReservaGestor();
            gestor.RegistrarReserva(usuarioLogueado, modeloVehiculo, fechaSeleccionada);

            string scriptExito = $"alert('¡Felicidades! Reservaste tu {modeloVehiculo}.'); window.location.href='MisReservas.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "PopupReserva", scriptExito, true);
        }
    }
}
