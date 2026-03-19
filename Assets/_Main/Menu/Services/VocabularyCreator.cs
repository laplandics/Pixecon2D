using ObservableCollections;

namespace Menu
{
    public class VocabularyCreator
    {
        private readonly Cmd.ICommandProcessor _commandProcessor;


        public VocabularyCreator(Cmd.ICommandProcessor commandProcessor,
            ObservableList<Proxy.VocabularyDataProxy> vocabulariesProxy)
        {
            _commandProcessor = commandProcessor;
            GetVocabularies = vocabulariesProxy;
        }

        public void CreateVocabulary(string title)
        {
            _commandProcessor.Process(new CmdCreateVocabulary(title));
        }

        public void CreateNewVocabularyEntry(int vocabId, string word, string translation, bool isDone = false)
        {
            _commandProcessor.Process(new CmdCreateVocabularyEntry(vocabId, word, translation, isDone));
        }
            
        public ObservableList<Proxy.VocabularyDataProxy> GetVocabularies { get; }
    }
}