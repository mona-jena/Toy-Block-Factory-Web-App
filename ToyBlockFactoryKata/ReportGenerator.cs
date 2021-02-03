namespace ToyBlockFactoryKata
{
    internal class ReportGenerator
    {
        private static readonly IInvoiceCalculationStrategy PriceCalculator = new PricingCalculator();
        private readonly IReportGenerator _invoiceReportGenerator = new InvoiceReportGenerator(PriceCalculator);
        private readonly IReportGenerator _cuttingReportGenerator = new CuttingListReportGenerator();
        private readonly IReportGenerator _paintingReportGenerator = new PaintingReportGenerator();

        internal IReport GenerateInvoice(Order requestedOrder) //should I return an interface?
        {
            return _invoiceReportGenerator.GenerateReport(requestedOrder);
        }

        public IReport GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportGenerator.GenerateReport(requestedOrder);
        }

        public IReport GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportGenerator.GenerateReport(requestedOrder);
        }
    }

    
}