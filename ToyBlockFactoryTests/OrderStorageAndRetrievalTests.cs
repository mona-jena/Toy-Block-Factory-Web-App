using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class OrderStorageAndRetrievalTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;
        private readonly Order _customer3Order;

        public OrderStorageAndRetrievalTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new TestPricingCalculator());

            OrderWithDateGiven();
            OrderWithNoDateProvided();
            _customer3Order = EmptyOrder();
        }
        
        private void OrderWithDateGiven()
        {
            var orderDueDate = new DateTime(2021, 1, 19);
            var customerOrder = _toyBlockFactory
                .CreateOrder("David Rudd", "1 Bob Avenue, Auckland", orderDueDate);
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            _toyBlockFactory.SubmitOrder(customerOrder);
        }

        private void OrderWithNoDateProvided()
        {
            var customerOrder2 = _toyBlockFactory
                .CreateOrder("Ryan Chen", "1 Mt Eden Road, Auckland");
            customerOrder2.AddBlock(Shape.Triangle, Colour.Yellow);
            customerOrder2.AddBlock(Shape.Square, Colour.Blue);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue);
            _toyBlockFactory.SubmitOrder(customerOrder2);
        }
        
        private Order EmptyOrder()
        {
            var customer3Name = "Tony Williams";
            var customer3Address = "13 Stokes Road, Auckland";
            return _toyBlockFactory.CreateOrder(customer3Name, customer3Address);
        }
        
        
        [Fact]
        public void EmptyBlockOrderShouldNotBeAbleToBeSubmittedAndReturnEmptyId()
        {
            var orderId = _toyBlockFactory.SubmitOrder(_customer3Order);
            
            Assert.Equal(string.Empty, orderId);
        }
        

        [Fact]
        public void CanRetrieveOrderDetails()
        {
            var order = _toyBlockFactory.GetOrder("0001");

            Assert.Equal("David Rudd", order.Name);
            Assert.Equal("1 Bob Avenue, Auckland", order.Address);
            Assert.Equal(new DateTime(2021, 1, 19), order.DueDate);
            Assert.Equal("0001", order.OrderId);
        }

        [Fact]
        public void OrderContains1RedSquare() 
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
            Assert.Equal("0002", order2.OrderId);
        }

        [Fact]
        public void ChecksIfOrderExists()
        {
            var order1 = _toyBlockFactory.OrderExists("0001");
            var order3= _toyBlockFactory.OrderExists("0003");
            
            Assert.True(order1);
            Assert.False(order3);
        }

        [Fact]
        public void InvalidOrderNumberThrowsException()
        {
            const string nonExistingOrder = "0003";

            Assert.Throws<ArgumentException>(() => _toyBlockFactory.GetOrder(nonExistingOrder));
        }
        
    }
}


/*
 * new toyblock factory
 * new customer
 * system creates new order
 * get customer details
 * customer add new block each time - in the back blocks and dict of blocks will be created
 * return order
 * give back invoice
 * https://github.com/monajena27/Toy-Block-Factory/blob/4376343c8965954f24456c58b8ba30f24a3f7e0d/ToyBlockFactoryTests/OrderManagementSystemTests.cs
 */
 