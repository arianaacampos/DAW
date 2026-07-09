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
    public class VehiculoMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";

        public DataTable TraerTodos()
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT ID, Marca, Modelo, Patente FROM Vehiculos";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                return dt;
            }
        }

        public void Insertar(Vehiculo vehiculo)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO Vehiculos (Marca, Modelo, Patente, DVH) VALUES (@marca, @modelo, @patente, @dvh);";
                query += " UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Vehiculos) WHERE Tabla = 'Vehiculos';";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@patente", vehiculo.Patente);
                cmd.Parameters.AddWithValue("@dvh", vehiculo.DVH);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Vehiculo vehiculo)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "UPDATE Vehiculos SET Marca=@marca, Modelo=@modelo, Patente=@patente, DVH=@dvh WHERE ID=@id;";
                query += " UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Vehiculos) WHERE Tabla = 'Vehiculos';";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", vehiculo.ID);
                cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@patente", vehiculo.Patente);
                cmd.Parameters.AddWithValue("@dvh", vehiculo.DVH);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "DELETE FROM Vehiculos WHERE ID=@id;";
                query += " UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Vehiculos) WHERE Tabla = 'Vehiculos';";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
