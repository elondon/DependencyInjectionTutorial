using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionTutorial
{
    public class Player
    {
        private readonly Inventory _inventory;
        private readonly Hud _hud;
        private readonly Map _map;

        private int _health;

        public Player(int health, Inventory inventory, Hud hud, Map map)
        {
            _health = health;
            _inventory = inventory;
            _hud = hud;
            _map = map;
        }
    }
}
