using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportCreators
{
    internal class PaintingReportCreator : IReportCreator
    {
        private readonly ITableFactory _tableFactory;

        internal PaintingReportCreator(ITableFactory tableFactory)
        {
            _tableFactory = tableFactory;
        }

        public IReport GenerateReport(Order requestedOrder)
        {
            var report = new Report
            {
                ReportType = ReportType.Painting,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId,
                OrderTable = _tableFactory.GenerateTable(requestedOrder.BlockList)
            };

            return report;
        }
        
    }
}