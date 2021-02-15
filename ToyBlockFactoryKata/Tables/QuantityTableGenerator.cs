using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.Tables
{
    internal class QuantityTableGenerator : ITableGenerator
    {

        public IEnumerable<TableRow> GenerateTable(IReport report, Dictionary<Block, int> orderBlockList)
        {
            List<TableRow> table = new List<TableRow>();
            foreach (var shape in ShapesUsedInOrder(orderBlockList))
                table.Add(new TableRow(shape, RowQuantity(shape, orderBlockList)));

            return table;
        }
        
        private IEnumerable<Shape> ShapesUsedInOrder(Dictionary<Block, int> orderBlockList)
        {
            var shapesUsed = orderBlockList.Keys.ToList();
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private List<TableColumn> RowQuantity(Shape shape, Dictionary<Block, int> orderBlockList)
        {
            var shapeQuantity = orderBlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
            return new List<TableColumn> {new("Qty", shapeQuantity)};
        }
    }
}