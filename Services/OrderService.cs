using GGStoreProyecto.Models;
using System.Collections.Generic;

namespace GGStoreProyecto.Services
{
    public class OrderService
    {
        private List<Order> _orders = new List<Order>();

        public List<Order> GetOrders() => _orders;

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }
    }
}