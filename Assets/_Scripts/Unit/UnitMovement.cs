using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        NavMeshAgent _agent;
        void Awake()
        {
            TryGetComponent(out _agent);
        }
        
        
        public void MoveTo(Vector3 pos)
        {
            _agent.SetDestination(pos);
        }

        public bool Reached()
        {
            return !_agent.pathPending &&
                   _agent.remainingDistance <= _agent.stoppingDistance;
        }

        public void Stop()
        {
            _agent.ResetPath();
        }
    }
}