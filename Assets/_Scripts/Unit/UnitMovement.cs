using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        public bool isMoving;
        
        NavMeshAgent _agent;
        void Awake()
        {
            TryGetComponent(out _agent);
        }
        
        public void MoveTo(Vector3 pos, float distance = .5f)
        {
            _agent.stoppingDistance = distance;
            _agent.SetDestination(pos);
            isMoving = true;
        }

        public bool Reached()
        {
            return !_agent.pathPending &&
                   _agent.remainingDistance <= _agent.stoppingDistance;
        }

        public void Stop()
        {
            isMoving = false;
            _agent.ResetPath();
        }
    }
}