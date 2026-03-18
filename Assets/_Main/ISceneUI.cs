using UnityEngine;

public interface ISceneUI
{
    public RectTransform UITransform { get; }
    
    public void OnAttached();
    public void OnRemoved();
}