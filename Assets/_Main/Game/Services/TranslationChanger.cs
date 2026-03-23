using Cmd;
using ObservableCollections;
using Proxy;
using R3;

namespace Game
{
    public class TranslationChanger
    {
        private readonly ICommandProcessor _cmd;

        public ObservableList<VocabularyEntryDataProxy> AllEntries { get; } = new();

        public TranslationChanger(ICommandProcessor cmd, IObservableCollection<VocabularyDataProxy>
            vocabulariesDataProxy)
        {
            _cmd = cmd;
            foreach (var vocab in vocabulariesDataProxy)
            { foreach (var entry in vocab.VocabularyEntries) { AllEntries.Add(entry); } }

            foreach (var entryDataProxy in AllEntries)
            { entryDataProxy.IsDone.Skip(1).Subscribe(_ => ChangeCurrentTranslation()); }
        }

        private void ChangeCurrentTranslation()
        {
            _cmd.Process(new CmdChangeCurrentTranslation());
        }
    }
}