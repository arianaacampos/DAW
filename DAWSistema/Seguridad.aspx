<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seguridad.aspx.cs" Inherits="DAWSistema.Seguridad" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Seguridad - Web Master</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #1a1a1a;
            color: white;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            background-color: #2a2a2a;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.8);
            width: 100%;
            max-width: 500px;
            text-align: center;
            border: 1px solid #444;
        }

        h2 {
            color: #28a745;
            margin-bottom: 20px;
            font-size: 2rem;
        }

        .box {
            background-color: #333;
            padding: 25px;
            border-radius: 8px;
            margin-bottom: 25px;
            text-align: left;
        }

        .box h4 {
            color: #fff;
            margin-bottom: 10px;
            font-size: 1.2rem;
        }

        .btn-green {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 6px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            font-size: 1rem;
            transition: 0.3s;
        }

        .btn-green:hover {
            background-color: #218838;
        }

        .btn-red {
            background-color: #dc3545;
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 6px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            font-size: 1rem;
            margin-top: 15px;
            transition: 0.3s;
        }

        .btn-red:hover {
            background-color: #c82333;
        }

        .btn-volver {
            background-color: transparent;
            color: #aaa;
            border: 1px solid #666;
            padding: 10px;
            border-radius: 6px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            margin-top: 10px;
            text-decoration: none;
            display: inline-block;
            text-align: center;
            transition: 0.3s;
        }

        .btn-volver:hover {
            background-color: #444;
            color: white;
        }

        .file-upload {
            color: white;
            margin-bottom: 10px;
            width: 100%;
            font-size: 0.9rem;
        }
        .form-control {
            width: 100%;
            padding: 14px;
            border: 1px solid #ccc;
            border-radius: 8px;
            font-size: 1rem;
            color: #333;
            outline: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2> Panel de Seguridad </h2>
            <p style="color: #aaa; margin-bottom: 30px;">Acceso restringido. Solo Web Master.</p>

            <div class="box" style="border-left: 5px solid #28a745;">
                <h4>Generar Copia de Seguridad</h4>
                <p style="font-size: 0.85rem; color: #aaa; margin-bottom: 10px;">Escribí la ruta de la carpeta donde queres guardar el archivo (ej: C:\Backups\)</p>

                <asp:TextBox ID="txtRutaBackup" runat="server" CssClass="form-control" Text="C:\Backups\" style="margin-bottom: 15px; width: 90%; padding: 10px; border-radius: 6px; border: 1px solid #ccc; color: black; font-weight: bold;"></asp:TextBox>

                <asp:Button ID="btnBackup" runat="server" Text=" Generar Backup" CssClass="btn-green" OnClick="btnBackup_Click" />
            </div>

            <div class="box" style="border-left: 5px solid #dc3545;">
                <h4 style="color: #ff6b6b;">Restaurar Sistema (Peligro)</h4>
                <p style="font-size: 0.85rem; color: #aaa; margin-bottom: 15px;">Subi un archivo .bak. Esto borrara los datos actuales.</p>

                <asp:FileUpload ID="fuRestore" runat="server" CssClass="file-upload" />

                <asp:Button ID="btnRestore" runat="server" Text="⚠️ Ejecutar Restore" CssClass="btn-red" OnClick="btnRestore_Click" OnClientClick="return confirm('ATENCIÓN: Esta acción sobreescribirá la base de datos actual perdiendo todos los cambios nuevos. ¿Deseas continuar?');" />
            </div>

            <asp:Button ID="btnVolver" runat="server" Text="Volver al Panel" CssClass="btn-volver" OnClick="btnVolver_Click" />
        </div>
    </form>
</body>
</html>