using System;
using _Scripts.Buildings;
using UnityEngine.InputSystem;
using UnityEngine;
using _Scripts.Resource;
using _Scripts.Unit;
using _Scripts.Unit.Commands;

namespace _Scripts
{
    public class GameControls : MonoBehaviour
    {
        InputSystem_Actions actions;
        
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
        }
        
        private void TryAssignTask()
        {
            
            Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                hit.collider.TryGetComponent(out GameResource resource);
                hit.collider.TryGetComponent(out TownHall townHall);
                
                if (resource != null && GameManager.Instance.SelectedUnit != null)
                {
                    GameManager.Instance.SelectedUnit.IssueCommand(new GatherResourceCommand(resource));
                }
                else if(townHall !=null && GameManager.Instance.SelectedUnit.townHall == townHall)
                {
                    GameManager.Instance.SelectedUnit.IssueCommand(new DepositCommand());
                }
                else if (hit.collider.CompareTag("Ground"))
                {
                    GameManager.Instance.SelectedUnit.IssueCommand(new MoveCommand(hit.point));
                }
                
            }
        }

        private void OnClickLeft(InputAction.CallbackContext obj)
        {
            TrySelectUnit();
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
                }
                else
                {
                    DeselectUnit();
                }
            }
            else
            {
                DeselectUnit();
            }
        }

        void SelectUnit(VillageUnit newUnit)
        {
            GameManager.Instance.SelectedUnit = newUnit;
        }

        void DeselectUnit()
        {
            GameManager.Instance.SelectedUnit = null;
        }
        
        
        private void OnDisable()
        {
            actions.Disable();
        }
    }
}