using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAWSistema
{
    public partial class Seguridad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.GetInstance.Usuario == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (SessionManager.GetInstance.Rol != "WebMaster")
            {
                Response.Redirect("Principal.aspx");
            }

        }
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string carpetaDestino = txtRutaBackup.Text.Trim();

                if (!carpetaDestino.EndsWith("\\"))
                {
                    carpetaDestino += "\\";
                }

                string nombreArchivo = "DAW_Backup_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm") + ".bak";

                string rutaCompleta = carpetaDestino + nombreArchivo;

                SeguridadGestor gestor = new SeguridadGestor();
                gestor.HacerBackup(rutaCompleta, SessionManager.GetInstance.Usuario);

                ClientScript.RegisterStartupScript(this.GetType(), "Ok", $"alert('✅ Backup creado exitosamente en:\\n\\n{rutaCompleta}');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('❌ Error al hacer Backup: {ex.Message}\\n\\nVerificá que la carpeta que escribiste exista de verdad en tu computadora.');", true);
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuRestore.HasFile)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Falta", "alert('⚠️ Por favor seleccioná un archivo .bak primero.');", true);
                    return;
                }

                string rutaTemporal = @"C:\Backups\RestoreTemp.bak";
                fuRestore.SaveAs(rutaTemporal);

                SeguridadGestor gestor = new SeguridadGestor();
                gestor.HacerRestore(rutaTemporal, SessionManager.GetInstance.Usuario);

                ClientScript.RegisterStartupScript(this.GetType(), "Ok", "alert('✅ ¡Sistema restaurado exitosamente!'); window.location.href='Principal.aspx';", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('❌ Error en el Restore: {ex.Message}');", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }
    }
}