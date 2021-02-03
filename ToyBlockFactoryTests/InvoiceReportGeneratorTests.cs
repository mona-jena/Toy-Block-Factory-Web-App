using System;
using System.Linq;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class InvoiceReportGeneratorTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;
        private readonly string _customerAddress;
        private readonly string _customerName;

        public InvoiceReportGeneratorTests()
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
            customerOrder.SetDueDate(new DateTime(2019, 1, 19));
            _toyBlockFactory.SubmitOrder(customerOrder);

            var customer2Name = "Steve Richards";
            var customer2Address = "27 Valley Road, Auckland";
            var customer2Order = _toyBlockFactory.CreateOrder(customer2Name, customer2Address);
            customer2Order.AddBlock(Shape.Square, Colour.Yellow);
            customer2Order.AddBlock(Shape.Square, Colour.Blue);
            customer2Order.AddBlock(Shape.Circle, Colour.Blue);
            customer2Order.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.SetDueDate(new DateTime(2019, 2, 15));
            _toyBlockFactory.SubmitOrder(customer2Order);

            var customer3Name = "Tony Williams";
            var customer3Address = "13 Stokes Road, Auckland";
            var customer3Order = _toyBlockFactory.CreateOrder(customer3Name, customer3Address);
            customerOrder.SetDueDate(new DateTime(2019, 11, 21));
            _toyBlockFactory.SubmitOrder(customer3Order);
            
            var customer4Name = "Catherine Jenkins";
            var customer4Address = "56 Owens Road, Auckland";
            var customer4Order = _toyBlockFactory.CreateOrder(customer4Name, customer4Address);
            customer4Order.AddBlock(Shape.Circle, Colour.Red);
            customer4Order.AddBlock(Shape.Circle, Colour.Red);
            customer4Order.AddBlock(Shape.Circle, Colour.Blue);
            customer4Order.AddBlock(Shape.Circle, Colour.Blue);
            customer4Order.SetDueDate(new DateTime(2019, 4, 23));
            _toyBlockFactory.SubmitOrder(customer4Order);
        }


        [Fact]
        public void IsInvoice()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(ReportType.Invoice, invoice.ReportType);
        }

        [Fact]
        public void ReportContainsCustomerName()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(_customerName, invoice.Name);
        }

        [Fact]
        public void ReportContainsCustomerAddress()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(_customerAddress, invoice.Address);
        }

        [Fact]
        public void ReportContainsOrderDueDate()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(new DateTime(2019, 1, 19), invoice.DueDate);
        }
        
        [Fact]
        public void ReportContainsOrderDueDateTEST()
        {
            const string orderId = "0002";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(new DateTime(2019, 2, 15), invoice.DueDate);
        }

        [Fact]
        public void ReportContainsOrderId()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(orderId, invoice.OrderId);
        }
        
        
        [Theory]
        [InlineData("Square", 2, 1, 2)]
        [InlineData("Circle", 3, 3, 9)]
        [InlineData("Red colour surcharge", 1, 1, 1)]
        [InlineData("Triangle", 2, 2, 4)]
        public void LineItemsContainDetailsAboutEachOrderItem(string description, int quantity, decimal price, decimal total)
        {
            const string orderId = "0001";
            
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            var invoiceLine = (invoice as InvoiceReport).LineItems.SingleOrDefault(l => l.Description == description);
            //read up on this
            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
            Assert.Equal(total, invoiceLine.Total);
        }

        [Fact]
        public void ReportCalculatesOrderTotal()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            Assert.Equal(16.00m, (invoice as InvoiceReport).Total);
        }
        
        [Theory]
        [InlineData("Square", 2, 1, 2)]
        [InlineData("Circle", 2, 3, 6)]
        public void OnlyItemsInOrderAreListedInLineItems(string description, int quantity, decimal price, decimal total)
        {
            const string orderId = "0002";
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            var invoiceLine = (invoice as InvoiceReport).LineItems.SingleOrDefault(l => l.Description == description);

            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
            Assert.Equal(total, invoiceLine.Total);
        }
        
        [Theory]
        [InlineData("Circle", 4, 3, 12)]
        [InlineData("Red colour surcharge", 2, 1, 2)]        //What did we want to check for this test case?
        public void CheckPriceFor2RedAnd2BlueCircles(string description, int quantity, decimal price, decimal total)
        {
            const string orderId = "0004";
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            var invoiceLine = (invoice as InvoiceReport).LineItems.SingleOrDefault(l => l.Description == description);

            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
            Assert.Equal(total, invoiceLine.Total);
        }
        
        [Fact]
        public void EmptyOrderReturnsEmptyLineItems()
        {
            var invoice = _toyBlockFactory.GetInvoiceReport("0003");

            Assert.Empty((invoice as InvoiceReport).LineItems);
        }
        
        
        [Theory]
        [InlineData(Shape.Square, "Red", 1)]
        [InlineData(Shape.Square, "Yellow", 1)]
        [InlineData(Shape.Triangle, "Blue", 2)]
        [InlineData(Shape.Circle, "Blue", 1)]
        [InlineData(Shape.Circle, "Yellow", 2)]
        public void ReportGeneratesOrderTable(Shape shape, string colour, int quantity)
        {
            const string orderId = "0001";
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);

            var tableRow = invoice.OrderTable.SingleOrDefault(l => l.Shape == shape);
            var tableColumn = tableRow.TableColumn.SingleOrDefault(l => l.MeasuredItem == colour);

            Assert.NotNull(tableRow);
            Assert.NotNull(tableColumn);      
            Assert.Equal(shape, tableRow.Shape);
            Assert.Equal(colour, tableColumn.MeasuredItem);
            Assert.Equal(quantity, tableColumn.Quantity);
        }
    }
}

/*var expectedInvoiceReport = new List<string>()
{
    "Your invoice report has been generated:",
    "\n",
    "Name: David Rudd ",
    "Address: 1 Bob Avenue, Auckland ",
    "Due Date: 19 Jan 2019 ",
    "Order #: 0001,",
    "\n",
    "|          | Red | Blue | Yellow |",
    "|----------|-----|------|--------|",
    "| Square   | 1   | -    | 1      |",
    "| Triangle | -   | 2    | -      |",
    "| Circle   | -   | 1    | 2      |",
    "\n",
    "Squares 		        2 @ $1 ppi = $2",
    "Triangles		        2 @ $2 ppi = $4",
    "Circles			    3 @ $3 ppi = $9",
    "Red colour surcharge   1 @ $1 ppi = $1",
    "\n",
    "Total                  $16"
};*/


//string[,] invoiceLine2 = new string[Enum.GetNames(typeof(Colour)).Length,Enum.GetNames(typeof(Shape)).Length];
/*string[,] invoiceLine = new string[4, 4];
invoiceLine[1, 0] = "Red";
invoiceLine[2, 0] = "Blue";
invoiceLine[3, 0] = "Yellow";
invoiceLine[0, 1] = "Square";
invoiceLine[1, 1] = "1";
invoiceLine[2, 1] = "-";
invoiceLine[3, 1] = "1";
invoiceLine[0, 2] = "Triangle";
invoiceLine[1, 2] = "-";
invoiceLine[2, 2] = "2";
invoiceLine[3, 2] = "-";
invoiceLine[0, 3] = "Circle";
invoiceLine[1, 3] = "-";
invoiceLine[2, 3] = "1";
invoiceLine[3, 3] = "2";*/