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

            // 2. Si hay alguien, pero su ROL NO es "WebMaster", lo echamos al menú principal
            if (SessionManager.GetInstance.Rol != "WebMaster")
            {
                Response.Redirect("Principal.aspx");
            }

        }
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Agarramos la carpeta que escribió el Master en la cajita
                string carpetaDestino = txtRutaBackup.Text.Trim();

                // 2. Validación de oro: Nos aseguramos de que termine con la barra '\'
                // Así, si el usuario pone "C:\Backups" o "C:\Backups\", funciona igual
                if (!carpetaDestino.EndsWith("\\"))
                {
                    carpetaDestino += "\\";
                }

                // 3. Armamos el nombre del archivo con la fecha
                string nombreArchivo = "DAW_Backup_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm") + ".bak";

                // 4. Juntamos la carpeta que eligió + el nombre del archivo
                string rutaCompleta = carpetaDestino + nombreArchivo;

                // 5. Ejecutamos
                SeguridadGestor gestor = new SeguridadGestor();
                gestor.HacerBackup(rutaCompleta, SessionManager.GetInstance.Usuario);

                ClientScript.RegisterStartupScript(this.GetType(), "Ok", $"alert('✅ Backup creado exitosamente en:\\n\\n{rutaCompleta}');", true);
            }
            catch (Exception ex)
            {
                // Si la carpeta que escribió no existe o no tiene permisos, salta este error
                ClientScript.RegisterStartupScript(this.GetType(), "Error", $"alert('❌ Error al hacer Backup: {ex.Message}\\n\\nVerificá que la carpeta que escribiste exista de verdad en tu computadora.');", true);
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                // Validamos que haya subido un archivo
                if (!fuRestore.HasFile)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Falta", "alert('⚠️ Por favor seleccioná un archivo .bak primero.');", true);
                    return;
                }

                // Guardamos el archivo que subió temporalmente en la carpeta Backups
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