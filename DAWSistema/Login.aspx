<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DAWSistema.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Ingreso al Sistema - AutoRent</title>
    <style>
        * { 
            box-sizing: border-box; 
            margin: 0; 
            padding: 0; 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
        }
        
        body {
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
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.05);
            width: 100%;
            max-width: 400px;
            text-align: center;
            border: 1px solid #eaeaea;
        }

        .brand-icon {
            font-size: 3rem;
            color: #8B0000;
            margin-bottom: 15px;
            display: inline-block;
        }

        .login-container h2 {
            color: #111111;
            font-size: 1.8rem;
            font-weight: 700;
            margin-bottom: 8px;
        }

        .login-container p {
            color: #777777;
            font-size: 0.95rem;
            margin-bottom: 30px;
        }

        .form-group {
            text-align: left;
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-size: 0.85rem;
            font-weight: 600;
            color: #444444;
            margin-bottom: 8px;
        }

        .form-control {
            width: 100%;
            padding: 14px;
            border: 1px solid #cccccc;
            border-radius: 8px;
            font-size: 1rem;
            color: #333333;
            outline: none;
            transition: border-color 0.2s;
            background-color: #ffffff;
        }

        .form-control:focus {
            border-color: #8B0000;
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
            transition: background-color 0.2s;
        }

        .btn-primary:hover {
            background-color: #660000;
        }

        .error-message {
            display: block;
            margin-top: 15px;
            font-size: 0.9rem;
            font-weight: 600;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="brand-icon">🚗</div>
            <h2>Inicia Sesión en tu cuenta</h2>
            
            <div class="form-group">
                <label>Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Pepe"></asp:TextBox> 
            </div>
            
            <div class="form-group">
                <label>Contraseña</label>
                <asp:TextBox ID="txtClave" runat="server" TextMode="Password" CssClass="form-control" placeholder="••••••••"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnIngresar" runat="server" Text="Iniciar sesión" CssClass="btn-primary" OnClick="btnIngresar_Click" />
            <a href="Registro.aspx" style="display: block; text-align: center; margin-top: 20px; font-size: 0.9rem; color: #555; text-decoration: none;">
    ¿No tenés cuenta? <span style="color: #8B0000; font-weight: bold;">Registrate acá</span>
</a>
            <asp:Label ID="lblMensaje" runat="server" CssClass="error-message" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>