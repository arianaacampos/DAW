using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BitacoraGestor
    {
        public static void RegistrarAccion(string usuarioLogueado, string accionRealizada)
        {
            try
            {
                BitacoraBE nuevoRegistro = new BitacoraBE();
                nuevoRegistro.FechaHora = DateTime.Now; 
                nuevoRegistro.Usuario = usuarioLogueado;
                nuevoRegistro.Accion = accionRealizada;

                BitacoraMapper mapper = new BitacoraMapper();
                mapper.Insertar(nuevoRegistro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al grabar bitacora: " + ex.Message);
            }
        }
    }
}
