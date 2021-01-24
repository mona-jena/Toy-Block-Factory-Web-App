using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class Report
    {
        public ReportType ReportType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string DueDate { get; set; }
        public string OrderId { get; set; }
        public List<object> LineItems { get; } = new();
    }
}
