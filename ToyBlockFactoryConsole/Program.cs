using System;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Toy Block Factory!\n");
            
            var toyBlockFactory = new ToyBlockFactory();
            
            Console.WriteLine("Would you like to [1] Place an order or [2] Get reports due on a particular date?");
            Console.Write("Please input your choice: ");
            var functionalityOption = int.Parse(Console.ReadLine());
            switch (functionalityOption)
            {
                case 1:
                    UserInterface.PlaceOrder(toyBlockFactory);
                    break;
                case 2:
                    UserInterface.GenerateReportsForADate(toyBlockFactory);
                    break;
            }
        }
    }

    
}