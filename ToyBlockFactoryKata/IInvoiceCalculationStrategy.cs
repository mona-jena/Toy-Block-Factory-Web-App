namespace ToyBlockFactoryKata
{
    internal interface IInvoiceCalculationStrategy
    {
        int Square { get; }
        int Triangle { get; }
        int Circle { get; }
        int Red { get; }

        int CalculateInvoiceLine(int quantity, int shapePrice);
    }
}