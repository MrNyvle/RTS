using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Resource
{
    public class GameResource : MonoBehaviour
    {
        public EResourceType eResourceType;
        public EResource eResource;
        [Tooltip("Use this for the amount that will be given to the villager")]
        public int amount;
        public float harvestTime = 2f;
        public bool isValid = true;
        public GameObject pickupPoint;

        private void Awake()
        {
            AutoSetupColliders();
        }

        private void AutoSetupColliders()
        {
            var colliders = GetComponentsInChildren<Collider>(true);

            foreach (Collider col in colliders)
            {
                if (!col.TryGetComponent(out ResourceCollider resourceCollider))
                {
                    resourceCollider = col.gameObject.AddComponent<ResourceCollider>();
                }

                resourceCollider.Resource = this;
            }
        }

        public Vector3 GetPosition()
        {
            if (pickupPoint != null)
                return pickupPoint.transform.position;
            return transform.position;
        }

        public void FinishTaking(VillageUnit unit)
        {
            if (!isValid) return;
            unit.AddResource(eResource, amount);
            if (eResourceType == EResourceType.Unique)
            {
                isValid = false;
            }
        }
    }
}