<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisReservas.aspx.cs" Inherits="DAWSistema.MisReservas" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Mis Reservas</title>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; /* Usamos la misma fuente que en Principal */
            background-color: #8B0000;
            display: flex;
            flex-direction: column;
            height: 100vh;
            margin: 0;
        }

        .navbar {
            background-color: white;
            padding: 20px 40px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .btn-volver-outline {
            background-color: transparent;
            color: #333;
            border: 1px solid #ccc;
            padding: 10px 20px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            transition: 0.2s;
            font-size: 0.95rem; 
        }

        .btn-volver-outline:hover {
            background-color: #f0f0f0;
        }

        .container {
            background-color: white;
            padding: 40px;
            border-radius: 10px;
            margin: 50px auto;
            width: 90%;
            max-width: 900px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .grid-view th {
            background-color: #f2f2f2;
            padding: 15px;
            text-align: left;
        }

        .grid-view td {
            padding: 12px 15px;
            border-bottom: 1px solid #eee;
        }

        .grid-view input[type="text"] {
            padding: 5px;
            width: 100%;
        }

        .grid-view a {
            color: #8B0000;
            text-decoration: none;
            font-weight: bold;
            margin-right: 15px;
        }

        .grid-view a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <h2>Gestión de Mis Reservas</h2>
            <asp:Button ID="btnVolver" runat="server" Text="Volver a Vehículos" CssClass="btn-volver-outline" OnClick="btnVolver_Click" BorderColor="DarkRed" ForeColor="DarkRed" />
        </div>

        <div class="container">
            <h3>Tus viajes:</h3>
            <asp:GridView ID="gvMisReservas" runat="server" CssClass="grid-view" AutoGenerateColumns="False" DataKeyNames="ID"
                          OnRowDeleting="gvMisReservas_RowDeleting"
                          OnRowEditing="gvMisReservas_RowEditing"
                          OnRowCancelingEdit="gvMisReservas_RowCancelingEdit"
                          OnRowUpdating="gvMisReservas_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="N°" ReadOnly="True" />
                    <asp:BoundField DataField="Vehiculo" HeaderText="Vehículo" ReadOnly="True" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha (dd/mm/aaaa)" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:CommandField ShowEditButton="True" EditText="✏️ Modificar" UpdateText=" Guardar" CancelText=" Cancelar" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText=" Anular Reserva" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>