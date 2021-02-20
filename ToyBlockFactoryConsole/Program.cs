using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryConsole
{
    internal static class Program
    {
        internal static void Main()
        {
            Console.WriteLine("Welcome to the Toy Block Factory!\n");
            
            var toyBlockFactory = new ToyBlockFactory(new PricingCalculator());

            UserInterface.Menu(toyBlockFactory);
        }
    }
}