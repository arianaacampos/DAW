<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="DAWSistema.Bitacora" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Bitácora del Sistema - AutoRent</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        body { background-color: #f4f7f6; color: #333; display: flex; height: 100vh; overflow: hidden; }
        
        /* Barra Lateral (Igual a la principal) */
        .sidebar { width: 260px; background-color: #ffffff; border-right: 1px solid #e0e0e0; display: flex; flex-direction: column; padding: 20px 0; }
        .logo { padding: 0 20px 30px 20px; font-size: 1.5rem; font-weight: bold; color: #333; display: flex; align-items: center; gap: 10px; }
        .logo-icon { color: #8B0000; font-size: 1.8rem; }
        .menu { flex: 1; display: flex; flex-direction: column; }
        .menu-item { padding: 12px 20px; text-decoration: none; color: #555; font-size: 1rem; display: flex; align-items: center; gap: 15px; transition: all 0.2s; }
        .menu-item.active { background-color: #8B0000; color: #ffffff; border-radius: 8px; margin: 0 15px; padding: 12px 15px; font-weight: bold; }
        .menu-item:hover:not(.active) { background-color: #f0f0f0; border-radius: 8px; margin: 0 15px; padding: 12px 15px; }

        /* Contenido Principal */
        .main-content { flex: 1; padding: 40px 50px; overflow-y: auto; display: flex; flex-direction: column; }
        
        /* Encabezado */
        .header-bitacora { display: flex; justify-content: space-between; align-items: flex-end; margin-bottom: 30px; }
        .header-text h1 { font-size: 2rem; color: #111; margin-bottom: 5px; font-weight: bold; }
        .header-text p { color: #777; font-size: 1rem; }
        .header-actions { display: flex; gap: 10px; }
        .btn-export { background-color: #8B0000; color: white; border: none; padding: 10px 20px; border-radius: 8px; font-weight: bold; cursor: pointer; }
        .btn-filter { background-color: white; border: 1px solid #ccc; padding: 10px 20px; border-radius: 8px; font-weight: bold; cursor: pointer; color: #333; }

        /* Tarjetas de Resumen (KPIs) */
        .kpi-container { display: flex; gap: 20px; margin-bottom: 30px; }
        .kpi-card { background-color: white; border: 1px solid #eaeaea; border-radius: 12px; padding: 20px; flex: 1; display: flex; align-items: center; gap: 15px; box-shadow: 0 2px 10px rgba(0,0,0,0.02); }
        .kpi-icon { font-size: 1.5rem; width: 45px; height: 45px; display: flex; justify-content: center; align-items: center; border-radius: 50%; }
        .icon-blue { background-color: #e6f0fa; color: #0056b3; }
        .icon-green { background-color: #e8f5e9; color: #2e7d32; }
        .icon-red { background-color: #ffebee; color: #c62828; }
        .kpi-data p { font-size: 0.8rem; color: #888; font-weight: bold; margin-bottom: 5px; }
        .kpi-data h3 { font-size: 1.6rem; color: #111; }

        /* Contenedor de la Tabla */
        .table-container { background-color: white; border-radius: 12px; border: 1px solid #eaeaea; overflow: hidden; padding: 20px; box-shadow: 0 2px 10px rgba(0,0,0,0.02); }
        
        /* Grilla (GridView) */
        .grid-view { width: 100%; border-collapse: collapse; text-align: left; }
        .grid-view th { color: #888; font-size: 0.85rem; padding: 15px; border-bottom: 2px solid #f0f0f0; text-transform: uppercase; }
        .grid-view td { padding: 15px; border-bottom: 1px solid #f4f4f4; color: #444; font-size: 0.95rem; }
        .grid-view tr:last-child td { border-bottom: none; }
        .grid-view tr:hover { background-color: #fafafa; }

        /* Etiquetas de Criticidad (Badges generados desde C#) */
        .status-badge { padding: 5px 12px; border-radius: 20px; font-size: 0.85rem; font-weight: bold; }
        .status-exito { background-color: #e8f5e9; color: #2e7d32; border: 1px solid #c8e6c9; }
        .status-fallo { background-color: #ffebee; color: #c62828; border: 1px solid #ffcdd2; }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="display: flex; width: 100%;">
        
        <aside class="sidebar">
            <div class="logo"><span class="logo-icon">🚗</span> <span>AutoRent</span></div>
            <nav class="menu">
                <a href="Principal.aspx" class="menu-item">🏠 Inicio</a>
                <a href="Bitacora.aspx" class="menu-item active">📖 Bitácora del Sistema</a>
                <a href="Vehiculos.aspx" class="menu-item">🚘 Flota</a>
            </nav>
        </aside>

        <main class="main-content">
            
            <div class="header-bitacora">
                <div class="header-text">
                    <h1>Bitácora del Sistema</h1>
                    <p>Registro de eventos, accesos y actividades de los usuarios.</p>
                </div>
                <div class="header-actions">
                    <button type="button" class="btn-filter">⚙️ Filtrar</button>
                    <button type="button" class="btn-export">Exportar Registro</button>
                </div>
            </div>

            <div class="kpi-container">
                <div class="kpi-card">
                    <div class="kpi-icon icon-blue">🕒</div>
                    <div class="kpi-data"><p>EVENTOS TOTALES</p><h3>Últimos 100</h3></div>
                </div>
                <div class="kpi-card">
                    <div class="kpi-icon icon-green">✅</div>
                    <div class="kpi-data"><p>SISTEMA</p><h3>Estable</h3></div>
                </div>
                <div class="kpi-card">
                    <div class="kpi-icon icon-red">❌</div>
                    <div class="kpi-data"><p>ALERTAS</p><h3>Revisar</h3></div>
                </div>
            </div>

            <div class="table-container">
                <asp:GridView ID="dgvBitacora" runat="server" CssClass="grid-view" AutoGenerateColumns="false" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="FECHA Y HORA" />
                        <asp:BoundField DataField="Usuario" HeaderText="USUARIO" />
                        <asp:BoundField DataField="Accion" HeaderText="EVENTO" />
                        
                        <%-- Columna especial para la criticidad (HtmlEncode="false" permite inyectar el diseño CSS) --%>
                        <asp:BoundField DataField="Criticidad" HeaderText="ESTADO" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </div>

        </main>
    </form>
</body>
</html>