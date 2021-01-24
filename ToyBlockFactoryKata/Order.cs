using System;
using System.Collections.Generic;
using System.Globalization;

namespace ToyBlockFactoryKata
{
    public class Order
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        
        //private DateTime _dueDate;
        //public DateTime DueDate { get => _dueDate; set => _dueDate = Convert.ToDateTime(value); }

        public string DueDate { get; set; }

        public string OrderId { get; set; }
        public Dictionary<Block, int> BlockList { get; }
        
        internal Order()
        {
            BlockList = new Dictionary<Block, int>();
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