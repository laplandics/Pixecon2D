using UnityEngine;

public interface IRootUI
{
    public RectTransform UITransform { get; }
    
    public void OnAttached(UIContainer container);
    public void OnDetached();
}