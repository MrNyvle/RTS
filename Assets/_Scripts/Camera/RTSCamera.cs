using System;
using _Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSCamera : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction zoomAction;
    private InputAction rotateAction;
    private InputAction rotateButtonAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float edgeSpeed = 35f;
    [SerializeField] private float edgeSize = 15f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 8f;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 40f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 120f;

    [Header("Ground Lock")]
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Camera mainCam;

    private void Awake()
    {
        var map = inputActions.FindActionMap("RTS");

        moveAction = map.FindAction("Move");
        zoomAction = map.FindAction("Zoom");
        rotateAction = map.FindAction("Rotate");
        rotateButtonAction = map.FindAction("RotateButton");
    }
    private void OnEnable()
    {
        moveAction.Enable();
        zoomAction.Enable();
        rotateAction.Enable();
        rotateButtonAction.Enable();
    }

    private void Start()
    {
        GameManager.Instance.mainCamera = mainCam;
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        zoomAction.Disable();
        rotateAction.Disable();
        rotateButtonAction.Disable();
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    void LateUpdate()
    {
        LockToGround();
    }

    // ---------------- MOVEMENT ----------------
    void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        // Edge scrolling
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (mousePos.x < edgeSize)
            move.x -= 1;
        if (mousePos.x > Screen.width - edgeSize)
            move.x += 1;
        if (mousePos.y < edgeSize)
            move.z -= 1;
        if (mousePos.y > Screen.height - edgeSize)
            move.z += 1;

        transform.position += move.normalized * (moveSpeed * Time.deltaTime);
    }

    // ---------------- ZOOM ----------------
    void HandleZoom()
    {
        float scroll = zoomAction.ReadValue<float>();
        Vector3 camPos = mainCam.transform.localPosition;
        
        if (Mathf.Abs(scroll) > 0.01f)
        {
            camPos += mainCam.transform.forward * (scroll * zoomSpeed);

            if (camPos.y < minZoom || camPos.y > maxZoom)
                return;
            
            mainCam.transform.localPosition = camPos;
        }
    }

    // ---------------- ROTATION ----------------
    void HandleRotation()
    {
        if (!rotateButtonAction.IsPressed())
            return;

        float rotInput = rotateAction.ReadValue<float>();
        transform.Rotate(Vector3.up, rotInput * rotationSpeed * Time.deltaTime);
    }

    // ---------------- GROUND LOCK ----------------
    void LockToGround()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 100f,
            Vector3.down, out RaycastHit hit, 200f, groundMask))
        {
            transform.position = new Vector3(
                transform.position.x,
                hit.point.y,
                transform.position.z
            );
        }
    }
}
