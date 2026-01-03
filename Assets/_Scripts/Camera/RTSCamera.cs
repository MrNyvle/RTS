using System;
using _Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSCamera : MonoBehaviour
{
    public bool handleMovement = true;
    
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction zoomAction;
    private InputAction rotateAction;
    private InputAction rotateButtonAction;
    private InputAction centerCam;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 25f;
    //[SerializeField] private float edgeSpeed = 35f;
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
        centerCam = map.FindAction("CenterCam");
    }
    private void OnEnable()
    {
        moveAction.Enable();
        zoomAction.Enable();
        rotateAction.Enable();
        rotateButtonAction.Enable();
        centerCam.Enable();
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
        centerCam.Disable();
    }

    void Update()
    {
        if (handleMovement)
            HandleMovement();
        HandleZoom();
        HandleRotation();
        HandleCamCentering();
    }

    void LateUpdate()
    {
        LockToGround();
    }

    private void HandleCamCentering()
    {
        if (!centerCam.IsPressed())
            return;

        transform.position = Vector3.zero;
    }
    
    void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        
        Vector3 forward = transform.forward;
        forward.y = 0;               // keep movement flat
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = (right * input.x + forward * input.y);
        
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (mousePos.x < edgeSize) move -= right;
        if (mousePos.x > Screen.width - edgeSize) move += right;
        if (mousePos.y < edgeSize) move -= forward;
        if (mousePos.y > Screen.height - edgeSize) move += forward;

        transform.position += move.normalized * (moveSpeed * Time.deltaTime);
    }
    
    void HandleZoom()
    {
        float scroll = zoomAction.ReadValue<float>();
        if (Mathf.Abs(scroll) < 0.01f)
            return;
        
        Vector3 offset = mainCam.transform.position - transform.position;
        
        offset += mainCam.transform.forward * (scroll * zoomSpeed);
        
        float distance = Mathf.Clamp(offset.magnitude, minZoom, maxZoom);
        
        offset = offset.normalized * distance;
        
        mainCam.transform.position = transform.position + offset;
    }
    
    void HandleRotation()
    {
        if (!rotateButtonAction.IsPressed())
            return;

        float rotInput = rotateAction.ReadValue<float>();
        transform.Rotate(Vector3.up, rotInput * rotationSpeed * Time.deltaTime);
    }
    
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
