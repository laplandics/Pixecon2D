using Proxy;
using UnityEngine;

namespace GameView
{
    public class CellBinder : MonoBehaviour
    {
        public CellDataProxy CellProxy;
        
        public void Bind(CellViewModel vm)
        {
            transform.position = vm.Position.CurrentValue;
            CellProxy = vm.CellProxy;
        }
    }
}