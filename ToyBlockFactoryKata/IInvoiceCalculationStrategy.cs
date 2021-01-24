namespace ToyBlockFactoryKata
{
    internal interface IInvoiceCalculationStrategy
    {
        int Square { get; }
        int Triangle { get; }
        int Circle { get; }
        
        int CalculateInvoiceLine(int shapeQuantity, int shapePrice);
    }
}