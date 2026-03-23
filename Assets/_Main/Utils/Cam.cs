using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class Cam
    {
        private readonly string _camName;
        
        public Cam(string prefabPath) { _camName = prefabPath; }
        
        public void Instantiate()
        {
            var camPref = Resources.Load<Camera>(Constant.Names.World.CAM);
            var cam = Object.Instantiate(camPref);
            cam.name = _camName;
            cam.transform.position = new Vector3(0, 0, -10);
            cam.tag = "MainCamera";
            Get = cam;
            Resources.UnloadUnusedAssets();
        }
        
        public Camera Get { get; private set; }
    }
}