using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public interface IInvoiceCalculationStrategy
    {
        List<LineItem> GenerateLineItems(InvoiceReport report, Order requestedOrder);
    }
}