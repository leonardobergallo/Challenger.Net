using System;
using System.Collections.Generic;

namespace challenger.Net_Core.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public int? Precio { get; set; }
        public DateTime? FechaCarga { get; set; }
        public string? Categoria { get; set; }
    }
}
