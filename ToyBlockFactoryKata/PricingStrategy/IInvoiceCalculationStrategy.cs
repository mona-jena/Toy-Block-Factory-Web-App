using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public interface IInvoiceCalculationStrategy         
    {
        IEnumerable<LineItem> GenerateLineItems(Order requestedOrder);      
    }
}