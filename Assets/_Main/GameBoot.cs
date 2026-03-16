using System;
using System.Collections;
using UnityEngine;

public class GameBoot : MonoBehaviour
{
    public Action GoToMenuScene;
    private GameUI _gameUI;
    
    public IEnumerator Boot()
    {
        Utils.Cam.Instantiate();
        Utils.UI.New(Utils.UI.GAME_UI);
        Utils.UI.AttachSceneUI(out _gameUI);
        AssignToUiEvents();
        yield return null;
    }

    private void AssignToUiEvents()
    {
        _gameUI.PauseButtonClicked += OnPauseButtonClicked;
    }

    private void OnPauseButtonClicked()
    {
        Utils.UI.RemoveSceneUI<GameUI>();
        Utils.UI.Clear();
        GoToMenuScene?.Invoke();
    }
}