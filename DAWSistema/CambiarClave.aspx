<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="DAWSistema.CambiarClave" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Cambiar Contraseña</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f8f9fa;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .login-container {
            background-color: white;
            padding: 40px;
            border-radius: 16px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.05);
            width: 100%;
            max-width: 400px;
            text-align: center;
            border: 1px solid #eaeaea;
        }

        .form-group {
            text-align: left;
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-size: 0.85rem;
            font-weight: 600;
            color: #444;
            margin-bottom: 8px;
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

        .btn-primary {
            background-color: #8B0000;
            color: white;
            border: none;
            padding: 14px;
            border-radius: 8px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            font-size: 1rem;
            margin-top: 10px;
        }

        .btn-volver {
            background-color: transparent;
            color: #555;
            border: 1px solid #ccc;
            padding: 10px;
            border-radius: 8px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            margin-top: 10px;
            text-decoration: none;
            display: inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2 style="color: #111;">🔑 Nueva Contraseña</h2>
            <p style="color: #777; margin-bottom: 30px;">Ingresá tu nueva clave segura.</p>

            <div class="form-group">
                <label>Nueva Contraseña</label>
                <asp:TextBox ID="txtNuevaClave" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Contraseña" CssClass="btn-primary" OnClick="btnGuardar_Click" />
            <a href="Principal.aspx" class="btn-volver">Cancelar y Volver</a>
        </div>
    </form>
</body>
</html>