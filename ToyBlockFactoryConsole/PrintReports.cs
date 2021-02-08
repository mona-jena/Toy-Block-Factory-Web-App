using System;
using System.Globalization;
using System.Linq;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    static class PrintReports
    {
        public static void PrintReport(IReport report)
        {
            Console.WriteLine(
                "Your " + report.ReportType.ToString().ToLower() + " report has been generated:" +
                "\n\n" +
                "Name: " + report.Name + " Address: " + report.Address + " Due Date: " + report.DueDate.ToString("dd/MM/yyyy") + " Order #: " + report.OrderId);

            if (report.ReportType == ReportType.CuttingList)
            {
                PrintCuttingTable();
            }

            var topRowLabels = report.OrderTable.SelectMany(l => l.TableColumn).Select(l => l.MeasuredItem).Distinct();
            Console.Write("|          | ");
            foreach (var label in topRowLabels)
            {
                Console.Write( label + " | ");
            }
            Console.Write("\n|----------|");
            foreach (var label in topRowLabels)
            {
                var length = label.Length + 2;
                Console.Write(string.Concat((Enumerable.Repeat("-", length))));
                Console.Write("|");
            }
            
            foreach (var row in report.OrderTable)
            {
                var space = 8 - row.Shape.ToString().Length + 1;
                Console.Write("\n| " + row.Shape);
                Console.Write(string.Concat((Enumerable.Repeat(" ", space))));
                //Console.Write("|");
                
                foreach (var column in row.TableColumn)
                {
                    var columnSpace = column.MeasuredItem.Length;
                    Console.Write("| " + column.Quantity);
                    Console.Write(string.Concat((Enumerable.Repeat(" ", columnSpace))));
                    Console.Write( "|");
                }
            }
           
        }

        private static void PrintCuttingTable()
        {
            throw new NotImplementedException();
        }
    }
}