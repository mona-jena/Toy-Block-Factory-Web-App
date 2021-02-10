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

        internal InvoiceReportGenerator(IInvoiceCalculationStrategy pricingCalculator)
        {
            _pricingCalculator = pricingCalculator;
        }

        public IReport GenerateReport(Order requestedOrder) //Should sep setup and getting part of report?
        {
            var report = new InvoiceReport
            {
                ReportType = ReportType.Invoice,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            
            GenerateTable(report, requestedOrder);
            var lineItems = _pricingCalculator.GenerateLineItems(requestedOrder); //RENAME
            report.LineItems.AddRange(lineItems);

            return report;
        }

        private void GenerateTable(InvoiceReport report, Order requestedOrder)
        {
            foreach (var shape in ShapesUsedInOrder(requestedOrder))
                report.OrderTable.Add(new TableRow(shape, RowItems(shape, requestedOrder)));
        }

        private IEnumerable<Shape> ShapesUsedInOrder(Order requestedOrder)
        {
            var shapesUsed = requestedOrder.BlockList.Keys.ToList();
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private List<TableColumn> RowItems(Shape shape, Order requestedOrder)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (var colour in requestedOrder.BlockList.Select(b => b.Key.Colour).Distinct())
            {
                var block = new Block(shape, colour);
                if (requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), requestedOrder.BlockList[block]));
                else
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), 0));
            }

            return rowItemQuantities;
        }
    }
}