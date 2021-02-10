using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public interface IInvoiceCalculationStrategy            //why use this interface??
    {
        IEnumerable<LineItem> GenerateLineItems(Order requestedOrder);      
    }
}