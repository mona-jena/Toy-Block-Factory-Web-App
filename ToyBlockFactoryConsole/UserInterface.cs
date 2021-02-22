using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryConsole
{
    public static class UserInterface
    {
        public static void Menu(ToyBlockFactory toyBlockFactory)
        {
            Console.WriteLine(
                "Please choose an option: \n[1] Place an order \n[2] Get an existing order \n[3] Get reports due on a particular date?");
            Console.Write("Please input your choice: ");
            var functionalityOption = int.Parse(Console.ReadLine());
            switch (functionalityOption)
            {
                case 1:
                    OrderCollector.PlaceOrder(toyBlockFactory);
                    break;
                case 2:
                    GetOrder(toyBlockFactory);
                    break;
                case 3:
                    GetReportsForADate(toyBlockFactory);
                    break;
            }
        }

        private static void GetOrder(ToyBlockFactory toyBlockFactory)
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

        internal static void GetReport(ToyBlockFactory toyBlockFactory, string orderId)
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
                        var invoiceReport = toyBlockFactory.GetReport(orderId, ReportType.Invoice);
                        ReportPrinter.PrintReport(invoiceReport);
                        break;
                    case 2:
                        var cuttingListReport = toyBlockFactory.GetReport(orderId, ReportType.CuttingList);
                        ReportPrinter.PrintReport(cuttingListReport);
                        break;
                    case 3:
                        var paintingReport = toyBlockFactory.GetReport(orderId, ReportType.Painting);
                        ReportPrinter.PrintReport(paintingReport);
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
        
        
        private static void GetReportsForADate(ToyBlockFactory toyBlockFactory)
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
                        var filteredCuttingLists = toyBlockFactory.GetReportsByDate(cuttingReportDate, ReportType.CuttingList);
                        foreach (var cuttingList in filteredCuttingLists) 
                            ReportPrinter.PrintReport(cuttingList);
                        break;

                    case 2:
                        var paintingReportDate = GetDateInput();
                        var filteredPaintingReports = toyBlockFactory.GetReportsByDate(paintingReportDate, ReportType.Painting);
                        foreach (var paintingReport in filteredPaintingReports)
                            ReportPrinter.PrintReport(paintingReport);
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