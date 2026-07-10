using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
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

                SeguridadGestor segGestor = new SeguridadGestor();
                string cadenaFila = nuevoRegistro.Usuario + nuevoRegistro.Accion; 
                long dvh = segGestor.CalcularDVH(cadenaFila);

                BitacoraMapper mapper = new BitacoraMapper();
                mapper.Insertar(nuevoRegistro, dvh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al grabar bitacora: " + ex.Message);
            }
        }


        public DataTable ObtenerHistorialAvanzado(string desde, string hasta, string usuario, string accion)
        {
            BitacoraMapper mapper = new BitacoraMapper();
            return mapper.TraerFiltrado(desde, hasta, usuario, accion);
        }
    }
}
