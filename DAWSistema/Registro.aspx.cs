using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAWSistema
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUsername.Text.Trim();
                string pass = txtPassword.Text;
                string confirmPass = txtConfirmPassword.Text;

                if (pass != confirmPass)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Las contraseñas no coinciden.');", true);
                    return;
                }

                UsuarioGestor gestor = new UsuarioGestor();
                gestor.RegistrarCliente(user, pass);

                ClientScript.RegisterStartupScript(this.GetType(), "Ok", "alert('Cuenta creada con exito.'); window.location.href='Login.aspx';", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('{ex.Message}');", true);
            }
        }
    }
}