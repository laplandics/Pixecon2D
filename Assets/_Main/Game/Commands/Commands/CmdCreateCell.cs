using UnityEngine;
using Cmd;

namespace Game
{
    public class CmdCreateCell : ICommand
    {
        public Vector2 Position;
        
        public CmdCreateCell(Vector2 position)
        {
            Position = position;
        }
    }
}