<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="DAWSistema.Principal" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <title>Menú Principal - AutoRent</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f4f7f6; color: #333; display: flex; height: 100vh; overflow: hidden; }
        
        /* Barra Lateral */
        .sidebar { width: 260px; background-color: #ffffff; border-right: 1px solid #e0e0e0; display: flex; flex-direction: column; padding: 20px 0; }
        .logo { padding: 0 20px 30px 20px; font-size: 1.5rem; font-weight: bold; color: #333; display: flex; align-items: center; gap: 10px; }
        .logo-icon { color: #8B0000; font-size: 1.8rem; }
        .menu { flex: 1; display: flex; flex-direction: column; }
        .menu-item { padding: 12px 20px; text-decoration: none; color: #555; font-size: 1rem; display: flex; align-items: center; gap: 15px; transition: all 0.2s; }
        .menu-item.active { background-color: #8B0000; color: #ffffff; border-radius: 8px; margin: 0 15px; padding: 12px 15px; font-weight: bold; }
        .menu-item:hover:not(.active) { background-color: #f0f0f0; border-radius: 8px; margin: 0 15px; padding: 12px 15px; }
        .sidebar-footer { padding: 20px; border-top: 1px solid #e0e0e0; display: flex; flex-direction: column; gap: 15px; }
        .user-info { display: flex; align-items: center; gap: 10px; font-weight: bold; color: #333; font-size: 0.95rem; }
        .btn-logout { background-color: transparent; color: #333; border: 1px solid #ccc; padding: 10px; border-radius: 8px; cursor: pointer; width: 100%; font-weight: bold; transition: 0.2s; }
        .btn-logout:hover { background-color: #f0f0f0; }

        /* Contenido Principal */
        .main-content { flex: 1; padding: 50px; overflow-y: auto; }
        .main-content h1 { font-size: 2.2rem; color: #1a1a1a; margin-bottom: 10px; font-weight: bold; }
        .subtitle { color: #666; font-size: 1.1rem; margin-bottom: 40px; }
        
        /* Tarjetas (Cards) */
        .cards-container { display: flex; gap: 20px; flex-wrap: wrap; }
        .card { background-color: #ffffff; border: 1px solid #eaeaea; border-radius: 12px; padding: 30px; width: 100%; max-width: 420px; display: flex; align-items: flex-start; gap: 20px; box-shadow: 0 4px 6px rgba(0,0,0,0.02); }
        .card-icon { width: 60px; height: 60px; border-radius: 15px; display: flex; justify-content: center; align-items: center; font-size: 1.8rem; flex-shrink: 0; }
        .icon-red { background-color: #ffe6e6; color: #8B0000; }
        .icon-dark { background-color: #eef2f5; color: #2c3e50; }
        .icon-blue { background-color: #e6f0ff; color: #004085; }
        .icon-green { background-color: #e6ffe6; color: #155724; }
        
        .card-content { flex: 1; display: flex; flex-direction: column; }
        .card-content h3 { font-size: 1.25rem; color: #333; margin-bottom: 8px; }
        .card-content p { color: #777; font-size: 0.95rem; line-height: 1.5; margin-bottom: 20px; }
        .btn-card { padding: 10px 15px; border-radius: 8px; cursor: pointer; font-weight: bold; align-self: flex-start; transition: 0.2s; font-size: 0.9rem; text-decoration: none; border: none; }
        .btn-card.outline { background-color: transparent; color: #8B0000; border: 1px solid #8B0000; }
        .btn-card.outline:hover { background-color: #ffe6e6; }
        .btn-card.primary { background-color: #f4f4f4; color: #333; border: 1px solid #ddd; }
        .btn-card.primary:hover { background-color: #e0e0e0; }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="display: flex; width: 100%;">
        
        <aside class="sidebar">
            <div class="logo"><span class="logo-icon">🚗</span> <span>AutoRent</span></div>
            <nav class="menu">
                <a href="Principal.aspx" class="menu-item active">🏠 Mi Panel</a>
                <a href="Vehiculos.aspx" class="menu-item">🚘 Alquilar Auto</a>
                <a href="MisReservas.aspx" id="linkReservas" runat="server" class="menu-item">📅 Mis Reservas</a>
                <a href="Bitacora.aspx" id="linkBitacora" runat="server" class="menu-item">📖 Bitácora</a>
            </nav>
            <div class="sidebar-footer">
                <div class="user-info"><span>👤</span> <asp:Label ID="lblUsuarioSidebar" runat="server"></asp:Label></div>
                <asp:Button ID="btnSalir" runat="server" Text="🚪 Cerrar Sesión" CssClass="btn-logout" OnClick="btnSalir_Click" />
            </div>
        </aside>

        <main class="main-content">
            <h1>¡Hola, <asp:Label ID="lblUsuario" runat="server"></asp:Label>!</h1>
            <p class="subtitle">Este es tu panel de control según tus permisos en el sistema.</p>

            <div class="cards-container">
                <div class="card" id="cardClave" runat="server">
                    <div class="card-icon icon-dark">🔑</div>
                    <div class="card-content">
                        <h3>Cambiar Contraseña</h3>
                        <p>Actualizá tu clave de acceso regularmente para mantener tu cuenta segura.</p>
                        <asp:Button ID="btnCambiarClave" runat="server" Text="Modificar Clave ➔" CssClass="btn-card outline" OnClick="btnCambiarClave_Click" />
                    </div>
                </div>
                
                <div class="card" id="cardReservas" runat="server">
                    <div class="card-icon icon-blue">📅</div>
                    <div class="card-content">
                        <h3>Gestión de Reservas</h3>
                        <p>Realiza nuevas reservas, consulta disponibilidad, modifica fechas o cancela alquileres.</p>
                        <asp:Button ID="btnMisReservas" runat="server" Text="Ir a Mis Reservas ➔" CssClass="btn-card outline" OnClick="btnMisReservas_Click" />
                    </div>
                </div>

                <div class="card" id="cardFlota" runat="server">
                    <div class="card-icon icon-red">🚘</div>
                    <div class="card-content">
                        <h3>Gestión de Flota (ABM)</h3>
                        <p>Administra los vehículos de la agencia. Alta, baja y modificación de unidades.</p>
                        <button type="button" class="btn-card primary" onclick="alert('Módulo ABM Vehículos en desarrollo')">Ir a Flota ➔</button>
                    </div>
                </div>

                <div class="card" id="cardBitacora" runat="server">
                    <div class="card-icon icon-dark">📖</div>
                    <div class="card-content">
                        <h3>Reportes y Bitácora</h3>
                        <p>Auditoría del sistema. Revisa los inicios de sesión y monitorea eventos de usuarios.</p>
                        <asp:Button ID="btnBitacora" runat="server" Text="Ir a Bitácora ➔" CssClass="btn-card primary" OnClick="btnBitacora_Click" />
                    </div>
                </div>

                <div class="card" id="cardSeguridad" runat="server">
                    <div class="card-icon icon-green">🛡️</div>
                    <div class="card-content">
                        <h3>Seguridad (Web Master)</h3>
                        <p>Control de Backups, Restore de la base de datos y validación de Dígitos Verificadores.</p>
                        <button type="button" class="btn-card primary" onclick="alert('Módulo de Seguridad en desarrollo')">Panel de Seguridad ➔</button>
                    </div>
                </div>

            </div>
        </main>
    </form>
</body>
</html>