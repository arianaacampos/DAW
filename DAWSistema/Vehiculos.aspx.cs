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
                if (Session["UsuarioLogueado"] != null)
                {
                    btnAcceso.Text = "🏠 Volver al Panel (" + Session["UsuarioLogueado"].ToString() + ")";
                }
                else
                {
                    btnAcceso.Text = "👤 Iniciar Sesión";
                }
            }
        }

        protected void btnAcceso_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] != null)
            {
                Response.Redirect("Principal.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btnResPeugeot_Click(object sender, EventArgs e)
        {
            ProcesarReserva("Peugeot 208 Allure");
        }

        protected void btnResCorolla_Click(object sender, EventArgs e)
        {
            ProcesarReserva("Toyota Corolla Cross XLI");
        }

        protected void btnResFrontier_Click(object sender, EventArgs e)
        {
            ProcesarReserva("Nissan Frontier S");
        }

        protected void btnResHiace_Click(object sender, EventArgs e)
        {
            ProcesarReserva("Toyota Hiace AT (9 Pasajeros)");
        }

        protected void btnResVirtus_Click(object sender, EventArgs e)
        {
            ProcesarReserva("Volkswagen Virtus MSI");
        }
        protected void btnMisReservas_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] != null) Response.Redirect("MisReservas.aspx");
            else Response.Redirect("Login.aspx");
        }

        private void ProcesarReserva(string modeloVehiculo)
        {
            // 1. Validamos que haya pasado por el Inicio a elegir fechas
            if (Session["FechaInicio"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "alert('⚠️ Primero tenés que elegir las fechas de tu viaje.'); window.location='Inicio.aspx';", true);
                return;
            }

            // 2. Si no está logueado, le avisamos y lo mandamos a loguearse
            if (Session["UsuarioLogueado"] == null)
            {
                string scriptLogin = "alert('⚠️ ¡Casi listo! Iniciá sesión o registrate para confirmar la reserva.'); window.location.href='Login.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "AlertaLogin", scriptLogin, true);
                return;
            }

            // 3. Si ya eligió fecha y está logueado, ¡RESERVAMOS DIRECTO!
            string usuarioLogueado = Session["UsuarioLogueado"].ToString();
            DateTime fechaSeleccionada = Convert.ToDateTime(Session["FechaInicio"].ToString());

            ReservaGestor gestor = new ReservaGestor();
            gestor.RegistrarReserva(usuarioLogueado, modeloVehiculo, fechaSeleccionada);

            // Lo mandamos a su panel de Mis Reservas para que vea que se guardó
            string scriptExito = $"alert('¡Felicidades! Reservaste tu {modeloVehiculo}.'); window.location.href='MisReservas.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "PopupReserva", scriptExito, true);
        }
    }
}