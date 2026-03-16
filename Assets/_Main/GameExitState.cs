namespace Game
{
    public class GameExitState
    {
        public Menu.MenuEntryState MenuEntryState { get; }

        public GameExitState(Menu.MenuEntryState menuEntryState)
        { MenuEntryState = menuEntryState; }
    }
}