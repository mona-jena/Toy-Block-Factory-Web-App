using System;
using System.Collections.Generic;

namespace ToyBlockFactoryKata
{
    public class Order
    {
        //private Dictionary<Block, int> BlockList { get; }
        
        internal Order()
        {
            //BlockList = new Dictionary<Block, int>();
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public void AddBlock(Shape shape, Colour colour)
        {
            throw new NotImplementedException();
        }

        public string SetDueDate()
        {
            throw new NotImplementedException();
        }
    }
}