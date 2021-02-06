using System;
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

            Console.WriteLine();

            Console.Write("Please input the number of Red Squares: ");
            var redSquareInput = Console.ReadLine();
            int redSquares;
            if (!string.IsNullOrEmpty(redSquareInput))
                redSquares = InputValidator.IfValidInteger(redSquareInput);

            Console.Write("Please input the number of Blue Squares: ");
            var blueSquareInput = Console.ReadLine();
            int blueSquares;
            if (!string.IsNullOrEmpty(blueSquareInput))
                blueSquares = InputValidator.IfValidInteger(blueSquareInput);

            Console.Write("Please input the number of Yellow Squares: ");
            var yellowSquareInput = Console.ReadLine();
            int yellowSquares;
            if (!string.IsNullOrEmpty(yellowSquareInput))
                yellowSquares = InputValidator.IfValidInteger(yellowSquareInput);

            Console.WriteLine();

            Console.Write("Please input the number of Red Triangles: ");
            var redTriangleInput = Console.ReadLine();
            int redTriangles;
            if (!string.IsNullOrEmpty(redTriangleInput))
                redTriangles = InputValidator.IfValidInteger(redTriangleInput);

            Console.Write("Please input the number of Blue Triangles : ");
            var blueTriangleInput = Console.ReadLine();
            int blueTriangles;
            if (!string.IsNullOrEmpty(blueTriangleInput))
                blueTriangles = InputValidator.IfValidInteger(blueTriangleInput);

            Console.Write("Please input the number of Yellow Triangles: ");
            var yellowTriangleInput = Console.ReadLine();
            int yellowTriangles;
            if (!string.IsNullOrEmpty(yellowTriangleInput))
                yellowTriangles = InputValidator.IfValidInteger(yellowTriangleInput);

            Console.WriteLine();

            Console.Write("Please input the number of Red Circles: ");
            var redCircleInput = Console.ReadLine();
            int redCircles;
            if (!string.IsNullOrEmpty(redCircleInput))
                redCircles = InputValidator.IfValidInteger(redCircleInput);

            Console.Write("Please input the number of Blue Circles: ");
            var blueCircleInput = Console.ReadLine();
            int blueCircles;
            if (!string.IsNullOrEmpty(blueCircleInput))
                blueCircles = InputValidator.IfValidInteger(blueCircleInput);

            Console.Write("Please input the number of Yellow Circles: ");
            var yellowCircleInput = Console.ReadLine();
            int yellowCircles;
            if (!string.IsNullOrEmpty(yellowCircleInput))
                yellowCircles = InputValidator.IfValidInteger(yellowCircleInput);

            
            Order order;
            if (dueDate != default)
            {
                order = toyBlockFactory.CreateOrder(name, address);
            }
            order = toyBlockFactory.CreateOrder(name, address, dueDate);
            var orderId = toyBlockFactory.SubmitOrder(order);


            var option = 1;
            while (option != 4)
            {
                Console.Write(
                    "\nWhich report would you like? \n[1] Invoice Report \n[2] Cutting List Report \n[3] Painting Report \n[4] Exit Program \n" +
                    "Please enter an option: ");
                option = int.Parse(Console.ReadLine());
                //var userOrder = toyBlockFactory.GetOrder(orderId);
                switch (option)
                {
                    case 1:
                        var invoiceReport = toyBlockFactory.GetInvoiceReport(orderId);
                        Console.WriteLine("invoice\n");
                        //PrintingClass(invoiceReport)
                        break;
                    case 2:
                        var cuttingListReport = toyBlockFactory.GetCuttingListReport(orderId);
                        Console.WriteLine("cutting\n");
                        //PrintingClass(cuttingListReport)
                        break;
                    case 3:
                        var paintingReport = toyBlockFactory.GetPaintingReport(orderId);
                        //PrintingClass(paintingReport)
                        Console.WriteLine("painting\n");
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
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
                        Console.WriteLine("cutting\n");
                        //PrintingReport(filteredCuttingLists)
                        break;
                        
                    case 2:
                        Console.Write("For which date would you like painting list for: ");
                        var paintingReportDate = Console.ReadLine();
                        DateTime paintingFilterDate = default;
                        if (!string.IsNullOrEmpty(paintingReportDate))
                            paintingFilterDate = InputValidator.ConvertToDateTime(paintingReportDate);
                        
                        var paintingReport = toyBlockFactory.GetPaintingReportsByDate(paintingFilterDate);
                        //PrintingClass(paintingReport)
                        Console.WriteLine("painting\n");
                        break;
                    
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        
    }
}
