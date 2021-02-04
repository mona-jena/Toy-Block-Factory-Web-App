using System.Collections.Generic;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryKataTests
{
    public class TestPricingCalculator : IInvoiceCalculationStrategy
    {
        public TestPricingCalculator()
        {
            
        }

        

        public List<LineItem> GenerateLineItems(Order requestedOrder)
        {
            throw new System.NotImplementedException();
        }
    }
}