using System;

namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private readonly OrderRepository _orderRepository = new();
        private readonly ReportGenerator _report = new();

        //IS IT BAD TO INITIALISE HERE OR SHOULD IT BE IN CONSTR?

        
        public Order CreateOrder(string customerName, string customerAddress)
        {
            return new Order (customerName, customerAddress);
        }
        
        //USED WHEN NO DATE SPECIFIED 
        public Order CreateOrder(string customerName, string customerAddress, DateTime date)
        {
            return new Order (customerName, customerAddress, date);
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