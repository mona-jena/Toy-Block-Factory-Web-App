using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    internal class PricingCalculator : IInvoiceCalculationStrategy
    {
        private Report _report;
        private Order _requestedOrder;

        public int Square { get; }
        public int Triangle { get; }
        public int Circle { get; }
        public int Red { get; }
        
        internal PricingCalculator()
        {
            Square = 1;
            Triangle = 2;
            Circle = 3;
            Red = 1;
        }

        //USE THIS INSTEAD OF THE ONE IN INVOICE LINE
        public int CalculateInvoiceLine(int quantity, int shapePrice) 
        {
            return quantity * shapePrice;
        }

        public int GetPrice(string chargedItem)
        {
            switch (chargedItem)
            {
                case "Square":
                    return Square;
                case "Triangle":
                    return Triangle;
                case "Circle":
                    return Circle;
                case "Red":
                    return Red;
                default:
                    return 0;                            //is this ok?
            }
        }

        
        /*public int GetPrice(Block block)
        {
            switch (block)
            {
                case "Square":
                    return Square;
                case "Triangle":
                    return Triangle;
                case "Circle":
                    return Circle;
                case "Red":
                    return Red;
                default:
                    return 0;                            //is this ok?
            }
        }*/
        
        
        public void AddLineItems(Report report, Order requestedOrder)
        {
            _report = report;
            _requestedOrder = requestedOrder;
            foreach (var shape in GetShapesUsedInOrder())
            {
                var shapeQuantity = CalculateItemQuantity(shape);
                var shapePrice = GetPrice(shape.ToString());
                _report.LineItems.Add(new InvoiceLine(
                    shape.ToString(),
                    shapeQuantity,
                    shapePrice
                ));
            }

            foreach (var colour in GetSurchargeItems()) //item or colour??
            {
                var itemQuantity = CalculateItemQuantity(colour);
                var colourPrice = GetPrice(colour.ToString());
                _report.LineItems.Add(new InvoiceLine(
                    colour + " colour surcharge",
                    itemQuantity,
                    colourPrice
                ));
            }
        }

        private IEnumerable<Shape> GetShapesUsedInOrder()
        {
            var shapesUsed = _requestedOrder.BlockList.Keys;
            return shapesUsed.Select(item => item.Shape).Distinct();
        }

        private IEnumerable<Colour> GetSurchargeItems()
        {
            var coloursUsed = _requestedOrder.BlockList.Keys;
            return coloursUsed.Select(item => item.Colour).Where(item => item == Colour.Red).Distinct();
            
        }

        private int CalculateItemQuantity(Shape shape)
        {
            return _requestedOrder.BlockList.Where(b => b.Key.Shape == shape).Sum(b => b.Value);
        }

        private int CalculateItemQuantity(Colour colour)
        {
            return _requestedOrder.BlockList.Where(b => b.Key.Colour == colour).Sum(b => b.Value);
        }
        
        
    }
}