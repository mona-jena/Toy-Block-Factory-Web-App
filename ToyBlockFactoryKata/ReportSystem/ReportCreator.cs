using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportSystem
{
    internal class ReportCreator
    {
        private readonly IReportCreator _cuttingReportCreator = new TableReportCreator(new QuantityTableFactory());
        private readonly IReportCreator _invoiceReportCreator;
        private readonly IReportCreator _paintingReportCreator = new TableReportCreator(new ColourTableFactory());

        internal ReportCreator(IInvoiceCalculator pricingCalculator)
        {
            _invoiceReportCreator = new InvoiceReportCreator(pricingCalculator, new ColourTableFactory());
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
            return _invoiceReportCreator.GenerateReport(ReportType.Invoice, requestedOrder);
        }

        private IReport GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportCreator.GenerateReport(ReportType.CuttingList, requestedOrder);
        }

        private IReport GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportCreator.GenerateReport(ReportType.Painting, requestedOrder);
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