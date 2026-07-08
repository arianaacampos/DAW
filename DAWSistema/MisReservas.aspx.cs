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
    public partial class MisReservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario == null) Response.Redirect("Login.aspx");
            if (!IsPostBack) CargarGrilla();
        }
        private void CargarGrilla()
        {
            string usuario = SessionManager.GetInstance.Usuario;
            ReservaGestor gestor = new ReservaGestor();
            gvMisReservas.DataSource = gestor.VerMisReservas(usuario);
            gvMisReservas.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vehiculos.aspx");
        }

        // --- EVENTO PARA BORRAR ---
        protected void gvMisReservas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvMisReservas.DataKeys[e.RowIndex].Value);
            ReservaGestor gestor = new ReservaGestor();
            gestor.CancelarReserva(id, SessionManager.GetInstance.Usuario);
            CargarGrilla();
        }

        // --- EVENTOS PARA EDITAR LA FECHA ---
        protected void gvMisReservas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMisReservas.EditIndex = e.NewEditIndex;
            CargarGrilla();
        }

        protected void gvMisReservas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMisReservas.EditIndex = -1;
            CargarGrilla();
        }

        protected void gvMisReservas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvMisReservas.DataKeys[e.RowIndex].Value);

            // Atrapamos la nueva fecha que escribió el usuario
            TextBox txtNuevaFecha = (TextBox)gvMisReservas.Rows[e.RowIndex].Cells[2].Controls[0];
            DateTime fechaActualizada = Convert.ToDateTime(txtNuevaFecha.Text);

            ReservaGestor gestor = new ReservaGestor();
            gestor.ModificarReserva(id, fechaActualizada, SessionManager.GetInstance.Usuario);

            gvMisReservas.EditIndex = -1;
            CargarGrilla();
        }
    }
}