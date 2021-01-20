namespace ToyBlockFactoryKata
{
    internal class ReportGeneratorSystem
    {
        private readonly IInvoiceCalculationStrategy _priceList;
        private readonly Order _requestedOrder;
        
        internal ReportGeneratorSystem(Order requestedOrder)
        {
            _priceList = new PricingList();
            _requestedOrder = requestedOrder;
        }

        internal InvoiceReportGenerator GenerateInvoice() //should I return an interface?
        {
            var invoiceReportGenerator = new InvoiceReportGenerator(_priceList, _requestedOrder);
            invoiceReportGenerator.GenerateReport();
            return invoiceReportGenerator;
        }
    }

    
}