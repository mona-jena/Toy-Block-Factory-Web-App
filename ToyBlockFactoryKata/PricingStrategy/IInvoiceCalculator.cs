using System.Collections.Generic;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public interface IInvoiceCalculator         
    {
        List<LineItem> GenerateLineItems(Dictionary<Block, int> orderBlockList);      
    }
}