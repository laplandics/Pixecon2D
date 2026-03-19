using Cmd;

namespace Menu
{
    public class CmdCreateVocabularyEntryHandler : ICommandHandler<CmdCreateVocabularyEntry>
    {
        private readonly Proxy.VocabularyDataProxy _vocabProxy;

        public CmdCreateVocabularyEntryHandler(Proxy.VocabularyDataProxy vocabProxy)
        { _vocabProxy = vocabProxy; }
        
        public bool Handle(CmdCreateVocabularyEntry command)
        {
            var newEntryData = new Data.VocabularyEntryData
            {
                entityID = _vocabProxy.VocabularyEntries.Count,
                key = Constant.Names.EntityData.VOCABULARY_ENTRY_BASE_KEY,
                isDone = command.IsDone,
                translation = command.Translation,
                word = command.Word
            };
            
            var newEntryProxy = new Proxy.VocabularyEntryDataProxy(newEntryData);
            
            command.Result.Value = newEntryProxy;
            _vocabProxy.VocabularyEntries.Add(newEntryProxy);
            return true;
        }
    }
}