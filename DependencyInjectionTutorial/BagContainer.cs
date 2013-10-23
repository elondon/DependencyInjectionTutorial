using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace DependencyInjectionTutorial
{
    public class BagContainer
    {
        public Bag[] Bags { get; set; }

        public BagContainer(int numberOfBags, int maxItemsPerBag)
        {
            Bags = new Bag[numberOfBags];
            for(var i = 0; i < numberOfBags; i++)
                Bags[i] = new Bag(maxItemsPerBag);
        }
    }
}