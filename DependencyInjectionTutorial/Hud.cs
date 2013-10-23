namespace DependencyInjectionTutorial
{
    public class Hud
    {
        public IWeapon ActiveWeapon { get; protected set; }

        public Hud(IWeapon weapon)
        {
            ActiveWeapon = weapon;
        }
    }
}