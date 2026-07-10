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
        private string chain = "Data Source=.;Initial Catalog=DAW;Integrated Security=True;Encrypt=False;";

        public UsuarioBE ObtenerUsuario(string username)
        {
            using (SqlConnection conn = new SqlConnection(chain))
            {
                string query = "SELECT ID, Username, Password, IntentosFallidos, Bloqueado, Rol FROM Usuarios WHERE Username = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UsuarioBE user = new UsuarioBE();
                        user.ID = Convert.ToInt32(reader["ID"]);
                        user.Username = reader["Username"].ToString();
                        user.Password = reader["Password"].ToString(); 
                        user.IntentosFallidos = Convert.ToInt32(reader["IntentosFallidos"]);
                        user.Bloqueado = Convert.ToBoolean(reader["Bloqueado"]);
                        user.Rol = reader["Rol"].ToString(); 
                        return user;
                    }
                }
                return null;
            }
        }

        public void ActualizarIntentos(string username, int intentos, bool bloqueado)
        {
            using (SqlConnection conn = new SqlConnection(chain))
            {
                string query = "UPDATE Usuarios SET IntentosFallidos = @int, Bloqueado = @bloq WHERE Username = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@int", intentos);
                cmd.Parameters.AddWithValue("@bloq", bloqueado);
                cmd.Parameters.AddWithValue("@user", username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarPassword(string username, string hashPassword)
        {
            using (SqlConnection conn = new SqlConnection(chain))
            {
                string query = "UPDATE Usuarios SET Password = @pass WHERE Username = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pass", hashPassword);
                cmd.Parameters.AddWithValue("@user", username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool ExisteUsuario(string username)
        {
            using (SqlConnection conn = new SqlConnection(chain))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Username = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public void InsertarUsuario(UsuarioBE usuario, long dvh)
        {
            using (SqlConnection conn = new SqlConnection(chain))
            {
                string query = "INSERT INTO Usuarios (Username, Password, IntentosFallidos, Bloqueado, Rol, DVH) VALUES (@user, @pass, @intentos, @bloq, @rol, @dvh);";
                query += " UPDATE Control_Seguridad SET DVV = (SELECT ISNULL(SUM(DVH),0) FROM Usuarios) WHERE Tabla = 'Usuarios';";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", usuario.Username);
                cmd.Parameters.AddWithValue("@pass", usuario.Password);
                cmd.Parameters.AddWithValue("@intentos", usuario.IntentosFallidos);
                cmd.Parameters.AddWithValue("@bloq", usuario.Bloqueado);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@dvh", dvh);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
