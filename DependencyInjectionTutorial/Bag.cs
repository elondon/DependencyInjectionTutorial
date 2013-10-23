using System.Collections.Generic;

namespace DependencyInjectionTutorial
{
    public class Bag
    {
        public int MaxBagItems { get; set; }
        public List<object> Items { get; protected set; }

        public Bag(int maxBagItems)
        {
            MaxBagItems = maxBagItems;
            Items = new List<object>();
        }
    }
}