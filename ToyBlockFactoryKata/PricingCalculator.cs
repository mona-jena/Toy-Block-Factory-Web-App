using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    public class PricingCalculator : IInvoiceCalculationStrategy
    {
        private Order _requestedOrder;
        private readonly Dictionary<Shape, decimal> _pricingList = new();               //condense??
        private readonly Dictionary<Shape, int> _shapeQuantities = new();
        private const decimal _redCost = 1;

        public PricingCalculator()
        {
            _pricingList.Add(Shape.Square, 1);          //what's the purpose of storing as shape, when we convert it to string at the end?
            _pricingList.Add(Shape.Triangle, 2);
            _pricingList.Add(Shape.Circle, 3);
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
            
            
            var _redQuantity = requestedOrder.BlockList.Where(b => b.Key.Colour == Colour.Red).Sum(b => b.Value);
            if(_redQuantity >= 1)
                lineItems.Add(new LineItem(
                    "Red colour surcharge",
                    _redQuantity,
                    _redCost,
                    _redQuantity * _redCost)
                );
            
            return lineItems;
        }
        
        private void BlockListIterator()
        {
            foreach (var block in _requestedOrder.BlockList)
            {
                CalculateShapeQuantity(block.Key.Shape, block.Value);
                
            }
        }

        private void CalculateShapeQuantity(Shape shape, int value)
        {
            if (_shapeQuantities.TryAdd(shape, value)) return;
            _shapeQuantities[shape] += value;
        }

    }
}