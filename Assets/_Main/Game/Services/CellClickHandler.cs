using System;
using System.Collections.Generic;
using GameView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class CellClickHandler : IDisposable
    {
        private readonly PointerEventData _eventData;
        private readonly GraphicRaycaster _raycaster;
        private readonly ChosenLetterChecker _chosenLetterChecker;
        private readonly CellBuilder _cellBuilder;
        private readonly Cam _cam;
        private readonly Inputs _inputs;

        public CellClickHandler(GameInputHandler gameInputHandler, Cam cam, GraphicRaycaster raycaster, 
            EventSystem eventSystem, ChosenLetterChecker chosenLetterChecker, CellBuilder cellBuilder)
        {
            _cam = cam;
            _raycaster = raycaster;
            _chosenLetterChecker = chosenLetterChecker;
            _cellBuilder = cellBuilder;
            _eventData = new PointerEventData(eventSystem);
            _inputs = gameInputHandler.Inputs;
            EnableCellClicks();
        }

        private void EnableCellClicks()
        {
            _inputs.Game.CellClick.Enable();
            _inputs.Game.PoinerPosition.Enable();
            _inputs.Game.CellClick.performed += OnCellClickPerformed;
        }
        
        private void OnCellClickPerformed(InputAction.CallbackContext ctx)
        {
            var screenPos = _inputs.Game.PoinerPosition.ReadValue<Vector2>();
            var results = new List<RaycastResult>();
            _eventData.position = screenPos;
            _raycaster.Raycast(_eventData, results);
            if (results.Count > 0) return;
            
            var worldPos = _cam.Get.ScreenToWorldPoint(screenPos);
            var ray = new Ray(worldPos, _cam.Get.transform.forward);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.gameObject.TryGetComponent<CellBinder>(out var cell)) return;
            HandleClick(cell.ViewModel);
        }

        private void HandleClick(CellViewModel cellViewModel)
        {
            if (!_chosenLetterChecker.CheckLetter(cellViewModel)) {}
            _cellBuilder.RemoveCell(cellViewModel.CellProxy);
        }

        private void DisableCellClicks()
        {
            _inputs.Game.CellClick.Disable();
            _inputs.Game.PoinerPosition.Disable();
            _inputs.Game.CellClick.performed -= OnCellClickPerformed;
        }
        
        public void Dispose() => DisableCellClicks();
    }
}