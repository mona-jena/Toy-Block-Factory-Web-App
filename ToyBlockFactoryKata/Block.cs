namespace ToyBlockFactoryKata
{
    public class Block
    {
        internal Colour Colour { get; }
        internal Shape Shape { get; }
        
        public Block(Shape shape, Colour colour)
        {
            Shape = shape;
            Colour = colour;
        }

    }
}