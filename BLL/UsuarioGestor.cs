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
        public string IntentarLogin(string username, string passwordPlana)
        {
            UsuarioMapper mapper = new UsuarioMapper();
            UsuarioBE user = mapper.ObtenerUsuario(username);

            if (user == null) return "El usuario no existe.";
            if (user.Bloqueado) return "La cuenta está bloqueada por seguridad.";

            // 🔥 ENCRIPTAMOS LO QUE ESCRIBIÓ EL USUARIO EN LA PANTALLA
            string passwordHasheada = Criptografia.EncriptarSHA256(passwordPlana);

            // COMPRAMOS HASH VS HASH
            if (user.Password != passwordHasheada)
            {
                // Falló: Le sumamos 1 intento
                user.IntentosFallidos++;
                if (user.IntentosFallidos >= 3) user.Bloqueado = true;

                mapper.ActualizarIntentos(username, user.IntentosFallidos, user.Bloqueado);

                return user.Bloqueado ? "Cuenta bloqueada por múltiples intentos." : $"Contraseña incorrecta. Intento {user.IntentosFallidos}/3.";
            }
            else
            {
                // Éxito: Reseteamos los intentos a 0
                mapper.ActualizarIntentos(username, 0, false);

                // 🔥 GUARDAMOS USUARIO Y ROL EN EL SINGLETON (Dejamos de depender de la UI)
                SessionManager.GetInstance.Usuario = user.Username;
                SessionManager.GetInstance.Rol = user.Rol;

                return "OK";
            }
        }

        public void CambiarClave(string username, string nuevaClavePlana)
        {
            // 🔥 Encriptamos la clave nueva antes de mandarla a SQL
            string hashNuevaClave = Criptografia.EncriptarSHA256(nuevaClavePlana);

            UsuarioMapper mapper = new UsuarioMapper();
            mapper.ActualizarPassword(username, hashNuevaClave);

            // Registramos en Bitácora
            BitacoraGestor.RegistrarAccion(username, "Modificó su contraseña.");
        }
    }
}
