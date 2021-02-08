using System;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    internal static class Program
    {
        internal static void Main()
        {
            Console.WriteLine("Welcome to the Toy Block Factory!\n");

            var toyBlockFactory = new ToyBlockFactory();

            Console.WriteLine(
                "Would you like to [1] Place an order or [2] Get an existing order [3] Get reports due on a particular date?");
            Console.Write("Please input your choice: ");
            var functionalityOption = int.Parse(Console.ReadLine());
            switch (functionalityOption)
            {
                case 1:
                    UserInterface.PlaceOrder(toyBlockFactory);
                    break;
                case 2:
                    UserInterface.GetOrder(toyBlockFactory);
                    break;
                case 3:
                    UserInterface.GenerateReportsForADate(toyBlockFactory);
                    break;
            }
        }
    }
}