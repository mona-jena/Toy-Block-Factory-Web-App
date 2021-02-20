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
                s += String.Format("| {0,8} ", label);
            
            s += "|\n";
            for (var i =0; i <= topRowLabels.Count(); i++)
            {
                s += "|----------";
            }
            s += "|\n";
            
            foreach (var row in report.OrderTable)
            {
                s += String.Format("| {0,-8} |", row.Shape.ToString());
                foreach (var column in row.TableColumn)
                {
                    s += String.Format(" {0,8} |", column.Quantity.ToString());
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
                l += String.Format("{0,-25} ", line.Description);
                l += line.Quantity + " @ $" + line.Price + " ppi = $" + line.Total + "\n";
            }

            l += "\nTotal : $" + ((InvoiceReport) report).Total;
            Console.WriteLine($"{l}");
        }
        
    }
}