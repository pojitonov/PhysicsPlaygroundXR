using UnityEngine;
using UnityEngine.XR;

namespace Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private void Start()
        {
            XRSettings.useOcclusionMesh = false;
        }
    }
}