using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class PricingCalculator : IInvoiceCalculationStrategy
    {

        private readonly Dictionary<string, int> _chargedItemQuantities = new();
        private readonly Dictionary<string, decimal> _chargedItemPrice = new();

        private InvoiceReport _report;
        private Order _requestedOrder;
        private decimal Square { get; }
        private decimal Triangle { get; }
        private decimal Circle { get; }
        private decimal Red { get; }

        public PricingCalculator()
        {
            Square = 1;
            Triangle = 2;
            Circle = 3;
            Red = 1;
        }
        
        public List<LineItem> AddLineItems(InvoiceReport report, Order requestedOrder)
        {
            _report = report;
            _requestedOrder = requestedOrder;
            BlockListIterator();
            
            var lineItems = new List<LineItem>();
            foreach (var chargedItem in _chargedItemQuantities)
            {
                lineItems.Add(new LineItem(
                    chargedItem.Key,
                    chargedItem.Value,
                    _chargedItemPrice[chargedItem.Key],
                    CalculateInvoiceLine(chargedItem.Value, _chargedItemPrice[chargedItem.Key])
                ));
            }

            return lineItems;
        }
        
        private void BlockListIterator()
        {
            foreach (var block in _requestedOrder.BlockList)
            {
                HandleShapes(block);
                HandleSurcharges(block);
                GetPrice(block.Key);
            }
        }

        private void HandleShapes(KeyValuePair<Block, int> block)
        {
            var shape = block.Key.Shape.ToString();
            
            if (_chargedItemQuantities.TryGetValue(shape, out var quantity))
                _chargedItemQuantities[shape] = ++quantity;
            else 
                _chargedItemQuantities.Add(shape, 1);

            var price = GetPrice(block.Key);
            if (_chargedItemPrice.ContainsKey(shape))
                _chargedItemPrice[shape] += price;
            else
            {
                _chargedItemPrice.Add(shape, price);
            }
        }

        private void HandleSurcharges(KeyValuePair<Block, int> block)
        {
            if (block.Key.Colour != Colour.Red) return;
            
            var surchargeItem = block.Key.Colour.ToString();
            if (_chargedItemQuantities.TryGetValue(surchargeItem, out var quantity))
                _chargedItemQuantities[surchargeItem] = ++quantity;
            else
                _chargedItemQuantities.Add(surchargeItem, 1);
            
            var price = GetPrice(block.Key);
            if (_chargedItemPrice.ContainsKey(surchargeItem))
                _chargedItemPrice[surchargeItem] += price;
            else
            {
                _chargedItemPrice.Add(surchargeItem, price);
            }
        }
        
        private decimal GetPrice(Block block) //eg (Square, Red) 
        {
            var price = 0m; //is this how you initialise decimal?
            switch (block.Shape) //are switch stat bad?
            {
                case Shape.Square:
                {
                    price += Square;
                    if (block.Colour == Colour.Red) //should this be in a surcharge method?
                        price += Red;
                    break;
                }
                case Shape.Triangle:
                {
                    price += Triangle;
                    if (block.Colour == Colour.Red)
                        price += Red;
                    break;
                }
                case Shape.Circle:
                {
                    price += Circle;
                    if (block.Colour == Colour.Red)
                        price += Red;
                    break;
                }
            }

            return price;
        }
        
        
        private decimal CalculateInvoiceLine(int quantity, decimal shapePrice)
        {
            return quantity * shapePrice;
        }


        
    }
}