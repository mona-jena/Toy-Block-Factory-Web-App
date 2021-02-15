using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;

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

        public IReport GenerateReport(Order requestedOrder)
        {
            var report = new InvoiceReport
            {
                ReportType = ReportType.Invoice,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            
            var table = _tableGenerator.GenerateTable(report, requestedOrder);
            report.OrderTable.AddRange(table);
            
            var lineItems = _pricingCalculator.GenerateLineItems(requestedOrder);
            report.LineItems.AddRange(lineItems);

            return report;
        }

        
    }
}