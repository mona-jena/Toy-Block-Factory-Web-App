using System;

namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private readonly OrderRepository _orderRepository = new();
        private readonly ReportGenerator _report;
        

        //IS IT BAD TO INITIALISE HERE OR SHOULD IT BE IN CONSTR?

        public ToyBlockFactory() : this(new PricingCalculator())
        {
            
        }
        
        public ToyBlockFactory(IInvoiceCalculationStrategy priceCalculator) 
        {
            _report = new(priceCalculator);
        }
        
        public Order CreateOrder(string customerName, string customerAddress)
        {
            return new Order (customerName, customerAddress);
        }
        
        //USED WHEN NO DATE SPECIFIED 
        public Order CreateOrder(string customerName, string customerAddress, DateTime dueDate)
        {
            return new Order (customerName, customerAddress, dueDate);
        }
        
        public void SubmitOrder(Order customerOrder)
        {
            _orderRepository.SubmitOrder(customerOrder); 
        }

        public Order GetOrder(string orderId)
        {
            //should we ask customer what report they want?
            var orderExists = _orderRepository.GetOrder(orderId, out var order); 
            return order;
        }

        public IReport GetInvoiceReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId);
            return _report.GenerateInvoice(requestedOrder);
        }

        public IReport GetCuttingListReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId);
            return _report.GenerateCuttingList(requestedOrder);
        }

        public IReport GetPaintingReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId);
            return _report.GeneratePaintingReport(requestedOrder);
        }
    }
}