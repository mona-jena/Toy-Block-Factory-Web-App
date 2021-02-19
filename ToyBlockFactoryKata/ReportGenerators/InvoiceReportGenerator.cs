using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class InvoiceReportGenerator : IReportGenerator
    {
        private readonly IInvoiceCalculationStrategy _pricingCalculator;
        private readonly ITableFactory _tableFactory;

        internal InvoiceReportGenerator(IInvoiceCalculationStrategy pricingCalculator, ITableFactory tableFactory)
        {
            _pricingCalculator = pricingCalculator;
            _tableFactory = tableFactory;
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
            
            var table = _tableFactory.GenerateTable(requestedOrder.BlockList);
            report.OrderTable.AddRange(table);
            
            var lineItems = _pricingCalculator.GenerateLineItems(requestedOrder.BlockList);
            report.LineItems.AddRange(lineItems);

            return report;
        }

        
    }
}