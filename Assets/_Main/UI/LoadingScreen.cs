using UnityEngine;

public class LoadingScreen : MonoBehaviour, IRootUI
{
    public void OnAttached() {}
    public void OnRemoved() {}
    
    public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
}