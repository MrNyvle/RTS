using System;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Buildings
{
    [Serializable]
    public class Building : MonoBehaviour
    {
        public virtual EBuilding eBuildingType { get; set; }
        public BuildingResource buildingResource;
        public GameObject entrance;
        public float depositTime;
        
        public Vector3 GetEntrancePosition()
        {
            return entrance.transform.position;
        }
    }
}