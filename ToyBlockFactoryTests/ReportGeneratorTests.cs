using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class ReportGeneratorTests 
    {
        private readonly string customerName;
        private readonly string customerAddress;
        private readonly ToyBlockFactory _toyBlockFactory;

        public ReportGeneratorTests()
        {
            _toyBlockFactory = new ToyBlockFactory();
            customerName = "David Rudd";                                                                                                                             
            customerAddress = "1 Bob Avenue, Auckland";
            var customerOrder = _toyBlockFactory.CreateOrder(customerName, customerAddress);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.SetDueDate("19 Jan 2019");
            _toyBlockFactory.SubmitOrder(customerOrder);
            
            var customer2Name = "Steve Richards";                                                                                                                             
            var customer2Address = "27 Valley Road, Auckland";
            var customer2Order = _toyBlockFactory.CreateOrder(customer2Name, customer2Address);
            customer2Order.AddBlock(Shape.Square, Colour.Yellow);
            customer2Order.AddBlock(Shape.Square, Colour.Blue);
            customer2Order.AddBlock(Shape.Circle, Colour.Blue);
            customer2Order.AddBlock(Shape.Circle, Colour.Blue);
            customer2Order.SetDueDate("15 Feb 2019");
            _toyBlockFactory.SubmitOrder(customer2Order);
            
            var customer3Name = "Tony Williams";                                                                                                                             
            var customer3Address = "13 Stokes Road, Auckland";
            var customer3Order = _toyBlockFactory.CreateOrder(customer3Name, customer3Address);
            customer3Order.SetDueDate("21 Nov 2019");
            _toyBlockFactory.SubmitOrder(customer3Order);
        }
        
        
        [Theory]
        [InlineData("Square", 2, 1)]
        [InlineData("Circle", 3, 3)]
        [InlineData("Red colour surcharge", 1, 1)]
        [InlineData("Triangle", 2, 2)]
        public void NAMETHIS(string description, int quantity, decimal price)
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(ReportType.Invoice, invoice.ReportType); 
            Assert.Equal(customerName, invoice.Name);
            Assert.Equal(customerAddress, invoice.Address);
            Assert.Equal(new DateTime(2019, 1, 19), invoice.DueDate); 
            Assert.Equal(orderId, invoice.OrderId);

            var invoiceLine = invoice.LineItems.SingleOrDefault(l => l.Description == description);
            Assert.NotNull(invoiceLine); //understand these 2 lines
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
    
            Assert.Equal(16.00m, invoice.Total);
        }

        [Fact]
        public void IsInvoice()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(ReportType.Invoice, invoice.ReportType); 
        }

        [Fact]
        public void MatchesCustomerName()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(customerName, invoice.Name);
        }
        
        [Fact]
        public void MatchesCustomerAddress()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(customerAddress, invoice.Address);
        }
        
        [Fact]
        public void MatchesDueDate()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(new DateTime(2019, 1, 19), invoice.DueDate);
        }
        
        [Fact]
        public void MatchesOrderId()
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
        public void HasLineItems(string description, int quantity, decimal price, decimal total)
        {
            const string orderId = "0001";
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            var invoiceLine = invoice.LineItems.SingleOrDefault(l => l.Description == description);
            
            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
            Assert.Equal(total, invoiceLine.Total);
        }
        
        [Fact]
        public void MatchesTotal()
        {
            const string orderId = "0001";

            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            Assert.Equal(16.00m, invoice.Total);
        }
        
        
        [Theory]
        [InlineData("Square", 2, 1, 2)]
        [InlineData("Circle", 3, 3, 9)]
        public void RENAME(string description, int quantity, decimal price, decimal total)
        {
            const string orderId = "0001";
            var invoice = _toyBlockFactory.GetInvoiceReport(orderId);
            
            var invoiceLine = invoice.LineItems.SingleOrDefault(l => l.Description == description);
            
            Assert.NotNull(invoiceLine);
            Assert.Equal(description, invoiceLine.Description);
            Assert.Equal(quantity, invoiceLine.Quantity);
            Assert.Equal(price, invoiceLine.Price);
            Assert.Equal(total, invoiceLine.Total);
        }
        
        [Fact]
        public void EmptyOrderReturnsEmptyInvoiceLines()
        {
            var invoice = _toyBlockFactory.GetInvoiceReport("0003");
            
            Assert.Empty(invoice.LineItems);
        }

        [Fact]
        public void GenerateReportShouldReturnOrderTable()
        {
            // TableColumn =  List<(string, int)
           // List<(Shape, List<TableRow>)> orderTable = new();
            List<TableRow> orderTable = new();
            orderTable.Add(new(Shape.Square, 
                new List<TableColumn>{new (Colour.Red, 1), 
                                                   new (Colour.Yellow, 1)}));
            orderTable.Add(new(Shape.Triangle, 
                new List<TableColumn>{new(Colour.Blue, 2)}));
            orderTable.Add(new(Shape.Circle, 
                new List<TableColumn> {new(Colour.Blue, 1), 
                                                    new(Colour.Yellow, 2)}));

            var invoice = _toyBlockFactory.GetInvoiceReport("0001");

            Assert.Equal(orderTable[0], invoice.OrderTable[0]);
            Assert.Equal(orderTable[1], invoice.OrderTable[1]);
            Assert.Equal(orderTable[2], invoice.OrderTable[2]);
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