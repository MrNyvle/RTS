using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.AntiCheat;
using _Scripts.Unit;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace _Scripts.Resource
{
    public class GameResource : MonoBehaviour
    {
        public EResourceType eResourceType;
        public EResource eResource;
        [Tooltip("Use this for the amount that will be given to the villager")]
        public int amount;

        private long checksum;
        
        public float harvestTime = 2f;
        public bool isValid = true;
        public GameObject pickupPoint;
        public GameObject usedModel;
        public GameObject currentModel;
        public VisualEffect visualEffect;
        
        private void Awake()
        {
            AutoSetupColliders();
            UpdateChecksum();
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
            if (checksum != CalculateChecksum())
                AntiCheatManager.Instance.FlagCheat("Resource checksum tampering detected");
            
            if (!isValid) return;
            unit.AddResource(eResource, amount);
            if (eResourceType == EResourceType.Unique)
            {
                if (visualEffect is not null) visualEffect.Play();
                if (currentModel is not null) currentModel.SetActive(false);
                if (usedModel is not null) usedModel.SetActive(true);
                isValid = false;
            }
        }
        
        private int CalculateChecksum()
        {
            return Mathf.RoundToInt(amount * 1000) ^ 0xABCDEF;
        }

        private void UpdateChecksum()
        {
            checksum = CalculateChecksum();
        }

    }
}