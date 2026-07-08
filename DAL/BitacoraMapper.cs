using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BitacoraMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";

        public void Insertar(BitacoraBE registro)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO Bitacora (FechaHora, Usuario, Accion) VALUES (@fecha, @user, @accion)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@fecha", registro.FechaHora);
                cmd.Parameters.AddWithValue("@user", registro.Usuario);
                cmd.Parameters.AddWithValue("@accion", registro.Accion);



                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        }
}
