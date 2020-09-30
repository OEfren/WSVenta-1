using System;
using System.Collections.Generic;

namespace WSVenta.Models
{
    public partial class Venta
    {
        public Venta()
        {
            Conceptos = new HashSet<Conceptos>();
        }

        public long IdVenta { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Total { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<Conceptos> Conceptos { get; set; }
    }
}
