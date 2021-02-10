using System;
using System.Linq;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    internal static class PrintReports
    {
        public static void PrintReport(IReport report)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Your " + report.ReportType.ToString().ToLower() + " report has been generated:" +
                "\n\n" +
                "Name: " + report.Name + " Address: " + report.Address + " Due Date: " +
                report.DueDate.ToString("dd/MM/yyyy") + " Order #: " + report.OrderId);
            
            Console.WriteLine();

            var topRowLabels = report.OrderTable.SelectMany(l => l.TableColumn).Select(l => l.MeasuredItem).Distinct();
            Console.Write("|          ");
            foreach (var label in topRowLabels) 
                Console.Write("| " + label.PadRight(9));
            
            Console.WriteLine("|");
            for (var i =0; i <= topRowLabels.Count(); i++)
            {
                Console.Write("|----------");
            }
            Console.WriteLine("|");
            
            foreach (var row in report.OrderTable)
            {
                Console.Write("| " +  row.Shape.ToString().PadRight(9) + "|");

                foreach (var column in row.TableColumn)
                {
                    Console.Write(column.Quantity.ToString().PadLeft(9) + " |");
                }
                Console.WriteLine();
            }
           
            Console.WriteLine();

            if (report.ReportType == ReportType.Invoice)
            {
                var lineItems = ((InvoiceReport) report).LineItems.Select(l => l);
                foreach (var line in lineItems)
                {
                    Console.WriteLine(line.Description.PadRight(25) + line.Quantity + " @ $" + line.Price + " ppi = $" + line.Total);
                }
            }
        }
        
    }
}