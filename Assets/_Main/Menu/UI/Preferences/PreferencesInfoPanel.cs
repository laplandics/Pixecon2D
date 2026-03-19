using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Menu
{
    public class PreferencesInfoPanel : MonoBehaviour, IMenuUiInfoPanel
    {
        private Proxy.ProjectDataProxy _pdp;
        
        public void LoadElements(Proxy.ProjectDataProxy pdp,
            Dictionary<string, Subject<Unit>> uiSignals)
        {
            
        }

        public void ClearElements()
        {
            Destroy(gameObject);
        }
    }
}
