using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Buildings;
using _Scripts.Unit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class BuildManager : Singleton<BuildManager>
    {
        public List<Building> buildingsBP = new List<Building>();
        
        private InputSystem_Actions _gameControls;
        private GameObject _building;
        private Coroutine _followMouseCoroutine;

        private void Start()
        { 
            _gameControls = GameControls.Instance.actions;
            _gameControls.RTS.LeftClick.canceled += PlaceBuilding;
        }

        private void PlaceBuilding(InputAction.CallbackContext obj)
        {
            StopCoroutine(_followMouseCoroutine);
            if (CanBuild(out TownHall hall))
            {
                _building.TryGetComponent(out Building building);
                hall.AssignBuilding(building);
                building.transform.parent = hall.transform.parent;
            }
            EnableCameraControls();
        }

        private void EnableCameraControls()
        {
            RTSCamera rts = GameManager.Instance.mainCamera.gameObject.GetComponentInParent<RTSCamera>();
            rts.CanMove(true);
        }

        private void DisableCameraControls()
        {
            RTSCamera rts = GameManager.Instance.mainCamera.gameObject.GetComponentInParent<RTSCamera>();
            rts.CanMove(false);
        }

        public void BuildBuilding(EBuilding buildingType)
        {
            DisableCameraControls();

            Building buildingBP = buildingsBP.First(building => building.eBuildingType == buildingType);
            _building = Instantiate(buildingBP.gameObject);
            _followMouseCoroutine = StartCoroutine(BuildingFollowMouse());
        }

        public IEnumerator BuildingFollowMouse()
        {
            float time = Time.time;
            while (Time.time - time <= 60f)
            {
                Vector2 mousePos = _gameControls.RTS.MousePosition.ReadValue<Vector2>();
                Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0));
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Ground")))
                {
                    _building.transform.position = hit.point;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        public bool CanBuild(out TownHall townHall)
        {
            foreach (TownHall hall in GameManager.Instance.townHalls)
            { 
                if (Vector3.Distance(_building.transform.position, hall.transform.position) <= hall.buildRange)
                {
                    townHall = hall;
                    return true;
                }
            }
            townHall = null;
            return false;
        }
    }
}