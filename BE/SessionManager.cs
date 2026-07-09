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

        public static SessionManager GetInstance
        {
            get
            {
                if (HttpContext.Current.Session["SessionManager"] == null)
                    HttpContext.Current.Session["SessionManager"] = new SessionManager();
                return (SessionManager)HttpContext.Current.Session["SessionManager"];
            }
        }

        public string Usuario { get; set; }
        public string Rol { get; set; } // 🔥 AGREGAMOS EL ROL ACÁ

        public void Logout()
        {
            HttpContext.Current.Session["SessionManager"] = null;
        }
    }
}

