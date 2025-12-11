using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        NavMeshAgent agent;
    }
}