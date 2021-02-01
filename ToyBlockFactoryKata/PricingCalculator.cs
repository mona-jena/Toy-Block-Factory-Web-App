namespace ToyBlockFactoryKata
{
    internal class PricingCalculator : IInvoiceCalculationStrategy
    {
        
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
        
    }
}