<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlIntegridad.aspx.cs" Inherits="DAWSistema.ControlIntegridad" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <title>SISTEMA BLOQUEADO - Integridad Comprometida</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #111;
            color: white;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            text-align: center;
        }

        .container {
            background-color: #222;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 0 30px rgba(220, 53, 69, 0.4);
            width: 100%;
            max-width: 650px;
            border: 2px solid #dc3545;
        }

        h1 {
            color: #dc3545;
            font-size: 2.5rem;
            margin-bottom: 10px;
        }

        .error-detalle {
            background-color: #333;
            padding: 15px;
            border-left: 5px solid #dc3545;
            color: #ffcccc;
            margin: 20px 0;
            font-family: monospace;
            font-size: 1.1rem;
            text-align: left;
        }

        .btn-group {
            display: flex;
            gap: 15px;
            justify-content: center;
            align-items: flex-end;
            margin-top: 30px;
            flex-wrap: wrap;
        }

        .btn {
            padding: 12px 25px;
            border: none;
            border-radius: 6px;
            font-weight: bold;
            cursor: pointer;
            font-size: 1rem;
            transition: 0.3s;
        }

        .btn-recalcular {
            background-color: #ffc107;
            color: #111;
        }

        .btn-cancelar {
            background-color: #555;
            color: white;
        }

        .btn-restore {
            background-color: #17a2b8;
            color: white;
            width: 100%;
            margin-top: 10px;
        }

        .restore-box {
            background-color: #333;
            padding: 15px;
            border-radius: 8px;
            border: 1px solid #17a2b8;
            text-align: left;
        }

        .bloqueado-msg {
            font-size: 1.2rem;
            color: #aaa;
            margin: 30px 0;
            line-height: 1.6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>⚠️ ALERTA DE INTEGRIDAD</h1>

            <asp:Panel ID="panelMaster" runat="server" Visible="false">
                <p>El sistema ha detectado una modificacion externa no autorizada en la base de datos.</p>
                <div class="error-detalle">
                    <asp:Label ID="lblDetalleError" runat="server"></asp:Label>
                </div>

                <div class="btn-group">
                    <asp:Button ID="btnRecalcular" runat="server" Text="Recalcular DV" CssClass="btn btn-recalcular" OnClick="btnRecalcular_Click" OnClientClick="return confirm('ALERTA: ¿Estás seguro de RECALCULAR todos los Dígitos Verificadores? Esto actualizará la firma de seguridad oficial de toda la base de datos.');" />

                    <div class="restore-box">
                        <label style="font-size: 0.85rem; color: #17a2b8; display:block; margin-bottom: 5px;">Subir archivo .bak para Restore:</label>
                        <asp:FileUpload ID="fuRestore" runat="server" ForeColor="White" />
                        <asp:Button ID="btnRestore" runat="server" Text="💾 Ejecutar Restore" CssClass="btn btn-restore" OnClick="btnRestore_Click" OnClientClick="return confirm('PELIGRO EXTREMO: ¿Estás absolutamente seguro de PISAR toda la base de datos con este archivo .bak? Todos los datos actuales que no estén en la copia se perderán para siempre.');" />
                    </div>

                    <asp:Button ID="btnCancelar" runat="server" Text="❌ Cancelar" CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" OnClientClick="return confirm('¿Deseas cancelar la operación y salir al Login?');" />
                </div>
            </asp:Panel>

            <asp:Panel ID="panelBloqueado" runat="server" Visible="false">
                <div class="bloqueado-msg">
                    El sistema se encuentra temporalmente bloqueado por motivos de seguridad.<br /><br />
                    <strong>Por favor contacte al Web Master.</strong>
                </div>
                <asp:Button ID="btnSalir" runat="server" Text="Salir al Login" CssClass="btn btn-cancelar" OnClick="btnCancelar_Click" OnClientClick="return confirm('¿Desea volver a la pantalla de inicio de sesión?');" />
            </asp:Panel>

        </div>
    </form>
</body>
</html>