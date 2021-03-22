using System;
using System.Collections.Generic;
using System.Data;

namespace ToyBlockFactoryKata.Orders
{
    internal class OrderManagementSystem
    {
        private int _orderId;
        internal Dictionary<string, Order> orderRecords { get; } = new();
        
        internal string SubmitOrder(Order order)
        {
            /*++_orderId;
            var orderId = GetOrderNumber();
            order = order with {OrderId = orderId};*/
            
            order.isSubmitted = true;
            return order.OrderId;
        }

        internal Order CreateOrder(string customerName, string customerAddress)
        {
            return CreateOrder(customerName, customerAddress, DateTime.Today.AddDays(7));
        }
        
        internal Order CreateOrder(string customerName, string customerAddress, DateTime dueDate)
        {
            ++_orderId;
            var orderId = GetOrderNumber();
            var order = new Order(customerName, customerAddress, dueDate, orderId);
            
            //order = order with {OrderId = orderId};
            orderRecords.Add(order.OrderId, order);
            return order;
        }

        internal bool GetOrder(string orderId, out Order order)
        {
            return orderRecords.TryGetValue(orderId, out order);
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderId.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }

        public void DeleteOrder(string orderId)
        {
            if (GetOrder(orderId, out var order) && !order.isSubmitted)
            {
                orderRecords.Remove(order.OrderId);
            }
        }
    }
}