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
    public class ReservaGestor
    {
        public void RegistrarReserva(string usuario, string vehiculo, DateTime fecha)
        {
            ReservaBE nuevaReserva = new ReservaBE();
            nuevaReserva.Usuario = usuario;
            nuevaReserva.Vehiculo = vehiculo;
            nuevaReserva.Fecha = fecha;

            ReservaMapper mapper = new ReservaMapper();
            mapper.Insertar(nuevaReserva);

            BitacoraGestor.RegistrarAccion(usuario, "Reservó: " + vehiculo + " para el " + fecha.ToShortDateString());
        }

        public DataTable VerMisReservas(string usuario)
        {
            ReservaMapper mapper = new ReservaMapper();
            return mapper.ObtenerPorUsuario(usuario);
        }

        public void CancelarReserva(int id, string usuario)
        {
            ReservaMapper mapper = new ReservaMapper();
            mapper.Eliminar(id);
            BitacoraGestor.RegistrarAccion(usuario, "Canceló la reserva ID: " + id);
        }

        public void ModificarReserva(int id, DateTime nuevaFecha, string usuario)
        {
            ReservaMapper mapper = new ReservaMapper();
            mapper.Modificar(id, nuevaFecha);
            BitacoraGestor.RegistrarAccion(usuario, "Modificó reserva ID: " + id + " al " + nuevaFecha.ToShortDateString());
        }
    }
}
