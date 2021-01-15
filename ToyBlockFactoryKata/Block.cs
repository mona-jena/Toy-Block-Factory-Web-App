using System;

namespace ToyBlockFactoryKata
{
    public class Block : IEquatable<Block>
    {
        internal Colour Colour { get; }
        internal Shape Shape { get; }
        
        public Block(Shape shape, Colour colour)
        {
            Shape = shape;
            Colour = colour;
        }

        public bool Equals(Block other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Colour == other.Colour && Shape == other.Shape;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Block) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Colour, (int) Shape);
        }
    }
}