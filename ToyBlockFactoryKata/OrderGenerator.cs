using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class OrderGenerator
    {
        private static int _orderNumber;
        private readonly Dictionary<string, Order> _orderDirectory = new(); //same as new Dictionary<string, Order>()

        internal void CreateOrder(Order order)
        {
            ++_orderNumber;
            order.OrderId = GetOrderNumber();
            _orderDirectory.Add(order.OrderId, order);
        }

        internal bool GetOrder(string orderId, out Order order)
        {
            return _orderDirectory.TryGetValue(orderId, out order);
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderNumber.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }
    }
}