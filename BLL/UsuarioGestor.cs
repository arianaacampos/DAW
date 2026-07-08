using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioGestor
    {
        public bool ValidarComplejidadClave(string clave)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(clave);
        }
        public string IntentarLogin(string username, string password)
        {
            UsuarioMapper mapper = new UsuarioMapper();
            UsuarioBE user = mapper.ObtenerPorUsername(username);

            if (user == null)
            {
                return "Usuario no encontrado.";
            }

            if (user.Bloqueado)
            {
                return "Cuenta bloqueada por seguridad. Contacte al administrador.";
            }

            if (user.Password == password)
            {
                mapper.ActualizarIntentos(username, 0, false);
                return "OK";
            }
            else
            {
                user.IntentosFallidos++;

                if (user.IntentosFallidos >= 3)
                {
                    mapper.ActualizarIntentos(username, user.IntentosFallidos, true);
                    return "Cuenta bloqueada automáticamente por 3 intentos fallidos.";
                }
                else
                {
                    mapper.ActualizarIntentos(username, user.IntentosFallidos, false);
                    return $"Contraseña incorrecta. Intento {user.IntentosFallidos} de 3.";
                }
            }
        }
        }
}
