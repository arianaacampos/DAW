using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioMapper
    {
        private string cadenaConexion = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";
        public UsuarioBE ObtenerPorUsername(string username)
        {
            UsuarioBE user = null;
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT * FROM Usuarios WHERE Username = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new UsuarioBE();
                    user.ID = (int)reader["ID"];
                    user.Username = reader["Username"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.IntentosFallidos = (int)reader["IntentosFallidos"];
                    user.Bloqueado = (bool)reader["Bloqueado"];
                    user.Rol = reader["Rol"].ToString();
                }
            }
            return user;
        }
        public void ActualizarIntentos(string username, int intentos, bool bloqueado)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                string query = "UPDATE Usuarios SET IntentosFallidos = @intentos, Bloqueado = @bloqueado WHERE Username = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@intentos", intentos);
                cmd.Parameters.AddWithValue("@bloqueado", bloqueado);
                cmd.Parameters.AddWithValue("@user", username);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
