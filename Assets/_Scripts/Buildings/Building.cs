using System;
using _Scripts.Unit;
using UnityEngine;

namespace _Scripts.Buildings
{
    [Serializable]
    public class Building : MonoBehaviour
    {
        public BuildingResource buildingResource;
        public GameObject entrance;
        public float depositTime;
        
        public Vector3 GetEntrancePosition()
        {
            return entrance.transform.position;
        }
    }
}