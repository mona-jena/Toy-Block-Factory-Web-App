using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.Tables
{
    internal class ColourTableGenerator : ITableGenerator
    {
        public IEnumerable<TableRow> GenerateTable(IReport report, Dictionary<Block, int> orderBlockList)
        {
            List<TableRow> table = new List<TableRow>();
            foreach (var shape in ShapesUsedInOrder(orderBlockList))
                table.Add(new TableRow(shape, RowItems(shape, orderBlockList)));

            return table;
        }

        private IEnumerable<Shape> ShapesUsedInOrder(Dictionary<Block, int> orderBlockList)
        {
            var shapesUsed = orderBlockList.Keys.ToList();
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private List<TableColumn> RowItems(Shape shape, Dictionary<Block, int> orderBlockList)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (var colour in orderBlockList.Select(b => b.Key.Colour).Distinct())
            {
                var block = new Block(shape, colour);
                if (orderBlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), orderBlockList[block]));
                else
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), 0));
            }

            return rowItemQuantities;
        }

        
    }

    
}