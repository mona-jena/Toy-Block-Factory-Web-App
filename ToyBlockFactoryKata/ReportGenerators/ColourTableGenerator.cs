using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class ColourTableGenerator : ITableGenerator
    {
        public IEnumerable<TableRow> GenerateTable(IReport report, Order requestedOrder)
        {
            List<TableRow> table = new List<TableRow>();
            foreach (var shape in ShapesUsedInOrder(requestedOrder))
                report.OrderTable.Add(new TableRow(shape, RowItems(shape, requestedOrder)));

            return table;
        }

        private IEnumerable<Shape> ShapesUsedInOrder(Order requestedOrder)
        {
            var shapesUsed = requestedOrder.BlockList.Keys.ToList();
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private List<TableColumn> RowItems(Shape shape, Order requestedOrder)
        {
            var rowItemQuantities = new List<TableColumn>();
            foreach (var colour in requestedOrder.BlockList.Select(b => b.Key.Colour).Distinct())
            {
                var block = new Block(shape, colour);
                if (requestedOrder.BlockList.ContainsKey(block))
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), requestedOrder.BlockList[block]));
                else
                    rowItemQuantities.Add(new TableColumn(colour.ToString(), 0));
            }

            return rowItemQuantities;
        }

       
    }

    
}