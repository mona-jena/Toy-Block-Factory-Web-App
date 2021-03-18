using System;
using System.Net;
using System.Text.Json;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = {"http://*:3000/"};
            ToyBlockFactory toyBlockFactory = new (new LineItemsCalculator());
            var server = new ToyServer(prefixes, toyBlockFactory);
        }
    }
}