using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    internal class CuttingListReportGenerator : IReportGenerator
    {
        private readonly Order _requestedOrder;
        private readonly Report _report = new();

        public CuttingListReportGenerator(Order requestedOrder)
        {
            _requestedOrder = requestedOrder;
        }

        public Report InputOrderDetails()
        {
            _report.ReportType = ReportType.CuttingList;
            _report.Name = _requestedOrder.Name;
            _report.Address = _requestedOrder.Address;
            _report.DueDate = _requestedOrder.DueDate;
            _report.OrderId = _requestedOrder.OrderId;
            GenerateTable();
            return _report;
        }

        private void GenerateTable()
        {
            foreach (Shape shape in Enum.GetValues(typeof(Shape)))
                _report.OrderTable.Add(new TableRow(shape, RowQuantity(shape)));
        }

        private List<TableColumn> RowQuantity(Shape shape)
        {
            var shapeQuantity = _requestedOrder.BlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
            return new List<TableColumn>{new("Qty", shapeQuantity)};
        }
    }
    
}