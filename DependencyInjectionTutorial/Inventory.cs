namespace DependencyInjectionTutorial
{
    public class Inventory
    {
        public BagContainer BagContainer { get; set; }

        public Inventory(BagContainer bagContainer)
        {
            BagContainer = bagContainer;
        }
    }
}