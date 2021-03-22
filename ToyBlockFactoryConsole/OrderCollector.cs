using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;

namespace ToyBlockFactoryConsole
{
    public static class OrderCollector
    {
        internal static void PlaceOrder(ToyBlockFactory toyBlockFactory)
        {
            Console.Write("\nPlease input your Name: ");
            var name = Console.ReadLine();
            name = CheckWhileEmpty(name);

            Console.Write("Please input your Address: ");
            var address = Console.ReadLine();
            address = CheckWhileEmpty(address);

            Console.Write("Please input your Due Date: ");
            var dateInput = Console.ReadLine();
            DateTime dueDate = DateTime.Today;
            if (!string.IsNullOrEmpty(dateInput))
                dueDate = InputValidator.ConvertToDateTime(dateInput);
            
            /*Order order;
            if (dueDate >= DateTime.Today)
            {
                order = toyBlockFactory.CreateOrder(name, address, dueDate);
            }
            else
            {
                order = toyBlockFactory.CreateOrder(name, address);
            }

            Console.WriteLine();

            var orderId = EnterBlockOrder(toyBlockFactory, order);
            while (orderId == string.Empty)
            {
                Console.WriteLine("\nYou need to add blocks to create an order. Please try again.");
                orderId = EnterBlockOrder(toyBlockFactory, order);
            }

            UserInterface.GetReport(toyBlockFactory, orderId);*/
        }

        private static string CheckWhileEmpty(string input)
        {
            while (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("This field can't be empty! Please enter again: ");
                Console.ResetColor();

                input = Console.ReadLine();
            }

            return input;
        }

        private static string EnterBlockOrder(ToyBlockFactory toyBlockFactory, Order order)
        {
            foreach (var shape in Enum.GetValues(typeof(Shape)))
            {
                foreach (var colour in Enum.GetValues(typeof(Colour)))
                {
                    Console.Write("Please input the number of " + colour + " " + shape + ": ");
                    var input = Console.ReadLine();
                    for (var i = 0; i < BlockQuantity(input); i++)
                        order.AddBlock((Shape)shape, (Colour)colour, 1);
                }
                Console.WriteLine();
            }
            
            return toyBlockFactory.SubmitOrder(order);
        }

        private static int BlockQuantity(string input)
        {
            var block = 0;
            if (!string.IsNullOrEmpty(input))
                block = InputValidator.IfValidInteger(input);
            return block;
        }
    }
}