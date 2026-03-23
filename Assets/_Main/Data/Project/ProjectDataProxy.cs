using System.Linq;
using ObservableCollections;
using R3;

namespace Proxy
{
    public class ProjectDataProxy
    {
        private readonly Data.ProjectData _origin;

        public int GetGlobalEntityId => _origin.globalEntityId++;
        public ObservableList<VocabularyDataProxy> Vocabularies { get; } = new();
        public ObservableList<CellDataProxy> Cells { get; } = new();
        
        public ProjectDataProxy(Data.ProjectData origin)
        {
            _origin = origin;
            origin.vocabularies.ForEach(vocabularyData =>
                Vocabularies.Add(new VocabularyDataProxy(vocabularyData)));
            SubscribeToVocabulariesChange();
            
            origin.cells.ForEach(cellData => Cells.Add(new CellDataProxy(cellData)));
            SubscribeToCellsChange();
        }

        private void SubscribeToVocabulariesChange()
        {
            Vocabularies.ObserveAdd().Subscribe(addEvent =>
            {
                var addedVocabularyDataProxy = addEvent.Value;
                _origin.vocabularies.Add(addedVocabularyDataProxy.Origin);
            });

            Vocabularies.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedVocabularyDataProxy = removeEvent.Value;
                var removedVocabularyData = _origin.vocabularies.FirstOrDefault(vocabulary =>
                    vocabulary.entityID == removedVocabularyDataProxy.Id);
                _origin.vocabularies.Remove(removedVocabularyData);
            });
        }

        private void SubscribeToCellsChange()
        {
            Cells.ObserveAdd().Subscribe(addEvent =>
            {
                var addedCellDataProxy = addEvent.Value;
                _origin.cells.Add(addedCellDataProxy.Origin);
            });

            Cells.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedCellDataProxy = removeEvent.Value;
                var removedCellData = _origin.cells.FirstOrDefault(cell =>
                    cell.entityID == removedCellDataProxy.Id);
                _origin.cells.Remove(removedCellData);
            });
        }
    }
}