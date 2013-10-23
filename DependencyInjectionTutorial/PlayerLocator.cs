using System.Collections.Generic;
using System.Reflection;

namespace DependencyInjectionTutorial
{
    public class PlayerLocator
    {
        public PlayerLocator()
        {
            
        }

        public List<Point> GetPlayerLocations()
        {
            return new List<Point>();
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}