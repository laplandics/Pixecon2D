using UnityEngine;

namespace Utils
{
    public static class Cam
    {
        public static Camera Get { get; private set; }
    
        public static void Instantiate()
        {
            if (Get != null)
            { Debug.LogWarning("Camera already instantiated"); Object.Destroy(Get); }
            var camPref = Resources.Load<Camera>("Prefabs/Cam");
            var cam = Object.Instantiate(camPref);
            cam.name = "Cam";
            cam.transform.position = new Vector3(0, 0, -10);
            cam.tag = "MainCamera";
            Get = cam;
            Resources.UnloadUnusedAssets();
        }

        public static void Destroy()
        {
            if (Get == null)
            { Debug.LogWarning("Camera not instantiated"); return; }
            Object.Destroy(Get.gameObject);
        }
    }
}