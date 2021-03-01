using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.Tables
{
    internal interface ITableFactory
    {
        List<TableRow> GenerateTable(Dictionary<Block, int> orderBlockList);
    }
}