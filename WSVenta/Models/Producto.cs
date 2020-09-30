using System;
using System.Collections.Generic;

namespace WSVenta.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Conceptos = new HashSet<Conceptos>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Costo { get; set; }

        public virtual ICollection<Conceptos> Conceptos { get; set; }
    }
}
