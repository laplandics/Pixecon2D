using UnityEngine;

namespace GameView
{
    public class CellBinder : MonoBehaviour
    {
        public CellViewModel ViewModel { get; private set; }
        
        public void Bind(CellViewModel vm)
        {
            ViewModel = vm;
            transform.position = vm.Position.CurrentValue;
        }
    }
}