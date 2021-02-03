namespace ToyBlockFactoryKata
{
    public interface IReportGenerator
    {
        IReport GenerateReport(Order requestedOrder);
        
    }
}