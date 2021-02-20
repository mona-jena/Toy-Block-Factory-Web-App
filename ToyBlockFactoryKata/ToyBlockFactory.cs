using System;
using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.ReportGenerators;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private readonly OrderManagementSystem _orderManagementSystem = new();
        private readonly ReportGenerator _reportGenerator;

        public ToyBlockFactory(IInvoiceCalculator priceCalculator)
        {
            _reportGenerator = new ReportGenerator(priceCalculator);
        }

        public Order CreateOrder(string customerName, string customerAddress)
        {
            return new(customerName, customerAddress);
        }

        public Order CreateOrder(string customerName, string customerAddress, DateTime dueDate)
        {
            return new(customerName, customerAddress, dueDate);
        }

        public string SubmitOrder(Order customerOrder)
        {
            if (customerOrder.BlockList.Count > 0)
            {
                return _orderManagementSystem.SubmitOrder(customerOrder);
            }

            return string.Empty;  
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

        public IReport GetInvoiceReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId);
            return _reportGenerator.GenerateInvoice(requestedOrder);
        }

        public IReport GetCuttingListReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId);
            return _reportGenerator.GenerateCuttingList(requestedOrder);
        }

        public IReport GetPaintingReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId);
            return _reportGenerator.GeneratePaintingReport(requestedOrder);
        }

        public IEnumerable<IReport> GetCuttingListsByDate(DateTime date)
        {
            var orderRecords = _orderManagementSystem.orderRecords;
            return _reportGenerator.FilterCuttingReportsByDate(date, orderRecords);
        }

        public IEnumerable<IReport> GetPaintingReportsByDate(DateTime date)
        {
            var orderRecords = _orderManagementSystem.orderRecords;
            return _reportGenerator.FilterPaintingReportsByDate(date, orderRecords);
        }
    }
}