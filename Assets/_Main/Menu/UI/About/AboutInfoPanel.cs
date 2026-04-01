using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class AboutInfoPanel : MonoBehaviour, IMenuUiInfoPanel
    {
        public void LoadElements(VocabularyCreator vocabCreator,
            Dictionary<string, Subject<Unit>> signals, ScrollRect rootScrollRect)
        {
            
        }

        public void ClearElements()
        {
            Destroy(gameObject);
        }
    }

}