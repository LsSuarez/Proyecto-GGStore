using System;
using System.Collections.Generic;
using System.Linq;  // Necesario para el uso de LINQ

namespace GGStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // Inicialización con una lista vacía para evitar que sea null
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // Agregar una propiedad para los detalles del pedido
        public string OrderDetails
        {
            get
            {
                // Verificar si Products es nulo y proporcionar una lista vacía si lo es
                return string.Join(", ", Products?.Select(p => p.Name) ?? Enumerable.Empty<string>());
            }
        }
    }
}
