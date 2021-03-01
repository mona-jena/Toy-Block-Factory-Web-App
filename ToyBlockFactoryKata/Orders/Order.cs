using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata.Orders
{
    public record Order
    {
        private Dictionary<Shape, int> _shapeQuantities = new();

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
        }
        
        /*private void BlockListIterator()
        {
            foreach (var block in BlockList) 
                CalculateShapeQuantity(block.Key.Shape, block.Value);
        }*/

        internal Dictionary<Shape, int> shapeQuantities 
        {
            get
            {
                foreach (var block in BlockList)
                {
                    if (_shapeQuantities.TryAdd(block.Key.Shape, block.Value)) continue;
                    _shapeQuantities[block.Key.Shape] += block.Value;
                }
                return _shapeQuantities;
            }
           
        } 
        /*private void CalculateShapeQuantity()
        {
            foreach (var block in BlockList)
            {
                if (shapeQuantities.TryAdd(block.Key.Shape, block.Value)) return;
                shapeQuantities[block.Key.Shape] += block.Value;
            }
        } */
    }
}