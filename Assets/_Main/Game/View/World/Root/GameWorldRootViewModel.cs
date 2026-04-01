using Game;
using ObservableCollections;

namespace GameView
{
    public class GameWorldRootViewModel
    {
        public readonly ObjectSpawner Spawner;
        public readonly IObservableCollection<CellViewModel> AllCells;

        public GameWorldRootViewModel(CellBuilder builder, ObjectSpawner spawner)
        {
            Spawner = spawner;
            AllCells = builder.AllCells;
        }
    }
}