namespace ToyBlockFactoryKata
{
    public class Block
    {
        public Block(Colour colour, Shape shape)
        {
            BlockColour = colour;
            BlockShape = shape;
        }

        private Colour BlockColour { get; }
        private Shape BlockShape { get; }
    }
}