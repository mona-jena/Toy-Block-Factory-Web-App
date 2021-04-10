using System;
using System.Threading;
using System.Threading.Tasks;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("MONA_PORT") ?? throw new ApplicationException("No port defined!");
            string[] prefixes = {$"http://*:{port}/"};
            CancellationTokenSource source = new();
            CancellationToken token = source.Token;
            ToyBlockFactory toyBlockFactory = new (new LineItemsCalculator());

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(4000);
                source.Cancel();
            });

            var toyServer = new ToyServer(prefixes, token, toyBlockFactory);
            await toyServer.Start();
        }
    }
}