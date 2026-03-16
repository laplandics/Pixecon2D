using System;
using System.Collections;
using UnityEngine;

public class MenuBoot : MonoBehaviour
{
    public Action GoToGameScene;
    private MenuUI _menuUI;
    
    public IEnumerator Boot()
    {
        Utils.Cam.Instantiate();
        Utils.UI.New(Utils.UI.MENU_UI);
        Utils.UI.AttachSceneUI(out _menuUI);
        AssignToUiEvents();
        yield return null;
    }

    private void AssignToUiEvents()
    {
        _menuUI.PlayButtonClicked += OnPlayButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        Utils.UI.RemoveSceneUI<MenuUI>();
        Utils.UI.Clear();
        GoToGameScene?.Invoke();
    }
}