using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public class LineItemsCalculator : ILineItemsCalculator
    {
        private const decimal RedCost = 1;
        private readonly List<(Shape shape, decimal price)> _pricingList = new()
        {
            (Shape.Square, 1),
            (Shape.Triangle, 2),
            (Shape.Circle, 3)
        };

        public List<LineItem> GenerateLineItems(Order order)
        {
            var lineItems = new List<LineItem>();
            foreach (var (shape, quantity) in order.shapeQuantities)
            {
                lineItems.Add(new LineItem(
                    shape.ToString(),
                    quantity,
                    _pricingList.First(p => p.shape == shape).price,
                    quantity * _pricingList.First(p => p.shape == shape).price)
                ); 
            }
               

            var redQuantity = order.BlockList.Where(b => b.Key.Colour == Colour.Red).Sum(b => b.Value);
            if (redQuantity > 0)
            {
                lineItems.Add(new LineItem(
                    "Red colour surcharge",
                    redQuantity,
                    RedCost,
                    redQuantity * RedCost)
                );
            }

            return lineItems;
        }
       
    }
}