using GameView;
using UnityEngine;

public interface IPopup
{
    public RectTransform PopupTransform { get; }

    public void Bind(PopupViewModel vm);
    public void OnShow();
    public void OnHide();
}