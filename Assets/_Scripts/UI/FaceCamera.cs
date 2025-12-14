using UnityEngine;

namespace _Scripts.UI
{
    public class FaceCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            Transform cam = GameManager.Instance.mainCamera.transform;
            transform.forward = cam.forward;
        }
    }
}