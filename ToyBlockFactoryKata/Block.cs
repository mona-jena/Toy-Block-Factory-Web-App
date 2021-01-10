namespace ToyBlockFactoryKata
{
    public class Block
    {
        private Colour Colour { get; }
        private Shape Shape { get; }
        
        public Block(Shape shape, Colour colour)
        {
            Shape = shape;
            Colour = colour;
        }

    }
}