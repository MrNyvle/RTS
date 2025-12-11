using System;
using UnityEngine.InputSystem;
using UnityEngine;

namespace _Scripts.Unit
{
    public class UnitSelector : MonoBehaviour
    {
        InputSystem_Actions actions;
        
        private void OnEnable()
        {
            actions = new InputSystem_Actions();
            actions.Enable();
        }

        private void Start()
        {
            actions.RTS.LeftClick.performed += OnClick;
        }

        private void OnClick(InputAction.CallbackContext obj)
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