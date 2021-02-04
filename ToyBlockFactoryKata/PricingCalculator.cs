using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    public class PricingCalculator : IInvoiceCalculationStrategy
    {
        private Order _requestedOrder;
        private readonly Dictionary<Shape, decimal> _pricingList;          
        private readonly Dictionary<Shape, int> _shapeQuantities = new();
        private const decimal RedCost = 1;

        public PricingCalculator()
        {
            _pricingList = new Dictionary<Shape, decimal>
            {
                {Shape.Square, 1}, 
                {Shape.Triangle, 2}, 
                {Shape.Circle, 3}
            };
        }
        
        public List<LineItem> GenerateLineItems(Order requestedOrder)
        {
            _requestedOrder = requestedOrder;
            BlockListIterator();
            
            var lineItems = new List<LineItem>();
            foreach (var shape in _shapeQuantities)
            {
                lineItems.Add(new LineItem(
                    shape.Key.ToString(),
                    shape.Value,
                    _pricingList[shape.Key],
                    shape.Value * _pricingList[shape.Key])
                );
            }
            
            var redQuantity = requestedOrder.BlockList.Where(b => b.Key.Colour == Colour.Red).Sum(b => b.Value);
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
        
        private void BlockListIterator()
        {
            foreach (var block in _requestedOrder.BlockList)
            {
                CalculateShapeQuantity(block.Key.Shape, block.Value);
            }
        }
                                                                                //condense these??
        private void CalculateShapeQuantity(Shape shape, int value)
        {
            foreach (var block in _requestedOrder.BlockList)
            {
                if (_shapeQuantities.TryAdd(shape, value)) return;
                _shapeQuantities[shape] += value;
            }
        }

    }
}