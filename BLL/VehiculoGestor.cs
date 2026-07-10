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
    public class VehiculoGestor
    {
        public DataTable ObtenerFlota()
        {
            VehiculoMapper mapper = new VehiculoMapper();
            return mapper.TraerTodos();
        }

        public void GuardarVehiculo(Vehiculo vehiculo, string usuarioLogueado)
        {
            if (string.IsNullOrWhiteSpace(vehiculo.Patente))
            {
                throw new Exception("La patente es obligatoria.");
            }

            SeguridadGestor segGestor = new SeguridadGestor();
            string cadenaFila = vehiculo.Marca + vehiculo.Modelo + vehiculo.Patente;
            vehiculo.DVH = segGestor.CalcularDVH(cadenaFila);

            VehiculoMapper mapper = new VehiculoMapper();

            if (vehiculo.ID == 0) 
            {
                mapper.Insertar(vehiculo);
                BitacoraGestor.RegistrarAccion(usuarioLogueado, $"Dio de ALTA el vehículo Patente: {vehiculo.Patente}");
            }
            else 
            {
                mapper.Actualizar(vehiculo);
                BitacoraGestor.RegistrarAccion(usuarioLogueado, $"MODIFICÓ el vehículo ID {vehiculo.ID}");
            }
        }

        public void EliminarVehiculo(int id, string usuarioLogueado)
        {
            VehiculoMapper mapper = new VehiculoMapper();
            mapper.Eliminar(id);
            BitacoraGestor.RegistrarAccion(usuarioLogueado, $"ELIMINÓ el vehículo ID {id}");
        }
    }
}
