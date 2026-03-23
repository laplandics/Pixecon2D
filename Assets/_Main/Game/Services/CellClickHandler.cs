using System;
using Cmd;
using GameView;
using Proxy;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Game
{
    public class CellClickHandler : IDisposable
    {
        private readonly Cam _cam;
        private readonly ICommandProcessor _cmd;
        private readonly Inputs _inputs;

        public CellClickHandler(GameInputHandler gameInputHandler, Cam cam, ICommandProcessor cmd)
        {
            _cam = cam;
            _cmd = cmd;
            _inputs = gameInputHandler.Inputs;
            _inputs.Game.CellClick.Enable();
            _inputs.Game.PoinerPosition.Enable();
            _inputs.Game.CellClick.performed += OnCellClickPerformed;
        }

        private void OnCellClickPerformed(InputAction.CallbackContext ctx)
        {
            var screenPos = _inputs.Game.PoinerPosition.ReadValue<Vector2>();
            var worldPos = _cam.Get.ScreenToWorldPoint(screenPos);
            var ray = new Ray(worldPos, _cam.Get.transform.forward);
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.collider.gameObject.TryGetComponent<CellBinder>(out var cell)) return;
            HandleClick(cell.CellProxy);
        }

        private void HandleClick(CellDataProxy cellProxy)
        {
            _cmd.Process(new CmdHandleCellClick(cellProxy));
        }

        public void Dispose()
        {
            _inputs.Game.CellClick.Disable();
            _inputs.Game.PoinerPosition.Disable();
            _inputs.Game.CellClick.performed -= OnCellClickPerformed;
        }
    }
}