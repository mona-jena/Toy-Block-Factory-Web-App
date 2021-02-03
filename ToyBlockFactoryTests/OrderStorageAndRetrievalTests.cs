using System;
using ToyBlockFactoryKata;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class OrderStorageAndRetrievalTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;

        public OrderStorageAndRetrievalTests()
        {
            _toyBlockFactory = new ToyBlockFactory();
            var customerOrder = _toyBlockFactory
                .CreateOrder("David Rudd", "1 Bob Avenue, Auckland");
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.SetDueDate(new DateTime(2019, 1, 19));
            _toyBlockFactory.SubmitOrder(customerOrder);
            
            var customerOrder2 = _toyBlockFactory
                .CreateOrder("Ryan Chen", "1 Mt Eden Road, Auckland");
            customerOrder2.AddBlock(Shape.Triangle, Colour.Yellow);
            customerOrder2.AddBlock(Shape.Square, Colour.Blue);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.AddBlock(Shape.Triangle, Colour.Red);
            customerOrder2.AddBlock(Shape.Triangle, Colour.Red);
            customerOrder2.AddBlock(Shape.Triangle, Colour.Yellow);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.AddBlock(Shape.Triangle, Colour.Yellow);
            customerOrder2.AddBlock(Shape.Triangle, Colour.Red);
            customerOrder2.AddBlock(Shape.Triangle, Colour.Yellow);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.SetDueDate(new DateTime(2019, 1, 30)); 
            _toyBlockFactory.SubmitOrder(customerOrder2);
        }

        [Fact]
        public void CanRetrieveOrderDetails()
        {
            var order = _toyBlockFactory.GetOrder("0001");

            Assert.Equal("David Rudd", order.Name);
            Assert.Equal("1 Bob Avenue, Auckland", order.Address);
            Assert.Equal(new DateTime(2019, 1, 19), order.DueDate);
            Assert.Equal("0001", order.OrderId);
        }

        [Fact]
        public void OrderContains1RedSquare()  //should I be clear on which order in const?
        {
            var order = _toyBlockFactory.GetOrder("0001");
            var block = new Block(Shape.Square, Colour.Red);

            var blockValue = order.BlockList[block];

            Assert.True(order.BlockList.ContainsKey(block));
            Assert.Equal(1, blockValue);
        }

        [Fact]
        public void OrderContains1YellowSquare()
        {
            var order = _toyBlockFactory.GetOrder("0001");
            var block = new Block(Shape.Square, Colour.Yellow);

            var blockValue = order.BlockList[block];

            Assert.True(order.BlockList.ContainsKey(block));
            Assert.Equal(1, blockValue);
        }

        [Fact]
        public void OrderContains2BlueTriangles()
        {
            var order = _toyBlockFactory.GetOrder("0001");
            var block = new Block(Shape.Triangle, Colour.Blue);

            var blockValue = order.BlockList[block];

            Assert.True(order.BlockList.ContainsKey(block));
            Assert.Equal(2, blockValue);
        }

        [Fact]
        public void OrderContains1BlueCircle()
        {
            var order = _toyBlockFactory.GetOrder("0001");
            var block = new Block(Shape.Circle, Colour.Blue);

            var blockValue = order.BlockList[block];

            Assert.True(order.BlockList.ContainsKey(block));
            Assert.Equal(1, blockValue);
        }

        [Fact]
        public void OrderContains2YellowCircles()
        {
            var order = _toyBlockFactory.GetOrder("0001");
            var block = new Block(Shape.Circle, Colour.Yellow);

            var blockValue = order.BlockList[block];

            Assert.True(order.BlockList.ContainsKey(block));
            Assert.Equal(2, blockValue);
        }

        [Fact]
        public void MultipleOrdersCanBeStoredAndRetrieved()
        {
            var order = _toyBlockFactory.GetOrder("0001");
            var order2 = _toyBlockFactory.GetOrder("0002");
            
            Assert.Equal("0001", order.OrderId);
            Assert.Equal("David Rudd", order.Name); //SHOULD THIS BE STORED IN VARIABLES?
            Assert.Equal("1 Bob Avenue, Auckland", order.Address);
            Assert.Equal(new DateTime(2019, 1, 19), order.DueDate);
            
            Assert.Equal("0002", order2.OrderId);
            Assert.Equal("Ryan Chen", order2.Name);
            Assert.Equal("1 Mt Eden Road, Auckland", order2.Address);
            Assert.Equal(new DateTime(2019, 1, 30), order2.DueDate);
        }

        /*var block = new Block(Shape.Circle, Colour.Blue);
        block.Colour = Colour.Red;
        var block2 = block with {Name = "tom", Colour = Colour.Yellow};*/
    }
}


//IS IT BAD TO DO MULTIPLE ASSERTS IN ONE?


//var orderManagementSystem = new OrderManagementSystem(customerDetails);
//var customerOrder = orderManagementSystem.GetOrder();
//var reportOrderManagement = new ReportOrderManagementSystem(customerOrder);
/*var pricingList = new PricingList();
pricingList.Square = 1;
pricingList.Triangle = 2;
pricingList.Circle = 1;
pricingList.Red = 1;*/

//var invoiceReport = reportOrderManagement.GenerateInvoice(customerOrder);


/*
 * new toyblock factory
 * new customer
 * system creates new order
 * get customer details
 * customer add new block each time - in the back blocks and dict of blocks will be created
 * return order
 * give back invoice
 */

/*var expectedInvoiceReport = 
        "Your invoice report has been generated:" +
        "\n" +
        "Name: David Rudd Address: 1 Bob Avenue, Auckland Due Date: 19 Jan 2019 Order #: 0001" +
        "\n" +
        "|          | Red | Blue | Yellow |" +
        "|----------|-----|------|--------|" +
        "| Square   | 1   | -    | 1      |" +
        "| Triangle | -   | 2    | -      |" +
        "| Circle   | -   | 1    | 2      |" +
        "\n" +
        "Squares 		        2 @ $1 ppi = $2" +
        "Triangles		        2 @ $2 ppi = $4" +
        "Circles			    3 @ $3 ppi = $9" +
        "Red colour surcharge   1 @ $1 ppi = $1" +
        "\n" +
        "Total                  $16";*/


/*[Theory]
[ClassData(typeof(GreetingTestData))]
public void ShouldReturnCorrectGreetingMessage_WhenGivenAListOfNames(IEnumerable<string> names, string expected)
{
    // Arrange
    var greeter = new Greeter();
    // Act
    var result = greeter.Greet(names);
    // Assert
    Assert.Equal(expected, result);
}

public class GreetingTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {new List<string> {"Jill", "Jane"}, "Hello, Jill and Jane."};
        yield return new object[]
        {
            new List<string> {"Amy", "Brian", "Charlotte"}, "Hello, Amy, Brian and Charlotte."
        };
        yield return new object[]
        {
            new List<string> {"Amy", "BRIAN", "Charlotte"}, "Hello, Amy and Charlotte. AND HELLO BRIAN!"
        };
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}*/