using System;

namespace ToyBlockFactoryKata
{
    public record Block(Shape Shape, Colour Colour)
    {
        public string Name { get; init; } = "doesn't have a name yet";
    }
}