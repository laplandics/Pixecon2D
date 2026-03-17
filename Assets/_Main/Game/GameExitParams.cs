namespace Game
{
    public class GameExitParams
    {
        public Menu.MenuEntryParams MenuEntryParams { get; }

        public GameExitParams(Menu.MenuEntryParams menuEntryParams)
        {
            MenuEntryParams = menuEntryParams;
        }
    }
}