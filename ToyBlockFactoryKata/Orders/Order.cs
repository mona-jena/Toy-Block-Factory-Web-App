using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata.Orders
{
    public record Order
    {

        public Order(string customerName, string customerAddress, DateTime date, string orderId)
        {
            Name = customerName;
            Address = customerAddress;
            DueDate = date;
            OrderId = orderId;
        }

        public bool IsDeleted { get; set; }
        internal bool IsSubmitted { get; set; }
        public string Name { get; }
        public string Address { get; }
        public string OrderId { get; }
        public Dictionary<Block, int> BlockList { get; } = new();
        public DateTime DueDate { get; }
        internal Dictionary<Shape, int> shapeQuantities { get; } = new();


        public void AddBlock(Shape shape, Colour colour, int quantity)
        {
            var block = new Block(shape, colour);
            if (BlockList.ContainsKey(block))
                BlockList[block] += quantity;
            else
                BlockList.Add(block, quantity);

            AddShapeQuantity(block.Shape, quantity);
        }

        private void AddShapeQuantity(Shape blockShape, int blockQuantity)
        {
            if (shapeQuantities.TryGetValue(blockShape, out _))
                shapeQuantities[blockShape] += blockQuantity;
            else
                shapeQuantities.Add(blockShape, blockQuantity);
        }
    }
}