using System;
using System.Collections.Generic;
using System.Linq;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryKata.Tables;

namespace ToyBlockFactoryKata.ReportCreators
{
    internal class ReportSystem
    {
        private readonly IReportCreator _cuttingReportCreator = new CuttingReportCreator(new QuantityTableFactory());
        private readonly IReportCreator _invoiceReportCreator;
        private readonly IReportCreator _paintingReportCreator = new PaintingReportCreator(new ColourTableFactory());

        internal ReportSystem(ILineItemsCalculator pricingCalculator)
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
        
        internal IEnumerable<IReport> FilterReportsByDate(List<Order> orderRecords, ReportType reportType)
        {
            IEnumerable<IReport> reports;
            switch (reportType)
            {
                case ReportType.CuttingList:
                    reports = FilterCuttingReportsByDate(orderRecords);
                    break;
                case ReportType.Painting:
                    reports = FilterPaintingReportsByDate(orderRecords);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reportType), reportType, "Invalid report type");
            }

            return reports;
        }

        private IReport GenerateInvoice(Order requestedOrder)
        {
            return _invoiceReportCreator.GenerateReport(requestedOrder);
        }

        private IReport GenerateCuttingList(Order requestedOrder)
        {
            return _cuttingReportCreator.GenerateReport(requestedOrder);
        }

        private IReport GeneratePaintingReport(Order requestedOrder)
        {
            return _paintingReportCreator.GenerateReport(requestedOrder);
        }

        private IEnumerable<IReport> FilterCuttingReportsByDate(List<Order> orderRecords)
        {
            return orderRecords.Select(o => GenerateCuttingList(o));
        }

        private IEnumerable<IReport> FilterPaintingReportsByDate(List<Order> orderRecords)
        {
            return orderRecords.Select(o => GeneratePaintingReport(o));
        }
        
    }
}