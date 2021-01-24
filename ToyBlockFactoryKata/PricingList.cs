namespace ToyBlockFactoryKata
{
    internal class PricingList : IInvoiceCalculationStrategy
    {
        public int Square { get; }
        public int Triangle { get; }
        public int Circle { get; }

        internal PricingList()
        {
            Square = 1;
            Triangle = 2;
            Circle = 3;
        }


        public int CalculateInvoiceLine(int shapeQuantity, int shapePrice)
        {
            return shapeQuantity * shapePrice;
        }
    }
}