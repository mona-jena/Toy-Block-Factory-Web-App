using System;

namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private readonly OrderGenerator _orderGenerator = new();
        private readonly ReportGenerator _report = new();
        private Order _customerOrder;

        //IS IT BAD TO INITIALISE HERE OR SHOULD IT BE IN CONSTR?

        
        public Order CreateOrder(string customerName, string customerAddress)
        {
            return CreateOrder(customerName, customerAddress, DateTime.Now.AddDays(7));
        }
        
        //USED WHEN NO DATE SPECIFIED 
        public Order CreateOrder(string customerName, string customerAddress, DateTime date = default)
        {
            date = DateTime.Now.AddDays(7);
            _customerOrder = new Order {Name = customerName, Address = customerAddress};
            return _customerOrder;
        }
        
        public void SubmitOrder(Order customerOrder)
        {
            _orderGenerator.CreateOrder(customerOrder); //.SubmitOrder()???
        }

        public Order GetOrder(string orderId)
        {
            //should we ask customer what report they want?
            var orderExists = _orderGenerator.GetOrder(orderId, out var order); 
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