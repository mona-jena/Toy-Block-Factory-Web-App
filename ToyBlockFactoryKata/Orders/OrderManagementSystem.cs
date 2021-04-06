using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata.Orders
{
    internal class OrderManagementSystem
    {
        private int _orderId;
        private Dictionary<string, Order> orderRecords { get; } = new();

        internal Order CreateOrder(string customerName, string customerAddress)
        {
            return CreateOrder(customerName, customerAddress, DateTime.Today.AddDays(7));
        }
        
        internal Order CreateOrder(string customerName, string customerAddress, DateTime dueDate)
        {
            ++_orderId;
            var orderId = GetOrderNumber();
            var order = new Order(customerName, customerAddress, dueDate, orderId);
            orderRecords.Add(order.OrderId, order);
            return order;
        }

        internal bool GetOrder(string orderId, out Order order)
        {
            order = orderRecords.Select(kvp => kvp.Value).FirstOrDefault(o => o.IsSubmitted && o.OrderId == orderId);
            return order != null;
        }
        
        internal string SubmitOrder(Order order)
        {
            order.IsSubmitted = true;
            return order.OrderId;
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderId.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }

        internal void DeleteOrder(string orderId)
        {
            if (GetOrder(orderId, out var order) && !order.IsSubmitted)
            {
                order.IsDeleted = true;
                orderRecords.Remove(order.OrderId);
            }
        }

        internal Dictionary<string, Order> FilterOrders(DateTime dueDate)
        {
            var orders = orderRecords.Where(o => o.Value.DueDate == dueDate);
            return orders.ToDictionary(order => order.Key, order => order.Value);
        }
    }
}