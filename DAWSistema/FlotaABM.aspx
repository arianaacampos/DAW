<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlotaABM.aspx.cs" Inherits="DAWSistema.FlotaABM" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Gestión de Flota - Admin</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, sans-serif;
            background-color: #f4f7f6;
            color: #333;
            margin: 0;
            padding: 40px;
        }

        .container {
            background-color: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            max-width: 1000px;
            margin: auto;
        }

        h2 {
            color: #8B0000;
            margin-bottom: 20px;
            border-bottom: 2px solid #eee;
            padding-bottom: 10px;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
        }

        .grid-view th {
            background-color: #f8f9fa;
            padding: 12px;
            text-align: left;
        }

        .grid-view td {
            padding: 12px;
            border-bottom: 1px solid #eee;
        }

        .form-row {
            display: flex;
            gap: 15px;
            margin-bottom: 15px;
        }

        .form-group {
            flex: 1;
            display: flex;
            flex-direction: column;
        }

        .form-group label {
            font-size: 0.85rem;
            font-weight: bold;
            color: #555;
            margin-bottom: 5px;
        }

        .form-control {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            font-weight: bold;
            cursor: pointer;
            color: white;
        }

        .btn-save {
            background-color: #28a745;
        }

        .btn-clear {
            background-color: #6c757d;
        }

        .btn-volver {
            background-color: transparent;
            border: 1px solid #8B0000;
            color: #8B0000;
            float: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:Button ID="btnVolver" runat="server" Text="Volver al Panel" CssClass="btn btn-volver" OnClick="btnVolver_Click" CausesValidation="false" formnovalidate="formnovalidate" />
            <h2>🚘 Gestión de Flota (ABM)</h2>

            <asp:GridView ID="dgvFlota" runat="server" CssClass="grid-view" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="dgvFlota_RowCommand" OnRowDeleting="dgvFlota_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                    <asp:BoundField DataField="Patente" HeaderText="Patente" />
                    <asp:ButtonField CommandName="Select" Text="✏️ Editar" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="🗑️ Borrar" />
                </Columns>
            </asp:GridView>

            <h3 style="margin-top: 20px;">Datos del Vehículo</h3>
            <asp:HiddenField ID="hfID" runat="server" Value="0" />

            <div class="form-row">
                <div class="form-group">
                    <label>Marca</label>
                    <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Modelo</label>
                    <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" Required="true"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Patente</label>
                    <asp:TextBox ID="txtPatente" runat="server" CssClass="form-control" Required="true" placeholder="Ej: AB 123 CD"></asp:TextBox>
                </div>
            </div>

            <div style="margin-top: 15px;">
                <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar Vehículo" CssClass="btn btn-save" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-clear" OnClick="btnLimpiar_Click" formnovalidate="formnovalidate" />
            </div>
        </div>
    </form>
</body>
</html>