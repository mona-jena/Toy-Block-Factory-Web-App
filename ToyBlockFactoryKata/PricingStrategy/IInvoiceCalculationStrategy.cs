using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public interface IInvoiceCalculationStrategy            //why use this interface??
    {
        IEnumerable<LineItem> GenerateLineItems(Order requestedOrder);      
    }
}