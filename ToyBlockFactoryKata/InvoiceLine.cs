namespace ToyBlockFactoryKata
{
    public record InvoiceLine(string Description, int Quantity, int Price)
    {
        public decimal Total => Quantity * Price;


        private string Print()
        {
            return "ffgdg";
        }
    }
}