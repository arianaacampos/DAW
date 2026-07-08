<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlIntegridad.aspx.cs" Inherits="DAWSistema.ControlIntegridad" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>SISTEMA BLOQUEADO</title>
    <style>
        body { font-family: sans-serif; background-color: #111; color: white; display: flex; justify-content: center; align-items: center; height: 100vh; text-align: center; }
        .container { background-color: #222; padding: 40px; border-radius: 12px; border: 2px solid #dc3545; max-width: 600px; }
        h1 { color: #dc3545; }
        .error { background-color: #333; padding: 15px; color: #ffcccc; margin: 20px 0; border-left: 5px solid #dc3545; }
        .btn { padding: 12px 25px; border: none; border-radius: 6px; font-weight: bold; cursor: pointer; margin: 5px; }
        .btn-recalcular { background-color: #ffc107; color: #111; }
        .btn-cancelar { background-color: #555; color: white; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>⚠️ ALERTA DE INTEGRIDAD</h1>
            
            <asp:Panel ID="panelMaster" runat="server" Visible="false">
                <p>Se ha detectado una modificación en la base de datos.</p>
                <div class="error"><asp:Label ID="lblDetalleError" runat="server"></asp:Label></div>
                <asp:Button ID="btnRecalcular" runat="server" Text="⚙️ Recalcular DV" CssClass="btn btn-recalcular" OnClick="btnRecalcular_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="❌ Cancelar y Salir" CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" />
            </asp:Panel>

            <asp:Panel ID="panelBloqueado" runat="server" Visible="false">
                <p style="font-size: 1.2rem; color: #aaa;">El sistema se encuentra en cuarentena.<br />Contacte al Web Master.</p>
                <asp:Button ID="btnSalir" runat="server" Text="Volver al Login" CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>