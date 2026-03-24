using Cmd;
using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class TranslationChanger
    {
        private readonly ICommandProcessor _cmd;
        private readonly GamePopupHandler _popupHandler;

        public ObservableList<VocabularyEntryDataProxy> AllEntries { get; } = new();

        public TranslationChanger(
            ICommandProcessor cmd,
            IObservableCollection<VocabularyDataProxy> vocabulariesDataProxy,
            GamePopupHandler popupHandler)
        {
            _cmd = cmd;
            _popupHandler = popupHandler;

            foreach (var vocab in vocabulariesDataProxy)
            {
                foreach (var entry in vocab.VocabularyEntries)
                {
                    AllEntries.Add(entry);
                }
            }

            foreach (var entryDataProxy in AllEntries)
            {
                entryDataProxy.IsDone.Subscribe(done =>
                { if (!done) return; ChangeCurrentTranslation(); });
            }
        }

        private void ChangeCurrentTranslation()
        {
            _cmd.Process(new CmdChangeCurrentTranslation());
            Debug.Log("Translation changed");
        }
    }
}