using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    internal class CuttingListReportGenerator : IReportGenerator
    {
        public IReport GenerateReport(Order requestedOrder)
        {
            var report = new Report
            {
                ReportType = ReportType.CuttingList,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            GenerateTable(report, requestedOrder);
            return report;
        }

        private void GenerateTable(Report report, Order requestedOrder)
        {
            foreach (Shape shape in Enum.GetValues(typeof(Shape)))
                report.OrderTable.Add(new TableRow(shape, RowQuantity(shape, requestedOrder)));
        }

        private List<TableColumn> RowQuantity(Shape shape, Order requestedOrder)
        {
            var shapeQuantity = requestedOrder.BlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
            return new List<TableColumn> {new("Qty", shapeQuantity)};
        }
    }
}