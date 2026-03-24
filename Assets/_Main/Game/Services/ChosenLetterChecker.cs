using Cmd;
using GameView;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class ChosenLetterChecker
    {
        private readonly ICommandProcessor _cmd;

        public ChosenLetterChecker(ObservableList<VocabularyDataProxy> vocabulariesProxy, ICommandProcessor cmd)
        {
            _cmd = cmd;
            foreach (var vocabularyDataProxy in vocabulariesProxy)
            {
                foreach (var entryDataProxy in vocabularyDataProxy.VocabularyEntries)
                { entryDataProxy.IsCurrent.Subscribe(current =>
                    { if (current) UpdateCurrentEntry(entryDataProxy); }); }
            }
        }

        private void UpdateCurrentEntry(VocabularyEntryDataProxy newEntryDataProxy)
        {
            _cmd.RegisterHandler(new CmdCheckLetterHandler(newEntryDataProxy));
        }

        public bool CheckLetter(CellViewModel checkingCell)
        {
            return _cmd.Process(new CmdCheckLetter(checkingCell));
        }
    }
}