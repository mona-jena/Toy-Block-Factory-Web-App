namespace ToyBlockFactoryKata
{
    internal class ReportGenerator
    {
        
        private readonly IReportGenerator _invoiceReportGenerator;
        private readonly IReportGenerator _cuttingReportGenerator = new CuttingListReportGenerator();
        private readonly IReportGenerator _paintingReportGenerator = new PaintingReportGenerator();

        public ReportGenerator(IInvoiceCalculationStrategy priceCalculator)
        {
            _invoiceReportGenerator = new InvoiceReportGenerator(priceCalculator);
        }

        internal IReport GenerateInvoice(Order requestedOrder) 
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