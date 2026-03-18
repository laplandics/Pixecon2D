using UnityEngine;

internal class Ui : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform[] layers;
            
    public void ResetUi()
    {
        foreach (var layer in layers)
        {
            for (var child = 0; child < layer.childCount; child++)
            { Destroy(layer.GetChild(child).gameObject); }
        }
    }
}