using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;

namespace ToyBlockFactoryKata.ReportSystem
{
    internal interface IReportCreator
    {
        IReport GenerateReport(ReportType reportType, Order requestedOrder);
    }
}