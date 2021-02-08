using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    public static class UserInterface
    {
        public static void PlaceOrder(ToyBlockFactory toyBlockFactory)
        {
            Console.Write("Please input your Name: ");
            var name = Console.ReadLine();
            name = InputValidator.IfEmptyInput(name);

            Console.Write("Please input your Address: ");
            var address = Console.ReadLine();
            address = InputValidator.IfEmptyInput(address);

            Console.Write("Please input your Due Date: ");
            var dateInput = Console.ReadLine();
            DateTime dueDate = default;
            if (!string.IsNullOrEmpty(dateInput))
                dueDate = InputValidator.ConvertToDateTime(dateInput);
            
            
            Order order;
            if (dueDate != default)
            {
                order = toyBlockFactory.CreateOrder(name, address);
            }
            order = toyBlockFactory.CreateOrder(name, address, dueDate);

            Console.WriteLine();

            Console.Write("Please input the number of Red Squares: ");
            var redSquareInput = Console.ReadLine();
            int redSquares = 0;
            if (!string.IsNullOrEmpty(redSquareInput))
                redSquares = InputValidator.IfValidInteger(redSquareInput);
            for(var i = 0; i < redSquares; i++)
            {
                order.AddBlock(Shape.Square, Colour.Red);
            }

            Console.Write("Please input the number of Blue Squares: ");
            var blueSquareInput = Console.ReadLine();
            int blueSquares = 0;
            if (!string.IsNullOrEmpty(blueSquareInput))
                blueSquares = InputValidator.IfValidInteger(blueSquareInput);
            for(var i = 0; i < blueSquares; i++)
            {
                order.AddBlock(Shape.Square, Colour.Blue);
            }

            Console.Write("Please input the number of Yellow Squares: ");
            var yellowSquareInput = Console.ReadLine();
            int yellowSquares = 0;
            if (!string.IsNullOrEmpty(yellowSquareInput))
                yellowSquares = InputValidator.IfValidInteger(yellowSquareInput);
            for(var i = 0; i < yellowSquares; i++)
            {
                order.AddBlock(Shape.Square, Colour.Yellow);
            }

            Console.WriteLine();

            Console.Write("Please input the number of Red Triangles: ");
            var redTriangleInput = Console.ReadLine();
            int redTriangles = 0;
            if (!string.IsNullOrEmpty(redTriangleInput))
                redTriangles = InputValidator.IfValidInteger(redTriangleInput);
            for(var i = 0; i < redTriangles; i++)
            {
                order.AddBlock(Shape.Triangle, Colour.Red);
            }

            Console.Write("Please input the number of Blue Triangles : ");
            var blueTriangleInput = Console.ReadLine();
            int blueTriangles = 0;
            if (!string.IsNullOrEmpty(blueTriangleInput))
                blueTriangles = InputValidator.IfValidInteger(blueTriangleInput);
            for(var i = 0; i < blueTriangles; i++)
            {
                order.AddBlock(Shape.Triangle, Colour.Blue);
            }

            Console.Write("Please input the number of Yellow Triangles: ");
            var yellowTriangleInput = Console.ReadLine();
            int yellowTriangles = 0;
            if (!string.IsNullOrEmpty(yellowTriangleInput))
                yellowTriangles = InputValidator.IfValidInteger(yellowTriangleInput);
            for(var i = 0; i < yellowTriangles; i++)
            {
                order.AddBlock(Shape.Triangle, Colour.Yellow);
            }

            Console.WriteLine();

            Console.Write("Please input the number of Red Circles: ");
            var redCircleInput = Console.ReadLine();
            int redCircles = 0;
            if (!string.IsNullOrEmpty(redCircleInput))
                redCircles = InputValidator.IfValidInteger(redCircleInput);
            for(var i = 0; i < redCircles; i++)
            {
                order.AddBlock(Shape.Circle, Colour.Red);
            }

            Console.Write("Please input the number of Blue Circles: ");
            var blueCircleInput = Console.ReadLine();
            int blueCircles = 0;
            if (!string.IsNullOrEmpty(blueCircleInput))
                blueCircles = InputValidator.IfValidInteger(blueCircleInput);
            for(var i = 0; i < blueCircles; i++)
            {
                order.AddBlock(Shape.Circle, Colour.Blue);
            }

            Console.Write("Please input the number of Yellow Circles: ");
            var yellowCircleInput = Console.ReadLine();
            int yellowCircles = 0;
            if (!string.IsNullOrEmpty(yellowCircleInput))
                yellowCircles = InputValidator.IfValidInteger(yellowCircleInput);
            for(var i = 0; i < yellowCircles; i++)
            {
                order.AddBlock(Shape.Circle, Colour.Yellow);
            }
            
            
            var orderId = toyBlockFactory.SubmitOrder(order);


            var option = 1;
            while (option != 4)
            {
                Console.Write(
                    "\nWhich report would you like? \n[1] Invoice Report \n[2] Cutting List Report \n[3] Painting Report \n[4] Exit Program \n" +
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
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void GetOrder(ToyBlockFactory toyBlockFactory)
        {
            Console.WriteLine("What is your Order Id?");
            var orderId = Console.ReadLine();
            var order = toyBlockFactory.GetOrder(orderId);
            //FINISH LOGIC!!!
            
        }

        public static void GenerateReportsForADate(ToyBlockFactory toyBlockFactory)
        {
            var reportOption = 1;
            while (reportOption != 3)
            {
                Console.Write(
                    "\nWhich report would you like? \n[1] Cutting List Report \n[2] Painting Report \n[3] Exit Program\n" +
                    "Please enter an option: ");
                reportOption = int.Parse(Console.ReadLine());

                switch (reportOption)
                {
                    case 1:
                        Console.Write("For which date would you like cutting list for: ");
                        var cuttingReportDate = Console.ReadLine();
                        DateTime cuttingFilterDate = default;
                        if (!string.IsNullOrEmpty(cuttingReportDate))
                            cuttingFilterDate = InputValidator.ConvertToDateTime(cuttingReportDate);

                        var filteredCuttingLists = toyBlockFactory.GetCuttingListsByDate(cuttingFilterDate);
                        foreach (var cuttingList in filteredCuttingLists)
                        {
                            PrintReports.PrintReport(cuttingList);
                        }
                        break;

                    case 2:
                        Console.Write("For which date would you like painting list for: ");
                        var paintingReportDate = Console.ReadLine();
                        DateTime paintingFilterDate = default;
                        if (!string.IsNullOrEmpty(paintingReportDate))
                            paintingFilterDate = InputValidator.ConvertToDateTime(paintingReportDate);

                        var filteredPaintingReports = toyBlockFactory.GetPaintingReportsByDate(paintingFilterDate);
                        foreach (var paintingReport in filteredPaintingReports)
                        {
                            PrintReports.PrintReport(paintingReport);
                        }
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }

    }

   
}