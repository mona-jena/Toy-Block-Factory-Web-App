using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata.Reports
{
    public record InvoiceReport : Report
    {
        public List<LineItem> LineItems { get; init; } = new();
        public decimal Total { get; init; }
    }
}