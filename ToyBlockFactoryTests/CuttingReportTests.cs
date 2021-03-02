using System;
using System.Linq;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;
using ToyBlockFactoryTests.TestDoubles;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class CuttingReportTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;

        public CuttingReportTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new LineItemsCalculatorStub());

            CreateListOfOrders();
        }
        

        [Fact]
        public void IsCuttingList()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.CuttingList);

            Assert.Equal(ReportType.CuttingList, cuttingList.ReportType);
        }

        string _david = string.Empty;
        
        [Fact]
        public void ReportContainsCustomerName()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.CuttingList);

            Assert.Equal(_david, cuttingList.Name);
        }

        
        string _davidAddress = string.Empty;
        
        [Fact]
        public void ReportContainsCustomerAddress()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.CuttingList);

            Assert.Equal(_davidAddress, cuttingList.Address);
        }

        [Fact]
        public void ReportContainsOrderDueDate()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.CuttingList);

            Assert.Equal(new DateTime(2021, 1, 19), cuttingList.DueDate);
        }

        [Fact]
        public void ReportContainsOrderId()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.CuttingList);

            Assert.Equal(orderId, cuttingList.OrderId);
        }

        [Theory]
        [InlineData(Shape.Square, 2)]
        [InlineData(Shape.Triangle, 2)]
        [InlineData(Shape.Circle, 3)]
        public void ReportGeneratesOrderTable(Shape shape, int quantity)
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetReport(orderId, ReportType.CuttingList);
            var tableRow = cuttingList.OrderTable.SingleOrDefault(l => l.Shape == shape);

            Assert.NotNull(tableRow);
            Assert.Equal(shape, tableRow?.Shape);
            Assert.Equal(quantity, tableRow?.TableColumn[0].Quantity);
        }

        [Fact]
        public void CanFilterReportsByDueDate()
        {
            var filteredReports = _toyBlockFactory.GetReportsByDate(new DateTime(2021, 1, 19), ReportType.CuttingList).ToList();

            Assert.Equal(3, filteredReports.Count);
            Assert.Equal("0001", filteredReports[0].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[0].DueDate);
            Assert.Equal("0002", filteredReports[1].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[1].DueDate);
            Assert.Equal("0004", filteredReports[2].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[2].DueDate);
        }
        
        
        private void CreateListOfOrders()
        {
            _david = "David Rudd";
            _davidAddress = "1 Bob Avenue, Auckland";
            var davidOrderDueDate = new DateTime(2021, 1, 19);
            var davidOrder = _toyBlockFactory.CreateOrder(_david, _davidAddress, davidOrderDueDate);
            davidOrder.AddBlock(Shape.Square, Colour.Red);
            davidOrder.AddBlock(Shape.Square, Colour.Yellow);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue);
            davidOrder.AddBlock(Shape.Circle, Colour.Blue);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(davidOrder);

            var james = "James Soho"; 
            var jamesAddress = "34 Anzac Avenue, Auckland"; 
            var jamesOrderDueDate = new DateTime(2021, 1, 19);
            var jamesOrder = _toyBlockFactory.CreateOrder(james, jamesAddress, jamesOrderDueDate);
            jamesOrder.AddBlock(Shape.Triangle, Colour.Red);
            jamesOrder.AddBlock(Shape.Square, Colour.Yellow);
            jamesOrder.AddBlock(Shape.Triangle, Colour.Blue);
            _toyBlockFactory.SubmitOrder(jamesOrder);

            var alex = "Alex Wright";
            var alexAddress = "101 South Road, Auckland";
            var alexOrderDueDate = new DateTime(2021, 4, 21);
            var alexOrder = _toyBlockFactory.CreateOrder(alex, alexAddress, alexOrderDueDate);
            alexOrder.AddBlock(Shape.Triangle, Colour.Blue);
            alexOrder.AddBlock(Shape.Square, Colour.Yellow);
            alexOrder.AddBlock(Shape.Circle, Colour.Blue);
            alexOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(alexOrder);

            var tom = "Tom Night";
            var tomAddress = "23 Country Avenue, Hamilton";
            var tomOrderDueDate = new DateTime(2021, 1, 19);
            var tomOrder = _toyBlockFactory.CreateOrder(tom, tomAddress, tomOrderDueDate);
            tomOrder.AddBlock(Shape.Circle, Colour.Yellow);
            tomOrder.AddBlock(Shape.Circle, Colour.Yellow);
            tomOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(tomOrder);
        }
    }
}