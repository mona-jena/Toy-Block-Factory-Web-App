using System;
using System.Linq;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryConsole
{
    internal static class ReportPrinter
    {
        public static void PrintReport(IReport report)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Your " + report.ReportType.ToString().ToLower() + " report has been generated:" +
                "\n\n" +
                "Name: " + report.Name + "  Address: " + report.Address + "  Due Date: " +
                report.DueDate.ToString("dd/MM/yyyy") + "  Order #: " + report.OrderId);
            
            Console.WriteLine();

            var topRowLabels = report.OrderTable.SelectMany(l => l.TableColumn).Select(l => l.MeasuredItem).Distinct();
            String s = "|          ";
            foreach (var label in topRowLabels)
                s += $"| {label,8} ";
            
            s += "|\n";
            for (var i =0; i <= topRowLabels.Count(); i++)
            {
                s += "|----------";
            }
            s += "|\n";
            
            foreach (var row in report.OrderTable)
            {
                s += $"| {row.Shape.ToString(),-8} |";
                foreach (var column in row.TableColumn)
                {
                    if (column.Quantity > 0)
                        s += $" {column.Quantity.ToString(),8} |";
                    else
                        s += $" {"-",8} |";
                }
                s += "\n";
            }
            Console.WriteLine($"{s}");

            
            if (report.ReportType == ReportType.Invoice)
                PrintLineItems(report);
            
        }
        
        private static void PrintLineItems(IReport report)
        {
            String l = "";
            var lineItems = ((InvoiceReport) report).LineItems.Select(l => l);
            foreach (var line in lineItems)
            {
                l += $"{line.Description,-25} ";
                l += line.Quantity + " @ $" + line.Price + " ppi = $" + line.Total + "\n";
            }

            l += "\nTotal : $" + ((InvoiceReport) report).Total;
            Console.WriteLine($"{l}");
        }
        
    }
}