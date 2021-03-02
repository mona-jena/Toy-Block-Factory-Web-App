using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportCreators
{
    internal class InvoiceReportCreator : IReportCreator
    {
        private readonly ILineItemsCalculator _lineItemsCalculator;
        private readonly ITableFactory _tableFactory;

        internal InvoiceReportCreator(ILineItemsCalculator lineItemsCalculator, ITableFactory tableFactory)
        {
            _lineItemsCalculator = lineItemsCalculator;
            _tableFactory = tableFactory;
        }

        public IReport GenerateReport(Order requestedOrder)
        {
            var report = new InvoiceReport
            {
                ReportType = ReportType.Invoice,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId,
                OrderTable = _tableFactory.GenerateTable(requestedOrder.BlockList),
                LineItems = _lineItemsCalculator.GenerateLineItems(requestedOrder),
                Total = CalculateTotal(requestedOrder)
            };
            
            return report;
        }

        private decimal CalculateTotal(Order requestedOrder)
        {
            return _lineItemsCalculator.GenerateLineItems(requestedOrder).Sum(item => item.Total);
        }
        
    }
}