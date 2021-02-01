using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal class PaintingReportGenerator
    {
        private readonly Order _requestedOrder;
        private readonly Report _report = new();

        public PaintingReportGenerator(Order requestedOrder)
        {
            _requestedOrder = requestedOrder;
        }

        public Report InputOrderDetails()
        {
            _report.ReportType = ReportType.Painting;
            _report.Name = _requestedOrder.Name;
            _report.Address = _requestedOrder.Address;
            _report.DueDate = _requestedOrder.DueDate;
            _report.OrderId = _requestedOrder.OrderId;
            GenerateTable();
            return _report;
        }

        private void GenerateTable()
        {
            // how to write in LINQ??
            foreach (Shape shape in Enum.GetValues(typeof(Shape)))
                _report.OrderTable.Add(new TableRow(shape, RowItems(shape)));
        }

        private List<TableColumn> RowItems(Shape shape)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (Colour colour in Enum.GetValues(typeof(Colour)))
            {
                var block = new Block(shape, colour);
                if (_requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), _requestedOrder.BlockList[block]));
            }

            return rowItemQuantities;
        }
    }
}