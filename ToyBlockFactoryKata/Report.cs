using System;
using System.Collections.Generic;

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
        //public string[,] OrderTable { get; }
    }
    
        
    public class CuttingLine
    {
        private int quantity;
        private int bladeWear;
        private string shape;
            
        string Print()
        {
            return "ffgdg";
        }
    }
}
