using System;
using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.ReportCreators;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private readonly OrderManagementSystem _orderManagementSystem = new();
        private readonly ReportSystem _reportSystem;

        public ToyBlockFactory(ILineItemsCalculator priceCalculator)
        {
            _reportSystem = new ReportSystem(priceCalculator);
        }

        public Order CreateOrder(string customerName, string customerAddress) 
        {
            return _orderManagementSystem.CreateOrder(customerName, customerAddress);
        }
        
        public Order CreateOrder(string customerName, string customerAddress, DateTime dueDate)
        {
            return _orderManagementSystem.CreateOrder(customerName, customerAddress, dueDate);
        }

        public string SubmitOrder(Order customerOrder)
        {
            if (customerOrder.BlockList.Count <= 0) return string.Empty;
            return _orderManagementSystem.SubmitOrder(customerOrder);
        }

        public bool OrderExists(string orderId) 
        {
            return _orderManagementSystem.Exists(orderId);
        }

        public bool OrderSubmitted(string orderId)
        {
            return _orderManagementSystem.OrderSubmitted(orderId);
        }

        public Order GetOrder(string orderId, bool submitted = true)
        {
            var orderSubmitted = _orderManagementSystem.GetOrder(orderId, out var order, submitted);
            if (!orderSubmitted)
                throw new ArgumentException("This order does not exist or has already been submitted!");
            
            return order;
        }
        
        public bool DeleteOrder(string orderId)
        {
            return _orderManagementSystem.DeleteOrder(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _orderManagementSystem.GetAllOrders();
        }

        public IReport GetReport(string orderId, ReportType reportType)
        {
            var requestedOrder = GetOrder(orderId);
            return _reportSystem.GenerateReport(requestedOrder, reportType);
        }
        
        public IEnumerable<IReport> GetReportsByDate(DateTime date, ReportType reportType)
        {
            var orderRecords = _orderManagementSystem.FilterOrders(date); 
            return _reportSystem.FilterReportsByDate(orderRecords, reportType);
        }
        
    }
}