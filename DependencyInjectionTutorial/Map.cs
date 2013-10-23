using System.Collections.Generic;

namespace DependencyInjectionTutorial
{
    public class Map
    {
        public PlayerLocator LocatePlayerService { get; set; }

        private List<Point> _playerLocations;

        public Map(PlayerLocator playerLocator)
        {
            LocatePlayerService = playerLocator;
            _playerLocations = LocatePlayerService.GetPlayerLocations();
        }
    }
}