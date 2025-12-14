using System.Threading.Tasks;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts.Resource
{
    public class GameResource : MonoBehaviour
    {
        public EResourceType eResourceType;
        public EResource eResource;
        [Tooltip("Use this for the amount that will be given to the villager")]
        public int amount;
        public float harvestTime = 2f;
        
        public bool _isValid = true;
        float _timer;

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void StartTaking()
        {
            _timer = harvestTime;
        }

        public bool TickTaking(float deltaTime)
        {
            if (!_isValid) return false;
            
            _timer -= deltaTime;
            
            return _timer <= 0f;
        }

        public void FinishTaking(VillageUnit unit)
        {
            unit.AddResource(eResource, amount);
            if (eResourceType == EResourceType.Unique)
            {
                _isValid = false;
            }
        }

        public void CancelTaking()
        {
            _timer = 0f;
        }
    }
}