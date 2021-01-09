using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class Order
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string DueDate { get; set; }
        public int OrderNumber { get; set; }
        public Dictionary<Block, int> BlockList { get; }
        
        internal Order()
        {
            //BlockList = new Dictionary<Block, int>();
        }

        public void AddBlock(Shape shape, Colour colour)
        {
            throw new NotImplementedException();
        }

        public string SetDueDate(string dueDate)
        {
            throw new NotImplementedException();
        }
    }
}