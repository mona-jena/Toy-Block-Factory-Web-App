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

        internal bool GetOrder(string orderId, out Order order, bool submitted = false)
        {
            order = orderRecords.Select(kvp => kvp.Value).FirstOrDefault(o => o.IsSubmitted == submitted && o.OrderId == orderId);
            return order != null;
        }
        
        internal List<Order> GetAllOrders()
        {
            return orderRecords.Values.ToList();
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
        
        internal bool Exists(string orderId)
        {
            return orderRecords.ContainsKey(orderId);
        }
        
        // delete order that doesn't exist --> throw exception - can use c# one
        // delete order that has been submitted --> return false
        // delete order that has not be submitted --> return true
        internal bool DeleteOrder(string orderId)
        {
            if (!GetOrder(orderId, out var order))
                throw new ArgumentException("This order does not exist!");

            return !order.IsSubmitted && orderRecords.Remove(order.OrderId);
        }
        
        internal bool OrderSubmitted(string orderId)
        {
            return orderRecords[orderId].IsSubmitted;
        }

        internal List<Order> FilterOrders(DateTime dueDate)
        {
            var orders = orderRecords.Where(o => o.Value.DueDate == dueDate);
            return orders.Select(order => order.Value).ToList();
        }
        
    }
}