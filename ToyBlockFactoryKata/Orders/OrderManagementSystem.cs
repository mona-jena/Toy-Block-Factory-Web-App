using System.Collections.Generic;
using System.Data;

namespace ToyBlockFactoryKata.Orders
{
    internal class OrderManagementSystem
    {
        private int _orderId;
        internal Dictionary<string, Order> orderRecords { get; } = new();
        
        internal Dictionary<string, Order> tempOrderRecords { get; } = new();

        internal string SubmitOrder(Order order)
        {
            /*++_orderId;
            var orderId = GetOrderNumber();
            order = order with {OrderId = orderId};*/
            tempOrderRecords.Remove(order.OrderId);
            orderRecords.Add(order.OrderId, order);
            return order.OrderId;
        }

        internal string CreateOrder(string customerName, string customerAddress)
        {
            var order = new Order(customerName, customerAddress);
            ++_orderId;
            var orderId = GetOrderNumber();
            order = order with {OrderId = orderId};
            tempOrderRecords.Add(order.OrderId, order);
            return orderId;
        }

        internal bool GetOrder(string orderId, out Order order)
        {
            //return orderRecords.TryGetValue(orderId, out order);
            return tempOrderRecords.TryGetValue(orderId, out order);
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderId.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }

        public void DeleteOrder(string orderId)
        {
            --_orderId;
            if (GetOrder(orderId, out _))
            {
                tempOrderRecords.Remove(orderId);
            }
        }
    }
}