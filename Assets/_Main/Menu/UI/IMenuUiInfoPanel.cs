using System.Collections.Generic;
using R3;

namespace Menu
{
    public interface IMenuUiInfoPanel
    {
        public void LoadElements(Proxy.ProjectDataProxy pdp, Dictionary<string, Subject<Unit>> signals);
        public void ClearElements();
    }
}