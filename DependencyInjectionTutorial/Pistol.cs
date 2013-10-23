using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionTutorial
{
    public class Pistol : IWeapon
    {
        public string Name { get { return "Pistol"; } }

        public Pistol()
        {

        }
    }
}
