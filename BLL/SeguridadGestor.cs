using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SeguridadGestor
    {
        public long CalcularDVH(string cadenaFila)
        {
            long dvh = 0;
            for (int i = 0; i < cadenaFila.Length; i++)
            {
                int valorAscii = (int)cadenaFila[i];
                dvh += (valorAscii * (i + 1));
            }
            return dvh;
        }

        public string VerificarSistemaCompleto()
        {
            SeguridadMapper mapper = new SeguridadMapper();

            DataTable dtUsuarios = mapper.ObtenerDatosTabla("Usuarios");
            long sumaDvhCalculado = 0;

            foreach (DataRow fila in dtUsuarios.Rows)
            {
                string username = fila["Username"].ToString();
                string password = fila["Password"].ToString();
                string rol = fila["Rol"].ToString();
                string intentos = fila["IntentosFallidos"].ToString();

                long dvhGuardadoEnBd = Convert.ToInt64(fila["DVH"]);

                string cadenaFila = username + password + rol + intentos;
                long dvhCalculadoEnVivo = CalcularDVH(cadenaFila);

                if (dvhCalculadoEnVivo != dvhGuardadoEnBd)
                {
                    return $"Error de Integridad (Horizontal) -> Tabla: Usuarios | Usuario alterado: {username}";
                }
                sumaDvhCalculado += dvhCalculadoEnVivo;
            }

            if (!mapper.ValidarIntegridadTabla("Usuarios")) return "Error Vertical -> Tabla: Usuarios (El total no coincide)";
            if (!mapper.ValidarIntegridadTabla("Reservas")) return "Error Vertical -> Tabla: Reservas (El total no coincide)";
            if (!mapper.ValidarIntegridadTabla("Bitacora")) return "Error Vertical -> Tabla: Bitacora (El total no coincide)";

            return "OK";
        }

        public void RecalcularDigitos()
        {
            SeguridadMapper mapper = new SeguridadMapper();

            DataTable dtUsuarios = mapper.ObtenerDatosTabla("Usuarios");
            foreach (DataRow fila in dtUsuarios.Rows)
            {
                string username = fila["Username"].ToString();
                string password = fila["Password"].ToString();
                string rol = fila["Rol"].ToString();
                string intentos = fila["IntentosFallidos"].ToString();

                string cadenaFila = username + password + rol + intentos;
                long dvhReal = CalcularDVH(cadenaFila);

                int idUsuario = Convert.ToInt32(fila["ID"]);
                mapper.RepararDVHFila("Usuarios", "ID", idUsuario, dvhReal);
            }

            mapper.SincronizarTodosLosDVV();

            BitacoraGestor.RegistrarAccion("Master", "Ejecutó Recálculo Real de DVH y DVV. El sistema fue salvado.");
        }

        public void HacerBackup(string ruta, string usuarioMaster)
        {
            SeguridadMapper mapper = new SeguridadMapper();
            mapper.RealizarBackup(ruta);
            BitacoraGestor.RegistrarAccion(usuarioMaster, "Generó un Backup en: " + ruta);
        }

        public void HacerRestore(string ruta, string usuarioMaster)
        {
            SeguridadMapper mapper = new SeguridadMapper();
            mapper.RealizarRestore(ruta);
            BitacoraGestor.RegistrarAccion(usuarioMaster, "Restauró el sistema desde un Backup.");
        }
    }
}
