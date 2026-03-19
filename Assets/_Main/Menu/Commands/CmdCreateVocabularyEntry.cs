using Cmd;

namespace Menu
{
    public class CmdCreateVocabularyEntry : ICommand
    {
        public readonly int VocabId;
        public readonly string Word;
        public readonly string Translation;
        public readonly bool IsDone;
        
        public CmdCreateVocabularyEntry(int vocabId, string word, string translation, bool isDone = false)
        {
            VocabId = vocabId;
            Word = word;
            Translation = translation;
            IsDone = isDone;
        }
    }
}