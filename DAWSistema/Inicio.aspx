<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="DAWSistema.Inicio" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>AutoRent - Encuentra tu viaje perfecto</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        body { background-color: #111; color: white; }
        
        .navbar { 
            display: flex; 
            justify-content: space-between; 
            align-items: center; 
            background-color: white; 
            padding: 15px 50px; 
            color: #333;
        }
        .nav-left { display: flex; align-items: center; gap: 30px; }
        .logo { font-size: 1.5rem; font-weight: bold; color: #111; display: flex; align-items: center; gap: 10px; }
        .logo-icon { color: #8B0000; font-size: 1.5rem; }
        .nav-links a { text-decoration: none; color: #555; font-weight: bold; font-size: 0.95rem; }
        .nav-links a.active { color: #111; border-bottom: 2px solid #8B0000; padding-bottom: 5px; }
        
        .btn-login { 
            background-color: #8B0000; 
            color: white; 
            border: none; 
            padding: 10px 20px; 
            border-radius: 6px; 
            font-weight: bold; 
            cursor: pointer; 
            font-size: 0.95rem;
            transition: 0.3s;
        }
        .btn-login:hover { background-color: #660000; }

        .hero { 
            text-align: center; 
            padding: 120px 20px; 
            
            background-image: linear-gradient(135deg, rgba(139,0,0,0.8) 0%, rgba(17,17,17,0.95) 100%), url('Imagenes/inicio.png');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            
            min-height: calc(100vh - 70px);
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }
        .hero h1 { font-size: 3.5rem; margin-bottom: 15px; font-weight: 900; letter-spacing: -1px; }
        .hero p { font-size: 1.2rem; margin-bottom: 40px; color: #ddd; max-width: 600px; line-height: 1.5; }

        .search-container {
            background-color: white;
            padding: 15px;
            border-radius: 12px;
            display: flex;
            gap: 15px;
            align-items: center;
            box-shadow: 0 10px 30px rgba(0,0,0,0.5);
        }
        .input-group {
            display: flex;
            flex-direction: column;
            text-align: left;
            padding: 5px 15px;
            border-right: 1px solid #eee;
        }
        .input-group:last-of-type { border-right: none; }
        .input-group label { font-size: 0.75rem; color: #888; font-weight: bold; margin-bottom: 5px; }
        .input-group input { 
            border: none; 
            outline: none; 
            font-size: 1rem; 
            color: #333; 
            background: transparent;
        }
        
        .btn-search {
            background-color: #8B0000;
            color: white;
            border: none;
            padding: 15px 35px;
            border-radius: 8px;
            font-weight: bold;
            font-size: 1.1rem;
            cursor: pointer;
            transition: 0.3s;
        }
        .btn-search:hover { background-color: #660000; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <nav class="navbar">
            <div class="nav-left">
                <div class="logo"><span class="logo-icon"></span> AutoRent</div>
                <div class="nav-links">
                    <a href="Inicio.aspx" class="active">Inicio</a>
                    <a href="Vehiculos.aspx" style="margin-left: 15px;">Flota</a>
                </div>
            </div>
          <asp:Button ID="btnIrLogin" runat="server" Text=" Iniciar Sesion" CssClass="btn-login" OnClick="btnIrLogin_Click" formnovalidate="formnovalidate" CausesValidation="false" />
        </nav>

        <div class="hero">
            <h1>Encuentra tu viaje perfecto</h1>
            <p>Alquila los mejores autos para tus viajes o escapadas de fin de semana con AutoRent.</p>
            
            <div class="search-container">
                <div class="input-group">
                    <label>RECOGIDA</label>
                    <asp:TextBox ID="txtCiudad" runat="server" placeholder="Ciudad, Aeropuerto..." Required="true"></asp:TextBox>
                </div>
                <div class="input-group">
                    <label>FECHA INICIO</label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" Required="true"></asp:TextBox>
                </div>
                <div class="input-group">
                    <label>FECHA FIN</label>
                    <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date" Required="true"></asp:TextBox>
                </div>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn-search" OnClick="btnBuscar_Click" />
            </div>
        </div>

    </form>
</body>
</html>