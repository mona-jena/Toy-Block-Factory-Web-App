using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata.Reports
{
    public record Report : IReport
    {
        public ReportType ReportType { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public DateTime DueDate { get; init; }
        public string OrderId { get; init; }
        public List<TableRow> OrderTable { get; init; } = new();
    }
}