using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class VillageUnit : MonoBehaviour
    {
        UnitMovement unitMovement;
        UnitSelector unitSelector;
        UnitVillagerStats unitVillagerStats;
        UnitResource unitResource;
        
        NavMeshAgent navMeshAgent;

        public void MoveToLocation(Vector3 position)
        {
            navMeshAgent.SetDestination(position);
        }
        
    }
}