<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="DAWSistema.Principal" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <title>Menú Principal - AutoRent</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            background-color: #f4f7f6; 
            color: #333; 
            display: flex; 
            height: 100vh; 
            overflow: hidden; 
        }
        
        /* Barra Lateral (Sidebar) */
        .sidebar { 
            width: 260px; 
            background-color: #ffffff; 
            border-right: 1px solid #e0e0e0; 
            display: flex; 
            flex-direction: column; 
            padding: 20px 0; 
        }
        .logo { 
            padding: 0 20px 30px 20px; 
            font-size: 1.5rem; 
            font-weight: bold; 
            color: #333; 
            display: flex; 
            align-items: center; 
            gap: 10px; 
        }
        .logo-icon { color: #8B0000; font-size: 1.8rem; }
        
        .menu { flex: 1; display: flex; flex-direction: column; }
        .menu-item { 
            padding: 12px 20px; 
            text-decoration: none; 
            color: #555; 
            font-size: 1rem; 
            display: flex; 
            align-items: center; 
            gap: 15px; 
            transition: all 0.2s; 
        }
        .menu-item.active { 
            background-color: #8B0000; 
            color: #ffffff; 
            border-radius: 8px; 
            margin: 0 15px; 
            padding: 12px 15px;
            font-weight: bold; 
        }
        .menu-item:hover:not(.active) { 
            background-color: #f0f0f0; 
            border-radius: 8px; 
            margin: 0 15px;
            padding: 12px 15px;
        }
        
        .sidebar-footer { 
            padding: 20px; 
            border-top: 1px solid #e0e0e0; 
            display: flex; 
            flex-direction: column; 
            gap: 15px; 
        }
        .user-info { 
            display: flex; 
            align-items: center; 
            gap: 10px; 
            font-weight: bold; 
            color: #333; 
            font-size: 0.95rem; 
        }
        .btn-logout { 
            background-color: transparent; 
            color: #333; 
            border: 1px solid #ccc; 
            padding: 10px; 
            border-radius: 8px; 
            cursor: pointer; 
            width: 100%; 
            font-weight: bold; 
            transition: 0.2s; 
        }
        .btn-logout:hover { background-color: #f0f0f0; }

        /* Contenido Principal */
        .main-content { flex: 1; padding: 50px; overflow-y: auto; }
        .main-content h1 { font-size: 2.2rem; color: #1a1a1a; margin-bottom: 10px; font-weight: bold; }
        .subtitle { color: #666; font-size: 1.1rem; margin-bottom: 40px; }
        
        /* Tarjetas de Opciones (Cards) */
        .cards-container { display: flex; gap: 20px; flex-wrap: wrap; }
        .card { 
            background-color: #ffffff; 
            border: 1px solid #eaeaea; 
            border-radius: 12px; 
            padding: 30px; 
            width: 100%; 
            max-width: 420px; 
            display: flex; 
            align-items: flex-start; 
            gap: 20px; 
            box-shadow: 0 4px 6px rgba(0,0,0,0.02); 
        }
        
        .card-icon { 
            width: 60px; 
            height: 60px; 
            border-radius: 15px; 
            display: flex; 
            justify-content: center; 
            align-items: center; 
            font-size: 1.8rem; 
            flex-shrink: 0; 
        }
        .icon-red { background-color: #ffe6e6; color: #8B0000; }
        .icon-dark { background-color: #eef2f5; color: #2c3e50; }
        
        .card-content { flex: 1; display: flex; flex-direction: column; }
        .card-content h3 { font-size: 1.25rem; color: #333; margin-bottom: 8px; }
        .card-content p { color: #777; font-size: 0.95rem; line-height: 1.5; margin-bottom: 20px; }
        
        .btn-card { 
            padding: 10px 15px; 
            border-radius: 8px; 
            cursor: pointer; 
            font-weight: bold; 
            align-self: flex-start; 
            transition: 0.2s; 
            font-size: 0.9rem; 
            text-decoration: none; 
            display: inline-block; 
        }
        .btn-card.outline { background-color: transparent; color: #8B0000; border: 1px solid #8B0000; }
        .btn-card.outline:hover { background-color: #ffe6e6; }
        .btn-card.primary { background-color: #f4f4f4; color: #333; border: 1px solid #ddd; }
        .btn-card.primary:hover { background-color: #e0e0e0; }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="display: flex; width: 100%;">
        
        <aside class="sidebar">
            <div class="logo">
                <span class="logo-icon">🚗</span> <span>AutoRent</span>
            </div>
           <nav class="menu">
    <a href="Principal.aspx" class="menu-item active">🏠 Inicio</a>
    <a href="Bitacora.aspx" class="menu-item">📖 Bitácora del Sistema</a>
    <a href="Vehiculos.aspx" class="menu-item">🚘 Flota</a>
</nav>
            <div class="sidebar-footer">
                <div class="user-info">
                    <span>👤</span>
                    <asp:Label ID="lblUsuarioSidebar" runat="server"></asp:Label>
                </div>
                <asp:Button ID="btnSalir" runat="server" Text="🚪 Cerrar Sesión" CssClass="btn-logout" OnClick="btnSalir_Click" />
               
            </div>
        </aside>

        <main class="main-content">
            <h1>¡Hola, <asp:Label ID="lblUsuario" runat="server"></asp:Label>!</h1>
            <p class="subtitle">¿Qué te gustaría hacer hoy? Selecciona una de las opciones a continuación.</p>

            <div class="cards-container">
                <div class="card">
                    <div class="card-icon icon-red">🚗</div>
                    <div class="card-content">
                        <h3>Alquilar un Auto</h3>
                        <p>Explora nuestra flota de vehículos premium y reserva tu próximo viaje al instante.</p>
                        <span class="btn-card outline">Próximamente ➔</span>
                    </div>
                </div>

                <div class="card">
                    <div class="card-icon icon-dark">📖</div>
                    <div class="card-content">
                        <h3>Ver Bitácora del Sistema</h3>
                        <p>Accede al registro de auditoría, revisa los inicios de sesión y monitorea eventos.</p>
                        <asp:Button ID="btnBitacora" runat="server" Text="Ir a Bitácora ➔" CssClass="btn-card primary" OnClick="btnBitacora_Click" />
                    </div>
                </div>
            </div>
        </main>

    </form>
</body>
</html>;