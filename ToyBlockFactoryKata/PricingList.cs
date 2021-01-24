namespace ToyBlockFactoryKata
{
    internal class PricingList : IInvoiceCalculationStrategy
    {
        public int Square { get; }
        public int Triangle { get; }
        public int Circle { get; }
        public int Red { get; }

        internal PricingList()
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
    }
}