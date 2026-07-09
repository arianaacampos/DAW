using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BitacoraMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";

        public void Insertar(BitacoraBE registro, long dvh)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                // 1. Insertamos el registro con su DVH
                string query = "INSERT INTO Bitacora (FechaHora, Usuario, Accion, DVH) VALUES (@fecha, @user, @accion, @dvh);";

                // 2. Actualizamos el DVV general para que el sistema no se bloquee
                query += " UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Bitacora) WHERE Tabla = 'Bitacora';";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fecha", registro.FechaHora);
                cmd.Parameters.AddWithValue("@user", registro.Usuario);
                cmd.Parameters.AddWithValue("@accion", registro.Accion);
                cmd.Parameters.AddWithValue("@dvh", dvh);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Nuevo método que busca los datos, opcionalmente filtrando por usuario
        public DataTable TraerFiltrado(string fechaDesde, string fechaHasta, string usuario, string accion)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                // Truco SQL: WHERE 1=1 siempre es verdad. Sirve para ir pegándole los "AND" cómodamente.
                string query = "SELECT FechaHora AS Fecha, Usuario, Accion FROM Bitacora WHERE 1=1 ";
                SqlCommand cmd = new SqlCommand();

                // 1. Filtro por Fecha Desde
                if (!string.IsNullOrEmpty(fechaDesde))
                {
                    query += " AND FechaHora >= @desde ";
                    cmd.Parameters.AddWithValue("@desde", Convert.ToDateTime(fechaDesde));
                }

                // 2. Filtro por Fecha Hasta (Le sumamos 23:59:59 para que incluya todo el día)
                if (!string.IsNullOrEmpty(fechaHasta))
                {
                    query += " AND FechaHora <= @hasta ";
                    DateTime hasta = Convert.ToDateTime(fechaHasta).AddDays(1).AddSeconds(-1);
                    cmd.Parameters.AddWithValue("@hasta", hasta);
                }

                // 3. Filtro por Usuario
                if (!string.IsNullOrEmpty(usuario))
                {
                    query += " AND Usuario LIKE @user ";
                    cmd.Parameters.AddWithValue("@user", "%" + usuario + "%");
                }

                // 4. Filtro por Acción (Dropdown)
                if (accion != "Todos")
                {
                    query += " AND Accion LIKE @accion ";
                    cmd.Parameters.AddWithValue("@accion", "%" + accion + "%");
                }

                query += " ORDER BY FechaHora DESC";

                cmd.CommandText = query;
                cmd.Connection = conn;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);

                return dt;
            }
        }
    }
}
