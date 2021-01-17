namespace ToyBlockFactoryKata
{
    public class ToyBlockFactory
    {
        private Order _customerOrder;
        private readonly OrderManagementSystem _orderManagementSystem = new OrderManagementSystem();
        private ReportOrderManagementSystem _report;
            //IS IT BAD TO INITIALISE HERE OR SHOULD IT BE IN CONSTR?
        public Order CreateOrder(string customerName, string customerAddress)
        {
            _customerOrder = new Order {Name = customerName, Address = customerAddress};
            return _customerOrder;
        }

        public void SubmitOrder(Order customerOrder)
        {
            _orderManagementSystem.SetOrder(customerOrder);
        }

        public Order GetOrder(string orderId)
        {
            //should this also return the reports? should we ask customer what report they want?
           
            return _orderManagementSystem.GetOrder(orderId); //Is calling GetOrder() twice bad?
        }

        //OR
        
        public string GetInvoiceReport(string orderNumber)
        {
            var requestedOrder = _orderManagementSystem.GetOrder(orderNumber);
            var priceList = new PricingList();
            _report = new ReportOrderManagementSystem(priceList, requestedOrder);
            var invoiceReport = _report.GenerateInvoice();
            return invoiceReport;
        }

        //GetInvoiceStatement(int requestedReport)
            // calls private GetOrder(orderId) 
        
        //once user selected what report they want, call that particular method
            // this method will call private GetOrder() ???
    

    
    }

    
}