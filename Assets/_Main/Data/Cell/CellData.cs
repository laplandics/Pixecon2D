using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class CellData : EntityData
    {
        public string key;

        public char letter;
        public Vector2 position;
    }
}