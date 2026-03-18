using UnityEngine;

public class LoadingScreen : MonoBehaviour, ISceneUI
{
    public void OnAttached() {}
    public void OnRemoved() {}
    
    public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
}