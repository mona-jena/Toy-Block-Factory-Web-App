using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("MONA_PORT");
            string[] prefixes = {$"http://*:{port}/"};
            ToyBlockFactory toyBlockFactory = new (new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
            
        }
    }
}