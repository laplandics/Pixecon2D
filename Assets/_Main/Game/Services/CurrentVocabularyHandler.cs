using System.Linq;
using Cmd;
using ObservableCollections;
using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class CurrentVocabularyHandler
    {
        private readonly IObservableCollection<VocabularyDataProxy> _vocabularies;
        private readonly ICommandProcessor _cmd;
        
        public ReactiveProperty<VocabularyDataProxy> CurrentVocabulary { get; private set; }
        public ReactiveProperty<VocabularyEntryDataProxy> CurrentVocabularyEntry { get; private set; }
        public ReactiveProperty<char> LastCorrectLetter { get; private set; }
        
        public CurrentVocabularyHandler(IObservableCollection<VocabularyDataProxy> vocabularies, ICommandProcessor cmd)
        {
            _cmd = cmd;
            _vocabularies = vocabularies;
            CurrentVocabulary = new ReactiveProperty<VocabularyDataProxy>(vocabularies.ElementAt(0));
            CurrentVocabularyEntry = new ReactiveProperty<VocabularyEntryDataProxy>(
                CurrentVocabulary.Value.VocabularyEntries.ElementAt(0));
            LastCorrectLetter = new ReactiveProperty<char>();
        }

        public bool ChangeLastCorrectLetter(int letterIndex)
        {
            var word = CurrentVocabularyEntry.Value.Word.Value;
            var letter = word[letterIndex];
            LastCorrectLetter.Value = letter;
            if (letterIndex < word.Length - 1) return true;
            
            if (ChangeCurrentEntry()) return false;
            if (ChangeCurrentVocabulary()) return false;
            
            Debug.LogWarning("No more uncompleted vocabularies");
            return false;
        }

        private bool ChangeCurrentEntry()
        {
            var entry = CurrentVocabularyEntry.Value;
            var vocab = CurrentVocabulary.Value;
            var command = new CmdChangeCurrentEntry(vocab, entry);
            var result = _cmd.Process(command);
            if (!result) return false;
            var nextEntry = vocab.VocabularyEntries.FirstOrDefault(e => !e.IsCompleted.Value);
            SetNewCurrentEntry(nextEntry);
            
            return true;
        }

        private bool ChangeCurrentVocabulary()
        {
            var vocab = CurrentVocabulary.Value;
            var command = new CmdChangeCurrentVocabulary(vocab);
            var result = _cmd.Process(command);
            if (!result) return false;
            var nextVocab = _vocabularies.FirstOrDefault(v => !v.IsCompleted.Value);
            SetNewCurrentVocabulary(nextVocab);
            
            return true;
        }
        
        private void SetNewCurrentEntry(VocabularyEntryDataProxy newCurrentEntry)
        {
            Debug.Log("Current entry changed. New entry: " +
                      $"translation - {newCurrentEntry.Translation}, word - {newCurrentEntry.Word}");
            CurrentVocabularyEntry.Value = newCurrentEntry;
        }
        
        private void SetNewCurrentVocabulary(VocabularyDataProxy newCurrentVocabulary)
        {
            Debug.Log($"Current vocabulary changed. New vocabulary title: {newCurrentVocabulary.Title}");
            CurrentVocabulary.Value = newCurrentVocabulary;
        }
    }
}