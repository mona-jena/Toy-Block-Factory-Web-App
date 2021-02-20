using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public interface IInvoiceCalculator         
    {
        IEnumerable<LineItem> GenerateLineItems(Dictionary<Block, int> orderBlockList);      
    }
}