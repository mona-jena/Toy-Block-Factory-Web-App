using System;
using ToyBlockFactoryKata;
using ToyBlockFactoryKata.Orders;
using ToyBlockFactoryKata.PricingStrategy;

namespace ToyBlockFactoryTests
{
    public class ToyBlockFactoryFixture
    {
        public static ToyBlockFactory create()
        {
            var toyBlockFactory = new ToyBlockFactory(new LineItemsCalculator());
            
            var david = "David Rudd";  
            var davidAddress = "1 Bob Avenue, Auckland";
            var davidOrderDueDate = new DateTime(2021, 1, 19);
            var davidOrder = toyBlockFactory.CreateOrder(david, davidAddress, davidOrderDueDate);
            davidOrder.AddBlock(Shape.Square, Colour.Red, 1);
            davidOrder.AddBlock(Shape.Square, Colour.Yellow, 1);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            davidOrder.AddBlock(Shape.Triangle, Colour.Blue, 1);
            davidOrder.AddBlock(Shape.Circle, Colour.Blue, 1);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            davidOrder.AddBlock(Shape.Circle, Colour.Yellow, 1);
            toyBlockFactory.SubmitOrder(davidOrder);
            
            return toyBlockFactory;
        }

    }
}