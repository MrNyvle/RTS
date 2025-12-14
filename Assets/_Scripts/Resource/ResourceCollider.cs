using UnityEngine;

namespace _Scripts.Resource
{
    public class ResourceCollider : MonoBehaviour
    {
        public GameResource Resource { get; set; }

        void Awake()
        {
            Resource = GetComponentInParent<GameResource>();
        }
    }
}