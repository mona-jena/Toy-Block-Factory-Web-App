using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _pricingCalculator;
        private readonly ITableGenerator _tableGenerator;

        internal InvoiceReportGenerator(IInvoiceCalculationStrategy pricingCalculator, ITableGenerator tableGenerator)
        {
            _pricingCalculator = pricingCalculator;
            _tableGenerator = tableGenerator;
        }

        public IReport GenerateReport(ReportType reportType, Order requestedOrder)
        {
            var report = new InvoiceReport
            {
                ReportType = reportType,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            
            var table = _tableGenerator.GenerateTable(requestedOrder.BlockList);
            report.OrderTable.AddRange(table);
            
            var lineItems = _pricingCalculator.GenerateLineItems(requestedOrder.BlockList);
            report.LineItems.AddRange(lineItems);

            return report;
        }

        
    }
}