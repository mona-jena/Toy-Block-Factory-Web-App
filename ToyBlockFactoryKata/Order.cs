using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace ToyBlockFactoryKata
{
    public class Order
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string DueDate { get; set; }
        public string OrderNumber { get; set; }
        public Dictionary<Block, int> BlockList { get; }
        
        internal Order()
        {
            BlockList = new Dictionary<Block, int>();
        }

        public void AddBlock(Shape shape, Colour colour)
        {
            var block = new Block(shape, colour);
            var blockQuantity = 0;
            if (BlockList.ContainsKey(block))
                BlockList[block] = ++blockQuantity;
            else
                BlockList.Add(block, 1);
        }
        
        
        
    }
}