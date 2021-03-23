using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = {"http://*:3000/"};
            ToyBlockFactory toyBlockFactory = new (new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
        }
    }
}