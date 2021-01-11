using System.Collections.Generic;

namespace ToyBlockFactoryKata{
    public class BlockMatcher : IEqualityComparer<Block>
    {
        public bool Equals(Block x, Block y) => x?.Colour == y?.Colour && x?.Shape == y?.Shape;
        public int GetHashCode(Block obj) => (int) obj?.Colour.GetHashCode(); // what is this doing? Is this right?
    }
}
