using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;

namespace ToyBlockFactoryConsole
{
    public static class UserInterface
    {
        public static void Menu(ToyBlockFactory toyBlockFactory)
        {
            Console.WriteLine(
                "Would you like to [1] Place an order or [2] Get an existing order [3] Get reports due on a particular date?");
            Console.Write("Please input your choice: ");
            var functionalityOption = int.Parse(Console.ReadLine());
            switch (functionalityOption)
            {
                case 1:
                    PlaceOrder(toyBlockFactory);
                    break;
                case 2:
                    GetOrder(toyBlockFactory);
                    break;
                case 3:
                    GenerateReportsForADate(toyBlockFactory);
                    break;
            }
        }
        
        private static void PlaceOrder(ToyBlockFactory toyBlockFactory)
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

            GetReport(toyBlockFactory, orderId);
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
        
        private static void GetReport(ToyBlockFactory toyBlockFactory, string orderId)
        {
            var option = 1;
            while (option != 4)
            {
                Console.Write(
                    "\nWhich report would you like? \n[1] Invoice Report \n[2] Cutting List Report \n[3] Painting Report \n[4] Back to menu \n[5] Exit Program\n" +
                    "Please enter an option: ");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        var invoiceReport = toyBlockFactory.GetInvoiceReport(orderId);
                        PrintReports.PrintReport(invoiceReport);
                        break;
                    case 2:
                        var cuttingListReport = toyBlockFactory.GetCuttingListReport(orderId);
                        PrintReports.PrintReport(cuttingListReport);
                        break;
                    case 3:
                        var paintingReport = toyBlockFactory.GetPaintingReport(orderId);
                        PrintReports.PrintReport(paintingReport);
                        break;
                    case 4:
                        Menu(toyBlockFactory);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        

        public static void GetOrder(ToyBlockFactory toyBlockFactory)
        {
            Console.WriteLine("What is your Order Id?");
            var orderId = Console.ReadLine();
            var orderExists = toyBlockFactory.OrderExists(orderId);
            if (!orderExists)
            {
                Console.WriteLine("Your order does not exist!");
                return;
            }

            GetReport(toyBlockFactory, orderId);
        }

        
        private static void GenerateReportsForADate(ToyBlockFactory toyBlockFactory)
        {
            var reportOption = 1;
            while (reportOption != 3)
            {
                Console.Write(
                    "\nWhich report would you like? \n[1] Cutting List Report \n[2] Painting Report \n[3] Back to menu \n[4] Exit Program\n" +
                    "Please enter an option: ");
                reportOption = int.Parse(Console.ReadLine());

                switch (reportOption)
                {
                    case 1:
                        var cuttingReportDate = GetDateInput();
                        var filteredCuttingLists = toyBlockFactory.GetCuttingListsByDate(cuttingReportDate);
                        foreach (var cuttingList in filteredCuttingLists) 
                            PrintReports.PrintReport(cuttingList);
                        break;

                    case 2:
                        var paintingReportDate = GetDateInput();
                        var filteredPaintingReports = toyBlockFactory.GetPaintingReportsByDate(paintingReportDate);
                        foreach (var paintingReport in filteredPaintingReports)
                            PrintReports.PrintReport(paintingReport);
                        break;

                    case 3:
                        Menu(toyBlockFactory);
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static DateTime GetDateInput()
        {
            Console.Write("Which date would you like to filter for: ");
            var userSpecifiedDate = Console.ReadLine();
            DateTime filterDate = default;
            if (!string.IsNullOrEmpty(userSpecifiedDate))
                filterDate = InputValidator.ConvertToDateTime(userSpecifiedDate);

            return filterDate;
        }

        
    }
}