using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using IBuildObjects;

namespace DependencyInjectionTutorial
{
    public class BagContainer
    {
        public Bag[] Bags { get; set; }

        private readonly IObjectBuilder _objectBuilder;

        public BagContainer(IObjectBuilder objectBuilder, int numberOfBags)
        {
            _objectBuilder = objectBuilder;
            Bags = new Bag[numberOfBags];
            for (var i = 0; i < numberOfBags; i++)
                Bags[i] = _objectBuilder.GetInstance<Bag>();
        }
    }
}