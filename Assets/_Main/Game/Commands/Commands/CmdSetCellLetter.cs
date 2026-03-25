using Cmd;
using Proxy;

namespace Game
{
    public class CmdSetCellLetter : ICommand
    {
        public readonly string CurrentWord;
        public readonly CellDataProxy Cell;

        public CmdSetCellLetter(CellDataProxy cell, string currentWord)
        {
            CurrentWord = currentWord;
            Cell = cell;
        }
    }
}