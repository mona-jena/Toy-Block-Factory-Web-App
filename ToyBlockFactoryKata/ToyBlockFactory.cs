namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private Order _customerOrder;
        private readonly OrderManagementSystem _orderManagementSystem = new OrderManagementSystem();
        private ReportGeneratorSystem _report;
            //IS IT BAD TO INITIALISE HERE OR SHOULD IT BE IN CONSTR?
        public Order CreateOrder(string customerName, string customerAddress)
        {
            _customerOrder = new Order {Name = customerName, Address = customerAddress};
            return _customerOrder;
        }

        public void SubmitOrder(Order customerOrder)
        {
            _orderManagementSystem.CreateOrder(customerOrder);
        }

        public Order GetOrder(string orderId)
        {
            //should this also return the reports? should we ask customer what report they want?
            var orderExists = _orderManagementSystem.GetOrder(orderId, out var order);
            return order;
        }

        //OR
        
        public InvoiceReportGenerator GetInvoiceReport(string orderId)
        {
            var requestedOrder = GetOrder(orderId); //should I bring this method down?
            _report = new ReportGeneratorSystem(requestedOrder);
            var invoiceReport = _report.GenerateInvoice();
            return invoiceReport;
        }

        //GetInvoiceStatement(int requestedReport)
            // calls private GetOrder(orderId) 
        
        //once user selected what report they want, call that particular method
            // this method will call private GetOrder() ???
    

    
    }

    
}