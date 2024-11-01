using System.Collections.Generic;
using System.Linq;

namespace Tienda.Models
{
    public class Presupuestos
    {
        public int IdPresupuesto { get; set; }
        public string NombreDestinatario { get; set; }
        public List<PresupuestoDetalle> Detalle { get; set; } = new List<PresupuestoDetalle>();

        // Método para calcular el monto total del presupuesto sin IVA
        public int MontoPresupuesto()
        {
            return Detalle.Sum(d => d.Producto.Precio * d.Cantidad);
        }

        // Método para calcular el monto total del presupuesto con IVA (21%)
        public int MontoPresupuestoConIva()
        {
            double iva = 0.21;
            return (int)(MontoPresupuesto() * (1 + iva));
        }

        // Método para calcular la cantidad total de productos en el presupuesto
        public int CantidadProductos()
        {
            return Detalle.Sum(d => d.Cantidad);
        }
    }
}