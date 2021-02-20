using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryTests.TestDoubles
{
    public class TestPricingCalculator : IInvoiceCalculator
    {
        
        public IEnumerable<LineItem> GenerateLineItems(Dictionary<Block, int> orderBlockList)
        {
            List<LineItem> lineItems = new();
            
            switch (orderBlockList.Count)
            {
                case 3:
                    lineItems.Add((new LineItem("Square", 2, 1, 2)));
                    lineItems.Add((new LineItem("Circle", 2, 3, 6)));
                    break;
                case 5:
                    lineItems.Add((new LineItem("Square", 2, 1, 2)));
                    lineItems.Add((new LineItem("Triangle", 2, 2, 4)));
                    lineItems.Add((new LineItem("Circle", 3, 3, 9)));
                    lineItems.Add((new LineItem("Red colour surcharge", 1, 1, 1)));
                    break;
            }

            return lineItems;
        }
        
    }
}