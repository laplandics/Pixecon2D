using Cmd;

namespace Game
{
    public class CmdChangeCurrentEntryHandler : ICommandHandler<CmdChangeCurrentEntry>
    {
        public bool Handle(CmdChangeCurrentEntry command)
        {
            var vocab = command.CurrentVocabulary;
            var entry = command.CurrentEntry;
            entry.IsCompleted.Value = true;
            
            var currentEntryIndex = vocab.VocabularyEntries.IndexOf(entry);
            return currentEntryIndex < vocab.VocabularyEntries.Count - 1;
        }
    }
}