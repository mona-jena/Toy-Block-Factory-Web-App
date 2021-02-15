using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class QuantityTableGenerator : ITableGenerator
    {

        public IEnumerable<TableRow> GenerateTable(IReport report, Order requestedOrder)
        {
            List<TableRow> table = new List<TableRow>();
            foreach (var shape in ShapesUsedInOrder(requestedOrder))
                report.OrderTable.Add(new TableRow(shape, RowQuantity(shape, requestedOrder)));

            return table;
        }
        
        private IEnumerable<Shape> ShapesUsedInOrder(Order requestedOrder)
        {
            var shapesUsed = requestedOrder.BlockList.Keys.ToList();
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private List<TableColumn> RowQuantity(Shape shape, Order requestedOrder)
        {
            var shapeQuantity = requestedOrder.BlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
            return new List<TableColumn> {new("Qty", shapeQuantity)};
        }
    }
}