using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    public class PricingCalculator : IInvoiceCalculationStrategy
    {
        private Order _requestedOrder;
        private readonly Dictionary<Shape, decimal> _pricingList = new();               //condense??
        private readonly Dictionary<string, decimal> _surchargePricingList = new();
        private readonly Dictionary<Shape, int> _shapeQuantities = new();
        private readonly Dictionary<string, int> _surchargeQuantities = new();

        public PricingCalculator()
        {
            _pricingList.Add(Shape.Square, 1);          //what's the purpose of storing as shape, when we convert it to string at the end?
            _pricingList.Add(Shape.Triangle, 2);
            _pricingList.Add(Shape.Circle, 3);
            _surchargePricingList.Add("Red", 1);        // too soon to assume its not always Colour?
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

            foreach (var surcharge in _surchargeQuantities)
            {
                lineItems.Add(new LineItem(
                    surcharge.Key + " colour surcharge",
                    surcharge.Value,
                    _surchargePricingList[surcharge.Key],
                    surcharge.Value * _surchargePricingList[surcharge.Key])
                );
            }
            return lineItems;
        }
        
        private void BlockListIterator()
        {
            foreach (var block in _requestedOrder.BlockList)
            {
                CalculateShapeQuantity(block);
                CalculateSurchargeQuantity(block);
            }
        }

        private void CalculateShapeQuantity(KeyValuePair<Block, int> block)
        {
            var shape = block.Key.Shape;
            
            if (_shapeQuantities.ContainsKey(shape))
                _shapeQuantities[shape] += block.Value;
            else 
                _shapeQuantities.Add(shape, block.Value);
        }
        

        private void CalculateSurchargeQuantity(KeyValuePair<Block, int> block)
        {
            if (block.Key.Colour != Colour.Red) return;
            
            var surchargeItem = block.Key.Colour.ToString();
            if (_surchargeQuantities.ContainsKey(surchargeItem))
                _surchargeQuantities[surchargeItem] += block.Value;
            else
                _surchargeQuantities.Add(surchargeItem, block.Value);
        }
        
    }
}