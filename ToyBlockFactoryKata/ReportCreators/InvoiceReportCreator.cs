using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportCreators
{
    internal class InvoiceReportCreator : IReportCreator
    {
        private readonly IInvoiceCalculator _pricingCalculator;
        private readonly ITableFactory _tableFactory;

        internal InvoiceReportCreator(IInvoiceCalculator pricingCalculator, ITableFactory tableFactory)
        {
            _pricingCalculator = pricingCalculator;
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
                LineItems = _pricingCalculator.GenerateLineItems(requestedOrder),
                Total = CalculateTotal(requestedOrder)
            };
            
            return report;
        }

        private decimal CalculateTotal(Order requestedOrder)
        {
            return _pricingCalculator.GenerateLineItems(requestedOrder).Sum(item => item.Total);
        }
        
    }
}