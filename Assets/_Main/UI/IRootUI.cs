using UnityEngine;

public interface IRootUI
{
    public RectTransform UITransform { get; }
    
    public void OnAttached();
    public void OnRemoved();
}