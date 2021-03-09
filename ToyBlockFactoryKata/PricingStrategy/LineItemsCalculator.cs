using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public class LineItemsCalculator : ILineItemsCalculator
    {
        private const decimal RedCost = 1;

        private readonly Dictionary<Shape, decimal> _pricingList = new()
        {
            {Shape.Square, 1},
            {Shape.Triangle, 2},
            {Shape.Circle, 3}
        };
        

        public LineItemsCalculator()
        {
            // _pricingList = new Dictionary<Shape, decimal>
            // {
            //     {Shape.Square, 1},
            //     {Shape.Triangle, 2},
            //     {Shape.Circle, 3}
            // };
        }

        public List<LineItem> GenerateLineItems(Order order)
        {
            var lineItems = new List<LineItem>();
            foreach (var shape in order.shapeQuantities)
                lineItems.Add(new LineItem(
                    shape.Key.ToString(),
                    shape.Value,
                    _pricingList[shape.Key],
                    shape.Value * _pricingList[shape.Key])
                );

            var redQuantity = order.BlockList.Where(b => b.Key.Colour == Colour.Red).Sum(b => b.Value);
            if (redQuantity > 0)
                lineItems.Add(new LineItem(
                    "Red colour surcharge",
                    redQuantity,
                    RedCost,
                    redQuantity * RedCost)
                );

            return lineItems;
        }
       
    }
}