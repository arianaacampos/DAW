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
    public partial class FlotaABM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario == null) Response.Redirect("Login.aspx");
            if (SessionManager.GetInstance.Rol != "Administrador") Response.Redirect("Principal.aspx");

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }
        private void CargarGrilla()
        {
            VehiculoGestor gestor = new VehiculoGestor();
            dgvFlota.DataSource = gestor.ObtenerFlota();
            dgvFlota.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Vehiculo auto = new Vehiculo();
                auto.ID = Convert.ToInt32(hfID.Value);
                auto.Marca = txtMarca.Text.Trim();
                auto.Modelo = txtModelo.Text.Trim();
                auto.Patente = txtPatente.Text.Trim();

                VehiculoGestor gestor = new VehiculoGestor();
                gestor.GuardarVehiculo(auto, SessionManager.GetInstance.Usuario);

                LimpiarFormulario();
                CargarGrilla();
                ClientScript.RegisterStartupScript(this.GetType(), "ok", "alert('✅ Vehículo guardado con éxito');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "err", $"alert('❌ Error: {ex.Message}');", true);
            }
        }

        protected void dgvFlota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = dgvFlota.Rows[index];

                hfID.Value = row.Cells[0].Text;
                txtMarca.Text = Server.HtmlDecode(row.Cells[1].Text);
                txtModelo.Text = Server.HtmlDecode(row.Cells[2].Text);
                txtPatente.Text = Server.HtmlDecode(row.Cells[3].Text);
            }
        }

        protected void dgvFlota_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgvFlota.DataKeys[e.RowIndex].Value);
            VehiculoGestor gestor = new VehiculoGestor();
            gestor.EliminarVehiculo(id, SessionManager.GetInstance.Usuario);
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfID.Value = "0";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtPatente.Text = "";
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
    }
}