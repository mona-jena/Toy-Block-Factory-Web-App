using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.Table
{
    internal interface ITableGenerator
    {
        IEnumerable<TableRow> GenerateTable(IReport report, Dictionary<Block, int> orderBlockList);
    }
}