using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Menu
{
    public class AboutInfoPanel : MonoBehaviour, IMenuUiInfoPanel
    {
        public void LoadElements(VocabularyCreator vocabCreator, Dictionary<string, Subject<Unit>> signals)
        {
            
        }

        public void ClearElements()
        {
            Destroy(gameObject);
        }
    }

}