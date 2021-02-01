using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    internal interface IInvoiceCalculationStrategy
    {
        int Square { get; }
        int Triangle { get; }
        int Circle { get; }
        int Red { get; }
        int CalculateInvoiceLine(int quantity, int shapePrice);
        int GetPrice(string chargedItem);
        void AddLineItems(Report report, Order requestedOrder);
    }
}