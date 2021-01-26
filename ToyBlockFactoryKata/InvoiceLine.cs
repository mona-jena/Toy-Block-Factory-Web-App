namespace ToyBlockFactoryKata
{
    public class InvoiceLine
    {
        public InvoiceLine(string description, int quantity, int price)
        {
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public string Description { get; }
        public int Quantity { get; }
        public int Price { get; }
        public int Total => Quantity * Price;
        

        string Print()
        {
            return "ffgdg";
        }
    }
}