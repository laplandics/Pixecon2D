using System;
using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using UnityEngine;

namespace GameView
{
    public class GameWorldRootBinder : MonoBehaviour, IRootWorld
    {
        private readonly Dictionary<int, CellBinder> _createdCellsMap = new();
        private readonly CompositeDisposable _disposables = new();
        
        public void Bind(GameWorldRootViewModel vm)
        {
            foreach (var cellVm in vm.AllCells) { CreateCell(cellVm); }
            
            _disposables.Add(vm.AllCells.ObserveAdd().Subscribe(addEvent => 
                CreateCell(addEvent.Value)));
            
            _disposables.Add(vm.AllCells.ObserveRemove().Subscribe(removeEvent => 
                DestroyCell(removeEvent.Value)));
        }
        
        private void CreateCell(CellViewModel cellVm)
        {
            var cellPrefab = Resources.Load<CellBinder>(Constant.Names.World.CELL_PREFAB);
            var createdCell = Instantiate(cellPrefab);
            createdCell.Bind(cellVm);
            createdCell.ViewModel.CellProxy.Letter.Subscribe(letter =>
            { if (!char.IsLetter(letter)) return; UpdateCellSprite(createdCell); });
            _createdCellsMap[cellVm.CellEntityId] = createdCell;
        }

        private void UpdateCellSprite(CellBinder cellBinder)
        {
            var letterSprite = Resources.LoadAll<Sprite>(Constant.Names.World.LETTERS_ATLAS)
                .FirstOrDefault(sprite => sprite.name == $"{cellBinder.ViewModel.CellProxy.Letter}");
            if (letterSprite == null) return;
            
            var cellMesh = cellBinder.GetComponentInChildren<MeshFilter>();
            
            var uMin = letterSprite.rect.x / letterSprite.texture.width;
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
            { Destroy(cellBinder.gameObject); }
            _createdCellsMap.Remove(cellVm.CellEntityId);
        }
        
        public Transform WorldTransform => transform;
        public void OnAttached() { }
        public void OnRemoved() { _disposables.Dispose(); }
    }
}