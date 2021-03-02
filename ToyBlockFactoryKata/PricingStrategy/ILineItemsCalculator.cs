using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public interface ILineItemsCalculator         
    {
        List<LineItem> GenerateLineItems(Order requestedOrder);      
    }
}