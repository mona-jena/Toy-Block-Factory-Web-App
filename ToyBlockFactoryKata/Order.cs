using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public record Order
    {
        public string Name { get; }
        public string Address { get; }
        public string OrderId { get; init; }  //if i make this init, can't set in OrderGenerator
        public Dictionary<Block, int> BlockList { get; } = new();
        public DateTime DueDate { get; }

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

        public void AddBlock(Shape shape, Colour colour)
        {
            var block = new Block(shape, colour);
            if (BlockList.TryGetValue(block, out var blockQuantity))
                BlockList[block] = ++blockQuantity;
            else
                BlockList.Add(block, 1);
        }
    }
}