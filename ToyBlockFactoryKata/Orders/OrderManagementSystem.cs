using System.Collections.Generic;

namespace ToyBlockFactoryKata.Orders
{
    internal class OrderManagementSystem
    {
        private int _orderNumber;
        internal Dictionary<string, Order> orderRecords { get; } = new();

        internal string SubmitOrder(Order order)
        {
            ++_orderNumber;
            var orderId = GetOrderNumber();
            order = order with {OrderId = orderId}; 
            orderRecords.Add(order.OrderId, order);
            return orderId;
        }

        internal bool GetOrder(string orderId, out Order order)
        {
            return orderRecords.TryGetValue(orderId, out order);
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderNumber.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }
    }
}