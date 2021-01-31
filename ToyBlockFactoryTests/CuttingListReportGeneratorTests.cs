using System;
using System.Linq;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class CuttingListReportGeneratorTests
    {
        private ToyBlockFactory _toyBlockFactory;
        private string _customerName;
        private string _customerAddress;

        public CuttingListReportGeneratorTests()
        {
            _toyBlockFactory = new ToyBlockFactory();
            _customerName = "David Rudd";
            _customerAddress = "1 Bob Avenue, Auckland";
            var customerOrder = _toyBlockFactory.CreateOrder(_customerName, _customerAddress);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.SetDueDate("19 Jan 2019");
            _toyBlockFactory.SubmitOrder(customerOrder);
        }
        
        [Fact]
        public void IsCuttingList()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(ReportType.CuttingList, cuttingList.ReportType);
        }

        [Fact]
        public void ReportCustomerNameMatchesOrder()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(_customerName, cuttingList.Name);
        }

        [Fact]
        public void ReportCustomerAddressMatchesOrder()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(_customerAddress, cuttingList.Address);
        }

        [Fact]
        public void ReportDueDateMatchesOrder()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(new DateTime(2019, 1, 19), cuttingList.DueDate);
        }

        [Fact]
        public void ReportOrderIdMatchesOrder()
        {
            const string orderId = "0001";

            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);

            Assert.Equal(orderId, cuttingList.OrderId);
        }
        
        [Theory]
        [InlineData(Shape.Square, 2)]
        [InlineData(Shape.Triangle, 2)]
        [InlineData(Shape.Circle, 3)]
        public void ReportShouldGenerateOrderTable(Shape shape, int quantity)
        {
            const string orderId = "0001";
            
            var cuttingList = _toyBlockFactory.GetCuttingListReport(orderId);
            var tableRow = cuttingList.OrderTable.SingleOrDefault(l => l.Shape == shape);

            Assert.NotNull(tableRow);
            Assert.Equal(shape, tableRow.Shape);
            Assert.Equal(quantity, tableRow.TableColumn[0].Quantity);
        }
    }
}