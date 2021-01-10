using System.Collections.Generic;
using System.Dynamic;

namespace ToyBlockFactoryKata
{
    public class OrderManagementSystem
    {
        private static int _orderNumber = 0;
        //private List<Order> _listOfOrders = new List<Order>();
        private Dictionary<int, Order> _allOrders = new Dictionary<int, Order>();
        
        public OrderManagementSystem()
        {
           
        }

        internal void SetOrder(Order order)
        {
            order.OrderNumber = ++_orderNumber;
            _allOrders.Add(_orderNumber, order);
        }

        internal Order GetOrder(int orderNumber)
        {
            var orderExists = _allOrders.TryGetValue(orderNumber, out var order);
            if (orderExists)
                return order;
        }

       
    }
}