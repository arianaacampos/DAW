<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="DAWSistema.Vehiculos" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <title>Nuestros Vehículos - AutoRent</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }
        body { background-color: #f8f9fa; color: #333; }
        
        /* Barra de Navegación */
        .navbar { 
            display: flex; 
            justify-content: space-between; 
            align-items: center; 
            background-color: white; 
            padding: 15px 50px; 
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
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
            text-decoration: none;
        }

        /* Encabezado de la página */
        .header-vehiculo {
            padding: 40px 50px;
            display: flex;
            justify-content: space-between;
            align-items: flex-end;
        }
        .header-text h1 { font-size: 2rem; color: #111; margin-bottom: 5px; }
        .header-text p { color: #777; font-size: 1rem; }
        .btn-filtros {
            background-color: white;
            border: 1px solid #ccc;
            padding: 10px 20px;
            border-radius: 8px;
            font-weight: bold;
            cursor: pointer;
            color: #333;
        }

        /* Grilla de Autos */
        .grid-vehiculos {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            gap: 25px;
            padding: 0 50px 50px 50px;
        }

        /* Tarjeta de Auto (Card) */
        .card {
            background-color: white;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            border: 1px solid #eaeaea;
            display: flex;
            flex-direction: column;
        }
        
        .card-img {
            height: 180px;
            background: linear-gradient(135deg, #444, #111);
            position: relative;
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 4rem;
        }
        .badge {
            position: absolute;
            top: 15px;
            left: 15px;
            background-color: white;
            color: #333;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 0.8rem;
            font-weight: bold;
        }

        .card-body { padding: 20px; flex: 1; display: flex; flex-direction: column; }
        .title-row { display: flex; justify-content: space-between; align-items: center; margin-bottom: 15px; }
        .title-row h3 { font-size: 1.1rem; color: #111; }
        .rating { color: #f39c12; font-weight: bold; font-size: 0.9rem; }

        .specs {
            display: flex;
            justify-content: space-between;
            color: #888;
            font-size: 0.8rem;
            margin-bottom: 25px;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }
        .spec-item { display: flex; flex-direction: column; align-items: center; gap: 5px; }

        .footer-row { display: flex; justify-content: space-between; align-items: center; margin-top: auto; }
        .price { font-size: 1.2rem; color: #8B0000; font-weight: bold; }
        .price span { font-size: 0.8rem; color: #888; font-weight: normal; }
        .btn-reservar {
            background-color: #8B0000;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 8px;
            font-weight: bold;
            cursor: pointer;
            transition: 0.3s;
        }
        .btn-reservar:hover { background-color: #660000; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <nav class="navbar">
            <div class="nav-left">
                <div class="logo"><span class="logo-icon">🚗</span> AutoRent</div>
                <div class="nav-links">
                    <a href="Inicio.aspx">Inicio</a>
                    <a href="Vehiculo.aspx" class="active" style="margin-left: 15px;">Vehículos</a>
                </div>
            </div>
            <asp:Button ID="btnAcceso" runat="server" Text="Ir al Panel" CssClass="btn-login" OnClick="btnAcceso_Click" />
        </nav>

        <div class="header-vehiculo">
          
            <div class="header-text">
                <h1>Nuestros Vehículos</h1>
                <p>Encuentra el auto perfecto para tu próxima aventura.</p>
            </div>
            <button type="button" class="btn-filtros">⚙️ Filtros</button>
        </div>

        <div class="grid-vehiculos">
            
            <div class="card">
               <div style="position: relative; height: 180px; overflow: hidden;">
                   <span class="badge" style="z-index: 10;">Familiar</span>
                   <img src="Imagenes/peugeot.jpeg" alt="Peugeot 208 Allure AT AM25.5" style="width: 100%; height: 100%; object-fit: cover;" />
               </div>
               <div class="card-body">
                   <div class="title-row">
                       <h3>Peugeot 208 Allure</h3>
                       <span class="rating">⭐ 4.9</span>
                   </div>
                   <div class="specs">
                       <div class="spec-item"><span>👥</span> 5 Asientos</div>
                       <div class="spec-item"><span>⚙️</span> Automático</div>
                       <div class="spec-item"><span>🧳</span> 2 Maletas</div>
                   </div>
                   <div class="footer-row">
                       <div class="price">$76.999<span>/día</span></div>
                       <asp:Button ID="btnResPeugeot" runat="server" Text="Reservar" CssClass="btn-reservar" OnClick="btnResPeugeot_Click" />
                   </div>
               </div>
            </div>

            <div class="card">
                <div style="position: relative; height: 180px; overflow: hidden;">
                    <span class="badge" style="z-index: 10;">SUV</span>
                    <img src="Imagenes/corolla.jpeg" alt="Toyota Corolla Cross XLI" style="width: 100%; height: 100%; object-fit: cover;" />
                </div>
                <div class="card-body">
                    <div class="title-row">
                        <h3>Toyota Corolla Cross XLI</h3>
                        <span class="rating">⭐ 4.8</span>
                    </div>
                    <div class="specs">
                        <div class="spec-item"><span>👥</span> 5 Asientos</div>
                        <div class="spec-item"><span>⚙️</span> Automático</div>
                        <div class="spec-item"><span>🧳</span> 3 Maletas</div>
                    </div>
                    <div class="footer-row">
                        <div class="price">$95.000<span>/día</span></div>
                        <asp:Button ID="btnResCorolla" runat="server" Text="Reservar" CssClass="btn-reservar" OnClick="btnResCorolla_Click" />
                    </div>
                </div>
            </div>

            <div class="card">
                <div style="position: relative; height: 180px; overflow: hidden;">
                    <span class="badge" style="z-index: 10;">Pick-up</span>
                    <img src="Imagenes/nissan.jpeg" alt="Nissan Frontier S" style="width: 100%; height: 100%; object-fit: cover;" />
                </div>
                <div class="card-body">
                    <div class="title-row">
                        <h3>Nissan Frontier S</h3>
                        <span class="rating">⭐ 4.7</span>
                    </div>
                    <div class="specs">
                        <div class="spec-item"><span>👥</span> 5 Asientos</div>
                        <div class="spec-item"><span>⚙️</span> Manual</div>
                        <div class="spec-item"><span>🧳</span> 4 Maletas</div>
                    </div>
                    <div class="footer-row">
                        <div class="price">$115.000<span>/día</span></div>
                        <asp:Button ID="btnResFrontier" runat="server" Text="Reservar" CssClass="btn-reservar" OnClick="btnResFrontier_Click" />
                    </div>
                </div>
            </div>

            <div class="card">
                <div style="position: relative; height: 180px; overflow: hidden;">
                    <span class="badge" style="z-index: 10;">Van</span>
                    <img src="Imagenes/toyota.jpeg" alt="Toyota Hiace AT" style="width: 100%; height: 100%; object-fit: cover;" />
                </div>
                <div class="card-body">
                    <div class="title-row">
                        <h3>Toyota Hiace AT</h3>
                        <span class="rating">⭐ 4.9</span>
                    </div>
                    <div class="specs">
                        <div class="spec-item"><span>👥</span> 9 Asientos</div>
                        <div class="spec-item"><span>⚙️</span> Automático</div>
                        <div class="spec-item"><span>🧳</span> 6 Maletas</div>
                    </div>
                    <div class="footer-row">
                        <div class="price">$145.000<span>/día</span></div>
                        <asp:Button ID="btnResHiace" runat="server" Text="Reservar" CssClass="btn-reservar" OnClick="btnResHiace_Click" />
                    </div>
                </div>
            </div>

            <div class="card">
                <div style="position: relative; height: 180px; overflow: hidden;">
                    <span class="badge" style="z-index: 10;">Sedán</span>
                    <img src="Imagenes/vaolkswagen.jpeg" alt="Volkswagen Virtus MSI" style="width: 100%; height: 100%; object-fit: cover;" />
                </div>
                <div class="card-body">
                    <div class="title-row">
                        <h3>Volkswagen Virtus MSI</h3>
                        <span class="rating">⭐ 4.6</span>
                    </div>
                    <div class="specs">
                        <div class="spec-item"><span>👥</span> 5 Asientos</div>
                        <div class="spec-item"><span>⚙️</span> Manual</div>
                        <div class="spec-item"><span>🧳</span> 2 Maletas</div>
                    </div>
                    <div class="footer-row">
                        <div class="price">$70.000<span>/día</span></div>
                        <asp:Button ID="btnResVirtus" runat="server" Text="Reservar" CssClass="btn-reservar" OnClick="btnResVirtus_Click" />
                    </div>
                </div>
            </div>
            
        </div>
    </form>
</body>
</html>