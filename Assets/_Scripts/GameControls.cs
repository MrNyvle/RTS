using System;
using _Scripts.Buildings;
using UnityEngine.InputSystem;
using UnityEngine;
using _Scripts.Resource;
using _Scripts.UI;
using _Scripts.Unit;
using _Scripts.Unit.Commands;
using Unity.VisualScripting.Dependencies.NCalc;

namespace _Scripts
{
    public class GameControls : Singleton<GameControls>
    {
        public InputSystem_Actions actions;
        
        private void OnEnable()
        {
            actions = new InputSystem_Actions();
            actions.Enable();
        }

        private void Start()
        {
            actions.RTS.LeftClick.performed += OnClickLeft;
            actions.RTS.RightClick.performed += OnClickRight;
        }

        private void OnClickRight(InputAction.CallbackContext obj)
        {
            TryAssignTask();
            DeselectBuilding();
        }

        private void OnClickLeft(InputAction.CallbackContext obj)
        {
            TrySelectUnit();
            TrySelectBuilding();
        }

        private void TryAssignTask()
        {
            Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                hit.collider.TryGetComponent(out GameResource resource);
                hit.collider.TryGetComponent(out ResourceCollider resourceUnlimitedCollider);
                hit.collider.TryGetComponent(out Building building);

                if (GameManager.Instance.SelectedUnit == null)
                    return;
                
                if (resource != null)
                {
                    GameManager.Instance.SelectedUnit.IssueCommand(new GatherResourceCommand(resource));
                }
                else if(building !=null)
                {
                    if (!building.isBuilt)
                        GameManager.Instance.SelectedUnit.IssueCommand(new BuildCommand(building));
                    else
                        GameManager.Instance.SelectedUnit.IssueCommand(new DepositCommand(building));
                }
                else if (resourceUnlimitedCollider != null)
                {
                    GameManager.Instance.SelectedUnit.IssueCommand(new GatherResourceCommand(resourceUnlimitedCollider.Resource));
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    GameManager.Instance.SelectedUnit.IssueCommand(new MoveCommand(hit.point));
                }
                else
                {
                    Debug.Log(hit.collider.name);
                }
                
            }
        }

        private void TrySelectBuilding()
        {
            Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.name);
                Building building = hit.collider.GetComponent<Building>();

                if (building != null)
                {
                    SelectBuilding(building);
                    return;
                }
                
            }
        }

        void TrySelectUnit()
        {
            Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                VillageUnit unit = hit.collider.GetComponent<VillageUnit>();

                if (unit != null)
                {
                    SelectUnit(unit);
                    return;
                }
            }
            DeselectUnit();
        }

        void SelectUnit(VillageUnit newUnit)
        {
            GameManager.Instance.SelectedUnit = newUnit;
        }

        void DeselectUnit()
        {
            GameManager.Instance.SelectedUnit = null;
        }
        
        void SelectBuilding(Building building)
        {
            GameManager.Instance.SelectedBuilding = building;
            UiManager.Instance.ShowUI(building);
        }

        void DeselectBuilding()
        {
            if (GameManager.Instance.SelectedBuilding != null)
                UiManager.Instance.HideUI();
            
            GameManager.Instance.SelectedBuilding = null;
        }
        
        private void OnDisable()
        {
            actions.Disable();
        }
    }
}