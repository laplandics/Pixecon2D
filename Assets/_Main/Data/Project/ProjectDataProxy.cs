using System.Linq;
using ObservableCollections;
using R3;

namespace Proxy
{
    public class ProjectDataProxy
    {
        private Data.ProjectData Origin { get; }
        public int GetGlobalEntityId => Origin.globalEntityId++;
        
        public ObservableList<VocabularyDataProxy> Vocabularies { get; } = new();
        public ObservableList<CellDataProxy> Cells { get; } = new();
        
        public ProjectDataProxy(Data.ProjectData origin)
        {
            Origin = origin;
            
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
                Origin.vocabularies.Add(addedVocabularyDataProxy.Origin);
            });

            Vocabularies.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedVocabularyDataProxy = removeEvent.Value;
                var removedVocabularyData = Origin.vocabularies.FirstOrDefault(vocabulary =>
                    vocabulary.entityID == removedVocabularyDataProxy.Id);
                Origin.vocabularies.Remove(removedVocabularyData);
            });
        }

        private void SubscribeToCellsChange()
        {
            Cells.ObserveAdd().Subscribe(addEvent =>
            {
                var addedCellDataProxy = addEvent.Value;
                Origin.cells.Add(addedCellDataProxy.Origin);
            });

            Cells.ObserveRemove().Subscribe(removeEvent =>
            {
                var removedCellDataProxy = removeEvent.Value;
                var removedCellData = Origin.cells.FirstOrDefault(cell =>
                    cell.entityID == removedCellDataProxy.Id);
                Origin.cells.Remove(removedCellData);
            });
        }
    }
}