using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;

namespace ToyBlockFactoryConsole
{
    public static class OrderInput
    {
        internal static void PlaceOrder(ToyBlockFactory toyBlockFactory)
        {
            Console.Write("Please input your Name: ");
            var name = Console.ReadLine();
            name = InputValidator.AskUserWhileEmpty(name);

            Console.Write("Please input your Address: ");
            var address = Console.ReadLine();
            address = InputValidator.AskUserWhileEmpty(address);

            Console.Write("Please input your Due Date: ");
            var dateInput = Console.ReadLine();
            DateTime dueDate = default;
            if (!string.IsNullOrEmpty(dateInput))
                dueDate = InputValidator.ConvertToDateTime(dateInput);


            Order order;
            if (dueDate != default) order = toyBlockFactory.CreateOrder(name, address);
            order = toyBlockFactory.CreateOrder(name, address, dueDate);

            Console.WriteLine();

            var orderId = EnterBlockOrder(toyBlockFactory, order);

            while (orderId == string.Empty)
            {
                Console.WriteLine("\nYou need to included blocks to create an order. Please try again.");
                orderId = EnterBlockOrder(toyBlockFactory, order);
            }

            UserInterface.GetReport(toyBlockFactory, orderId);
        }

        private static string EnterBlockOrder(ToyBlockFactory toyBlockFactory, Order order)
        {
            Console.Write("Please input the number of Red Squares: ");
            var redSquareInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(redSquareInput); i++)
                order.AddBlock(Shape.Square, Colour.Red);

            Console.Write("Please input the number of Blue Squares: ");
            var blueSquareInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(blueSquareInput); i++)
                order.AddBlock(Shape.Square, Colour.Blue);

            Console.Write("Please input the number of Yellow Squares: ");
            var yellowSquareInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(yellowSquareInput); i++)
                order.AddBlock(Shape.Square, Colour.Yellow);

            Console.WriteLine();

            Console.Write("Please input the number of Red Triangles: ");
            var redTriangleInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(redTriangleInput); i++)
                order.AddBlock(Shape.Triangle, Colour.Red);

            Console.Write("Please input the number of Blue Triangles: ");
            var blueTriangleInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(blueTriangleInput); i++)
                order.AddBlock(Shape.Triangle, Colour.Blue);

            Console.Write("Please input the number of Yellow Triangles: ");
            var yellowTriangleInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(yellowTriangleInput); i++)
                order.AddBlock(Shape.Triangle, Colour.Yellow);

            Console.WriteLine();

            Console.Write("Please input the number of Red Circles: ");
            var redCircleInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(redCircleInput); i++)
                order.AddBlock(Shape.Circle, Colour.Red);

            Console.Write("Please input the number of Blue Circles: ");
            var blueCircleInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(blueCircleInput); i++)
                order.AddBlock(Shape.Circle, Colour.Blue);

            Console.Write("Please input the number of Yellow Circles: ");
            var yellowCircleInput = Console.ReadLine();
            for (var i = 0; i < BlockQuantity(yellowCircleInput); i++)
                order.AddBlock(Shape.Circle, Colour.Yellow);

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