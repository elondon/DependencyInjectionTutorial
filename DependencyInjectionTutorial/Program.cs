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
            var playerOne = new Player(100, new Inventory(new BagContainer(5, 10)), new Hud(new Knife()), new Map(new PlayerLocator()));

            // and each time you need to change how one of these objects is configured, you need to find every line like this and change it.
            // hopefully, you don't make any mistakes. For example, you decide a player is going to start with 90 health instead of 100. Or 
            // you want players to start with a pistol instead of a knife. Or perhaps your player locator for placing players on the map
            // has drastically changed.

            // we can clean this up a little:

            const int STARTING_HEALTH = 100;

            var newBagContainer = new BagContainer(5, 10);
            var newInventory = new Inventory(newBagContainer);
            var startingWeapon = new Knife();
            var newHud = new Hud(startingWeapon);
            var playerLocator = new PlayerLocator();
            var playerMap = new Map(playerLocator);

            // and finally:
            var playerTwo = new Player(STARTING_HEALTH, newInventory, newHud, playerMap);

            // that is cleaner but still several places to make changes. The old time standard as far as solutions to this problem
            // is the factory pattern. A class that knows how to configure a player and can produce them whenever you need them.
            // Nice because now we've moved all of the configuration code to 1 place. Maintenence is much easier.

            var playerThree = PlayerFactory.GetConfiguredPlayer();

            // This really is a legitimate solution - but it can get messy when you have several of these factories in your project
            // and eventually the factory itself will need to be configured to have access to the components it needs to build what
            // it produces. 

            // Enter Inversion of Control and Dependency Injection. A dependency is just an object that another object needs to
            // operate properly. Since the player class needs an Inventory, Hud, and Map to function properly, those objects
            // are said to be dependencies of Player.

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
                x.Add<BagContainer>().WithCustomConstructor(new Dictionary<string, object>() { { "numberOfBags", 5 }, { "maxItemsPerBag", 10 } });
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

            var newBagContainer = new BagContainer(5, 10);
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
