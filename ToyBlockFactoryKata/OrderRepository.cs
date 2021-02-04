using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class OrderRepository
    {
        private readonly Dictionary<string, Order> _orderRecords = new();
        private int _orderNumber;

        internal void SubmitOrder(Order order)
        {
            ++_orderNumber;
            order = order with {OrderId = GetOrderNumber()};  //makes a copy of order and assigns orderID as of when this order is Submitted - so if someone changes it in order, it wont change here
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