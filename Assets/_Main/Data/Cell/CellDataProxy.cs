using Data;
using R3;
using UnityEngine;

namespace Proxy
{
    public class CellDataProxy
    {
        public int Id { get; }
        public string Key { get; }
        public CellData Origin { get; }
        
        public ReactiveProperty<char> Letter { get; }
        public ReactiveProperty<Vector2> Position { get; }

        public CellDataProxy(CellData origin)
        {
            Id = origin.entityID;
            Key = origin.key;
            Origin = origin;
            
            Letter = new ReactiveProperty<char>(origin.letter);
            Letter.Skip(1).Subscribe(letter => origin.letter = letter);
            
            Position = new ReactiveProperty<Vector2>(origin.position);
            Position.Skip(1).Subscribe(pos => origin.position = pos);
        }
    }
}