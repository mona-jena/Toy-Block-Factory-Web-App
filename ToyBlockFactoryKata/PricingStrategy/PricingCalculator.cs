using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.PricingStrategy
{
    public class PricingCalculator : IInvoiceCalculator
    {
        private const decimal RedCost = 1;
        private readonly Dictionary<Shape, decimal> _pricingList;
        private readonly Dictionary<Shape, int> _shapeQuantities = new();

        public PricingCalculator()
        {
            _pricingList = new Dictionary<Shape, decimal>
            {
                {Shape.Square, 1},
                {Shape.Triangle, 2},
                {Shape.Circle, 3}
            };
        }

        public List<LineItem> GenerateLineItems(Dictionary<Block, int> orderBlockList)
        {
            BlockListIterator(orderBlockList);

            var lineItems = new List<LineItem>();
            foreach (var shape in _shapeQuantities)
                lineItems.Add(new LineItem(
                    shape.Key.ToString(),
                    shape.Value,
                    _pricingList[shape.Key],
                    shape.Value * _pricingList[shape.Key])
                );

            var redQuantity = orderBlockList.Where(b => b.Key.Colour == Colour.Red).Sum(b => b.Value);
            if (redQuantity > 0)
                lineItems.Add(new LineItem(
                    "Red colour surcharge",
                    redQuantity,
                    RedCost,
                    redQuantity * RedCost)
                );

            return lineItems;
        }

        
        private void BlockListIterator(Dictionary<Block, int> orderBlockList)
        {
            foreach (var block in orderBlockList) 
                CalculateShapeQuantity(block.Key.Shape, block.Value);
        }

        private void CalculateShapeQuantity(Shape shape, int value)
        {
            if (_shapeQuantities.TryAdd(shape, value)) return;
            _shapeQuantities[shape] += value;
        }
    }
}