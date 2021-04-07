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
            return _orderManagementSystem.GetOrder(orderId, out _);
        }
        
        public Order GetOrder(string orderId)
        {
            var orderExists = _orderManagementSystem.GetOrder(orderId, out var order);
            if (!orderExists)
                throw new ArgumentException("This order does not exist!");
            
            return order;
        }

        public void DeleteOrder(string orderId)
        {
            _orderManagementSystem.DeleteOrder(orderId);
        }

        public IReport GetReport(string orderId, ReportType reportType)
        {
            var requestedOrder = GetOrder(orderId);
            if (!requestedOrder.IsSubmitted)
            {
                throw new ArgumentException("You need to submit your order to get a report!");
            }
            return _reportSystem.GenerateReport(requestedOrder, reportType);
        }
        
        public IEnumerable<IReport> GetReportsByDate(DateTime date, ReportType reportType)
        {
            var orderRecords = _orderManagementSystem.FilterOrders(date); 
            return _reportSystem.FilterReportsByDate(date, orderRecords, reportType);
        }
        
    }
}