using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public interface IInvoiceCalculationStrategy
    {
        IEnumerable<LineItem> GenerateLineItems(Order requestedOrder);
    }
}