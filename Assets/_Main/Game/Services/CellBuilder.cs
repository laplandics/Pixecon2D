using System.Collections.Generic;
using Cmd;
using GameView;
using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class CellBuilder
    {
        private readonly ICommandProcessor _cmd;
        private readonly Dictionary<int, CellViewModel> _cellsMap = new();
        
        private readonly ObservableList<CellViewModel> _allCells = new();
        public IObservableCollection<CellViewModel> AllCells => _allCells;
        
        public CellBuilder(IObservableCollection<CellDataProxy> cells, ICommandProcessor cmd)
        {
            _cmd = cmd;

            foreach (var cellProxy in cells)
            { CreateCellViewModel(cellProxy); }

            cells.ObserveAdd().Subscribe(addEvent =>
            { CreateCellViewModel(addEvent.Value); });
            
            cells.ObserveRemove().Subscribe(removeEvent =>
            { DestroyCellViewModel(removeEvent.Value); });
        }
        
        public bool AddCell(Vector2 position)
        {
            var createCommand = new CmdCreateCell(position);
            var result = _cmd.Process(createCommand);
            return result;
        }

        public bool RemoveCell(CellDataProxy proxy)
        {
            _cmd.Process(new CmdDestroyCell(proxy));
            return true;
        }

        private void CreateCellViewModel(CellDataProxy proxy)
        {
            var view = new CellViewModel(proxy);
            _allCells.Add(view);
            _cellsMap[proxy.Id] = view;
        }

        private void DestroyCellViewModel(CellDataProxy proxy)
        {
            if (!_cellsMap.TryGetValue(proxy.Id, out var view)) return;
            _allCells.Remove(view);
            _cellsMap.Remove(proxy.Id);
        }
    }
}