using System;
using System.Threading.Tasks;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("MONA_PORT");
            Console.WriteLine(port);
            string[] prefixes = {$"http://*:{port}/"};
            ToyBlockFactory toyBlockFactory = new (new LineItemsCalculator());
            var toyServer = new ToyServer(prefixes, toyBlockFactory);
            await toyServer.Start();
        }
    }
}