using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    public record InvoiceReport : Report
    {
        public List<LineItem> LineItems { get; } = new(); //change invoice line to generic name
        
        public decimal Total => LineItems.Sum(item => item.Total);
        
    }
}