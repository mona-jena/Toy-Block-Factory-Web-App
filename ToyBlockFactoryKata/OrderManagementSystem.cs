using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class OrderManagementSystem
    {
        private static int _orderNumber;
        private readonly Dictionary<string, Order> _allOrders = new Dictionary<string, Order>();

        internal void SetOrder(Order order)
        {
            ++_orderNumber;
            order.OrderNumber = GetOrderNumber();
            _allOrders.Add(order.OrderNumber, order);
        }

        internal Order GetOrder(string orderNumber)
        {
            var orderExists = _allOrders.TryGetValue(orderNumber, out var order);
            if (!orderExists)
                Console.WriteLine("Order does not exist"); //should get console to print this?
            return order;
        }

        private string GetOrderNumber()
        {
            var formattedOrderNumber = _orderNumber.ToString().PadLeft(4, '0');
            return formattedOrderNumber;
        }
    }
}