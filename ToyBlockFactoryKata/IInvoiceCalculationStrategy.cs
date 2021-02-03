using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public interface IInvoiceCalculationStrategy
    {
        List<LineItem> AddLineItems(InvoiceReport report, Order requestedOrder);
    }
}