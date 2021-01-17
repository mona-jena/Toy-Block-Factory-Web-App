using System;
using System.Collections.Generic;
using ToyBlockFactoryKata;

namespace ToyBlockFactoryTests
{
    public class ToyBlockFactoryFixture : IDisposable
    {
        public ToyBlockFactory Factory { get; private set; }

        public ToyBlockFactoryFixture()
        {
            Factory = new ToyBlockFactory();
        }

        public void Dispose()          
        {
            Factory = null;         //IS THIS FINE??
                                    
        }
        
    }
}