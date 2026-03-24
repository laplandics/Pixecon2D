using Game;

namespace GameView
{
    public class PopupViewModel
    {
        public readonly string PopupPath;
        public readonly GameCycleHandler CycleHandler;

        public PopupViewModel(string popupPath, GameCycleHandler cycleHandler)
        {
            PopupPath = popupPath;
            CycleHandler = cycleHandler;
        }
    }
}