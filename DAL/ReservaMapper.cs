using BE;
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
    public class ReservaMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";
        public void Insertar(ReservaBE reserva)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO Reservas (Usuario, Vehiculo, Fecha) VALUES (@usuario, @vehiculo, @fecha)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario", reserva.Usuario);
                cmd.Parameters.AddWithValue("@vehiculo", reserva.Vehiculo);
                cmd.Parameters.AddWithValue("@fecha", reserva.Fecha);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ObtenerPorUsuario(string usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ID, Vehiculo, Fecha FROM Reservas WHERE Usuario = @user ORDER BY Fecha DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@user", usuario);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "DELETE FROM Reservas WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Modificar(int id, DateTime nuevaFecha)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "UPDATE Reservas SET Fecha = @fecha WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fecha", nuevaFecha);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
