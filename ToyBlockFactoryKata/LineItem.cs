namespace ToyBlockFactoryKata
{
    public record LineItem(string Description, int Quantity, decimal Price, decimal Total)
    {
        //public decimal Total => Quantity * Price;


        private string Print()
        {
            return "ffgdg";
        }
    }
}