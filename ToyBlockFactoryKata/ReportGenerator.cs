namespace ToyBlockFactoryKata
{
    internal class ReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Order _requestedOrder;
        
        internal ReportGenerator(Order requestedOrder)
        {
            _priceList = new PricingList();
            _requestedOrder = requestedOrder;
        }

        internal string GenerateInvoice()
        {
            var invoiceReportGenerator = new InvoiceReportGenerator(_priceList, _requestedOrder);
            return invoiceReportGenerator.GenerateReport();
        }
    }

    
}