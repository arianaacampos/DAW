using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BE
{
    public class SessionManager
    {
        private SessionManager() { }

        // 2. Propiedad ESTÁTICA que devuelve la ÚNICA instancia
        public static SessionManager GetInstance
        {
            get
            {
                // Si la sesión no existe, la creamos por primera vez
                if (HttpContext.Current.Session["SessionManager"] == null)
                {
                    HttpContext.Current.Session["SessionManager"] = new SessionManager();
                }

                // Devolvemos la instancia guardada
                return (SessionManager)HttpContext.Current.Session["SessionManager"];
            }
        }

        // 3. Propiedades del usuario logueado
        public string Usuario { get; set; }
        public string Rol { get; set; }

        // 4. Método para destruir el Singleton al cerrar sesión
        public void Logout()
        {
            HttpContext.Current.Session["SessionManager"] = null;
        }
    }
}

