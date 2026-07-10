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

            string passwordHasheada = Criptografia.EncriptarSHA256(passwordPlana);

            if (user.Password != passwordHasheada)
            {
                user.IntentosFallidos++;
                if (user.IntentosFallidos >= 3) user.Bloqueado = true;

                mapper.ActualizarIntentos(username, user.IntentosFallidos, user.Bloqueado);

                return user.Bloqueado ? "Cuenta bloqueada por multiples intentos." : $"Contraseña incorrecta. Intento {user.IntentosFallidos}/3.";
            }
            else
            {
                mapper.ActualizarIntentos(username, 0, false);

                SessionManager.GetInstance.Usuario = user.Username;
                SessionManager.GetInstance.Rol = user.Rol;

                return "OK";
            }
        }

        public void CambiarClave(string username, string nuevaClavePlana)
        {
            string hashNuevaClave = Criptografia.EncriptarSHA256(nuevaClavePlana);

            UsuarioMapper mapper = new UsuarioMapper();
            mapper.ActualizarPassword(username, hashNuevaClave);

            BitacoraGestor.RegistrarAccion(username, "Modificó su contraseña.");
        }
        public void RegistrarCliente(string username, string passwordPlana)
        {
            UsuarioMapper mapper = new UsuarioMapper();

            if (mapper.ExisteUsuario(username))
            {
                throw new Exception("El nombre de usuario ya está en uso. Por favor, elegí otro.");
            }

            UsuarioBE nuevoUser = new UsuarioBE();
            nuevoUser.Username = username;
            nuevoUser.Password = Criptografia.EncriptarSHA256(passwordPlana); 
            nuevoUser.IntentosFallidos = 0;
            nuevoUser.Bloqueado = false;
            nuevoUser.Rol = "Cliente";

            SeguridadGestor segGestor = new SeguridadGestor();
            string cadenaFila = nuevoUser.Username + nuevoUser.Password + nuevoUser.Rol + nuevoUser.IntentosFallidos.ToString();
            long dvh = segGestor.CalcularDVH(cadenaFila);

            mapper.InsertarUsuario(nuevoUser, dvh);

            BitacoraGestor.RegistrarAccion(username, "Se registró como nuevo cliente en el sistema.");
        }
    }
}
