namespace ToyBlockFactoryKata
{
    public class Block
    {
        private Colour BlockColour { get; }
        private Shape BlockShape { get; }
        
        public Block(Shape shape, Colour colour)
        {
            BlockShape = shape;
            BlockColour = colour;
        }

    }
}