using Game;
using ObservableCollections;

namespace GameView
{
    public class GameWorldRootViewModel
    {
        public readonly IObservableCollection<CellViewModel> AllCells;

        public GameWorldRootViewModel(CellBuilder builder)
        {
            AllCells = builder.AllCells;
        }
    }
}