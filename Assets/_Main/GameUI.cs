using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour, ISceneUI
{
    public Action PauseButtonClicked;
    
    private UIDocument _uiDocument;
    private Button _pauseButton;
    
    public void OnAttached()
    {
        AssignReferences();
        AssignButtons();
    }

    private void AssignReferences()
    {
        _uiDocument = GetComponent<UIDocument>();
        _pauseButton = _uiDocument.rootVisualElement.Q<Button>("PauseButton");
    }

    private void AssignButtons()
    {
        _pauseButton.clicked += OnPauseButtonClicked;
    }

    private void OnPauseButtonClicked()
    {
        PauseButtonClicked?.Invoke();
    }

    public void OnRemoved()
    {
        _pauseButton.clicked -= OnPauseButtonClicked;
    }
}