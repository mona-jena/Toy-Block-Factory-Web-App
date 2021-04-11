using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryTests.TestDoubles;
using Xunit;

namespace ToyBlockFactoryTests
{
    public class OrderStorageAndRetrievalTests
    {
        private readonly ToyBlockFactory _toyBlockFactory;
        private readonly Order _emptyOrder;
        private readonly Order _unsubmittedOrder;

        public OrderStorageAndRetrievalTests()
        {
            _toyBlockFactory = new ToyBlockFactory(new LineItemsCalculatorStub());

            OrderWithDateGiven();
            OrderWithNoDateGiven();
            _emptyOrder = EmptyOrder();
            _unsubmittedOrder = OrderNotSubmitted();
        }

        [Fact]
        public void EmptyBlockOrderShouldNotBeAbleToBeSubmitted_AndThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _toyBlockFactory.SubmitOrder(_emptyOrder));
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
            var order3 = _toyBlockFactory.OrderExists("0010");
            
            Assert.True(order1);
            Assert.False(order3);
        }

        [Fact]
        public void InvalidOrderNumberThrowsException()
        {
            const string nonExistingOrder = "xxxx";

            Assert.Throws<ArgumentException>(() => _toyBlockFactory.GetOrder(nonExistingOrder));
        }

        [Fact]
        public void SubmittedOrderCantBeDeleted()
        {
            var deletedOrder = _unsubmittedOrder.OrderId;
            
            _toyBlockFactory.DeleteOrder(deletedOrder);

            Assert.Throws<ArgumentException>(() => _toyBlockFactory.GetOrder(deletedOrder));
        }
        
        private void OrderWithDateGiven()
        {
            var orderDueDate = new DateTime(2021, 1, 19);
            var customerOrder = _toyBlockFactory
                .CreateOrder("David Rudd", "1 Bob Avenue, Auckland", orderDueDate);
            customerOrder.AddBlock(Shape.Square, Colour.Red, 1);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow, 1);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            _toyBlockFactory.SubmitOrder(customerOrder);
        }

        private void OrderWithNoDateGiven()
        {
            var customerOrder2 = _toyBlockFactory
                .CreateOrder("Ryan Chen", "1 Mt Eden Road, Auckland");
            customerOrder2.AddBlock(Shape.Triangle, Colour.Yellow, 1);
            customerOrder2.AddBlock(Shape.Square, Colour.Blue, 1);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue, 1);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue, 1);
            customerOrder2.AddBlock(Shape.Circle, Colour.Blue, 1);
            _toyBlockFactory.SubmitOrder(customerOrder2);
        }
        
        private Order EmptyOrder()
        {
            return _toyBlockFactory.CreateOrder("Tony Williams", "13 Stokes Road, Auckland");
        }
        
        private Order OrderNotSubmitted()
        {
            var unsubmittedOrder = _toyBlockFactory
                .CreateOrder("Tom Knight", "20 Stokes Road, Auckland");
            unsubmittedOrder.AddBlock(Shape.Triangle, Colour.Yellow, 1);
            unsubmittedOrder.AddBlock(Shape.Square, Colour.Blue, 1);
            unsubmittedOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            unsubmittedOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            unsubmittedOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            return unsubmittedOrder;
        }
        
    }
}