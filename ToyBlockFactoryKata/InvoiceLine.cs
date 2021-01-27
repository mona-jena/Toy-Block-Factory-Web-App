namespace ToyBlockFactoryKata
{
    public record InvoiceLine(string Description, int Quantity, int Price)
    {
        public int Total => Quantity * Price;
        

        string Print()
        {
            return "ffgdg";
        }
    }
}