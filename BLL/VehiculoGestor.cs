using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehiculoGestor
    {
        public void RegistrarVehiculo(Vehiculo vehiculo)
        {
            if (string.IsNullOrWhiteSpace(vehiculo.Patente))
            {
                throw new Exception("La patente es obligatoria.");
            }

            VehiculoMapper mapper = new VehiculoMapper();
            mapper.Insertar(vehiculo);
        }
    }
}
