using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Menu
{
    public class PreferencesInfoPanel : MonoBehaviour, IMenuUiInfoPanel
    {
        private Proxy.ProjectDataProxy _pdp;

        public void LoadElements(VocabularyCreator vocabCreator, Dictionary<string, Subject<Unit>> signals)
        {
            
        }

        public void ClearElements()
        {
            Destroy(gameObject);
        }
    }
}
