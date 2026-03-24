using R3;

namespace Game
{
    public class GameCycleHandler
    {
        private readonly Subject<Unit> _exitSignal;
        private readonly Subject<Unit> _replaySignal;

        public GameCycleHandler(Subject<Unit> exitSignal, Subject<Unit> replaySignal)
        {
            _exitSignal = exitSignal;
            _replaySignal = replaySignal;
        }

        public void ReplayGame()
        {
            _replaySignal.OnNext(Unit.Default);
        }
        
        public void FinishGame()
        {
            _exitSignal.OnNext(Unit.Default);
        }
    }
}