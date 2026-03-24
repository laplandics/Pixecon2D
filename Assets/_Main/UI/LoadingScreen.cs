using UnityEngine;

public class LoadingScreen : MonoBehaviour, IRootUI
{
    public void OnAttached(UIContainer container) {}
    public void OnDetached() {}
    
    public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
}