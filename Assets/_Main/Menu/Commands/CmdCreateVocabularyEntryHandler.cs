using Cmd;

namespace Menu
{
    public class CmdCreateVocabularyEntryHandler : ICommandHandler<CmdCreateVocabularyEntry>
    {
        private readonly Proxy.ProjectDataProxy _pdp;


        public CmdCreateVocabularyEntryHandler(Proxy.ProjectDataProxy pdp)
        { _pdp = pdp; }
        
        public bool Handle(CmdCreateVocabularyEntry command)
        {
            var newEntryData = new Data.VocabularyEntryData
            {
                entityID = _pdp.GetGlobalEntityId,
                translation = command.Translation,
                word = command.Word
            };
            
            var newEntryProxy = new Proxy.VocabularyEntryDataProxy(newEntryData);
            foreach (var vocabulary in _pdp.Vocabularies)
            { if (vocabulary.Id == command.VocabId) { vocabulary.VocabularyEntries.Add(newEntryProxy); } }
            
            return true;
        }
    }
}