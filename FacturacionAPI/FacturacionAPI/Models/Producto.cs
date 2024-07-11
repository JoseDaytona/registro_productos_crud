﻿namespace FacturacionAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public short IdEstado { get; set; }
        public string? Estado { get; set; }
    }
}
