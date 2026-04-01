using UnityEngine;

namespace GameView
{
    public class CellBinder : MonoBehaviour
    {
        public MeshFilter mf;
        public Collider cldr;
        
        public CellViewModel ViewModel { get; private set; }
        
        public void Bind(CellViewModel vm)
        {
            ViewModel = vm;
            transform.position = vm.Position.CurrentValue;
        }
    }
}