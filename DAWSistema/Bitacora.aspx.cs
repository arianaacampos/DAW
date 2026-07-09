using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAWSistema
{
    public partial class Bitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario == null) Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                CargarBitacora("", "", "", "Todos");
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string desde = txtFechaDesde.Text;
            string hasta = txtFechaHasta.Text;
            string usuario = txtFiltroUsuario.Text.Trim();
            string accion = ddlFiltroAccion.SelectedValue;

            CargarBitacora(desde, hasta, usuario, accion);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            txtFiltroUsuario.Text = "";
            ddlFiltroAccion.SelectedValue = "Todos";

            CargarBitacora("", "", "", "Todos");
        }

        private void CargarBitacora(string desde, string hasta, string usuario, string accion)
        {
            BitacoraGestor gestor = new BitacoraGestor();
            // Le pasamos los 4 filtros a la BLL
            DataTable dt = gestor.ObtenerHistorialAvanzado(desde, hasta, usuario, accion);

            dt.Columns.Add("Criticidad", typeof(string));

            foreach (DataRow fila in dt.Rows)
            {
                string accionTexto = fila["Accion"].ToString().ToLower();

                if (accionTexto.Contains("fallo") || accionTexto.Contains("error"))
                    fila["Criticidad"] = "<span class='status-badge status-fallo'>Fallo</span>";
                else
                    fila["Criticidad"] = "<span class='status-badge status-exito'>Éxito</span>";
            }

            dgvBitacora.DataSource = dt;
            dgvBitacora.DataBind();
        }
    }

}
