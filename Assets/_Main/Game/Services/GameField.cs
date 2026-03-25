using System.Linq;
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
        private readonly CellLetterSetter _letterSetter;
        private readonly CellClickHandler _clickHandler;
        private readonly CurrentVocabularyHandler _currentVocabularyHandler;

        private VocabularyEntryDataProxy _currentEntry;
        
        public GameField(GameGrid grid, CellBuilder builder, CellLetterSetter letterSetter,
            CellClickHandler clickHandler, CurrentVocabularyHandler currentVocabularyHandler)
        {
            _grid = grid;
            _builder = builder;
            _letterSetter = letterSetter;
            _clickHandler = clickHandler;
            _currentVocabularyHandler = currentVocabularyHandler;
            
            _builder.AllCells.ObserveRemove().Subscribe(cell => SetCell(cell.Value.Position.CurrentValue));
            
            _currentVocabularyHandler.CurrentVocabularyEntry.Skip(1).Subscribe(_ => ClearField());
        }

        public void CreateField()
        {
            var shufflePositions = _grid.GridPositions.OrderBy(_ => Random.value).ToList();
            foreach (var gridPosition in shufflePositions)
            { SetCell(_grid.GridToWorld(gridPosition)); }
        }

        private void ClearField()
        {
            for (var i = _builder.AllCells.Count - 1; i >= 0; i--)
            { _builder.RemoveCell(_builder.AllCells.ElementAt(i).CellProxy); }
        }
        
        private void SetCell(Vector2 worldPos)
        {
            _builder.AddCell(worldPos);
        }
    }
}