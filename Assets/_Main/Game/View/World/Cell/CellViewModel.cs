using Proxy;
using R3;
using Settings;
using UnityEngine;

namespace GameView
{
    public class CellViewModel
    {
        public readonly int CellEntityId;
        public readonly CellDataProxy CellProxy;
        public readonly CellsSettings Settings;
        public readonly ReadOnlyReactiveProperty<Vector2> Position;
        public readonly ReadOnlyReactiveProperty<char> Letter;
        
        public CellViewModel(CellDataProxy cellProxy, CellsSettings settings)
        {
            Settings = settings;
            CellProxy = cellProxy;

            CellEntityId = cellProxy.Id;
            Position = cellProxy.Position;
            Letter = cellProxy.Letter;
        }
    }
}