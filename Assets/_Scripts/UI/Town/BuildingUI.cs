using System;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.UI
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] public EBuilding buildingType;

        public EBuilding BuildingType
        {
            get => buildingType;
            set => buildingType = value;
        }
    }
}