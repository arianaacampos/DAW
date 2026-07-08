using BE;
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
             if (SessionManager.GetInstance.Usuario == null)
                {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                CargarBitacora();
            }
        }

        private void CargarBitacora()
        {
            // Ajustá la cadena de conexión si tu servidor se llama distinto
            string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                // Traemos los datos de la BD
                string query = "SELECT FechaHora AS Fecha, Usuario, Accion FROM Bitacora ORDER BY FechaHora DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // MAGIA: Agregamos una columna nueva al DataTable "al vuelo" para la Criticidad
                dt.Columns.Add("Criticidad", typeof(string));

                // Recorremos las filas para clasificar si fue un éxito o un fallo
                foreach (DataRow fila in dt.Rows)
                {
                    string accion = fila["Accion"].ToString().ToLower();

                    // Si la acción contiene la palabra "fallo" o "error"
                    if (accion.Contains("fallo") || accion.Contains("error"))
                    {
                        // Le clavamos la etiqueta roja (clase status-fallo)
                        fila["Criticidad"] = "<span class='status-badge status-fallo'>Fallo</span>";
                    }
                    else
                    {
                        // Si no, le clavamos la etiqueta verde (clase status-exito)
                        fila["Criticidad"] = "<span class='status-badge status-exito'>Éxito</span>";
                    }
                }

                // Vinculamos la tabla a la grilla
                dgvBitacora.DataSource = dt;
                dgvBitacora.DataBind();
            }
        }

    }
}