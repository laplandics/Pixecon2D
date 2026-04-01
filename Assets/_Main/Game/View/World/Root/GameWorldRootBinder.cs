using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using UnityEngine;

namespace GameView
{
    public class GameWorldRootBinder : MonoBehaviour, IRootWorld
    {
        private GameWorldRootViewModel _vm;
        private readonly Dictionary<int, CellBinder> _createdCellsMap = new();
        private readonly CompositeDisposable _disposables = new();
        
        public void Bind(GameWorldRootViewModel vm)
        {
            _vm = vm;
            
            foreach (var cellVm in vm.AllCells) { CreateCell(cellVm); }
            
            _disposables.Add(vm.AllCells.ObserveAdd().Subscribe(addEvent => 
                CreateCell(addEvent.Value)));
            
            _disposables.Add(vm.AllCells.ObserveRemove().Subscribe(removeEvent => 
                DestroyCell(removeEvent.Value)));
        }
        
        private void CreateCell(CellViewModel cellVm)
        {
            const string path = Constant.Names.World.CELL_PREFAB;
            
            var pos = cellVm.Position.CurrentValue;
            var cellObject = _vm.Spawner.Spawn(path, pos);
            var cell = cellObject.GetComponent<CellBinder>();
            cell.Bind(cellVm);
            cell.ViewModel.CellProxy.Letter
                .Where(char.IsLetter)
                .Subscribe(_ => UpdateCellSprite(cell));
            _createdCellsMap.Add(cellVm.CellEntityId, cell);
            
            StartCoroutine(CellCreation(cell));
        }

        private IEnumerator CellCreation(CellBinder cell)
        {
            cell.cldr.enabled = false;
            var cellTr = cell.transform;
            cellTr.transform.localScale = Vector3.zero;
            while (cellTr.localScale.x < 1f)
            {
                cellTr.localScale += Vector3.one * (Time.deltaTime * cell.ViewModel.Settings.cellCreationSpeed);
                yield return null;
                if (!(cellTr.localScale.x >= 1f)) continue;
                cellTr.localScale = Vector3.one; break;
            }
            cell.cldr.enabled = true;
        }

        private void UpdateCellSprite(CellBinder cellBinder)
        {
            var letterSprite = Resources.LoadAll<Sprite>(Constant.Names.World.LETTERS_ATLAS)
                .FirstOrDefault(sprite => sprite.name == $"{cellBinder.ViewModel.CellProxy.Letter}");
            
            var cellMesh = cellBinder.mf;
            
            var uMin = letterSprite!.rect.x / letterSprite.texture.width;
            var vMin = letterSprite.rect.y / letterSprite.texture.height;
            var uMax = (letterSprite.rect.x + letterSprite.rect.width) / letterSprite.texture.width;
            var vMax = (letterSprite.rect.y + letterSprite.rect.height) / letterSprite.texture.height;
            
            var uvs = new Vector2[4];
            uvs[0] = new Vector2(uMin, vMin);
            uvs[1] = new Vector2(uMax, vMin);
            uvs[2] = new Vector2(uMin, vMax);
            uvs[3] = new Vector2(uMax, vMax);

            cellMesh.mesh.uv2 = uvs;
        }
        
        private void DestroyCell(CellViewModel cellVm)
        {
            cellVm.Letter.Dispose();
            if (_createdCellsMap.TryGetValue(cellVm.CellEntityId, out var cellBinder))
            { cellBinder.gameObject.SetActive(false); }
            _createdCellsMap.Remove(cellVm.CellEntityId);
        }
        
        public Transform WorldTransform => transform;
        public void OnAttached() { }
        public void OnRemoved() { _disposables.Dispose(); }
    }
}