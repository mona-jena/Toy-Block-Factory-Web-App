using System;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryTests
{
    public class ToyBlockFactoryFixture : IDisposable
    {
        public ToyBlockFactory Factory { get; private set; }

        public ToyBlockFactoryFixture()
        {
            Factory = new ToyBlockFactory();
            var customerOrder = Factory.CreateOrder("David Rudd", "1 Bob Avenue, Auckland");
            customerOrder.AddBlock(Shape.Square, Colour.Red);
            customerOrder.AddBlock(Shape.Square, Colour.Yellow);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Triangle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Blue);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.AddBlock(Shape.Circle, Colour.Yellow);
            customerOrder.DueDate = "19 Jan 2019";
            Factory.SubmitOrder(customerOrder);
        }

        public void Dispose()
        {
            Factory  = null; 
                                    //IS THIS FINE??
        }

    }
}