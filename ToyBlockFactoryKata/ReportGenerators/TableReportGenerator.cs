using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class TableReportGenerator : IReportGenerator
    {
        private readonly ITableGenerator _tableGenerator;

        internal TableReportGenerator(ITableGenerator tableGenerator)
        {
            _tableGenerator = tableGenerator;
        }

        public IReport GenerateReport(ReportType reportType, Order requestedOrder)
        {
            var report = new Report
            {
                ReportType = reportType,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            
            var table = _tableGenerator.GenerateTable(report, requestedOrder.BlockList);
            report.OrderTable.AddRange(table);
            
            return report;
        }
    }
}