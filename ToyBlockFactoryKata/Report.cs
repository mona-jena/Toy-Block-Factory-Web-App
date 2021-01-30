using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyBlockFactoryKata
{
    public class Report
    {
        public ReportType ReportType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DueDate { get; set; }
        public string OrderId { get; set; }
        public List<InvoiceLine> LineItems { get; } = new();
        public decimal Total => LineItems.Sum(item => item.Total);
        public List<TableRow> OrderTable { get; } = new();
    }


    public class CuttingLine
    {
        private int bladeWear;
        private int quantity;
        private string shape;

        private string Print()
        {
            return "ffgdg";
        }
    }
}