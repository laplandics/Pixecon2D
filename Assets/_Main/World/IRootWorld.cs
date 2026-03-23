using UnityEngine;

public interface IRootWorld
{
    public Transform WorldTransform { get; }
    
    public void OnAttached();
    public void OnRemoved();
}