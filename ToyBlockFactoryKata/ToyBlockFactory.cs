using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private readonly OrderRepository _orderRepository = new();
        private readonly ReportGenerator _reportGenerator;
        
        public ToyBlockFactory() : this(new PricingCalculator())
        {
        }
        
        public ToyBlockFactory(IInvoiceCalculationStrategy priceCalculator) 
        {
            _reportGenerator = new ReportGenerator(priceCalculator);
        }
        
        public Order CreateOrder(string customerName, string customerAddress)
        {
            return new Order (customerName, customerAddress);
        }
        
        public Order CreateOrder(string customerName, string customerAddress, DateTime dueDate)
        {
            return new Order (customerName, customerAddress, dueDate);
        }
        
        public string SubmitOrder(Order customerOrder)
        { 
            return _orderRepository.SubmitOrder(customerOrder); 
        }

        public Order GetOrder(string orderId)
        {
            var orderExists = _orderRepository.GetOrder(orderId, out var order); 
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

        public List<IReport> GetCuttingListsByDate(DateTime date)
        {
            var orderRecords = _orderRepository.OrderRecords;
            return _reportGenerator.FilterCuttingReportsByDate(date, orderRecords);
        }

        public List<IReport> GetPaintingReportsByDate(DateTime date)
        {
            var orderRecords = _orderRepository.OrderRecords;
            return _reportGenerator.FilterPaintingReportsByDate(date, orderRecords);
        }
    }
}