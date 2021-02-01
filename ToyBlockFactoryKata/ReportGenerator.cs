namespace ToyBlockFactoryKata
{
    internal class ReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _priceList = new PricingCalculation();
        private readonly InvoiceReportGenerator _invoiceReportGenerator = new InvoiceReportGenerator(_priceList);
        private readonly CuttingListReportGenerator _cuttingReportGenerator = new CuttingListReportGenerator();

        internal Report GenerateInvoice(Order requestedOrder) //should I return an interface?
        {
            return _invoiceReportGenerator.InputOrderDetails(requestedOrder);
        }

        public Report GenerateCuttingList(Order requestedOrder)
        {
            return _invoiceReportGenerator.InputOrderDetails(requestedOrder);
        }

        public Report GeneratePaintingReport(Order requestedOrder)
        {
            var invoiceReportGenerator = new PaintingReportGenerator(requestedOrder);
            return invoiceReportGenerator.InputOrderDetails();
        }
    }

    
}