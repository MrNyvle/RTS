using System;
using _Scripts.Buildings;
using _Scripts.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.UI
{
    public abstract class BuildingUI : MonoBehaviour
    {
        [SerializeField] public EBuilding buildingType;
        public TextMeshProUGUI buildingName;

        public virtual EBuilding BuildingType
        {
            get => buildingType;
            set => buildingType = value;
        }

        public abstract void FillUI(Building building);
    }
}