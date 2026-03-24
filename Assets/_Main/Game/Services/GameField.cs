using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class GameField
    {
        private readonly GameGrid _grid;
        private readonly CellBuilder _builder;
        private readonly CellLetterSetter _setter;
        private readonly CellClickHandler _clickHandler;
        private readonly ChosenLetterChecker _chosenLetterChecker;

        private VocabularyEntryDataProxy _currentEntry;
        
        public GameField
        (
            GameGrid grid,
            CellBuilder builder,
            CellLetterSetter setter,
            CellClickHandler clickHandler,
            ChosenLetterChecker chosenLetterChecker,
            IObservableCollection<VocabularyDataProxy> vocabularies)
        {
            _grid = grid;
            _builder = builder;
            _setter = setter;
            _clickHandler = clickHandler;
            _chosenLetterChecker = chosenLetterChecker;
            
            _builder.AllCells.ObserveRemove().Subscribe(removeEvent =>
            { UpdateField(removeEvent.Value.Position.CurrentValue); });
        }

        public void CreateField()
        {
            foreach (var gridPosition in _grid.GridPositions)
            { var worldPos = _grid.GridToWorld(gridPosition); _builder.AddCell(worldPos); }
        }

        private void UpdateField(Vector2 position)
        {
            _builder.AddCell(position);
        }
    }
}