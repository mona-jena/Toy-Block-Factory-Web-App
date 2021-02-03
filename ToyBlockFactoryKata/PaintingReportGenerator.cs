using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class PaintingReportGenerator : IReportGenerator
    {

        public IReport GenerateReport(Order requestedOrder)
        {
            var report = new Report
            {
                ReportType = ReportType.Painting,
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
                report.OrderTable.Add(new TableRow(shape, RowItems(shape, requestedOrder)));
        }

        private List<TableColumn> RowItems(Shape shape, Order requestedOrder)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (Colour colour in Enum.GetValues(typeof(Colour)))
            {
                var block = new Block(shape, colour);
                if (requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), requestedOrder.BlockList[block]));
            }

            return rowItemQuantities;
        }
    }
}