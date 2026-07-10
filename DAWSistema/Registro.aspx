<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="DAWSistema.Registro" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Crear Cuenta - AutoRent</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .register-container {
            background-color: white;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            width: 100%;
            max-width: 400px;
            border: 1px solid #eaeaea;
        }

        .register-container h2 {
            text-align: center;
            color: #111;
            margin-bottom: 5px;
        }

        .register-container p {
            text-align: center;
            color: #777;
            font-size: 0.9rem;
            margin-bottom: 25px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            font-size: 0.85rem;
            font-weight: bold;
            color: #444;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 1rem;
            box-sizing: border-box;
        }

        .btn-primary {
            background-color: #8B0000;
            color: white;
            border: none;
            padding: 12px;
            border-radius: 6px;
            cursor: pointer;
            width: 100%;
            font-weight: bold;
            font-size: 1rem;
            margin-top: 10px;
            transition: 0.3s;
        }

        .btn-primary:hover {
            background-color: #660000;
        }

        .login-link {
            display: block;
            text-align: center;
            margin-top: 20px;
            font-size: 0.9rem;
            color: #555;
            text-decoration: none;
        }

        .login-link span {
            color: #8B0000;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <h2>Crear Cuenta</h2>
            <p>Completa tus datos para empezar a alquilar.</p>

            <div class="form-group">
                <label>Nombre de Usuario</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Confirmar Contraseña</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true"></asp:TextBox>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarme" CssClass="btn-primary" OnClick="btnRegistrar_Click" />

            <a href="Login.aspx" class="login-link">¿Ya tenés una cuenta? <span>Inicia Sesión</span></a>
        </div>
    </form>
</body>
</html>