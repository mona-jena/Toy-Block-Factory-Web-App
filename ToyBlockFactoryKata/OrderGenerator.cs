using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class OrderGenerator
    {
        private readonly Dictionary<string, Order> _orderRecords = new();
        private int _orderNumber;

        internal void CreateOrder(Order order)
        {
            ++_orderNumber;
            order.OrderId = GetOrderNumber();
            _orderRecords.Add(order.OrderId, order);
        }

        internal bool GetOrder(string orderId, out Order order)
        {
            return _orderRecords.TryGetValue(orderId, out order);
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderNumber.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }
    }
}