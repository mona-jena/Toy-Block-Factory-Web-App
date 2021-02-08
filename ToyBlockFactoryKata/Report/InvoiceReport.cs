using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    public record InvoiceReport : Report
    {
        public List<LineItem> LineItems { get; } = new();

        public decimal Total => LineItems.Sum(item => item.Total);
    }
}