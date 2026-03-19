using System.Collections.Generic;
using Cmd;

namespace Menu
{
    public class CmdCreateVocabularyHandler : ICommandHandler<CmdCreateVocabulary>
    {
        private readonly Proxy.ProjectDataProxy _pdp;

        public CmdCreateVocabularyHandler(Proxy.ProjectDataProxy pdp)
        {
            _pdp = pdp;
        }
        
        public bool Handle(CmdCreateVocabulary command)
        {
            var entityId = _pdp.GetGlobalEntityId;
            var newVocabData = new Data.VocabularyData
            {
                entityID = entityId,
                key = Constant.Names.EntityData.VOCABULARY_BASE_KEY,
                title = command.VocabularyTitle,
                isDone = command.IsDone,
                isIncluded = command.IsIncluded,
                vocabularyEntries = new List<Data.VocabularyEntryData>()
            };
            var newVocabProxy = new Proxy.VocabularyDataProxy(newVocabData);
            _pdp.Vocabularies.Add(newVocabProxy);
            
            return true;
        }
    }
}