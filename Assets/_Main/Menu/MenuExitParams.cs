namespace Menu
{
    public class MenuExitParams
    {
        public Game.GameEntryParams GameEntryParams { get; }

        public MenuExitParams(Game.GameEntryParams gameEntryParams)
        {
            GameEntryParams = gameEntryParams;
        }
    }
}