using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBuildObjects;

namespace DependencyInjectionTutorial
{
    /// <summary>
    /// IoC/DI simple tutorial in the context of the object graph you may give a player in a video game.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var objectBuilder = GetConfiguredObjectBuilder();
            // set a breakpoint on return and run the program.

            // instanciate and configure your player without the use of IoC/DI - as if you just wanted to make a new player at some
            // random point in your code.
            var playerOne = new Player(100, new Inventory(new BagContainer(new ObjectBoss(), 5)), new Hud(new Knife()), new Map(new PlayerLocator()));

            // we can clean this up a little:

            const int STARTING_HEALTH = 100;

            var newBagContainer = new BagContainer(new ObjectBoss(), 5);
            var newInventory = new Inventory(newBagContainer);
            var startingWeapon = new Knife();
            var newHud = new Hud(startingWeapon);
            var playerLocator = new PlayerLocator();
            var playerMap = new Map(playerLocator);

            // and finally:
            var playerTwo = new Player(STARTING_HEALTH, newInventory, newHud, playerMap);

            // that is cleaner but still several places to make changes.

            var playerThree = PlayerFactory.GetConfiguredPlayer();
            // This really is a legitimate solution - but it can get messy when you have several of these factories in your project
            // and eventually the factory itself will need to be configured to have access to the components it needs to build what
            // it produces. 

            // Enter Inversion of Control and Dependency Injection. 
            var playerFour = objectBuilder.GetInstance<Player>();

            // hover over playerFour and observe the object graph is all connected. In fact, thanks to the configuration,
            // you can get any object you want from the object builder.
            var map = objectBuilder.GetInstance<Map>();

            // maybe you want to get an instance of every weapon in the game.
            var weapons = objectBuilder.GetAllInstances<IWeapon>();

            // That is just the tip of the ice berg. Once you configure the IoC tool, you have a factory in one place
            // for every object and dependencies for the entire graph are automatically set by the tool when you ask it for an instance. 
            return;
        }

        private static IObjectBuilder GetConfiguredObjectBuilder()
        {
            var objectBuilder = new ObjectBoss();
            objectBuilder.Configure(x =>
            {
                x.Add<Player>().WithCustomConstructor(new Dictionary<string, object>() { { "health", 100 } }); ;
                x.Add<Inventory>();
                x.Add<BagContainer>().WithCustomConstructor(new Dictionary<string, object>() { { "numberOfBags", 5 } });
                x.Add<Bag>().WithCustomConstructor(new Dictionary<string, object>() {{"maxBagItems", 10}});
                x.AddUsing<IWeapon, Knife>();
                x.AddUsing<IWeapon, Pistol>();
                x.Add<PlayerLocator>();
                x.Add<Map>();
            });

            return objectBuilder;
        }
    }

    static class PlayerFactory
    {
        public static Player GetConfiguredPlayer()
        {
            const int STARTING_HEALTH = 100;

            var newBagContainer = new BagContainer(new ObjectBoss(), 5);
            var newInventory = new Inventory(newBagContainer);
            var startingWeapon = new Knife();
            var newHud = new Hud(startingWeapon);
            var playerLocator = new PlayerLocator();
            var playerMap = new Map(playerLocator);

            // and finally:

            var playerTwo = new Player(STARTING_HEALTH, newInventory, newHud, playerMap);
            return playerTwo;
        }
    }
}
