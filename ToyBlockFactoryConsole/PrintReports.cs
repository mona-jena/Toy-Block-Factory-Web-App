using System;
using System.Linq;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryConsole
{
    internal static class PrintReports
    {
        public static void PrintReport(IReport report)
        {
            Console.WriteLine(
                "Your " + report.ReportType.ToString().ToLower() + " report has been generated:" +
                "\n\n" +
                "Name: " + report.Name + " Address: " + report.Address + " Due Date: " +
                report.DueDate.ToString("dd/MM/yyyy") + " Order #: " + report.OrderId);

            if (report.ReportType == ReportType.CuttingList)
                PrintCuttingTable();


            int[] years = {2013, 2014, 2015};
            int[] population = {1025632, 1105967, 1148203};
            var s = string.Format("{0,-10} {1,-10}\n\n", "Year", "Population");
            for (var index = 0; index < years.Length; index++)
                s += string.Format("{0,-10} {1,-10:N0}\n",
                    years[index], population[index]);
            Console.WriteLine($"\n{s}");


            var topRowLabels = report.OrderTable.SelectMany(l => l.TableColumn).Select(l => l.MeasuredItem).Distinct();
            Console.Write("|          | ");
            foreach (var label in topRowLabels) Console.Write(label + " | ");
            Console.Write("\n|----------|");
            foreach (var label in topRowLabels)
            {
                var length = label.Length + 2;
                Console.Write(string.Concat(Enumerable.Repeat("-", length)));
                Console.Write("|");
            }

            foreach (var row in report.OrderTable)
            {
                var space = 8 - row.Shape.ToString().Length + 1;
                Console.Write("\n| " + row.Shape);
                Console.Write(string.Concat(Enumerable.Repeat(" ", space)));
                //Console.Write("|");

                foreach (var column in row.TableColumn)
                {
                    var columnSpace = column.MeasuredItem.Length;
                    Console.Write("| " + column.Quantity);
                    Console.Write(string.Concat(Enumerable.Repeat(" ", columnSpace)));
                    Console.Write("|");
                }
            }
        }

        private static void PrintCuttingTable()
        {
            throw new NotImplementedException();
        }
    }
}