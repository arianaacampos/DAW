using System;
using System.Collections.Generic;
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
        public bool ValidarIntegridadTabla(string nombreTabla)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                // 1. Sumamos todos los DVH de la tabla en cuestión
                string querySum = $"SELECT ISNULL(SUM(DVH), 0) FROM {nombreTabla}";
                SqlCommand cmdSum = new SqlCommand(querySum, conn);

                // 2. Buscamos el DVV oficial guardado en la tabla de control
                string queryDvv = "SELECT DVV FROM Control_Seguridad WHERE Tabla = @tabla";
                SqlCommand cmdDvv = new SqlCommand(queryDvv, conn);
                cmdDvv.Parameters.AddWithValue("@tabla", nombreTabla);

                conn.Open();

                long sumaDvh = Convert.ToInt64(cmdSum.ExecuteScalar());
                long dvvGuardado = Convert.ToInt64(cmdDvv.ExecuteScalar());

                // Si son iguales, la tabla está perfecta. Si no, alguien la hackeó.
                return sumaDvh == dvvGuardado;
            }
        }

        // Este método actualiza el DVV oficial después de recalcular o hacer cambios legales
        public void ActualizarDVV(string nombreTabla, long nuevoDVV)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "UPDATE Control_Seguridad SET DVV = @dvv WHERE Tabla = @tabla";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dvv", nuevoDVV);
                cmd.Parameters.AddWithValue("@tabla", nombreTabla);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
