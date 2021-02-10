using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata.Reports
{
    public interface IReport                
    {
        ReportType ReportType { get; init; }
        string Name { get; init; }
        string Address { get; init; }
        DateTime DueDate { get; init; }
        string OrderId { get; init; }
        List<TableRow> OrderTable { get; }      
    }
}