namespace ToyBlockFactoryKata
{
    internal class ReportGenerator
    {
        private static readonly IInvoiceCalculationStrategy PriceList = new PricingCalculation();
        private readonly InvoiceReportGenerator _invoiceReportGenerator = new InvoiceReportGenerator(PriceList);
        private readonly CuttingListReportGenerator _cuttingReportGenerator = new CuttingListReportGenerator();
        private readonly PaintingReportGenerator _paintingReportGenerator = new PaintingReportGenerator();

        internal Report GenerateInvoice(Order requestedOrder) //should I return an interface?
        {
            return _invoiceReportGenerator.InputOrderDetails(requestedOrder);
        }

        public Report GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportGenerator.InputOrderDetails(requestedOrder);
        }

        public Report GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportGenerator.InputOrderDetails(requestedOrder);
        }
    }

    
}