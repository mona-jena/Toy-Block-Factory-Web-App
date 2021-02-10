using System;
using System.Linq;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.Reports;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class CuttingListReportGeneratorTests
    {
        string _customerAddress = string.Empty;
        string _customerName = string.Empty;
        readonly ToyBlockFactory _toyBlockFactory;

        public CuttingListReportGeneratorTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new TestPricingCalculator());

            CreateListOfOrders();
            
        }

        private void CreateListOfOrders()
        {
            _customerName = "David Rudd";
            _customerAddress = "1 Bob Avenue, Auckland";
            var dueDate = new DateTime(2021, 1, 19);
            var customerOrder = _toyBlockFactory.CreateOrder(_customerName, _customerAddress, dueDate);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customerOrder);

            var customer2Name = "James Soho"; 
            var customer2Address = "34 Anzac Avenue, Auckland"; 
            var order2DueDate = new DateTime(2021, 1, 19);
            var customer2Order = _toyBlockFactory.CreateOrder(customer2Name, customer2Address, order2DueDate);
            customer2Order.AddBlock(Shape.Triangle, Colour.Red);
            customer2Order.AddBlock(Shape.Square, Colour.Yellow);
            customer2Order.AddBlock(Shape.Triangle, Colour.Blue);
            _toyBlockFactory.SubmitOrder(customer2Order);

            var customer3Name = "Alex Wright";
            var customer3Address = "101 South Road, Auckland";
            var order3DueDate = new DateTime(2021, 4, 21);
            var customer3Order = _toyBlockFactory.CreateOrder(customer3Name, customer3Address, order3DueDate);
            customer3Order.AddBlock(Shape.Triangle, Colour.Blue);
            customer3Order.AddBlock(Shape.Square, Colour.Yellow);
            customer3Order.AddBlock(Shape.Circle, Colour.Blue);
            customer3Order.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customer3Order);

            var customer4Name = "Tom Night";
            var customer4Address = "23 Country Avenue, Hamilton";
            var order4DueDate = new DateTime(2021, 1, 19);
            var customer4Order = _toyBlockFactory.CreateOrder(customer4Name, customer4Address, order4DueDate);
            customer4Order.AddBlock(Shape.Circle, Colour.Yellow);
            customer4Order.AddBlock(Shape.Circle, Colour.Yellow);
            customer4Order.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customer4Order);
        }


        [Fact]
        public void IsCuttingList()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(ReportType.CuttingList, cuttingList.ReportType);
        }

        [Fact]
        public void ReportContainsCustomerName()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(_customerName, cuttingList.Name);
        }

        [Fact]
        public void ReportContainsCustomerAddress()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(_customerAddress, cuttingList.Address);
        }

        [Fact]
        public void ReportContainsOrderDueDate()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(new DateTime(2021, 1, 19), cuttingList.DueDate);
        }

        [Fact]
        public void ReportContainsOrderId()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(orderId, cuttingList.OrderId);
        }

        [Theory]
        [InlineData(Shape.Square, 2)]
        [InlineData(Shape.Triangle, 2)]
        [InlineData(Shape.Circle, 3)]
        public void ReportGeneratesOrderTable(Shape shape, int quantity)
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);
            var tableRow = cuttingList.OrderTable.SingleOrDefault(l => l.Shape == shape);

            Assert.NotNull(tableRow);
            Assert.Equal(shape, tableRow.Shape);
            Assert.Equal(quantity, tableRow.TableColumn[0].Quantity);
        }

        [Fact]
        public void CanFilterReportsByDueDate()
        {
            var filteredReports = _toyBlockFactory.GetCuttingListsByDate(new DateTime(2021, 1, 19)).ToList();

            Assert.Equal(3, filteredReports.Count);
            Assert.Equal("0001", filteredReports[0].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[0].DueDate);
            Assert.Equal("0002", filteredReports[1].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[1].DueDate);
            Assert.Equal("0004", filteredReports[2].OrderId);
            Assert.Equal(new DateTime(2021, 1, 19), filteredReports[2].DueDate);
        }
    }
}