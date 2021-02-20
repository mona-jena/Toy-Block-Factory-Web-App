using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportGenerators
{
    internal class ReportGenerator
    {
        private readonly IReportGenerator _cuttingReportGenerator = new TableReportGenerator(new QuantityTableFactory());
        private readonly IReportGenerator _invoiceReportGenerator;
        private readonly IReportGenerator _paintingReportGenerator = new TableReportGenerator(new ColourTableFactory());

        internal ReportGenerator(IInvoiceCalculator pricingCalculator)
        {
            _invoiceReportGenerator = new InvoiceReportGenerator(pricingCalculator, new ColourTableFactory());
        }
        
        public IReport GenerateReport(Order requestedOrder, ReportType reportType)
        {
            IReport report;
            switch (reportType)
            {
                case ReportType.Invoice:
                    report = GenerateInvoice(requestedOrder);
                    break;
                case ReportType.CuttingList:
                    report = GenerateCuttingList(requestedOrder);
                    break;
                case ReportType.Painting:
                    report = GeneratePaintingReport(requestedOrder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reportType), reportType, "Invalid report type");
            }

            return report;
        }
        
        public IEnumerable<IReport> FilterReportsByDate(DateTime date, Dictionary<string,Order> orderRecords, ReportType reportType)
        {
            IEnumerable<IReport> reports;
            switch (reportType)
            {
                case ReportType.CuttingList:
                    reports = FilterCuttingReportsByDate(date, orderRecords);
                    break;
                case ReportType.Painting:
                    reports = FilterPaintingReportsByDate(date, orderRecords);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reportType), reportType, "Invalid report type");
            }

            return reports;
        }

        private IReport GenerateInvoice(Order requestedOrder)
        {
            return _invoiceReportGenerator.GenerateReport(ReportType.Invoice, requestedOrder);
        }

        private IReport GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportGenerator.GenerateReport(ReportType.CuttingList, requestedOrder);
        }

        private IReport GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportGenerator.GenerateReport(ReportType.Painting, requestedOrder);
        }

        private IEnumerable<IReport> FilterCuttingReportsByDate(DateTime date, Dictionary<string, Order> orderRecords)
        {
            return orderRecords.Where(o => o.Value.DueDate == date).Select(o => GenerateCuttingList(o.Value));
        }

        private IEnumerable<IReport> FilterPaintingReportsByDate(DateTime date, Dictionary<string, Order> orderRecords)
        {
            return orderRecords.Where(o => o.Value.DueDate == date).Select(o => GeneratePaintingReport(o.Value));
        }

        
    }
}