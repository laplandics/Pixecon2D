using System.Collections.Generic;
using R3;
using UnityEngine.UI;

namespace Menu
{
    public interface IMenuUiInfoPanel
    {
        public void LoadElements(VocabularyCreator vocabCreator,
            Dictionary<string, Subject<Unit>> signals, ScrollRect parentScrollRect);
        public void ClearElements();
    }
}