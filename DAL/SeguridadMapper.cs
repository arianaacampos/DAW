using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SeguridadMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";
        private string cadenaMaster = "Data Source=.;Initial Catalog=master;Integrated Security=True;Encrypt=False;";

        // ==========================================
        // 1. MÉTODOS DE INTEGRIDAD Y DÍGITOS
        // ==========================================
        public bool ValidarIntegridadTabla(string nombreTabla)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string querySum = $"SELECT ISNULL(SUM(DVH), 0) FROM {nombreTabla}";
                SqlCommand cmdSum = new SqlCommand(querySum, conn);

                string queryDvv = "SELECT DVV FROM Control_Seguridad WHERE Tabla = @tabla";
                SqlCommand cmdDvv = new SqlCommand(queryDvv, conn);
                cmdDvv.Parameters.AddWithValue("@tabla", nombreTabla);

                conn.Open();
                long sumaDvh = Convert.ToInt64(cmdSum.ExecuteScalar());
                long dvvGuardado = Convert.ToInt64(cmdDvv.ExecuteScalar());

                return sumaDvh == dvvGuardado;
            }
        }

        public DataTable ObtenerDatosTabla(string nombreTabla)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = $"SELECT * FROM {nombreTabla}";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                return dt;
            }
        }

        // 🔥 LA CURA DEL BUCLE 1: Repara la fila rota
        public void RepararDVHFila(string tabla, string columnaId, int id, long dvhReal)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = $"UPDATE {tabla} SET DVH = @dvh WHERE {columnaId} = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dvh", dvhReal);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 🔥 LA CURA DEL BUCLE 2: Sincroniza la Bitácora y todas las tablas con su suma real
        public void SincronizarTodosLosDVV()
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                new SqlCommand("UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Usuarios) WHERE Tabla = 'Usuarios'", conn).ExecuteNonQuery();
                new SqlCommand("UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Reservas) WHERE Tabla = 'Reservas'", conn).ExecuteNonQuery();
                new SqlCommand("UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Bitacora) WHERE Tabla = 'Bitacora'", conn).ExecuteNonQuery();
            }
        }

        // ==========================================
        // 2. MÉTODOS DE BACKUP Y RESTORE
        // ==========================================
        public void RealizarBackup(string rutaDestino)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = $"BACKUP DATABASE DAW TO DISK = '{rutaDestino}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RealizarRestore(string rutaOrigen)
        {
            using (SqlConnection conn = new SqlConnection(cadenaMaster))
            {
                string query = $@"
                    ALTER DATABASE DAW SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    RESTORE DATABASE DAW FROM DISK = '{rutaOrigen}' WITH REPLACE;
                    ALTER DATABASE DAW SET MULTI_USER;";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
