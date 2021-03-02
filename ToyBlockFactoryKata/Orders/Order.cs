using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata.Orders
{
    public record Order
    {
        public Order(string customerName, string customerAddress)
            : this(customerName, customerAddress, DateTime.Today.AddDays(7))
        {
        }

        public Order(string customerName, string customerAddress, DateTime date)
        {
            Name = customerName;
            Address = customerAddress;
            DueDate = date;
        }

        public string Name { get; }
        public string Address { get; }
        public string OrderId { get; init; }
        public Dictionary<Block, int> BlockList { get; } = new();
        public DateTime DueDate { get; }
        
       
        
        public void AddBlock(Shape shape, Colour colour)
        {
            var block = new Block(shape, colour);
            if (BlockList.TryGetValue(block, out var blockQuantity))
                BlockList[block] = ++blockQuantity;
            else
                BlockList.Add(block, 1);
            
            AddShapeQuantity(block.Shape);
        }
        //could generate after each block is added??
        
        internal Dictionary<Shape, int> shapeQuantities { get; } = new();

        private void AddShapeQuantity(Shape blockShape)
        {
            if (shapeQuantities.TryGetValue(blockShape, out var shapeQuantity))
                shapeQuantities[blockShape] = ++shapeQuantity;
            else
                shapeQuantities.Add(blockShape, 1);
        }
        
        /*internal Dictionary<Shape, int> shapeQuantities
        {
            get
            {
                foreach (var block in BlockList)
                {
                    if (shapeQuantities.TryAdd(block.Key.Shape, block.Value)) continue;
                    shapeQuantities[block.Key.Shape] += block.Value;
                }

                return shapeQuantities;
            }
        }*/
    }
}