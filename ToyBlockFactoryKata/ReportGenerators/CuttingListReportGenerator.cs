using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class CuttingListReportGenerator : IReportGenerator
    {
        private readonly ITableGenerator _tableGenerator;

        public CuttingListReportGenerator(ITableGenerator tableGenerator)
        {
            _tableGenerator = tableGenerator;
        }
        
        public IReport GenerateReport(Order requestedOrder)
        {
            var report = new Report
            {
                ReportType = ReportType.CuttingList,
                Name = requestedOrder.Name,
                Address = requestedOrder.Address,
                DueDate = requestedOrder.DueDate,
                OrderId = requestedOrder.OrderId
            };
            var table = _tableGenerator.GenerateTable(report, requestedOrder);
            report.OrderTable.AddRange(table);
            
            return report;
        }

        
    }
}