using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VehiculoMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";

        public void Insertar(Vehiculo vehiculo)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO Vehiculos (Marca, Modelo, Patente) VALUES (@marca, @modelo, @patente)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@patente", vehiculo.Patente);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
